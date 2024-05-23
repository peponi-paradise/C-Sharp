using Workloads;
using Workloads.Step;

namespace BreakfastMaker.Step;

public class PreparingIngredientsStepExecution : IStepExecution
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private ExecutionData _executionData;
    private PreparingIngredientsStepContextData _contextData;
    private Task? _task;

    public PreparingIngredientsStepExecution(PreparingIngredientsStepContextData data)
    {
        _contextData = data;
        _executionData = new(_contextData.Name);
    }

    public ExecutionData? DoWork()
    {
        try
        {
            _task = Task.Run(Work);
        }
        catch
        {
            return null;
        }
        _executionData.ExecutionId = _contextData.StepNumber;
        return _executionData;
    }

    private async void Work()
    {
        RaiseEvent(ExecutionStatus.Run);

        // Preparing...
        await Task.Delay(2000);

        int number = Random.Shared.Next(10);
        if (number < 8) RaiseEvent(ExecutionStatus.Success);
        else RaiseEvent(ExecutionStatus.Failed);
    }

    private void RaiseEvent(ExecutionStatus status)
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

public class PlatingStepExecution : IStepExecution
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private ExecutionData _executionData;
    private PlatingStepContextData _contextData;
    private Task? _task;

    public PlatingStepExecution(PlatingStepContextData data)
    {
        _contextData = data;
        _executionData = new(_contextData.Name);
    }

    public ExecutionData? DoWork()
    {
        try
        {
            _task = Task.Run(Work);
        }
        catch
        {
            return null;
        }
        _executionData.ExecutionId = _contextData.StepNumber;
        return _executionData;
    }

    private async void Work()
    {
        RaiseEvent(ExecutionStatus.Run);

        // Plating...
        if (_contextData.IsEggLeft)
        {
            await Task.Delay(2000);
        }
        else
        {
            await Task.Delay(5000);
        }

        int number = Random.Shared.Next(10);
        if (number < 8) RaiseEvent(ExecutionStatus.Success);
        else RaiseEvent(ExecutionStatus.Failed);
    }

    private void RaiseEvent(ExecutionStatus status)
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

public class FryingStepExecution : IStepExecution
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private ExecutionData _executionData;
    private FryingStepContextData _contextData;
    private Task? _task;

    public FryingStepExecution(FryingStepContextData data)
    {
        _contextData = data;
        _executionData = new(_contextData.Name);
    }

    public ExecutionData? DoWork()
    {
        try
        {
            _task = Task.Run(Work);
        }
        catch
        {
            return null;
        }
        _executionData.ExecutionId = _contextData.StepNumber;
        return _executionData;
    }

    private async void Work()
    {
        RaiseEvent(ExecutionStatus.Run);

        // Frying...
        await Task.Delay(_contextData.FryingTime);

        int number = Random.Shared.Next(10);
        if (number < 8) RaiseEvent(ExecutionStatus.Success);
        else RaiseEvent(ExecutionStatus.Failed);
    }

    private void RaiseEvent(ExecutionStatus status)
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

public class BoilingStepExecution : IStepExecution
{
    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private ExecutionData _executionData;
    private BoilingStepContextData _contextData;
    private Task? _task;

    public BoilingStepExecution(BoilingStepContextData data)
    {
        _contextData = data;
        _executionData = new(_contextData.Name);
    }

    public ExecutionData? DoWork()
    {
        try
        {
            _task = Task.Run(Work);
        }
        catch
        {
            return null;
        }
        _executionData.ExecutionId = _contextData.StepNumber;
        return _executionData;
    }

    private async void Work()
    {
        RaiseEvent(ExecutionStatus.Run);

        // Boiling...
        await Task.Delay(_contextData.BoilingTime);

        int number = Random.Shared.Next(10);
        if (number < 8) RaiseEvent(ExecutionStatus.Success);
        else RaiseEvent(ExecutionStatus.Failed);
    }

    private void RaiseEvent(ExecutionStatus status)
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