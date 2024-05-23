namespace Workloads.Step;

public record StepContextData
{
    public string Name { get; init; }
    public uint StepNumber { get; set; }

    public bool IsMapped { get; set; }
    public List<StepContextData>? MappedContexts { get; set; }

    public StepContextData(string name)
    {
        Name = name;
    }
}