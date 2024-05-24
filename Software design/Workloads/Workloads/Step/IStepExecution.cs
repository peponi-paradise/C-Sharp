namespace Workloads.Step;

public interface IStepExecution
{
    event EventHandler<ExecutionData>? StepExecutionChanged;

    void Start();

    ExecutionData GetExecutionData();
}