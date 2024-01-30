using Peponi.WPF.ThemeProvider.Colors;
using Peponi.WPF.ThemeProvider.Fonts;
using System.Windows.Media;

namespace Peponi.WPF.ThemeProvider;

public static class ThemeProvider
{
    public static void UseWindowsAccentColor(ColorMode colorMode = ColorMode.Auto) => ColorProvider.UseWindowsAccentColor(colorMode);

    public static void SetColor(Color color, ColorMode colorMode = ColorMode.Auto) => ColorProvider.SetColor(color, colorMode);

    public static void SetColor(Color primary, Color secondary, Color tertiary, ColorMode colorMode = ColorMode.Auto) => ColorProvider.SetColor(primary, secondary, tertiary, colorMode);

    public static void SetColors(string xamlPath, ColorMode colorMode = ColorMode.Auto) => ColorProvider.SetColors(xamlPath, colorMode);

    public static void SetColors(Dictionary<string, object> collection, ColorMode colorMode = ColorMode.Auto) => ColorProvider.SetColors(collection, colorMode);

    public static void SetColorMode(ColorMode colorMode) => ColorProvider.SetColorMode(colorMode);

    public static bool SetFont(string dictionaryName, string fontFamilyName) => FontProvider.SetFont(dictionaryName, fontFamilyName);

    public static bool SetFont(string dictionaryName, string fontFamilyName, string uri) => FontProvider.SetFont(dictionaryName, fontFamilyName, uri);
}