using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Ioc;
using Prism.DryIoc;
using Prism.Mvvm;
using PrismStudy.Views;
using System.Reflection;

namespace PrismStudy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var window = Container.Resolve<Views.MainWindow>();
            return window;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<Services.ICustomerStore, Services.DbCustomerStore>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            //ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>(); 직접 등록. 뷰와 뷰모델 프로젝트가 분리되고, 규모가 커지면 너무 많은 규칙이 등록될 것.

            //이 방법으로 하면 다른 프로젝트의 뷰모델 참조 가능할듯.. 근데 적당하게 부트스트랩 만들 수 있으려나;?
            string viewModelAssemblyName = "ViewModel";
            string viewModelNameSpaceName = "ViewModels";
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewModelAssemblyFullName = AppDomain.CurrentDomain.GetAssemblies().Where(asm => asm.FullName.Contains(viewModelAssemblyName)).FirstOrDefault().ToString();     // 동일 이름의 어셈블리가 없어야 할 조건 필요..
                var viewModelName = $"{viewModelAssemblyName}.{viewModelNameSpaceName}.{viewType.Name}ViewModel, {viewModelAssemblyFullName}";
                return Type.GetType(viewModelName);
            });
        }
    }
}