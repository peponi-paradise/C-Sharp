namespace Workloads.Job;

public interface IJobExecution
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    void Start();

    bool Stop();

    Task Pause();

    void Resume();

    ExecutionData GetJobExecutionData();

    List<ExecutionData>? GetStepExecutionDatas();
}