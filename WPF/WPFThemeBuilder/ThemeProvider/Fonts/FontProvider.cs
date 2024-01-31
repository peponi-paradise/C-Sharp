using System.Windows;
using System.Windows.Media;

namespace Peponi.WPF.ThemeProvider.Fonts;

public static class FontProvider
{
    private static ResourceDictionary? _resource;
    private const string FontFamily = "FontFamily";

    public static bool AddFontFamily(string fontFamily)
    {
        try
        {
            FontFamily family = new(fontFamily);
            _resource![fontFamily] = family;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool AddFontFamily(string fontFamily, string uri)
    {
        try
        {
            FontFamily family = new(new Uri(uri), fontFamily);
            _resource![fontFamily] = family;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SetFontFamily(string fontFamily)
    {
        if (!_resource!.Contains(fontFamily)) return false;

        _resource[FontFamily] = _resource[fontFamily];
        return true;
    }

    internal static void InitializeInternal(ResourceDictionary resource)
    {
        _resource = resource;
        _resource[FontFamily] = _resource["RobotoFlex"];
    }
}