using System.Windows;
using System.Windows.Media;

namespace Peponi.WPF.ThemeProvider.Fonts;

public static class FontProvider
{
    private static ResourceDictionary? _resource;
    private const string Primary = "Primary";
    private const string Secondary = "Secondary";
    private const string Tertiary = "Tertiary";

    public static bool SetFont(string dictionaryName, string fontFamilyName)
    {
        try
        {
            FontFamily family = new(fontFamilyName);
            _resource![dictionaryName] = family;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SetFont(string dictionaryName, string fontFamilyName, string uri)
    {
        try
        {
            FontFamily family = new(new Uri(uri), fontFamilyName);
            _resource![dictionaryName] = family;
            return true;
        }
        catch
        {
            return false;
        }
    }

    internal static void InitializeInternal(ResourceDictionary resource)
    {
        _resource = resource;
        _resource[Primary] = _resource["Pretendard"];
        _resource[Secondary] = _resource["RobotoFlex"];
        _resource[Tertiary] = _resource["Consolas"];
    }
}