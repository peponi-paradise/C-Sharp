using Common.Services;
using Define.BootStrap;
using Define.Services;
using View.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using View.Components;
using View.Views;

namespace BootStrap.BootStrapper;

public class BaseBootStrapper : PrismBootstrapper, IBootStrap
{
    public BaseBootStrapper()
    {
    }

    public bool InitDataBase(string? defaultPath)
    {
        // Do nothing. 꼭 필요한 작업이 있으면 여기서
        throw new NotImplementedException();
    }

    public bool InitStaticResources()
    {
        // Do nothing. 꼭 필요한 작업이 있으면 여기서
        throw new NotImplementedException();
    }

    public new void Run() => base.Run();

    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();

        // ViewModel Assembly check
        string viewModelAssemblyName = "ViewModel";
        try
        {
            Assembly.Load(viewModelAssemblyName);

            // Register base rule of ViewModel. To connecting specific ViewModel, use bottom of this function

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelAssemblyFullName = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.FullName.Contains(viewModelAssemblyName)).FirstOrDefault().ToString();     // 동일 이름의 어셈블리가 없어야 할 조건 필요..
                var viewModelNamespaceUpper = viewType.FullName.Split('.')[viewType.FullName.Split('.').Length - 2];    // -2는 클래스 상위 네임스페이스 (Views, Windows 등..) 얻어올 목적
                var viewModelName = $"{viewModelAssemblyName}.{viewModelNamespaceUpper}.{viewType.Name}ViewModel, {viewModelAssemblyFullName}";
                return Type.GetType(viewModelName);
            });

            // Specific ViewModel
        }
        catch { /*ViewModel Assembly name Not matched or File not exist*/ }
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Configure objects (Singleton, Scoped, etc..)

        // Services

        containerRegistry.RegisterSingleton<IFileService<string>, TextFileService>();

        // Models

        // ViewModels

        // GUI

        containerRegistry.RegisterSingleton<TextView>();
        containerRegistry.RegisterSingleton<PictureView>();

        // Navigation

        containerRegistry.RegisterForNavigation<TextView>("ViewRegion");
        containerRegistry.RegisterForNavigation<PictureView>("ViewRegion");
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
    }

    protected override DependencyObject CreateShell()
    {
        // Region register
        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion<Navigator>("NavigationRegion");
        regionManager.RegisterViewWithRegion<TextView>("ViewRegion");
        regionManager.RegisterViewWithRegion<PictureView>("ViewRegion");
        regionManager.RegisterViewWithRegion<TextReader>("TextViewRegion");

        // Manual resolve
        Container.Resolve<IFileService<string>>();

        // Create default window
        return Container.Resolve<MainWindow>();
    }
}