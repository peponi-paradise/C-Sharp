using Peponi.WPF.ThemeProvider.Fonts;
using System.Windows;

namespace Peponi.WPF.ThemeProvider;

public partial class FontKeys : ResourceDictionary
{
    public FontKeys()
    {
        InitializeComponent();
        FontProvider.InitializeInternal(this);
    }
}