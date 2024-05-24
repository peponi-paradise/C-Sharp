namespace Workloads;

public record ExecutionData
{
    public string Name { get; init; }
    public ulong ExecutionId { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? Duration { get; set; }

    public ExecutionStatus ExecutionStatus { get; set; } = ExecutionStatus.Idle;

    public ExecutionData(string name) => Name = name;

    public ExecutionData(string name, ulong executionId)
    {
        Name = name;
        ExecutionId = executionId;
    }
}