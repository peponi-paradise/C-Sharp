using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace WPFThemeBuilder;

public enum ColorMode
{
    Bright,
    Dark,
    Both
}

public static class ThemeProvider
{
    private static ResourceDictionary? _appResource;

    public static void ApplyWindowsColorStyle()
    {
        ChangeColor(IsThemeLight());
        SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
    }

    public static void ApplyColors(Color primary, ColorMode colorMode = ColorMode.Both)
    {
    }

    public static void ApplyColors(Color primary, Color secondary, Color Tertiary, ColorMode colorMode = ColorMode.Both)
    {
    }

    public static void ApplyColorSet(string xamlPath)
    {
        var res = new ResourceDictionary() { Source = new Uri(xamlPath, UriKind.RelativeOrAbsolute) };
        foreach (var item in res.Keys) _appResource![item] = res[item];
    }

    public static void ApplyColorSet(Dictionary<string, object> collection)
    {
        if (collection != null)
        {
            foreach (var item in collection)
            {
                _appResource![item.Key] = item.Value;
            }
        }
    }

    private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        switch (e.Category)
        {
            case UserPreferenceCategory.General:
                ChangeColor(IsThemeLight());
                break;
        }
    }

    internal static void InitializeColor(ResourceDictionary appResource)
    {
        _appResource = appResource;
        foreach (var item in _appResource.Keys)
        {
            if (_appResource[item] is SolidColorBrush) _appResource[item] = new SolidColorBrush(Color.FromRgb(222, 155, 88));
            else if (_appResource[item] is Color) _appResource[item] = Color.FromRgb(102, 22, 155);
        }
    }

    private static void ChangeColor(bool isLight)
    {
        if (isLight)
        {
            var res = new ResourceDictionary() { Source = new Uri("./Colors/Purple.xaml", UriKind.RelativeOrAbsolute) };
            foreach (var item in res.Keys) _appResource![item] = res[item];
            //foreach (var item in res.Keys) _appResource![item] = new SolidColorBrush(GetAccentColor());
        }
        else
        {
            var res = new ResourceDictionary() { Source = new Uri("./Colors/Blue.xaml", UriKind.RelativeOrAbsolute) };
            foreach (var item in res.Keys) _appResource![item] = res[item];
        }
    }

    private static bool IsThemeLight()
    {
        RegistryKey registry =
            Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")!;
        return (int)registry.GetValue("SystemUsesLightTheme")! == 1;
    }

    [DllImport("uxtheme.dll", EntryPoint = "#95")]
    private static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType,
                                                                    bool bIgnoreHighContrast, uint dwHighContrastCacheMode);

    [DllImport("uxtheme.dll", EntryPoint = "#96")]
    private static extern uint GetImmersiveColorTypeFromName(IntPtr pName);

    [DllImport("uxtheme.dll", EntryPoint = "#98")]
    private static extern int GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

    private static Color GetAccentColor()
    {
        var userColorSet = GetImmersiveUserColorSetPreference(false, false);
        var colorType = GetImmersiveColorTypeFromName(Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground"));
        var colorSetEx = GetImmersiveColorFromColorSetEx((uint)userColorSet, colorType, false, 0);
        return ConvertDWordColorToRGB(colorSetEx);
    }

    private static Color ConvertDWordColorToRGB(uint colorSetEx)
    {
        byte redColor = (byte)((0x000000FF & colorSetEx) >> 0);
        byte greenColor = (byte)((0x0000FF00 & colorSetEx) >> 8);
        byte blueColor = (byte)((0x00FF0000 & colorSetEx) >> 16);
        byte alphaColor = (byte)((0xFF000000 & colorSetEx) >> 24);
        return Color.FromArgb(alphaColor, redColor, greenColor, blueColor);
    }

    private static (int H, float S, float L) ToHSL(this Color color)
    {
        float r = (color.R / 255.0f);
        float g = (color.G / 255.0f);
        float b = (color.B / 255.0f);

        float min = Math.Min(Math.Min(r, g), b);
        float max = Math.Max(Math.Max(r, g), b);
        float delta = max - min;

        int h;
        float s;
        float l = (max + min) / 2;

        if (delta == 0)
        {
            h = 0;
            s = 0.0f;
        }
        else
        {
            s = (l <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

            float hue;

            if (r == max)
            {
                hue = ((g - b) / 6) / delta;
            }
            else if (g == max)
            {
                hue = (1.0f / 3) + ((b - r) / 6) / delta;
            }
            else
            {
                hue = (2.0f / 3) + ((r - g) / 6) / delta;
            }

            if (hue < 0)
                hue += 1;
            if (hue > 1)
                hue -= 1;

            h = (int)(hue * 360);
        }

        return (h, s, l);
    }
}