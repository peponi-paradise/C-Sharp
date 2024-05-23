namespace Workloads.Step;

public interface IStepExecution
{
    event EventHandler<ExecutionData>? StepExecutionChanged;

    ExecutionData? DoWork();
}