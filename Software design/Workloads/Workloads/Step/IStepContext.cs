namespace Workloads.Step;

public interface IStepContext
{
    event EventHandler<ExecutionData>? StepExecutionChanged;

    bool Start();

    ExecutionData GetExecutionData();
}