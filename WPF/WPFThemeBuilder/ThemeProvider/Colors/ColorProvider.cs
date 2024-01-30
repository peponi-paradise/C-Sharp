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
                if (_useWindowsAccentColor) CreatePalette(GetAccentColor(), null, null);
                SetResources(_colorMode);
                break;
        }
    }

    public static void UseWindowsAccentColor(ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = true;

        CreatePalette(GetAccentColor(), null, null);

        SetResources(colorMode);
    }

    public static void SetColor(Color color, ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = false;

        CreatePalette(color, null, null);

        SetResources(colorMode);
    }

    public static void SetColor(Color primary, Color secondary, Color tertiary, ColorMode colorMode = ColorMode.Auto)
    {
        _useWindowsAccentColor = false;

        CreatePalette(primary, secondary, tertiary);

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

    private static void CreatePalette(Color primary, Color? secondary, Color? tertiary)
    {
        _palettes[Primary] = new TonalPalette(primary);
        var hue = _palettes[Primary].Hue;
        _palettes[Secondary] = secondary == null ? new TonalPalette(hue, 16) : new TonalPalette((Color)secondary);
        _palettes[Tertiary] = tertiary == null ? new TonalPalette(hue + 60, 24) : new TonalPalette((Color)tertiary);
        _palettes[Neutral] = new TonalPalette(hue, 4);
        _palettes[NeutralVariant] = new TonalPalette(hue, 8);
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
        _resource!["Color.OnPrimary"] = _palettes[Primary][100];
        _resource!["Color.PrimaryContainer"] = _palettes[Primary][90];
        _resource!["Color.OnPrimaryContainer"] = _palettes[Primary][10];
        _resource!["Color.Secondary"] = _palettes[Secondary][40];
        _resource!["Color.OnSecondary"] = _palettes[Secondary][100];
        _resource!["Color.SecondaryContainer"] = _palettes[Secondary][90];
        _resource!["Color.OnSecondaryContainer"] = _palettes[Secondary][10];
        _resource!["Color.Tertiary"] = _palettes[Tertiary][40];
        _resource!["Color.OnTertiary"] = _palettes[Tertiary][100];
        _resource!["Color.TertiaryContainer"] = _palettes[Tertiary][90];
        _resource!["Color.OnTertiaryContainer"] = _palettes[Tertiary][10];
        _resource!["Color.Error"] = _palettes[Error][40];
        _resource!["Color.OnError"] = _palettes[Error][100];
        _resource!["Color.ErrorContainer"] = _palettes[Error][90];
        _resource!["Color.OnErrorContainer"] = _palettes[Error][10];
        _resource!["Color.PrimaryFixed"] = _palettes[Primary][90];
        _resource!["Color.PrimaryFixedDim"] = _palettes[Primary][80];
        _resource!["Color.OnPrimaryFixed"] = _palettes[Primary][10];
        _resource!["Color.OnPrimaryFixedVariant"] = _palettes[Primary][30];
        _resource!["Color.SecondaryFixed"] = _palettes[Secondary][90];
        _resource!["Color.SecondaryFixedDim"] = _palettes[Secondary][80];
        _resource!["Color.OnSecondaryFixed"] = _palettes[Secondary][10];
        _resource!["Color.OnSecondaryFixedVariant"] = _palettes[Secondary][30];
        _resource!["Color.TertiaryFixed"] = _palettes[Tertiary][90];
        _resource!["Color.TertiaryFixedDim"] = _palettes[Tertiary][80];
        _resource!["Color.OnTertiaryFixed"] = _palettes[Tertiary][10];
        _resource!["Color.OnTertiaryFixedVariant"] = _palettes[Tertiary][30];
        _resource!["Color.SurfaceDim"] = _palettes[Neutral][87];
        _resource!["Color.Surface"] = _palettes[Neutral][98];
        _resource!["Color.SurfaceBright"] = _palettes[Neutral][98];
        _resource!["Color.SurfaceContainerLowest"] = _palettes[Neutral][100];
        _resource!["Color.SurfaceContainerLow"] = _palettes[Neutral][96];
        _resource!["Color.SurfaceContainer"] = _palettes[Neutral][94];
        _resource!["Color.SurfaceContainerHigh"] = _palettes[Neutral][92];
        _resource!["Color.SurfaceContainerHighest"] = _palettes[Neutral][90];
        _resource!["Color.OnSurface"] = _palettes[Neutral][10];
        _resource!["Color.OnSurfaceVariant"] = _palettes[NeutralVariant][30];
        _resource!["Color.Outline"] = _palettes[NeutralVariant][50];
        _resource!["Color.OutlineVariant"] = _palettes[NeutralVariant][80];
        _resource!["Color.InverseSurface"] = _palettes[Neutral][20];
        _resource!["Color.InverseOnSurface"] = _palettes[Neutral][95];
        _resource!["Color.InversePrimary"] = _palettes[Primary][80];
        _resource!["Color.Scrim"] = _palettes[Neutral][0];
        _resource!["Color.Shadow"] = _palettes[Neutral][0];

        _resource!["Brush.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][40] };
        _resource!["Brush.OnPrimary"] = new SolidColorBrush() { Color = _palettes[Primary][100] };
        _resource!["Brush.PrimaryContainer"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.OnPrimaryContainer"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _resource!["Brush.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][40] };
        _resource!["Brush.OnSecondary"] = new SolidColorBrush() { Color = _palettes[Secondary][100] };
        _resource!["Brush.SecondaryContainer"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.OnSecondaryContainer"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _resource!["Brush.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][40] };
        _resource!["Brush.OnTertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][100] };
        _resource!["Brush.TertiaryContainer"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.OnTertiaryContainer"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _resource!["Brush.Error"] = new SolidColorBrush() { Color = _palettes[Error][40] };
        _resource!["Brush.OnError"] = new SolidColorBrush() { Color = _palettes[Error][100] };
        _resource!["Brush.ErrorContainer"] = new SolidColorBrush() { Color = _palettes[Error][90] };
        _resource!["Brush.OnErrorContainer"] = new SolidColorBrush() { Color = _palettes[Error][10] };
        _resource!["Brush.PrimaryFixed"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.PrimaryFixedDim"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.OnPrimaryFixed"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _resource!["Brush.OnPrimaryFixedVariant"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _resource!["Brush.SecondaryFixed"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.SecondaryFixedDim"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _resource!["Brush.OnSecondaryFixed"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _resource!["Brush.OnSecondaryFixedVariant"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _resource!["Brush.TertiaryFixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.TertiaryFixedDim"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _resource!["Brush.OnTertiaryFixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _resource!["Brush.OnTertiaryFixedVariant"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _resource!["Brush.SurfaceDim"] = new SolidColorBrush() { Color = _palettes[Neutral][87] };
        _resource!["Brush.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][98] };
        _resource!["Brush.SurfaceBright"] = new SolidColorBrush() { Color = _palettes[Neutral][98] };
        _resource!["Brush.SurfaceContainerLowest"] = new SolidColorBrush() { Color = _palettes[Neutral][100] };
        _resource!["Brush.SurfaceContainerLow"] = new SolidColorBrush() { Color = _palettes[Neutral][96] };
        _resource!["Brush.SurfaceContainer"] = new SolidColorBrush() { Color = _palettes[Neutral][94] };
        _resource!["Brush.SurfaceContainerHigh"] = new SolidColorBrush() { Color = _palettes[Neutral][92] };
        _resource!["Brush.SurfaceContainerHighest"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _resource!["Brush.OnSurface"] = new SolidColorBrush() { Color = _palettes[Neutral][10] };
        _resource!["Brush.OnSurfaceVariant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][30] };
        _resource!["Brush.Outline"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][50] };
        _resource!["Brush.OutlineVariant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][80] };
        _resource!["Brush.InverseSurface"] = new SolidColorBrush() { Color = _palettes[Neutral][20] };
        _resource!["Brush.InverseOnSurface"] = new SolidColorBrush() { Color = _palettes[Neutral][95] };
        _resource!["Brush.InversePrimary"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.Scrim"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
        _resource!["Brush.Shadow"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
    }

    private static void SetDark()
    {
        _resource!["Color.Primary"] = _palettes[Primary][80];
        _resource!["Color.OnPrimary"] = _palettes[Primary][20];
        _resource!["Color.PrimaryContainer"] = _palettes[Primary][30];
        _resource!["Color.OnPrimaryContainer"] = _palettes[Primary][90];
        _resource!["Color.Secondary"] = _palettes[Secondary][80];
        _resource!["Color.OnSecondary"] = _palettes[Secondary][20];
        _resource!["Color.SecondaryContainer"] = _palettes[Secondary][30];
        _resource!["Color.OnSecondaryContainer"] = _palettes[Secondary][90];
        _resource!["Color.Tertiary"] = _palettes[Tertiary][80];
        _resource!["Color.OnTertiary"] = _palettes[Tertiary][20];
        _resource!["Color.TertiaryContainer"] = _palettes[Tertiary][30];
        _resource!["Color.OnTertiaryContainer"] = _palettes[Tertiary][90];
        _resource!["Color.Error"] = _palettes[Error][80];
        _resource!["Color.OnError"] = _palettes[Error][20];
        _resource!["Color.ErrorContainer"] = _palettes[Error][30];
        _resource!["Color.OnErrorContainer"] = _palettes[Error][90];
        _resource!["Color.PrimaryFixed"] = _palettes[Primary][90];
        _resource!["Color.PrimaryFixedDim"] = _palettes[Primary][80];
        _resource!["Color.OnPrimaryFixed"] = _palettes[Primary][10];
        _resource!["Color.OnPrimaryFixedVariant"] = _palettes[Primary][30];
        _resource!["Color.SecondaryFixed"] = _palettes[Secondary][90];
        _resource!["Color.SecondaryFixedDim"] = _palettes[Secondary][80];
        _resource!["Color.OnSecondaryFixed"] = _palettes[Secondary][10];
        _resource!["Color.OnSecondaryFixedVariant"] = _palettes[Secondary][30];
        _resource!["Color.TertiaryFixed"] = _palettes[Tertiary][90];
        _resource!["Color.TertiaryFixedDim"] = _palettes[Tertiary][80];
        _resource!["Color.OnTertiaryFixed"] = _palettes[Tertiary][10];
        _resource!["Color.OnTertiaryFixedVariant"] = _palettes[Tertiary][30];
        _resource!["Color.SurfaceDim"] = _palettes[Neutral][6];
        _resource!["Color.Surface"] = _palettes[Neutral][6];
        _resource!["Color.SurfaceBright"] = _palettes[Neutral][24];
        _resource!["Color.SurfaceContainerLowest"] = _palettes[Neutral][4];
        _resource!["Color.SurfaceContainerLow"] = _palettes[Neutral][10];
        _resource!["Color.SurfaceContainer"] = _palettes[Neutral][12];
        _resource!["Color.SurfaceContainerHigh"] = _palettes[Neutral][17];
        _resource!["Color.SurfaceContainerHighest"] = _palettes[Neutral][22];
        _resource!["Color.OnSurface"] = _palettes[Neutral][90];
        _resource!["Color.OnSurfaceVariant"] = _palettes[NeutralVariant][80];
        _resource!["Color.Outline"] = _palettes[NeutralVariant][60];
        _resource!["Color.OutlineVariant"] = _palettes[NeutralVariant][30];
        _resource!["Color.InverseSurface"] = _palettes[Neutral][90];
        _resource!["Color.InverseOnSurface"] = _palettes[Neutral][20];
        _resource!["Color.InversePrimary"] = _palettes[Primary][40];
        _resource!["Color.Scrim"] = _palettes[Neutral][0];
        _resource!["Color.Shadow"] = _palettes[Neutral][0];

        _resource!["Brush.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.OnPrimary"] = new SolidColorBrush() { Color = _palettes[Primary][20] };
        _resource!["Brush.PrimaryContainer"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _resource!["Brush.OnPrimaryContainer"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _resource!["Brush.OnSecondary"] = new SolidColorBrush() { Color = _palettes[Secondary][20] };
        _resource!["Brush.SecondaryContainer"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _resource!["Brush.OnSecondaryContainer"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _resource!["Brush.OnTertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][20] };
        _resource!["Brush.TertiaryContainer"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _resource!["Brush.OnTertiaryContainer"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.Error"] = new SolidColorBrush() { Color = _palettes[Error][80] };
        _resource!["Brush.OnError"] = new SolidColorBrush() { Color = _palettes[Error][20] };
        _resource!["Brush.ErrorContainer"] = new SolidColorBrush() { Color = _palettes[Error][30] };
        _resource!["Brush.OnErrorContainer"] = new SolidColorBrush() { Color = _palettes[Error][90] };
        _resource!["Brush.PrimaryFixed"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _resource!["Brush.PrimaryFixedDim"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _resource!["Brush.OnPrimaryFixed"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _resource!["Brush.OnPrimaryFixedVariant"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _resource!["Brush.SecondaryFixed"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _resource!["Brush.SecondaryFixedDim"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _resource!["Brush.OnSecondaryFixed"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _resource!["Brush.OnSecondaryFixedVariant"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _resource!["Brush.TertiaryFixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _resource!["Brush.TertiaryFixedDim"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _resource!["Brush.OnTertiaryFixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _resource!["Brush.OnTertiaryFixedVariant"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _resource!["Brush.SurfaceDim"] = new SolidColorBrush() { Color = _palettes[Neutral][6] };
        _resource!["Brush.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][6] };
        _resource!["Brush.SurfaceBright"] = new SolidColorBrush() { Color = _palettes[Neutral][24] };
        _resource!["Brush.SurfaceContainerLowest"] = new SolidColorBrush() { Color = _palettes[Neutral][4] };
        _resource!["Brush.SurfaceContainerLow"] = new SolidColorBrush() { Color = _palettes[Neutral][10] };
        _resource!["Brush.SurfaceContainer"] = new SolidColorBrush() { Color = _palettes[Neutral][12] };
        _resource!["Brush.SurfaceContainerHigh"] = new SolidColorBrush() { Color = _palettes[Neutral][17] };
        _resource!["Brush.SurfaceContainerHighest"] = new SolidColorBrush() { Color = _palettes[Neutral][22] };
        _resource!["Brush.OnSurface"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _resource!["Brush.OnSurfaceVariant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][80] };
        _resource!["Brush.Outline"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][60] };
        _resource!["Brush.OutlineVariant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][30] };
        _resource!["Brush.InverseSurface"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _resource!["Brush.InverseOnSurface"] = new SolidColorBrush() { Color = _palettes[Neutral][20] };
        _resource!["Brush.InversePrimary"] = new SolidColorBrush() { Color = _palettes[Primary][40] };
        _resource!["Brush.Scrim"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
        _resource!["Brush.Shadow"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
    }
}