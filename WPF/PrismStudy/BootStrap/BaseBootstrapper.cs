using Define.Bootstrap;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using GUI.Views.Windows;
using Prism.Mvvm;
using System;
using System.Linq;
using Prism.Regions;

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
        // View injection
        var regionManager = Container.Resolve<IRegionManager>();
        // Create default window
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();

        string viewModelAssemblyName = "ViewModelLib";
        ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
        {
            var viewModelAssemblyFullName = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.FullName.Contains(viewModelAssemblyName)).FirstOrDefault().ToString();     // 동일 이름의 어셈블리가 없어야 할 조건 필요..
            var viewModelNamespaceUpper = viewType.FullName.Split('.')[viewType.FullName.Split('.').Length - 1];
            var viewModelName = $"{viewModelAssemblyName}.{viewModelNamespaceUpper}.{viewType.Name}ViewModel, {viewModelAssemblyFullName}";
            return Type.GetType(viewModelName);
        });
    }
}