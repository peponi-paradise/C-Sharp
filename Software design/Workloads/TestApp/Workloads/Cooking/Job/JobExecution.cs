using Cooking.Step;
using Workloads.Job;
using Workloads.Step;

namespace Cooking.Job;

public class CookingJobExecution : JobExecution
{
    public CookingJobExecution(JobContextData jobData, List<StepContextData> stepDatas) : base(jobData, stepDatas)
    {
    }

    protected override void StartNextStep()
    {
        var nextStep = GetNextStep();
        if (nextStep != null)
        {
            nextStep.StepExecutionChanged += ProcessStepExecutionChanged;
            nextStep.Start();
            _stepExecutionDatas.Add(nextStep.GetExecutionData());
        }
    }

    IStepContext? GetNextStep()
    {
        foreach (var data in _stepContextDatas)
        {
            var executionData = _stepExecutionDatas.Find(execution => execution.Name == data.Name);
            if (executionData is null)
            {
                return CookingStepFactory.GetStepContext(data);
            }
        }
        return null;
    }
}