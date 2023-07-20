using CommunityToolkit.Mvvm.DependencyInjection;
using Library.Services;
using Library.Services.SerialPort;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace SerialPort
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(ConfigureServiceProvider());
        }

        private static IServiceProvider ConfigureServiceProvider()
        {
            var collection = new ServiceCollection();

            collection.AddSingleton<SerialPortService>();

            collection.AddSingleton<SerialPortModel>();

            collection.AddSingleton<SerialPortViewModel>();

            collection.AddSingleton<MainWindow>();

            return collection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Ioc.Default.GetRequiredService<SerialPortService>();

            Ioc.Default.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }
    }
}