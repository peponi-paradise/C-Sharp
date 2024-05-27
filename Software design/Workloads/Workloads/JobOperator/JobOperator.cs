using Workloads.Job;
using Workloads.Step;

namespace Workloads.JobOperator;

public abstract class JobOperator : IJobOperator
{
    public event EventHandler<ExecutionData>? JobExecutionChanged;

    public event EventHandler<ExecutionData>? StepExecutionChanged;

#nullable disable
    protected List<JobContextData> _jobContextDatas;
    protected List<IJobContext> _jobContexts;
#nullable enable

    public virtual bool CouldStart()
    {
        if (_jobContexts is null || _jobContexts.Count == 0)
            return false;

        foreach (var context in _jobContexts)
        {
            if (!context.CouldStart())
                return false;
        }

        return true;
    }

    public virtual bool CouldStart(string jobName)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldStart();
    }

    public virtual bool Start()
    {
        if (!CouldStart()) return false;

        foreach (var context in _jobContexts)
            context.Start();

        return true;
    }

    public virtual bool Start(string jobName)
    {
        if (!CouldStart(jobName)) return false;

        GetJobContext(jobName)?.Start();
        return true;
    }

    public virtual bool CouldStop()
    {
        if (_jobContexts is null || _jobContexts.Count == 0)
            return false;

        foreach (var context in _jobContexts)
        {
            if (!context.CouldStop())
                return false;
        }

        return true;
    }

    public virtual bool CouldStop(string jobName)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldStop();
    }

    public virtual bool CouldStop(string jobName, ulong executionId)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldStop(executionId);
    }

    public virtual bool Stop()
    {
        if (!CouldStop()) return false;

        foreach (var context in _jobContexts)
            context.Stop();

        return true;
    }

    public virtual bool Stop(string jobName)
    {
        if (!CouldStop(jobName)) return false;

        GetJobContext(jobName)?.Stop();

        return true;
    }

    public virtual bool Stop(string jobName, ulong executionId)
    {
        if (!CouldStop(jobName, executionId)) return false;

        GetJobContext(jobName)?.Stop(executionId);

        return true;
    }

    public virtual bool CouldPause()
    {
        if (_jobContexts is null || _jobContexts.Count == 0)
            return false;

        foreach (var context in _jobContexts)
        {
            if (!context.CouldPause())
                return false;
        }

        return true;
    }

    public virtual bool CouldPause(string jobName)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldPause();
    }

    public virtual bool CouldPause(string jobName, ulong executionId)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldPause(executionId);
    }

    public virtual bool Pause()
    {
        if (!CouldPause()) return false;

        foreach (var context in _jobContexts)
            context.Pause();

        return true;
    }

    public virtual bool Pause(string jobName)
    {
        if (!CouldPause(jobName)) return false;

        GetJobContext(jobName)?.Pause();
        return true;
    }

    public virtual bool Pause(string jobName, ulong executionId)
    {
        if (!CouldPause(jobName, executionId)) return false;

        GetJobContext(jobName)?.Pause(executionId);
        return true;
    }

    public virtual bool CouldResume()
    {
        if (_jobContexts is null || _jobContexts.Count == 0)
            return false;

        foreach (var context in _jobContexts)
        {
            if (!context.CouldResume())
                return false;
        }

        return true;
    }

    public virtual bool CouldResume(string jobName)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldResume();
    }

    public virtual bool CouldResume(string jobName, ulong executionId)
    {
        var context = GetJobContext(jobName);
        if (context is null)
            return false;

        return context.CouldResume(executionId);
    }

    public virtual bool Resume()
    {
        if (!CouldResume()) return false;

        foreach (var context in _jobContexts)
            context.Resume();

        return true;
    }

    public virtual bool Resume(string jobName)
    {
        if (!CouldResume(jobName)) return false;

        GetJobContext(jobName)?.Resume();
        return true;
    }

    public virtual bool Resume(string jobName, ulong executionId)
    {
        if (!CouldPause(jobName, executionId)) return false;

        GetJobContext(jobName)?.Resume(executionId);
        return true;
    }

    public virtual bool SetAbandon(string jobName, bool isAbandon)
    {
        var context = GetJobContext(jobName);
        var contextData = GetJobContextData(jobName);
        if (context is null || contextData is null)
            return false;

        context.GetJobContextData().IsAbandoned = isAbandon;
        contextData.IsAbandoned = isAbandon;

        return true;
    }

    public virtual int GetJobContextsCount() => _jobContextDatas != null ? _jobContextDatas.Count : 0;

    public virtual List<string>? GetJobNames()
    {
        if (_jobContextDatas is null)
            return null;

        var rtns = new List<string>();

        _jobContextDatas.ForEach(data => rtns.Add(data.Name));

        return rtns;
    }

    public virtual JobContextData? GetJobContextData(string jobName)
    {
        if (_jobContextDatas is null)
            return null;

        return _jobContextDatas.Find(context => context.Name == jobName);
    }

    public virtual List<JobContextData>? GetJobContextDatas() => _jobContextDatas;

    public virtual List<StepContextData>? GetStepContextDatas(string jobName) => GetJobContext(jobName)?.GetStepContextDatas();

    public virtual ExecutionData? GetJobExecutionData(string jobName, ulong executionId) => GetJobContext(jobName)?.GetJobExecutionData(executionId);

    public virtual List<ExecutionData>? GetJobExecutionDatas(string jobName) => GetJobContext(jobName)?.GetJobExecutionDatas();

    public virtual List<ExecutionData>? GetStepExecutionDatas(string jobName, ulong executionId) => GetJobContext(jobName)?.GetStepExecutionDatas(executionId);

    protected virtual void JobExecutionDataChanged(object? sender, ExecutionData data) => JobExecutionChanged?.Invoke(sender, data);

    protected virtual void StepExecutionDataChanged(object? sender, ExecutionData data) => StepExecutionChanged?.Invoke(sender, data);

    protected virtual IJobContext? GetJobContext(string jobName)
    {
        if (_jobContexts is null || _jobContexts.Count == 0)
            return null;

        return _jobContexts.Find(context => context.GetJobContextData().Name == jobName);
    }

    protected virtual void ConnectEvents(IJobContext context)
    {
        context.JobExecutionChanged += JobExecutionDataChanged;
        context.StepExecutionChanged += StepExecutionDataChanged;
    }
}