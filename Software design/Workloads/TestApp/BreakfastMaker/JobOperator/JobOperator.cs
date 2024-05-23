using BreakfastMaker.Job;
using Workloads;
using Workloads.Job;
using Workloads.JobOperator;
using Workloads.Step;

namespace BreakfastMaker.JobOperator;

public class BreakfastJobOperator : IJobOperator
{
    public event EventHandler<ExecutionData>? JobExecutionChanged;

    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private List<JobContextData>? _jobContextDatas;
    private List<IJobContext>? _jobContexts;

    public BreakfastJobOperator()
    {
        LoadData();
        SetContexts();
    }

    public JobContextData? GetJobContextData(string jobName)
    {
        if (_jobContextDatas is null)
            return null;

        foreach (var contextData in _jobContextDatas)
        {
            if (contextData.Name == jobName)
                return contextData;
        }

        return null;
    }

    public List<JobContextData>? GetJobContextDatas() => _jobContextDatas;

    public int GetJobContextsCount() => _jobContextDatas != null ? _jobContextDatas.Count : 0;

    public ExecutionData? GetJobExecutionData(string jobName, ulong executionId)
    {
        if (_jobContexts == null)
            return null;

        var context = _jobContexts.Find(context => context.GetJobContextData().Name == jobName);

        if (context is not null)
            return context.GetJobExecutionData(executionId);
        else
            return null;
    }

    public List<ExecutionData>? GetJobExecutionDatas(string jobName)
    {
        if (_jobContexts == null)
            return null;

        var context = _jobContexts.Find(context => context.GetJobContextData().Name == jobName);

        if (context is not null)
            return context.GetJobExecutionDatas();
        else
            return null;
    }

    public List<string>? GetJobNames()
    {
        if (_jobContextDatas is null)
            return null;

        List<string> rtn = [];

        foreach (var data in _jobContextDatas)
            rtn.Add(data.Name);

        return rtn;
    }

    public List<StepContextData>? GetStepContextDatas(string jobName)
    {
        if (_jobContexts == null)
            return null;

        var context = _jobContexts.Find(context => context.GetJobContextData().Name == jobName);

        if (context is not null)
            return context.GetStepContextDatas();
        else
            return null;
    }

    public List<ExecutionData>? GetStepExecutionDatas(string jobName, ulong executionId)
    {
        if (_jobContexts == null)
            return null;

        var context = _jobContexts.Find(context => context.GetJobContextData().Name == jobName);

        if (context is not null)
            return context.GetStepExecutionDatas(executionId);
        else
            return null;
    }

    public bool Pause(string jobName)
    {
        throw new NotImplementedException();
    }

    public bool Pause(string jobName, ulong executionId)
    {
        throw new NotImplementedException();
    }

    public bool Resume(string jobName)
    {
        throw new NotImplementedException();
    }

    public bool Resume(string jobName, ulong executionId)
    {
        throw new NotImplementedException();
    }

    public bool SetAbandon(string jobName, bool isAbandon)
    {
        throw new NotImplementedException();
    }

    public bool Start()
    {
        if (_jobContexts is null)
            return false;

        foreach (var context in _jobContexts)
        {
            if (!context.Start())
            {
                // Stop 처리
            }
        }
        return true;
    }

    public bool Start(string jobName)
    {
        throw new NotImplementedException();
    }

    public bool Stop(string jobName)
    {
        throw new NotImplementedException();
    }

    public bool Stop(string jobName, ulong executionId)
    {
        throw new NotImplementedException();
    }

    private void Context_JobExecutionChanged(object? sender, ExecutionData e) => JobExecutionChanged?.Invoke(sender, e);

    private void Context_StepExecutionChanged(object? sender, ExecutionData e) => StepExecutionChanged?.Invoke(sender, e);

    private void LoadData()
    {
        // 파일에서 불러오는 것으로 가정

        _jobContextDatas = [new JobContextData()
        {
            Name = "Breakfast",
        }];
    }

    private void SetContexts()
    {
        // Factory에서 꺼낸다고 가정

        var context = new BreakfastJobContext(_jobContextDatas![0]);
        context.JobExecutionChanged += Context_JobExecutionChanged;
        context.StepExecutionChanged += Context_StepExecutionChanged;

        _jobContexts = [context];
    }
}