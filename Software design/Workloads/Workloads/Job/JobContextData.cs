namespace Workloads.Job;

public record JobContextData
{
    public required string Name { get; set; }
    public bool IsAbandoned { get; set; }
    public bool UseScheduler { get; set; }

    public DateTime NextStartTime { get; set; } = DateTime.Now;
    public TimeSpan TimeInterval { get; set; } = TimeSpan.Zero;

    public bool AllowMultipleExecutions { get; set; }
    public uint MaxExecutionInstancesCount { get; set; }
}