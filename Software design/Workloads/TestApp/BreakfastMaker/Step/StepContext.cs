using Workloads;
using Workloads.Step;

namespace BreakfastMaker.Step;

public class PreparingIngredientsStepContext : StepContext
{
    public PreparingIngredientsStepContext(StepContextData data) : base(data)
    {
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
    }
}

public class CookingStepContext : StepContext
{
    public CookingStepContext(StepContextData data) : base(data)
    {
        if (data.MappedContexts is not null)
        {
            _mappedContexts = [];
            foreach (var contextData in data.MappedContexts)
            {
                var context = BreakfastStepFactory.GetStepContext(contextData);
                context.StepExecutionChanged += MappedContextsExecutionChanged;
                _mappedContexts.Add(context);
            }
        }
    }
}

public class PlatingStepContext : StepContext
{
    public PlatingStepContext(StepContextData data) : base(data)
    {
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
    }
}

public class FryingStepContext : StepContext
{
    public FryingStepContext(StepContextData data) : base(data)
    {
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
    }
}

public class BoilingStepContext : StepContext
{
    public BoilingStepContext(StepContextData data) : base(data)
    {
        _execution = BreakfastStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += ExecutionChanged;
    }
}