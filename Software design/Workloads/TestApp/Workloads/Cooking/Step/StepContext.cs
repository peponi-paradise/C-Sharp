using Workloads.Step;

namespace Cooking.Step;

public class PreparingIngredientsStepContext : StepContext
{
    public PreparingIngredientsStepContext(StepContextData data) : base(data)
    {
        _execution = CookingStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += StepExecutionDataChanged;
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
                var context = CookingStepFactory.GetStepContext(contextData);
                context.StepExecutionChanged += MappedContextsExecutionDataChanged;
                _mappedContexts.Add(context);
            }
        }
    }
}

public class PlatingStepContext : StepContext
{
    public PlatingStepContext(StepContextData data) : base(data)
    {
        _execution = CookingStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += StepExecutionDataChanged;
    }
}

public class FryingStepContext : StepContext
{
    public FryingStepContext(StepContextData data) : base(data)
    {
        _execution = CookingStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += StepExecutionDataChanged;
    }
}

public class BoilingStepContext : StepContext
{
    public BoilingStepContext(StepContextData data) : base(data)
    {
        _execution = CookingStepFactory.GetStepExecution(_contextData);
        _execution.StepExecutionChanged += StepExecutionDataChanged;
    }
}