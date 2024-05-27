namespace Workloads.Step;

public abstract class StepContext : IStepContext
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

#nullable disable
    protected StepContextData _contextData;

    /// <summary>
    /// For mapped context
    /// </summary>
    protected ExecutionData _mergedExecutionData;

    protected IStepExecution _execution;

    /// <summary>
    /// For mapped context
    /// </summary>
    protected List<IStepContext> _mappedContexts;

    protected object _statusLocker = new();
#nullable enable

    /// <summary>
    /// ctor : 상속받는 클래스에서 구현<br/>
    /// protected fields : 상속받는 클래스의 생성자에서 넣음
    /// </summary>
    /// <param name="data"></param>
    public StepContext(StepContextData data)
    {
        _contextData = data;
        _mergedExecutionData = new(_contextData.Name, _contextData.StepNumber);
    }

    public virtual void Start()
    {
        if (_mappedContexts is null)
            SingleStep();
        else
            MappedStep();
    }

    public virtual ExecutionData GetExecutionData()
    {
        if (_mappedContexts is null)
            return _execution.GetExecutionData();
        else
            return _mergedExecutionData;
    }

    protected virtual void StepExecutionDataChanged(object? sender, ExecutionData e) => StepExecutionChanged?.Invoke(sender, e);

    protected virtual void MappedContextsExecutionDataChanged(object? sender, ExecutionData e)
    {
        // Redirect mapped context's report
        StepExecutionChanged?.Invoke(sender, e);

        lock (_statusLocker)
        {
            // process this context's status

            bool statusChanged = false;
            if (_mappedContexts.All(context => context.GetExecutionData().ExecutionStatus == ExecutionStatus.Run) && _mergedExecutionData.ExecutionStatus == ExecutionStatus.Idle)
            {
                _mergedExecutionData.ExecutionId = _contextData.StepNumber;
                _mergedExecutionData.StartTime = DateTime.Now;
                _mergedExecutionData.ExecutionStatus = ExecutionStatus.Run;
                statusChanged = true;
            }
            else if (_mappedContexts.All(context => context.GetExecutionData().ExecutionStatus == ExecutionStatus.Success) && _mergedExecutionData.ExecutionStatus == ExecutionStatus.Run)
            {
                _mergedExecutionData.EndTime = DateTime.Now;
                _mergedExecutionData.Duration = _mergedExecutionData.EndTime - _mergedExecutionData.StartTime;
                _mergedExecutionData.ExecutionStatus = ExecutionStatus.Success;
                statusChanged = true;
            }
            else if (_mappedContexts.Any(context => context.GetExecutionData().ExecutionStatus == ExecutionStatus.Failed) && _mergedExecutionData.ExecutionStatus == ExecutionStatus.Run)
            {
                _mergedExecutionData.EndTime = DateTime.Now;
                _mergedExecutionData.Duration = _mergedExecutionData.EndTime - _mergedExecutionData.StartTime;
                _mergedExecutionData.ExecutionStatus = ExecutionStatus.Failed;
                statusChanged = true;
            }

            if (statusChanged)
                StepExecutionChanged?.Invoke(_mergedExecutionData.Name, _mergedExecutionData);
        }
    }

    protected virtual void SingleStep() => _execution.Start();

    protected virtual void MappedStep()
    {
        foreach (var context in _mappedContexts)
            context.Start();
    }
}