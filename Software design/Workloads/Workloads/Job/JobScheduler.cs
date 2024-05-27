using System.Collections.Concurrent;
using System.Timers;

namespace Workloads.Job;

public abstract class JobScheduler
{
    protected List<IJobContext> _jobContexts = new();
    protected ConcurrentDictionary<string, ConcurrentQueue<IJobContext>> _nextJobs = new();
    protected System.Timers.Timer _timer;

    public JobScheduler()
    {
        _timer = new();
        _timer.Interval = 500;
        _timer.Elapsed += Scheduling;
    }

    public virtual bool Register(IJobContext jobContext)
    {
        if (!_jobContexts.Contains(jobContext))
        {
            _jobContexts.Add(jobContext);
            return true;
        }
        else
            return false;
    }

    public virtual bool Unregister(IJobContext jobContext)
    {
        if (_jobContexts.Contains(jobContext))
        {
            _jobContexts.Remove(jobContext);
            return true;
        }
        else
            return false;
    }

    public virtual bool Start()
    {
        if (_jobContexts.Count > 0)
        {
            // Already running
            if (_timer.Enabled)
                return true;

            StartupSetting();
            _timer.Start();
            return true;
        }
        return false;
    }

    public virtual void Stop() => _timer.Stop();

    protected virtual void Scheduling(object? sender, ElapsedEventArgs e)
    {
        ProcessEnqueue();
        ProcessDequeue();
    }

    protected virtual void StartupSetting()
    {
        _nextJobs.Clear();
        foreach (var context in _jobContexts)
            _nextJobs.TryAdd(context.GetJobContextData().Name, new());
    }

    protected virtual void ProcessEnqueue()
    {
        foreach (var context in _jobContexts)
        {
            if (CheckAbandoned(context))
                continue;

            if (IsStartTime(context))
            {
                bool isContained = Contains(context);
                if (!isContained)
                    _nextJobs[context.GetJobContextData().Name].Enqueue(context);
                else
                {
                    while (CheckQueueable(context))
                        _nextJobs[context.GetJobContextData().Name].Enqueue(context);
                }
            }

            UpdateNextStartTime(context);
        }
    }

    protected virtual void ProcessDequeue()
    {
        foreach (var job in _nextJobs)
        {
            var queue = job.Value;
            if (queue.IsEmpty)
                continue;

            while (CouldStart(queue))
            {
                if (queue.TryDequeue(out var context))
                {
                    context.Start();
                }
            }
        }
    }

    protected virtual bool CheckAbandoned(IJobContext context) => context.GetJobContextData().IsAbandoned;

    protected virtual bool IsStartTime(IJobContext context) => context.GetJobContextData().NextStartTime <= DateTime.Now;

    protected virtual bool Contains(IJobContext context) => !_nextJobs[context.GetJobContextData().Name].IsEmpty;

    protected virtual bool CheckQueueable(IJobContext context)
    {
        if (!context.GetJobContextData().AllowMultipleExecutions)
            return false;
        if (_nextJobs[context.GetJobContextData().Name].Count >= context.GetJobContextData().MaxExecutionInstancesCount)
            return false;

        return true;
    }

    protected virtual void UpdateNextStartTime(IJobContext context)
    {
        if (!IsStartTime(context))
            context.GetJobContextData().NextStartTime += context.GetJobContextData().TimeInterval;
    }

    protected virtual bool CouldStart(ConcurrentQueue<IJobContext> queue)
    {
        if (queue.IsEmpty)
            return false;
        if (!queue.TryPeek(out var context))
            return false;
        if (!context.CouldStart())
            return false;

        return true;
    }
}