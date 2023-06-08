using BootStrap.Interfaces;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace BootStrap.Locator
{
    public static class ViewModelLocator
    {
        public static DependencyProperty ViewModelProperty = DependencyProperty.RegisterAttached("ViewModel", typeof(string), typeof(ViewModelLocator), new PropertyMetadata("", ViewModelSet));

        public static string GetViewModel(UIElement element)
        {
            return (string)element.GetValue(ViewModelProperty);
        }

        public static void SetViewModel(UIElement element, string value)
        {
            element.SetValue(ViewModelProperty, value);
        }

        private static void ViewModelSet(DependencyObject obj, DependencyPropertyChangedEventArgs e) => BindViewModel(obj, e.NewValue.ToString());

        private static void BindViewModel(DependencyObject view, string? propertyValue)
        {
            if (string.IsNullOrWhiteSpace(propertyValue)) throw new ArgumentException($"Invalid ViewModel value : {propertyValue}");
            if (view is FrameworkElement element)
            {
                var assembly = System.Reflection.Assembly.GetAssembly(view.GetType());
                if (assembly == null) throw new InvalidOperationException($"Could not find assembly of {view.GetType()}");

                var viewModelType = assembly.GetType(propertyValue);
                if (viewModelType == null) throw new InvalidOperationException($"Could not find object named of {propertyValue}");

                var viewModel = Ioc.Default.GetService(typeof(IViewModel));
                if (viewModel != null) element.DataContext = viewModel;
                else element.DataContext = Activator.CreateInstance(viewModelType);
            }
        }
    }
}