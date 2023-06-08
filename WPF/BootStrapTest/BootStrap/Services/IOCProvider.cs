using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using BootStrap.Interfaces;
using System.Configuration;
using System.CodeDom;
using System.Diagnostics.CodeAnalysis;

namespace BootStrap.Services
{
    public static class IOCProvider
    {
        private static ServiceCollection ServiceCollection = new ServiceCollection();

        public static ServiceCollection AddSingleton<T>() where T : class
        {
            return (ServiceCollection)ServiceCollection.AddSingleton<T>();
        }

        public static ServiceCollection AddSingleton<TInterface, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return (ServiceCollection)ServiceCollection.AddSingleton<TInterface, TImplementation>();
        }

        public static void Build()
        {
            var services = ServiceCollection.BuildServiceProvider();
            Ioc.Default.ConfigureServices(services);
        }
    }
}