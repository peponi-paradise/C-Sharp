using Workloads;
using Workloads.Job;
using Workloads.Step;
using BreakfastMaker.Step;

namespace BreakfastMaker.Job;

public class BreakfastJobContext : IJobContext
{
    public event EventHandler<ExecutionData>? JobExecutionChanged;

    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private JobContextData _jobContextData;
    private List<StepContextData> _stepContextDatas = new();
    private List<IJobExecution> _executions = new();

    public BreakfastJobContext(JobContextData data)
    {
        _jobContextData = data;
        LoadData();
    }

    public bool Start()
    {
        if (_jobContextData.UseScheduler)
        {
            // Register context to scheduler
            // Trigger start of scheduler
            return false;
        }
        else
        {
            // Manual start
            if (CouldStart())
            {
                IJobExecution execution = new BreakfastJobExecution(_jobContextData, _stepContextDatas);
                execution.JobExecutionChanged += JobExecution_JobExecutionChanged;
                execution.StepExecutionChanged += JobExecution_StepExecutionChanged;
                if (execution.Start())
                {
                    _executions.Add(execution);
                    return true;
                }
            }
            return false;
        }
    }

    public async Task<bool> Stop()
    {
        if (_executions.Count == 0)
            return false;

        foreach (var execution in _executions)
        {
            if (execution.GetJobExecutionData().ExecutionStatus != ExecutionStatus.Idle)
                return false;
        }

        if (_jobContextData.UseScheduler)
        {
            // Stop scheduler
            // Unregister context to scheduler
        }

        foreach (var execution in _executions)
        {
            if (!execution.Stop())
                return false;
        }

        await Task.Run(() =>
        {
            while (_executions.Any(execution => execution.GetJobExecutionData().ExecutionStatus == ExecutionStatus.Run))
            {
                Task.Delay(100);
            }
        });

        foreach (var execution in _executions)
        {
            execution.JobExecutionChanged -= JobExecution_JobExecutionChanged;
            execution.StepExecutionChanged -= JobExecution_StepExecutionChanged;
        }
        _executions.Clear();

        return true;
    }

    public async Task<bool> Stop(ulong executionId)
    {
        IJobExecution? currentExecution = null;
        foreach (var execution in _executions)
        {
            if (execution.GetJobExecutionData().ExecutionId == executionId)
            {
                currentExecution = execution;
                break;
            }
        }
        if (currentExecution is null)
            return false;

        if (!currentExecution.Stop())
            return false;

        await Task.Run(() =>
        {
            while (currentExecution.GetJobExecutionData().ExecutionStatus == ExecutionStatus.Run)
            {
                Task.Delay(100);
            }
        });

        currentExecution.JobExecutionChanged -= JobExecution_JobExecutionChanged;
        currentExecution.StepExecutionChanged -= JobExecution_StepExecutionChanged;
        _executions.Remove(currentExecution);

        return true;
    }

    public async Task<bool> Pause()
    {
        if (_executions.Count == 0)
            return false;

        foreach (var execution in _executions)
            execution.Pause();

        await Task.Run(() =>
        {
            while (_executions.Any(execution => execution.GetJobExecutionData().ExecutionStatus == ExecutionStatus.Run))
            {
                Task.Delay(100);
            }
        });

        return true;
    }

    public async Task<bool> Pause(ulong executionId)
    {
        IJobExecution? currentExecution = null;
        foreach (var execution in _executions)
        {
            if (execution.GetJobExecutionData().ExecutionId == executionId)
            {
                currentExecution = execution;
                break;
            }
        }
        if (currentExecution is null)
            return false;

        currentExecution.Pause();

        await Task.Run(() =>
        {
            while (currentExecution.GetJobExecutionData().ExecutionStatus == ExecutionStatus.Run)
            {
                Task.Delay(100);
            }
        });

        return true;
    }

    public bool Resume()
    {
        if (_executions.Count == 0)
            return false;

        foreach (var execution in _executions)
            execution.Resume();

        return true;
    }

    public bool Resume(ulong executionId)
    {
        IJobExecution? currentExecution = null;
        foreach (var execution in _executions)
        {
            if (execution.GetJobExecutionData().ExecutionId == executionId)
            {
                currentExecution = execution;
                break;
            }
        }
        if (currentExecution is null)
            return false;

        currentExecution.Resume();

        return true;
    }

    public JobContextData GetJobContextData() => _jobContextData;

    public List<StepContextData> GetStepContextDatas() => _stepContextDatas;

    public List<ExecutionData>? GetJobExecutionDatas()
    {
        if (_executions.Count == 0)
            return null;

        var rtn = new List<ExecutionData>();

        foreach (var execution in _executions)
            rtn.Add(execution.GetJobExecutionData());

        return rtn;
    }

    public ExecutionData GetJobExecutionData(ulong executionId)
    {
        IJobExecution? currentExecution = null;
        foreach (var execution in _executions)
        {
            if (execution.GetJobExecutionData().ExecutionId == executionId)
            {
                currentExecution = execution;
                break;
            }
        }
        if (currentExecution is null)
            throw new ArgumentOutOfRangeException(nameof(executionId), $"{executionId} is not present on executions");

        return currentExecution.GetJobExecutionData();
    }

    public List<ExecutionData>? GetStepExecutionDatas(ulong executionId)
    {
        IJobExecution? currentExecution = null;
        foreach (var execution in _executions)
        {
            if (execution.GetJobExecutionData().ExecutionId == executionId)
            {
                currentExecution = execution;
                break;
            }
        }
        if (currentExecution is null)
            throw new ArgumentOutOfRangeException(nameof(executionId), $"{executionId} is not present on executions");

        return currentExecution.GetStepExecutionDatas();
    }

    private void JobExecution_JobExecutionChanged(object? sender, ExecutionData e)
    {
        JobExecutionChanged?.Invoke(e.Name, e);
    }

    private void JobExecution_StepExecutionChanged(object? sender, ExecutionData e)
    {
        StepExecutionChanged?.Invoke(e.Name, e);
    }

    private void LoadData()
    {
        // 파일에서 불러온다 가정

        // Get Preparing step
        _stepContextDatas = [new PreparingIngredientsStepContextData("Preparing Ingredients") { StepNumber = 0 }];

        // Get Cooking step
        var cookingStep = new StepContextData("Cooking")
        {
            StepNumber = 1,
            MappedContexts =
            [
                new FryingStepContextData("Egg", TimeSpan.FromSeconds(5))
                {
                    IsMapped = true,
                    StepNumber = 1001
                },
                new FryingStepContextData("Bacon", TimeSpan.FromSeconds(3))
                {
                    IsMapped = true,
                    StepNumber = 1002
                },
                new BoilingStepContextData("Coffee", TimeSpan.FromSeconds(10))
                {
                    IsMapped = true,
                    StepNumber = 1010
                }
            ]
        };
        _stepContextDatas.Add(cookingStep);

        // Get Plating step
        _stepContextDatas.Add(new PlatingStepContextData("Plating", true) { StepNumber = 2 });
    }

    private bool CouldStart()
    {
        if (_executions.Count == 0)
            return true;
        else
        {
            if (_jobContextData.AllowMultipleExecutions)
            {
                if (_executions.Count < _jobContextData.MaxExecutionInstancesCount)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}