using Workloads.Job;
using Workloads.Step;

namespace Workloads.JobOperator;

public interface IJobOperator
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    bool CouldStart();

    bool CouldStart(string jobName);

    bool Start();

    bool Start(string jobName);

    bool CouldStop();

    bool CouldStop(string jobName);

    bool CouldStop(string jobName, ulong executionId);

    bool Stop();

    bool Stop(string jobName);

    bool Stop(string jobName, ulong executionId);

    bool CouldPause();

    bool CouldPause(string jobName);

    bool CouldPause(string jobName, ulong executionId);

    bool Pause();

    bool Pause(string jobName);

    bool Pause(string jobName, ulong executionId);

    bool CouldResume();

    bool CouldResume(string jobName);

    bool CouldResume(string jobName, ulong executionId);

    bool Resume();

    bool Resume(string jobName);

    bool Resume(string jobName, ulong executionId);

    bool SetAbandon(string jobName, bool isAbandon);

    int GetJobContextsCount();

    List<string>? GetJobNames();

    JobContextData? GetJobContextData(string jobName);

    List<JobContextData>? GetJobContextDatas();

    List<StepContextData>? GetStepContextDatas(string jobName);

    ExecutionData? GetJobExecutionData(string jobName, ulong executionId);

    List<ExecutionData>? GetJobExecutionDatas(string jobName);

    List<ExecutionData>? GetStepExecutionDatas(string jobName, ulong executionId);
}