namespace Workloads.Step;

public interface IStepContext
{
    event EventHandler<ExecutionData>? StepExecutionChanged;

    void Start();

    ExecutionData GetExecutionData();
}