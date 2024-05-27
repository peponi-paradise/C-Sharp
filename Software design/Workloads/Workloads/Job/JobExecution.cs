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

    public virtual void Start()
    {
        if (HasNextStep())
            StartNextStep();

        ProcessJobExecutionStatus(ExecutionStatus.Run);
    }

    public virtual bool CouldStop()
    {
        if (_jobExecutionData.ExecutionStatus != ExecutionStatus.Paused)
            return false;
        return true;
    }

    public virtual void Stop() => ProcessJobExecutionStatus(ExecutionStatus.Stopped);

    public virtual bool CouldPause()
    {
        if (_stepExecutionDatas is null || _stepExecutionDatas.Count == 0)
            return false;
        if (_jobExecutionData.ExecutionStatus != ExecutionStatus.Run)
            return false;

        return true;
    }

    public virtual void Pause() => _jobExecutionData.ExecutionStatus = ExecutionStatus.Pausing;

    public virtual bool CouldResume()
    {
        if (_stepExecutionDatas is null || _stepExecutionDatas.Count == 0)
            return false;
        if (_jobExecutionData.ExecutionStatus != ExecutionStatus.Paused)
            return false;

        return true;
    }

    public virtual void Resume()
    {
        _jobExecutionData.ExecutionStatus = ExecutionStatus.Run;

        ProcessStepExecutionData(_stepExecutionDatas.Last());
    }

    public virtual ExecutionData GetJobExecutionData() => _jobExecutionData;

    public virtual List<ExecutionData>? GetStepExecutionDatas() => _stepExecutionDatas;

    protected virtual void ProcessStepExecutionChanged(object? sender, ExecutionData data)
    {
        // Redirect step's report
        StepExecutionChanged?.Invoke(sender, data);

        // Check is right step
        var stepContext = _stepContextDatas.Find(context => context.Name == data.Name);
        if (stepContext is null)
            return;

        // Check if step is not sequential
        if (stepContext.ContextType != StepContextType.Sequential)
            return;

        // Process
        ProcessStepExecutionData(data);
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