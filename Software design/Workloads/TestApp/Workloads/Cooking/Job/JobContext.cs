using Cooking.Step;
using Workloads.Job;
using Workloads.Step;

namespace Cooking.Job;

public class CookingJobContext : JobContext
{
    public CookingJobContext(JobContextData data) : base(data)
    {
        if (_jobContextData.UseScheduler)
        {
            // Register context to scheduler
        }

        LoadData();
    }

    public override void Start()
    {
        if (_jobContextData.UseScheduler)
        {
            // Trigger start of scheduler
            return;
        }
        else
        {
            // Manual start
            if (CouldStart())
            {
                IJobExecution execution = new CookingJobExecution(_jobContextData, _stepContextDatas);
                execution.JobExecutionChanged += JobExecutionDataChanged;
                execution.StepExecutionChanged += StepExecutionDataChanged;
                execution.Start();
                _executions.Add(execution);
                return;
            }
        }
    }

    public override void Stop()
    {
        if (!CouldStop())
            return;

        if (_jobContextData.UseScheduler)
        {
            // Stop scheduler
        }

        foreach (var execution in _executions)
            execution.Stop();
    }

    private void LoadData()
    {
        // 파일에서 불러온다 가정

        // Get Preparing step
        _stepContextDatas = [new PreparingIngredientsStepContextData("Preparing Ingredients", 0)];

        // Get Cooking step
        var cookingStep = new StepContextData("Cooking", 1)
        {
            MappedContexts =
            [
                new FryingStepContextData("Egg", 1001, TimeSpan.FromSeconds(5))
                {
                    ContextType = StepContextType.Mapped
                },
                new FryingStepContextData("Bacon", 1002, TimeSpan.FromSeconds(3))
                {
                    ContextType = StepContextType.Mapped
                },
                new BoilingStepContextData("Coffee", 1010, TimeSpan.FromSeconds(10))
                {
                    ContextType = StepContextType.Mapped
                }
            ]
        };
        _stepContextDatas.Add(cookingStep);

        // Get Plating step
        _stepContextDatas.Add(new PlatingStepContextData("Plating", 2, true));
    }
}