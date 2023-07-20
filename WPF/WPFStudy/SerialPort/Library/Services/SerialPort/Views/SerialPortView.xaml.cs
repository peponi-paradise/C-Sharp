using CommunityToolkit.Mvvm.DependencyInjection;
using System.Windows.Controls;

namespace Library.Services.SerialPort;

/// <summary>
/// SerialPortControlView.xaml에 대한 상호 작용 논리
/// </summary>
public partial class SerialPortView : UserControl
{
    public SerialPortView()
    {
        DataContext = Ioc.Default.GetRequiredService<SerialPortViewModel>();
        InitializeComponent();
    }
}