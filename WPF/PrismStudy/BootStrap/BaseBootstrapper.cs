using Define.Bootstrap;
using GUI.Views.Windows;
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
        // Do nothing
        throw new System.NotImplementedException();
    }

    public bool InitStaticResources()
    {
        // Do nothing
        throw new System.NotImplementedException();
    }

    public new void Run() => base.Run();

    protected override DependencyObject CreateShell()
    {
        int a = 4;
        // Region injection
        var regionManager = Container.Resolve<IRegionManager>();

        // Create default window
        return Container.Resolve<MainWindowTypeA>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Configure objects (Singleton, Scoped, etc..)

        // Services

        // Models

        // ViewModels

        // GUI

        int a = 2;
    }

    protected override void ConfigureViewModelLocator()
    {
        int a = 1;
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

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        int a = 3;
        base.ConfigureModuleCatalog(moduleCatalog);
    }
}