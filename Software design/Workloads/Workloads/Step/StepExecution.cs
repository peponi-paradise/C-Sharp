namespace Workloads.Step;

public abstract class StepExecution : IStepExecution
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    protected StepContextData _contextData;
    protected ExecutionData _executionData;
    protected Task? _task;

    public StepExecution(StepContextData data)
    {
        _contextData = data;
        _executionData = new(_contextData.Name, _contextData.StepNumber);
    }

    public virtual void Start()
    {
        _task = Task.Run(Work);
    }

    public ExecutionData GetExecutionData() => _executionData;

    protected abstract Task Work();

    protected virtual void ProcessExecutionStatus(ExecutionStatus status)
    {
        switch (status)
        {
            case ExecutionStatus.Run:
                _executionData.StartTime = DateTime.Now;
                break;

            case ExecutionStatus.Success:
            case ExecutionStatus.Failed:
                _executionData.EndTime = DateTime.Now;
                _executionData.Duration = _executionData.StartTime - _executionData.EndTime;
                break;
        }
        _executionData.ExecutionStatus = status;
        StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
    }
}