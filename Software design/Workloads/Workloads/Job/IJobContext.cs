using Workloads.Step;

namespace Workloads.Job;

public interface IJobContext
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    bool Start();

    Task<bool> Stop();

    Task<bool> Stop(ulong executionId);

    Task<bool> Pause();

    Task<bool> Pause(ulong executionId);

    bool Resume();

    bool Resume(ulong executionId);

    JobContextData GetJobContextData();

    List<StepContextData> GetStepContextDatas();

    List<ExecutionData>? GetJobExecutionDatas();

    ExecutionData GetJobExecutionData(ulong executionId);

    List<ExecutionData>? GetStepExecutionDatas(ulong executionId);
}