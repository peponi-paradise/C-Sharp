using Cooking.Job;
using Workloads.Job;

namespace Cooking.JobOperator;

public class CookingJobOperator : Workloads.JobOperator.JobOperator
{
    public CookingJobOperator()
    {
        LoadData();
        SetContexts();
    }

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

        var context = new CookingJobContext(_jobContextDatas![0]);
        ConnectEvents(context);

        _jobContexts = [context];
    }
}