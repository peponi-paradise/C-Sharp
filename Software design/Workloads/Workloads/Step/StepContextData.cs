namespace Workloads.Step;

public record StepContextData
{
    public string Name { get; init; }
    public uint StepNumber { get; init; }

    /// <summary>
    /// Determine is it mapped context
    /// </summary>
    public bool IsMapped { get; set; }
    public List<StepContextData>? MappedContexts { get; set; }

    public StepContextData(string name, uint stepNumber)
    {
        Name = name;
        StepNumber = stepNumber;
    }
}