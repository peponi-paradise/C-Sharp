using MVVMStudy.ViewModels.Components;
using System.Windows.Controls;

namespace MVVMStudy.Views.Components
{
    /// <summary>
    /// Interaction logic for DateTimeSender.xaml
    /// </summary>
    public partial class DateTimeSender : UserControl
    {
        public DateTimeSender(DateTimeSenderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}