using Peponi.WPF.ThemeProvider.Colors;
using System.Windows;

namespace Peponi.WPF.ThemeProvider;

public partial class ColorKeys : ResourceDictionary
{
    public ColorKeys()
    {
        InitializeComponent();
        ColorProvider.InitializeInternal(this);
    }
}