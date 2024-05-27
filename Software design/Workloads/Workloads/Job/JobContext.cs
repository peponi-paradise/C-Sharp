using Workloads.Step;

namespace Workloads.Job;

public abstract class JobContext : IJobContext
{
    public event EventHandler<ExecutionData>? JobExecutionChanged;

    public event EventHandler<ExecutionData>? StepExecutionChanged;

    protected JobContextData _jobContextData;
#nullable disable
    protected List<StepContextData> _stepContextDatas;
#nullable enable
    protected List<IJobExecution> _executions = new();

    public JobContext(JobContextData data)
    {
        _jobContextData = data;
    }

    public virtual bool CouldStart()
    {
        if (_executions.Count == 0)
            return true;

        if (_jobContextData.IsAbandoned)
            return false;

        if (_jobContextData.AllowMultipleExecutions)
        {
            if (_executions.Count < _jobContextData.MaxExecutionInstancesCount)
                return true;
            else
                return false;
        }

        return true;
    }

    public abstract void Start();

    public virtual bool CouldStop()
    {
        if (_executions.Count == 0)
            return false;

        foreach (var execution in _executions)
        {
            if (!execution.CouldStop())
                return false;
        }

        return true;
    }

    public virtual bool CouldStop(ulong executionId) => GetJobExecution(executionId).CouldStop();

    public abstract void Stop();

    public virtual void Stop(ulong executionId)
    {
        if (!CouldStop(executionId))
            return;

        var execution = GetJobExecution(executionId);
        execution.Stop();
    }

    public virtual bool CouldPause()
    {
        if (_executions.Count == 0)
            return false;

        foreach (var execution in _executions)
        {
            if (!execution.CouldPause())
                return false;
        }

        return true;
    }

    public virtual bool CouldPause(ulong executionId)
    {
        if (_executions.Count == 0)
            return false;

        return GetJobExecution(executionId).CouldPause();
    }

    public virtual void Pause()
    {
        foreach (var execution in _executions)
            execution.Pause();
    }

    public virtual void Pause(ulong executionId) => GetJobExecution(executionId).Pause();

    public virtual bool CouldResume()
    {
        if (_executions.Count == 0)
            return false;

        foreach (var execution in _executions)
        {
            if (!execution.CouldResume())
                return false;
        }

        return true;
    }

    public virtual bool CouldResume(ulong executionId)
    {
        if (_executions.Count == 0)
            return false;

        return GetJobExecution(executionId).CouldResume();
    }

    public virtual void Resume()
    {
        foreach (var execution in _executions)
            execution.Resume();
    }

    public virtual void Resume(ulong executionId) => GetJobExecution(executionId).Resume();

    public virtual JobContextData GetJobContextData() => _jobContextData;

    public virtual List<StepContextData> GetStepContextDatas() => _stepContextDatas;

    public virtual ExecutionData GetJobExecutionData(ulong executionId) => GetJobExecution(executionId).GetJobExecutionData();

    public virtual List<ExecutionData>? GetJobExecutionDatas()
    {
        if (_executions.Count == 0)
            return null;

        var rtns = new List<ExecutionData>();

        _executions.ForEach(execution => rtns.Add(execution.GetJobExecutionData()));

        return rtns;
    }

    public virtual List<ExecutionData>? GetStepExecutionDatas(ulong executionId) => GetJobExecution(executionId).GetStepExecutionDatas();

    protected virtual void JobExecutionDataChanged(object? sender, ExecutionData data)
    {
        JobExecutionChanged?.Invoke(sender, data);

        if (IsJobEnd(data.ExecutionStatus))
        {
            var execution = GetJobExecution(data.ExecutionId);

            execution.JobExecutionChanged -= JobExecutionDataChanged;
            execution.StepExecutionChanged -= StepExecutionDataChanged;

            _executions.Remove(execution);
        }
    }

    protected virtual void StepExecutionDataChanged(object? sender, ExecutionData data) => StepExecutionChanged?.Invoke(sender, data);

    protected virtual IJobExecution GetJobExecution(ulong executionId)
    {
        var result = _executions.Find(execution => execution.GetJobExecutionData().ExecutionId == executionId);

        if (result is null)
            throw new ArgumentOutOfRangeException(nameof(executionId), $"{executionId} is not present on executions");
        else
            return result;
    }

    protected virtual bool IsJobEnd(ExecutionStatus status) => status == ExecutionStatus.Success || status == ExecutionStatus.Failed || status == ExecutionStatus.Stopped;
}