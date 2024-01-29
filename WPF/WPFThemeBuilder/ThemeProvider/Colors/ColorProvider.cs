using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

namespace Peponi.WPF.ThemeProvider.Colors;

public enum ColorMode
{
    Light,
    Dark,
    Auto
}

public static class ColorProvider
{
    private static ResourceDictionary? _resource;
    private static Dictionary<string, TonalPalette> _palettes;

    private const string Primary = "Primary";
    private const string Secondary = "Secondary";
    private const string Tertiary = "Tertiary";
    private const string Neutral = "Neutral";
    private const string NeutralVariant = "NeutralVariant";
    private const string Error = "Error";

    private static bool _useWindowsAccentColor = false;
    private static ColorMode _colorMode = ColorMode.Auto;

    static ColorProvider()
    {
        _palettes = new Dictionary<string, TonalPalette>
        {
            { Primary, new() },
            { Secondary, new() },
            { Tertiary, new() },
            { Neutral, new() },
            { NeutralVariant, new() },
            { Error, new(25, 84) }
        };

        SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
    }

    private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        switch (e.Category)
        {
            case UserPreferenceCategory.General:
                if (_useWindowsAccentColor) SetColor(GetAccentColor(), _colorMode);
                else SetResources(_colorMode);
                break;
        }
    }

    public static void UseWindowsAccentColor(ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = true;
        SetColor(GetAccentColor(), colorMode);
    }

    public static void SetColor(Color color, ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = false;

        _palettes[Primary] = new TonalPalette(color);
        var hue = _palettes[Primary].Hue;
        _palettes[Secondary] = new TonalPalette(hue, 16);
        _palettes[Tertiary] = new TonalPalette(hue + 60, 24);
        _palettes[Neutral] = new TonalPalette(hue, 4);
        _palettes[NeutralVariant] = new TonalPalette(hue, 8);

        SetResources(colorMode);
    }

    public static void SetColor(Color primary, Color secondary, Color tertiary, ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = false;

        _palettes[Primary] = new TonalPalette(primary);
        var hue = _palettes[Primary].Hue;
        _palettes[Secondary] = new TonalPalette(secondary);
        _palettes[Tertiary] = new TonalPalette(tertiary);
        _palettes[Neutral] = new TonalPalette(hue, 4);
        _palettes[NeutralVariant] = new TonalPalette(hue, 8);

        SetResources(colorMode);
    }

    public static void SetColors(string xamlPath, ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = false;

        var res = new ResourceDictionary() { Source = new Uri(xamlPath, UriKind.RelativeOrAbsolute) };
        foreach (var item in res.Keys) _resource![item] = res[item];

        SetResources(colorMode);
    }

    public static void SetColors(Dictionary<string, object> collection, ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = false;

        if (collection != null)
        {
            foreach (var item in collection)
            {
                _resource![item.Key] = item.Value;
            }
        }

        SetResources(colorMode);
    }

    public static void SetColorMode(ColorMode colorMode) => SetResources(colorMode);

    internal static void InitializeInternal(ResourceDictionary resource)
    {
        _resource = resource;
        SetColor(Color.FromRgb(0x75, 0x75, 0x75), IsSystemLight() ? ColorMode.Light : ColorMode.Dark);
    }

    private static void SetResources(ColorMode colorMode)
    {
        _colorMode = colorMode;
        switch (_colorMode)
        {
            case ColorMode.Light:
                SetLight();
                break;

            case ColorMode.Dark:
                SetDark();
                break;

            case ColorMode.Auto:
                if (IsSystemLight()) SetLight();
                else SetDark();
                break;
        }
    }

    private static bool IsSystemLight()
    {
        RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")!;
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
        byte redColor = (byte)((0x000000FF & colorSetEx) >> 0);
        byte greenColor = (byte)((0x0000FF00 & colorSetEx) >> 8);
        byte blueColor = (byte)((0x00FF0000 & colorSetEx) >> 16);
        byte alphaColor = (byte)((0xFF000000 & colorSetEx) >> 24);
        return Color.FromArgb(alphaColor, redColor, greenColor, blueColor);
    }

    private static void SetLight()
    {
        _resource!["Color.Primary"] = _palettes[Primary][40];
        _resource!["Color.On.Primary"] = _palettes[Primary][100];
        _resource!["Color.Primary.Container"] = _palettes[Primary][90];
        _resource!["Color.On.Primary.Container"] = _palettes[Primary][10];
        _resource!["Color.Secondary"] = _palettes[Secondary][40];
        _resource!["Color.On.Secondary"] = _palettes[Secondary][100];
        _resource!["Color.Secondary.Container"] = _palettes[Secondary][90];
        _resource!["Color.On.Secondary.Container"] = _palettes[Secondary][10];
        _resource!["Color.Tertiary"] = _palettes[Tertiary][40];
        _resource!["Color.On.Tertiary"] = _palettes[Tertiary][100];
        _resource!["Color.Tertiary.Container"] = _palettes[Tertiary][90];
        _resource!["Color.On.Tertiary.Container"] = _palettes[Tertiary][10];
        _resource!["Color.Error"] = _palettes[Error][40];
        _resource!["Color.On.Error"] = _palettes[Error][100];
        _resource!["Color.Error.Container"] = _palettes[Error][90];
        _resource!["Color.On.Error.Container"] = _palettes[Error][10];
        _resource!["Color.Primary.Fixed"] = _palettes[Primary][90];
        _resource!["Color.Primary.Fixed.Dim"] = _palettes[Primary][80];
        _resource!["Color.On.Primary.Fixed"] = _palettes[Primary][10];
        _resource!["Color.On.Primary.Fixed.Variant"] = _palettes[Primary][30];
        _resource!["Color.Secondary.Fixed"] = _palettes[Secondary][90];
        _resource!["Color.Secondary.Fixed.Dim"] = _palettes[Secondary][80];
        _resource!["Color.On.Secondary.Fixed"] = _palettes[Secondary][10];
        _resource!["Color.On.Secondary.Fixed.Variant"] = _palettes[Secondary][30];
        _resource!["Color.Tertiary.Fixed"] = _palettes[Tertiary][90];
        _resource!["Color.Tertiary.Fixed.Dim"] = _palettes[Tertiary][80];
        _resource!["Color.On.Tertiary.Fixed"] = _palettes[Tertiary][10];
        _resource!["Color.On.Tertiary.Fixed.Variant"] = _palettes[Tertiary][30];
        _resource!["Color.Surface.Dim"] = _palettes[Neutral][87];
        _resource!["Color.Surface"] = _palettes[Neutral][98];
        _resource!["Color.Surface.Bright"] = _palettes[Neutral][98];
        _resource!["Color.Surface.Container.Lowest"] = _palettes[Neutral][100];
        _resource!["Color.Surface.Container.Low"] = _palettes[Neutral][96];
        _resource!["Color.Surface.Container"] = _palettes[Neutral][94];
        _resource!["Color.Surface.Container.High"] = _palettes[Neutral][92];
        _resource!["Color.Surface.Container.Highest"] = _palettes[Neutral][90];
        _resource!["Color.On.Surface"] = _palettes[Neutral][10];
        _resource!["Color.On.Surface.Variant"] = _palettes[NeutralVariant][30];
        _resource!["Color.Outline"] = _palettes[NeutralVariant][50];
        _resource!["Color.Outline.Variant"] = _palettes[NeutralVariant][80];
        _resource!["Color.Inverse.Surface"] = _palettes[Neutral][20];
        _resource!["Color.Inverse.On.Surface"] = _palettes[Neutral][95];
        _resource!["Color.Inverse.Primary"] = _palettes[Primary][80];
        _resource!["Color.Scrim"] = _palettes[Neutral][0];
        _resource!["Color.Shadow"] = _palettes[Neutral][0];

        _resource!["Brush.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][40] };
        _resource!["Brush.On.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][100] };
        _resource!["Brush.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.On.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _resource!["Brush.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][40] };
        _resource!["Brush.On.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][100] };
        _resource!["Brush.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.On.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _resource!["Brush.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][40] };
        _resource!["Brush.On.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][100] };
        _resource!["Brush.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.On.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _resource!["Brush.Error"] = new SolidColorBrush() { Color = _palettes[Error][40] };
        _resource!["Brush.On.Error"] = new SolidColorBrush() { Color = _palettes[Error][100] };
        _resource!["Brush.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][90] };
        _resource!["Brush.On.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][10] };
        _resource!["Brush.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.Primary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.On.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _resource!["Brush.On.Primary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _resource!["Brush.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.Secondary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _resource!["Brush.On.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _resource!["Brush.On.Secondary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _resource!["Brush.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.Tertiary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _resource!["Brush.On.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _resource!["Brush.On.Tertiary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _resource!["Brush.Surface.Dim"] = new SolidColorBrush() { Color = _palettes[Neutral][87] };
        _resource!["Brush.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][98] };
        _resource!["Brush.Surface.Bright"] = new SolidColorBrush() { Color = _palettes[Neutral][98] };
        _resource!["Brush.Surface.Container.Lowest"] = new SolidColorBrush() { Color = _palettes[Neutral][100] };
        _resource!["Brush.Surface.Container.Low"] = new SolidColorBrush() { Color = _palettes[Neutral][96] };
        _resource!["Brush.Surface.Container"] = new SolidColorBrush() { Color = _palettes[Neutral][94] };
        _resource!["Brush.Surface.Container.High"] = new SolidColorBrush() { Color = _palettes[Neutral][92] };
        _resource!["Brush.Surface.Container.Highest"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _resource!["Brush.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][10] };
        _resource!["Brush.On.Surface.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][30] };
        _resource!["Brush.Outline"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][50] };
        _resource!["Brush.Outline.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][80] };
        _resource!["Brush.Inverse.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][20] };
        _resource!["Brush.Inverse.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][95] };
        _resource!["Brush.Inverse.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.Scrim"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
        _resource!["Brush.Shadow"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
    }

    private static void SetDark()
    {
        _resource!["Color.Primary"] = _palettes[Primary][80];
        _resource!["Color.On.Primary"] = _palettes[Primary][20];
        _resource!["Color.Primary.Container"] = _palettes[Primary][30];
        _resource!["Color.On.Primary.Container"] = _palettes[Primary][90];
        _resource!["Color.Secondary"] = _palettes[Secondary][80];
        _resource!["Color.On.Secondary"] = _palettes[Secondary][20];
        _resource!["Color.Secondary.Container"] = _palettes[Secondary][30];
        _resource!["Color.On.Secondary.Container"] = _palettes[Secondary][90];
        _resource!["Color.Tertiary"] = _palettes[Tertiary][80];
        _resource!["Color.On.Tertiary"] = _palettes[Tertiary][20];
        _resource!["Color.Tertiary.Container"] = _palettes[Tertiary][30];
        _resource!["Color.On.Tertiary.Container"] = _palettes[Tertiary][90];
        _resource!["Color.Error"] = _palettes[Error][80];
        _resource!["Color.On.Error"] = _palettes[Error][20];
        _resource!["Color.Error.Container"] = _palettes[Error][30];
        _resource!["Color.On.Error.Container"] = _palettes[Error][90];
        _resource!["Color.Primary.Fixed"] = _palettes[Primary][90];
        _resource!["Color.Primary.Fixed.Dim"] = _palettes[Primary][80];
        _resource!["Color.On.Primary.Fixed"] = _palettes[Primary][10];
        _resource!["Color.On.Primary.Fixed.Variant"] = _palettes[Primary][30];
        _resource!["Color.Secondary.Fixed"] = _palettes[Secondary][90];
        _resource!["Color.Secondary.Fixed.Dim"] = _palettes[Secondary][80];
        _resource!["Color.On.Secondary.Fixed"] = _palettes[Secondary][10];
        _resource!["Color.On.Secondary.Fixed.Variant"] = _palettes[Secondary][30];
        _resource!["Color.Tertiary.Fixed"] = _palettes[Tertiary][90];
        _resource!["Color.Tertiary.Fixed.Dim"] = _palettes[Tertiary][80];
        _resource!["Color.On.Tertiary.Fixed"] = _palettes[Tertiary][10];
        _resource!["Color.On.Tertiary.Fixed.Variant"] = _palettes[Tertiary][30];
        _resource!["Color.Surface.Dim"] = _palettes[Neutral][6];
        _resource!["Color.Surface"] = _palettes[Neutral][6];
        _resource!["Color.Surface.Bright"] = _palettes[Neutral][24];
        _resource!["Color.Surface.Container.Lowest"] = _palettes[Neutral][4];
        _resource!["Color.Surface.Container.Low"] = _palettes[Neutral][10];
        _resource!["Color.Surface.Container"] = _palettes[Neutral][12];
        _resource!["Color.Surface.Container.High"] = _palettes[Neutral][17];
        _resource!["Color.Surface.Container.Highest"] = _palettes[Neutral][22];
        _resource!["Color.On.Surface"] = _palettes[Neutral][90];
        _resource!["Color.On.Surface.Variant"] = _palettes[NeutralVariant][80];
        _resource!["Color.Outline"] = _palettes[NeutralVariant][60];
        _resource!["Color.Outline.Variant"] = _palettes[NeutralVariant][30];
        _resource!["Color.Inverse.Surface"] = _palettes[Neutral][90];
        _resource!["Color.Inverse.On.Surface"] = _palettes[Neutral][20];
        _resource!["Color.Inverse.Primary"] = _palettes[Primary][40];
        _resource!["Color.Scrim"] = _palettes[Neutral][0];
        _resource!["Color.Shadow"] = _palettes[Neutral][0];

        _resource!["Brush.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.On.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][20] };
        _resource!["Brush.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _resource!["Brush.On.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _resource!["Brush.On.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][20] };
        _resource!["Brush.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _resource!["Brush.On.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _resource!["Brush.On.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][20] };
        _resource!["Brush.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _resource!["Brush.On.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.Error"] = new SolidColorBrush() { Color = _palettes[Error][80] };
        _resource!["Brush.On.Error"] = new SolidColorBrush() { Color = _palettes[Error][20] };
        _resource!["Brush.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][30] };
        _resource!["Brush.On.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][90] };
        _resource!["Brush.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.Primary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.On.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _resource!["Brush.On.Primary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _resource!["Brush.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.Secondary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _resource!["Brush.On.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _resource!["Brush.On.Secondary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _resource!["Brush.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.Tertiary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _resource!["Brush.On.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _resource!["Brush.On.Tertiary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _resource!["Brush.Surface.Dim"] = new SolidColorBrush() { Color = _palettes[Neutral][6] };
        _resource!["Brush.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][6] };
        _resource!["Brush.Surface.Bright"] = new SolidColorBrush() { Color = _palettes[Neutral][24] };
        _resource!["Brush.Surface.Container.Lowest"] = new SolidColorBrush() { Color = _palettes[Neutral][4] };
        _resource!["Brush.Surface.Container.Low"] = new SolidColorBrush() { Color = _palettes[Neutral][10] };
        _resource!["Brush.Surface.Container"] = new SolidColorBrush() { Color = _palettes[Neutral][12] };
        _resource!["Brush.Surface.Container.High"] = new SolidColorBrush() { Color = _palettes[Neutral][17] };
        _resource!["Brush.Surface.Container.Highest"] = new SolidColorBrush() { Color = _palettes[Neutral][22] };
        _resource!["Brush.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _resource!["Brush.On.Surface.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][80] };
        _resource!["Brush.Outline"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][60] };
        _resource!["Brush.Outline.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][30] };
        _resource!["Brush.Inverse.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _resource!["Brush.Inverse.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][20] };
        _resource!["Brush.Inverse.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][40] };
        _resource!["Brush.Scrim"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
        _resource!["Brush.Shadow"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
    }
}