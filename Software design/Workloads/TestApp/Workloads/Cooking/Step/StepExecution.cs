using Workloads;
using Workloads.Step;

namespace Cooking.Step;

public class PreparingIngredientsStepExecution(StepContextData data) : StepExecution(data)
{
    protected override async Task Work()
    {
        ProcessExecutionStatus(ExecutionStatus.Run);

        // Preparing...
        await Task.Delay(2000);

        int number = Random.Shared.Next(100);
        if (number < 95) ProcessExecutionStatus(ExecutionStatus.Success);
        else ProcessExecutionStatus(ExecutionStatus.Failed);
    }
}

public class PlatingStepExecution(StepContextData data) : StepExecution(data)
{
    private readonly PlatingStepContextData _derivedContextData = (PlatingStepContextData)data;

    protected override async Task Work()
    {
        ProcessExecutionStatus(ExecutionStatus.Run);

        // Plating...
        if (_derivedContextData.IsEggLeft)
        {
            await Task.Delay(2000);
        }
        else
        {
            await Task.Delay(5000);
        }

        int number = Random.Shared.Next(100);
        if (number < 95) ProcessExecutionStatus(ExecutionStatus.Success);
        else ProcessExecutionStatus(ExecutionStatus.Failed);
    }
}

public class FryingStepExecution(StepContextData data) : StepExecution(data)
{
    private readonly FryingStepContextData _derivedContextData = (FryingStepContextData)data;

    protected override async Task Work()
    {
        ProcessExecutionStatus(ExecutionStatus.Run);

        // Frying...
        await Task.Delay(_derivedContextData.FryingTime);

        int number = Random.Shared.Next(100);
        if (number < 95) ProcessExecutionStatus(ExecutionStatus.Success);
        else ProcessExecutionStatus(ExecutionStatus.Failed);
    }
}

public class BoilingStepExecution(StepContextData data) : StepExecution(data)
{
    private readonly BoilingStepContextData _derivedContextData = (BoilingStepContextData)data;

    protected override async Task Work()
    {
        ProcessExecutionStatus(ExecutionStatus.Run);

        // Boiling...
        await Task.Delay(_derivedContextData.BoilingTime);

        int number = Random.Shared.Next(100);
        if (number < 95) ProcessExecutionStatus(ExecutionStatus.Success);
        else ProcessExecutionStatus(ExecutionStatus.Failed);
    }
}