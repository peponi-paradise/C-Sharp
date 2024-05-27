using Workloads.Step;

namespace Cooking.Step;

public record PreparingIngredientsStepContextData : StepContextData
{
    public PreparingIngredientsStepContextData(string name, uint stepNumber) : base(name, stepNumber)
    {
    }
}

public record PlatingStepContextData : StepContextData
{
    public bool IsEggLeft { get; init; }
    public PlatingStepContextData(string name, uint stepNumber, bool isEggLeft) : base(name, stepNumber)
    {
        IsEggLeft = isEggLeft;
    }
}

public record FryingStepContextData : StepContextData
{
    public TimeSpan FryingTime { get; init; }
    public FryingStepContextData(string name, uint stepNumber, TimeSpan fryingTime) : base(name, stepNumber)
    {
        FryingTime = fryingTime;
    }
}

public record BoilingStepContextData : StepContextData
{
    public TimeSpan BoilingTime { get; init; }
    public BoilingStepContextData(string name, uint stepNumber, TimeSpan boilingTime) : base(name, stepNumber)
    {
        BoilingTime = boilingTime;
    }
}