using System.Windows;

namespace Peponi.WPF.ThemeProvider.Colors;

public partial class ColorKeys : ResourceDictionary
{
    public ColorKeys()
    {
        InitializeComponent();
        ColorProvider.InitializeInternal(this);
    }
}