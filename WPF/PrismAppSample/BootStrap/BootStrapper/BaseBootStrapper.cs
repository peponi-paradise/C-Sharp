﻿using Common.Services;
using Define.BootStrap;
using Define.Constants;
using Define.Services;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using View.Components;
using View.Views;
using View.Windows;

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
        string viewModelAssemblyName = Assemblies.ViewModel;
        try
        {
            Assembly.Load(viewModelAssemblyName);

            // Register base rule of ViewModel. To connecting specific ViewModel, use bottom of this function

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelAssemblyFullName = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.FullName.Contains(viewModelAssemblyName)).FirstOrDefault().ToString();     // 동일 이름의 어셈블리가 없어야 할 조건 필요..
                var viewModelNamespaceUpper = viewType.FullName.Split('.')[viewType.FullName.Split('.').Length - 2];    // -2는 클래스 상위 네임스페이스 (Views, Windows 등..) 얻어올 목적
                var viewModelName = $"{viewModelAssemblyName}.{viewModelNamespaceUpper}.{viewType.Name}{Assemblies.ViewModel}, {viewModelAssemblyFullName}";
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
        containerRegistry.RegisterSingleton<IFileService<BitmapImage>, ImageFileService>();

        // Models

        // ViewModels

        // GUI

        containerRegistry.RegisterSingleton<TextView>();
        containerRegistry.RegisterSingleton<PictureView>();

        // Navigation

        containerRegistry.RegisterForNavigation<TextView>(Regions.ViewRegion);
        containerRegistry.RegisterForNavigation<PictureView>(Regions.ViewRegion);
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        base.ConfigureModuleCatalog(moduleCatalog);
    }

    protected override DependencyObject CreateShell()
    {
        // Region register
        var regionManager = Container.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion<Navigator>(Regions.NavigationRegion);
        regionManager.RegisterViewWithRegion<TextView>(Regions.ViewRegion);
        regionManager.RegisterViewWithRegion<PictureView>(Regions.ViewRegion);
        regionManager.RegisterViewWithRegion<TextReader>(Regions.TextViewRegion);

        // Manual resolve
        Container.Resolve<IFileService<string>>();
        Container.Resolve<IFileService<BitmapImage>>();

        // Create default window
        return Container.Resolve<MainWindow>();
    }
}