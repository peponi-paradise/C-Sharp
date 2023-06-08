using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMStudy.Models;
using MVVMStudy.ViewModels.Components;
using MVVMStudy.ViewModels.Windows;
using MVVMStudy.Views.Components;
using MVVMStudy.Views.Windows;
using System;
using System.Reflection;
using System.Windows;

namespace MVVMStudy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _Host;

        public App()
        {
            Messenger.Default = new Messenger(isMultiThreadSafe: true, actionReferenceType: ActionReferenceType.WeakReference);
            _Host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                context.HostingEnvironment.ApplicationName = $"{Assembly.GetExecutingAssembly().GetName()}";
                ConfigureServices(services);
            }).Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Services

            // Models

            services.AddTransient<DateTimeModel>();

            // ViewModels

            services.AddTransient<DateTimeSenderViewModel>();
            services.AddTransient<DateTimeViewViewModel>();
            services.AddSingleton<MainViewModel>();

            // Views
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _Host.StartAsync();
            _Host.Services.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_Host)
            {
                await _Host.StopAsync();
            }
            base.OnExit(e);
        }
    }
}