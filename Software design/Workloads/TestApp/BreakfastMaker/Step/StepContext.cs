using Workloads;
using Workloads.Step;

namespace BreakfastMaker.Step;

public class PreparingIngredientsStepContext : IStepContext
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private StepContextData _contextData;
    private IStepExecution _execution;
    private ExecutionData? _executionData;

    public PreparingIngredientsStepContext(StepContextData data)
    {
        _contextData = data;
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
    }

    public ExecutionData GetExecutionData() => _executionData ?? throw new ArgumentNullException(nameof(ExecutionData), $"{nameof(ExecutionData)} is null");

    public bool Start()
    {
        var execution = _execution.DoWork();
        if (execution is not null)
        {
            _executionData = execution;
            StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
            return true;
        }
        return false;
    }

    private void ExecutionChanged(object? sender, ExecutionData e)
    {
        _executionData = e;
        StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
    }
}

public class CookingStepContext : IStepContext
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private StepContextData _contextData;
    private readonly List<IStepContext>? _mappedContexts;
    private ExecutionData? _executionData;

    public CookingStepContext(StepContextData data)
    {
        _contextData = data;
        _executionData = new(_contextData.Name);
        if (data.IsMapped && data.MappedContexts is not null)
        {
            _mappedContexts = [];
            foreach (var contextData in data.MappedContexts)
            {
                var context = BreakfastStepFactory.GetStepContext(contextData);
                context.StepExecutionChanged += SubContext_StepExecutionChanged;
                _mappedContexts.Add(context);
            }
        }
    }

    public ExecutionData GetExecutionData() => _executionData ?? throw new ArgumentNullException(nameof(ExecutionData), $"{nameof(ExecutionData)} is null");

    public bool Start()
    {
        if (_mappedContexts is null) return false;

        // Executing...
        foreach (var context in _mappedContexts)
        {
            if (!context.Start()) return false;
        }
        return true;
    }

    private void SubContext_StepExecutionChanged(object? sender, ExecutionData e)
    {
        StepExecutionChanged?.Invoke(sender, e);

        bool statusChanged = false;
        if (_mappedContexts!.All(context => context.GetExecutionData().ExecutionStatus == ExecutionStatus.Run))
        {
            _executionData!.ExecutionId = _contextData.StepNumber;
            _executionData.StartTime = DateTime.Now;
            _executionData.ExecutionStatus = ExecutionStatus.Run;
            statusChanged = true;
        }
        else if (_mappedContexts!.All(context => context.GetExecutionData().ExecutionStatus == ExecutionStatus.Success))
        {
            _executionData!.EndTime = DateTime.Now;
            _executionData.Duration = _executionData.EndTime - _executionData.StartTime;
            _executionData.ExecutionStatus = ExecutionStatus.Success;
            statusChanged = true;
        }
        else if (_mappedContexts!.Any(context => context.GetExecutionData().ExecutionStatus == ExecutionStatus.Failed))
        {
            _executionData!.EndTime = DateTime.Now;
            _executionData.Duration = _executionData.EndTime - _executionData.StartTime;
            _executionData.ExecutionStatus = ExecutionStatus.Failed;
            statusChanged = true;
        }

        if (statusChanged) StepExecutionChanged?.Invoke(_contextData.Name, _executionData!);
    }
}

public class PlatingStepContext : IStepContext
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private StepContextData _contextData;
    private IStepExecution _execution;
    private ExecutionData? _executionData;

    public PlatingStepContext(StepContextData data)
    {
        _contextData = data;
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
    }

    public ExecutionData GetExecutionData() => _executionData ?? throw new ArgumentNullException(nameof(ExecutionData), $"{nameof(ExecutionData)} is null");

    public bool Start()
    {
        var execution = _execution.DoWork();
        if (execution is not null)
        {
            _executionData = execution;
            StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
            return true;
        }
        return false;
    }

    private void ExecutionChanged(object? sender, ExecutionData e)
    {
        _executionData = e;
        StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
    }
}

public class FryingStepContext : IStepContext
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private StepContextData _contextData;
    private IStepExecution _execution;
    private ExecutionData? _executionData;

    public FryingStepContext(StepContextData data)
    {
        _contextData = data;
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
        _executionData = new(_contextData.Name);
    }

    public ExecutionData GetExecutionData() => _executionData ?? throw new ArgumentNullException(nameof(ExecutionData), $"{nameof(ExecutionData)} is null");

    public bool Start()
    {
        var execution = _execution.DoWork();
        if (execution is not null)
        {
            _executionData = execution;
            StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
            return true;
        }
        return false;
    }

    private void ExecutionChanged(object? sender, ExecutionData e)
    {
        _executionData = e;
        StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
    }
}

public class BoilingStepContext : IStepContext
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private StepContextData _contextData;
    private IStepExecution _execution;
    private ExecutionData? _executionData;

    public BoilingStepContext(StepContextData data)
    {
        _contextData = data;
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
        _executionData = new(_contextData.Name);
    }

    public ExecutionData GetExecutionData() => _executionData ?? throw new ArgumentNullException(nameof(ExecutionData), $"{nameof(ExecutionData)} is null");

    public bool Start()
    {
        var execution = _execution.DoWork();
        if (execution is not null)
        {
            _executionData = execution;
            StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
            return true;
        }
        return false;
    }

    private void ExecutionChanged(object? sender, ExecutionData e)
    {
        _executionData = e;
        StepExecutionChanged?.Invoke(_executionData.Name, _executionData);
    }
}