namespace Workloads.Job;

public abstract class JobScheduler
{
    public abstract bool Register(IJobContext jobContext);

    public abstract bool Unregister(IJobContext jobContext);

    public abstract bool Start();

    public abstract bool Stop();
}