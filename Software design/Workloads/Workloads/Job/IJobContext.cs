using Workloads.Step;

namespace Workloads.Job;

public interface IJobContext
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    bool CouldStart();

    void Start();

    bool CouldStop();

    bool CouldStop(ulong executionId);

    void Stop();

    void Stop(ulong executionId);

    bool CouldPause();

    bool CouldPause(ulong executionId);

    void Pause();

    void Pause(ulong executionId);

    bool CouldResume();

    bool CouldResume(ulong executionId);

    void Resume();

    void Resume(ulong executionId);

    JobContextData GetJobContextData();

    List<StepContextData> GetStepContextDatas();

    ExecutionData GetJobExecutionData(ulong executionId);

    List<ExecutionData>? GetJobExecutionDatas();

    List<ExecutionData>? GetStepExecutionDatas(ulong executionId);
}