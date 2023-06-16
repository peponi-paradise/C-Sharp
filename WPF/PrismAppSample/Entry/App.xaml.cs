using BootStrap;
using System.Windows;

namespace Entry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            BootStrapBase bootstrapBase = new BootStrapBase();
            bootstrapBase.Run();
        }
    }
}