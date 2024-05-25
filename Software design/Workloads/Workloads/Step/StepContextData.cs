namespace Workloads.Step;

public enum StepContextType
{
    Sequential,
    Mapped,
    Stopping,
}

public record StepContextData
{
    public string Name { get; init; }
    public uint StepNumber { get; init; }

    /// <summary>
    /// Determine is it mapped context
    /// </summary>
    public StepContextType ContextType { get; set; }
    public List<StepContextData>? MappedContexts { get; set; }

    public StepContextData(string name, uint stepNumber)
    {
        Name = name;
        StepNumber = stepNumber;
    }
}