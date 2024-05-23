using Workloads.Step;

namespace BreakfastMaker.Step;

public static class BreakfastStepFactory
{
    public static IStepContext GetStepContext(StepContextData data)
    {
        return data switch
        {
            PreparingIngredientsStepContextData => new PreparingIngredientsStepContext(data),
            PlatingStepContextData => new PlatingStepContext(data),
            FryingStepContextData => new FryingStepContext(data),
            BoilingStepContextData => new BoilingStepContext(data),
            _ => new CookingStepContext(data)
        };
    }

    public static IStepExecution GetStepExecution(StepContextData data)
    {
        return data switch
        {
            PreparingIngredientsStepContextData prepare => new PreparingIngredientsStepExecution(prepare),
            PlatingStepContextData plate => new PlatingStepExecution(plate),
            FryingStepContextData fry => new FryingStepExecution(fry),
            BoilingStepContextData boil => new BoilingStepExecution(boil),
            _ => throw new ArgumentException(nameof(StepContextData), $"{nameof(StepContextData)} is not defined")
        };
    }
}