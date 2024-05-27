using CommunityToolkit.Mvvm.DependencyInjection;
using Cooking.JobOperator;
using Microsoft.Extensions.DependencyInjection;
using Workloads.JobOperator;

namespace TestApp;

internal static class BootStrap
{
    public static void ConfigureServices()
    {
        Ioc.Default.ConfigureServices(ConfigureServicesProvider());
    }

    private static IServiceProvider ConfigureServicesProvider()
    {
        var collection = new ServiceCollection();

        collection.AddSingleton<IJobOperator, CookingJobOperator>();

        return collection.BuildServiceProvider();
    }
}