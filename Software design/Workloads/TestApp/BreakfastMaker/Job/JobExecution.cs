using BreakfastMaker.Step;
using Workloads;
using Workloads.Job;
using Workloads.Step;

namespace BreakfastMaker.Job;

public class BreakfastJobExecution : IJobExecution
{
    public event EventHandler<ExecutionData>? JobExecutionChanged;

    public event EventHandler<ExecutionData>? StepExecutionChanged;

    private JobContextData _jobContextData;
    private List<StepContextData> _stepContextDatas;
    private ExecutionData _jobExecutionData;
    private List<ExecutionData>? _stepExecutionDatas;

    public BreakfastJobExecution(JobContextData jobData, List<StepContextData> stepDatas)
    {
        _jobContextData = jobData;
        _stepContextDatas = stepDatas;
        _jobExecutionData = new(_jobContextData.Name);
    }

    public ExecutionData GetJobExecutionData() => _jobExecutionData;

    public List<ExecutionData>? GetStepExecutionDatas() => _stepExecutionDatas;

    public bool Start()
    {
        if (_stepExecutionDatas is not null)
            return false;

        var firstStep = BreakfastStepFactory.GetStepContext(_stepContextDatas.First());
        firstStep.StepExecutionChanged += Step_StepExecutionChanged;
        if (firstStep.Start())
        {
            _stepExecutionDatas = [firstStep.GetExecutionData()];
            _jobExecutionData.ExecutionId = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            _jobExecutionData.StartTime = DateTime.Now;
            _jobExecutionData.ExecutionStatus = ExecutionStatus.Run;
            JobExecutionChanged?.Invoke(_jobExecutionData.Name, _jobExecutionData);
            return true;
        }
        else
        {
            firstStep.StepExecutionChanged -= Step_StepExecutionChanged;
            return false;
        }
    }

    public bool Stop()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Resume()
    {
        throw new NotImplementedException();
    }

    private void Step_StepExecutionChanged(object? sender, ExecutionData e)
    {
        StepExecutionChanged?.Invoke(e.Name, e);
        if (e.ExecutionStatus == ExecutionStatus.Success)
        {
            foreach (var data in _stepContextDatas)
            {
                if (_stepExecutionDatas!.Find(execution => execution.Name == data.Name) is null)
                {
                    // Start next
                    var nextStep = BreakfastStepFactory.GetStepContext(data);
                    nextStep.StepExecutionChanged += Step_StepExecutionChanged;
                    if (nextStep.Start())
                    {
                        _stepExecutionDatas.Add(nextStep.GetExecutionData());
                        StepExecutionChanged?.Invoke(nextStep.GetExecutionData().Name, nextStep.GetExecutionData());
                    }
                }
                else
                {
                    _jobExecutionData.EndTime = DateTime.Now;
                    _jobExecutionData.Duration = _jobExecutionData.EndTime - _jobExecutionData.StartTime;
                    _jobExecutionData.ExecutionStatus = ExecutionStatus.Success;
                    JobExecutionChanged?.Invoke(_jobExecutionData.Name, _jobExecutionData);
                }
            }
        }
    }
}