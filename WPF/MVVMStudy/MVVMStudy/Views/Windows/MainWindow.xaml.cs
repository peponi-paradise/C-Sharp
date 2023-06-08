using DevExpress.Xpf.Core;
using MVVMStudy.ViewModels.Windows;
using Microsoft.Extensions.Hosting;

namespace MVVMStudy.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow(MainViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}