using Workloads.Job;
using Workloads.Step;

namespace Workloads.JobOperator;

public interface IJobOperator
{
    event EventHandler<ExecutionData>? JobExecutionChanged;

    event EventHandler<ExecutionData>? StepExecutionChanged;

    bool Start();

    bool Start(string jobName);

    bool Stop(string jobName);

    bool Stop(string jobName, ulong executionId);

    bool Pause(string jobName);

    bool Pause(string jobName, ulong executionId);

    bool Resume(string jobName);

    bool Resume(string jobName, ulong executionId);

    bool SetAbandon(string jobName, bool isAbandon);

    List<string>? GetJobNames();

    int GetJobContextsCount();

    List<JobContextData>? GetJobContextDatas();

    JobContextData? GetJobContextData(string jobName);

    List<StepContextData>? GetStepContextDatas(string jobName);

    List<ExecutionData>? GetJobExecutionDatas(string jobName);

    ExecutionData? GetJobExecutionData(string jobName, ulong executionId);

    List<ExecutionData>? GetStepExecutionDatas(string jobName, ulong executionId);
}