using BreakfastMaker.JobOperator;
using CommunityToolkit.Mvvm.DependencyInjection;
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

        collection.AddSingleton<IJobOperator, BreakfastJobOperator>();

        return collection.BuildServiceProvider();
    }
}