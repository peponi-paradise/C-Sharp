using Workloads.Step;

namespace Workloads.Job;

public abstract class JobExecution : IJobExecution
{
    public event EventHandler<ExecutionData>? JobExecutionChanged;

    public event EventHandler<ExecutionData>? StepExecutionChanged;

    protected JobContextData _jobContextData;
    protected List<StepContextData> _stepContextDatas;
    protected ExecutionData _jobExecutionData;
#nullable disable
    protected List<ExecutionData> _stepExecutionDatas;
#nullable enable

    public JobExecution(JobContextData jobData, List<StepContextData> stepDatas)
    {
        if (stepDatas is null || stepDatas.Count == 0) throw new ArgumentException($"{nameof(stepDatas)} could not be null and Count need bigger than 0");

        _jobContextData = jobData;
        _stepContextDatas = stepDatas;
        _jobExecutionData = new(_jobContextData.Name);
    }

    public virtual ExecutionData GetJobExecutionData() => _jobExecutionData;

    public virtual List<ExecutionData>? GetStepExecutionDatas() => _stepExecutionDatas;

    public virtual void Start()
    {
        if (HasNextStep())
            StartNextStep();

        ProcessJobExecutionStatus(ExecutionStatus.Run);
    }

    public virtual bool Stop()
    {
        if (_jobExecutionData.ExecutionStatus != ExecutionStatus.Paused)
            return false;

        // If there is stopping process, maybe override this method
        var someStoppingStep = _stepContextDatas.Find(context => context.ContextType == StepContextType.Stopping);
        if (someStoppingStep is not null)
        {
            IStepContext stepContext = SomeStepFactory.GetStepContext(someStoppingStep);
            stepContext.StepExecutionChanged += ProcessStepExecutionChanged;
            _stepExecutionDatas.Add(stepContext.GetExecutionData());
            stepContext.Start();
            ProcessJobExecutionStatus(ExecutionStatus.Stopping);
        }
        else
        {
            ProcessJobExecutionStatus(ExecutionStatus.Stopped);
        }
        return true;
    }

    public virtual bool Pause()
    {
        if (_stepExecutionDatas is null || _stepExecutionDatas.Count == 0)
            return false;
        if (_jobExecutionData.ExecutionStatus != ExecutionStatus.Run)
            return false;

        _jobExecutionData.ExecutionStatus = ExecutionStatus.Pausing;
        return true;
    }

    public virtual void Resume()
    {
        if (_stepExecutionDatas is null || _stepExecutionDatas.Count == 0)
            return;
        if (_jobExecutionData.ExecutionStatus != ExecutionStatus.Paused)
            return;

        _jobExecutionData.ExecutionStatus = ExecutionStatus.Run;

        ProcessStepExecutionData(_stepExecutionDatas.Last());
    }

    protected virtual void ProcessStepExecutionChanged(object? sender, ExecutionData e)
    {
        // Check is right step
        var stepContext = _stepContextDatas.Find(context => context.Name == e.Name);
        if (stepContext is null)
            return;

        // Redirect step's report
        StepExecutionChanged?.Invoke(sender, e);

        // Check if step is not sequential
        if (stepContext.ContextType != StepContextType.Sequential)
            return;

        // Process
        ProcessStepExecutionData(e);
    }

    protected virtual bool HasNextStep()
    {
        if (_stepExecutionDatas is null)
        {
            _stepExecutionDatas = [];
            return true;
        }
        else
        {
            foreach (var contextData in _stepContextDatas)
            {
                var executionData = _stepExecutionDatas.Find(execution => execution.Name == contextData.Name);
                if (executionData is null)
                    return true;
            }
            return false;
        }
    }

    protected abstract void StartNextStep();

    protected virtual void ProcessJobExecutionStatus(ExecutionStatus status)
    {
        switch (status)
        {
            case ExecutionStatus.Run:
                _jobExecutionData.ExecutionId = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                _jobExecutionData.StartTime = DateTime.Now;
                break;

            case ExecutionStatus.Success:
            case ExecutionStatus.Failed:
            case ExecutionStatus.Stopped:
                _jobExecutionData.EndTime = DateTime.Now;
                _jobExecutionData.Duration = _jobExecutionData.StartTime - _jobExecutionData.EndTime;
                break;
        }
        _jobExecutionData.ExecutionStatus = status;
        JobExecutionChanged?.Invoke(_jobExecutionData.Name, _jobExecutionData);
    }

    protected virtual void ProcessStepExecutionData(ExecutionData data)
    {
        var executionData = _stepExecutionDatas.Find(execution => execution.Name == data.Name);
        if (executionData is null)
            _stepExecutionDatas.Add(data);
        else
        {
            executionData.StartTime = data.StartTime;
            executionData.EndTime = data.EndTime;
            executionData.Duration = data.Duration;
            executionData.ExecutionStatus = data.ExecutionStatus;
        }

        // Pend or kill process
        if (_jobExecutionData.ExecutionStatus == ExecutionStatus.Pausing)
        {
            ProcessJobExecutionStatus(ExecutionStatus.Paused);
            return;
        }
        else if (_jobExecutionData.ExecutionStatus == ExecutionStatus.Stopping)
        {
            ProcessJobExecutionStatus(ExecutionStatus.Stopped);
            return;
        }

        switch (data.ExecutionStatus)
        {
            case ExecutionStatus.Success:
                if (HasNextStep())
                    StartNextStep();
                else
                    ProcessJobExecutionStatus(ExecutionStatus.Success);
                break;

            case ExecutionStatus.Failed:
                ProcessJobExecutionStatus(ExecutionStatus.Failed);
                break;
        }
    }
}