using BootStrap.Interfaces;
using BootStrap.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace BootStrapTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IOCProvider.AddSingleton<MainViewModel>();
            IOCProvider.Build();
        }
    }
}