using Common.Services;
using Define.Bootstrap;
using Define.Services;
using GUI.Views.Windows;
using ModelLib.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using ViewModelLib.Windows;

namespace Bootstrap;

public class BaseBootstrapper : PrismBootstrapper, IBootstrap
{
    public BaseBootstrapper()
    {
    }

    public bool InitDataBase(string? defaultPath)
    {
        // Do nothing. 꼭 필요한 작업이 있으면 여기서
        throw new System.NotImplementedException();
    }

    public bool InitStaticResources()
    {
        // Do nothing. 꼭 필요한 작업이 있으면 여기서
        throw new System.NotImplementedException();
    }

    public new void Run() => base.Run();

    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();

        // Register base rule of ViewModel. To connecting specific ViewModel, use bottom of this function

        string viewModelAssemblyName = "ViewModelLib";
        Assembly.Load(viewModelAssemblyName);
        ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
        {
            var viewModelAssemblyFullName = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.FullName.Contains(viewModelAssemblyName)).FirstOrDefault().ToString();     // 동일 이름의 어셈블리가 없어야 할 조건 필요..
            var viewModelNamespaceUpper = viewType.FullName.Split('.')[viewType.FullName.Split('.').Length - 2];    // -2는 클래스 상위 네임스페이스 (Views, Windows 등..) 얻어올 목적
            var viewModelName = $"{viewModelAssemblyName}.{viewModelNamespaceUpper}.{viewType.Name}ViewModel, {viewModelAssemblyFullName}";
            return Type.GetType(viewModelName);
        });

        // Specific ViewModel

        ViewModelLocationProvider.Register<MainWindowTypeA, WindowViewModel>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Configure objects (Singleton, Scoped, etc..)

        // Services

        containerRegistry.RegisterSingleton<IFileService<string>, TextFileService>();
        containerRegistry.RegisterSingleton<IFileService<MainWindowModel>, YAMLFileService<MainWindowModel>>();

        // Models

        // ViewModels

        // GUI
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
    }

    protected override DependencyObject CreateShell()
    {
        // Region injection
        var regionManager = Container.Resolve<IRegionManager>();

        // Manual resolve
        Container.Resolve<IFileService<string>>();

        // Create default window
        return Container.Resolve<MainWindowTypeA>();
    }
}