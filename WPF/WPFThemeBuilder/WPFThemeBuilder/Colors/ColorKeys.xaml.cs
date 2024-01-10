using System.Windows;

namespace WPFThemeBuilder.Colors;

public partial class ColorKeys : ResourceDictionary
{
    public ColorKeys()
    {
        InitializeComponent();
        ThemeProvider.InitializeColor(this);
    }
}