namespace Workloads.Job;

public interface IJobExecution
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    bool Start();

    bool Stop();

    void Pause();

    void Resume();

    ExecutionData GetJobExecutionData();

    List<ExecutionData>? GetStepExecutionDatas();
}