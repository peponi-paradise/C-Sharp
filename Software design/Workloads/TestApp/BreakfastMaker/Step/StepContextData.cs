using Workloads.Step;

namespace BreakfastMaker.Step;

public record PreparingIngredientsStepContextData : StepContextData
{
    public PreparingIngredientsStepContextData(string name) : base(name)
    {
    }
}

public record PlatingStepContextData : StepContextData
{
    public bool IsEggLeft { get; init; }
    public PlatingStepContextData(string name, bool isEggLeft) : base(name)
    {
        IsEggLeft = isEggLeft;
    }
}

public record FryingStepContextData : StepContextData
{
    public TimeSpan FryingTime { get; init; }
    public FryingStepContextData(string name, TimeSpan fryingTime) : base(name)
    {
        FryingTime = fryingTime;
    }
}

public record BoilingStepContextData : StepContextData
{
    public TimeSpan BoilingTime { get; init; }
    public BoilingStepContextData(string name, TimeSpan boilingTime) : base(name)
    {
        BoilingTime = boilingTime;
    }
}