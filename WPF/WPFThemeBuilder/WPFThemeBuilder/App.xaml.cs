using Peponi.WPF.ThemeProvider;
using System.Windows;

namespace WPFThemeBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeProvider.UseWindowsAccentColor();
            base.OnStartup(e);
        }
    }
}