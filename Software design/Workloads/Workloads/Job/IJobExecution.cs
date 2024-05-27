namespace Workloads.Job;

public interface IJobExecution
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    void Start();

    bool CouldStop();

    void Stop();

    bool CouldPause();

    void Pause();

    bool CouldResume();

    void Resume();

    ExecutionData GetJobExecutionData();

    List<ExecutionData>? GetStepExecutionDatas();
}