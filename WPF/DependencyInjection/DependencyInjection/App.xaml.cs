using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DependencyInjection
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public readonly IHost _Host;

        public App()
        {
            _Host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                context.HostingEnvironment.ApplicationName = "DependencyInjection";
                ConfigureServices(services);
            }).Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<MainViewModel>();
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