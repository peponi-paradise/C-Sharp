using Peponi.MaterialDesign3.WPF;
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
            ThemeProvider.SetFontFamily("RobotoSerif");
            ThemeProvider.ChangeFontOption(FontKeys.DisplayMedium, new Peponi.MaterialDesign3.WPF.Fonts.FontOption(20, 26, FontWeights.Black));
            base.OnStartup(e);
        }
    }
}