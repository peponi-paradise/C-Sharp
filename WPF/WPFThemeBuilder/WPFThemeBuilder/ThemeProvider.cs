using Microsoft.Win32;
using System.Diagnostics;
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
    private static Dictionary<string, TonalPalette> _palettes;

    private const string Primary = "Primary";
    private const string Secondary = "Secondary";
    private const string Tertiary = "Tertiary";
    private const string Neutral = "Neutral";
    private const string NeutralVariant = "NeutralVariant";
    private const string Error = "Error";

    private static bool _isWindowsColor = false;
    private static bool _SystemEventRegistered = false;

    static ThemeProvider()
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
    }

    public static void ApplyWindowsColorStyle()
    {
        _isWindowsColor = true;
        ChangeColor(IsThemeLight());
        SystemEventRegistration(true);
    }

    public static void ApplyColors(Color primary, ColorMode colorMode = ColorMode.Both)
    {
        _isWindowsColor = false;
        _palettes[Primary] = new TonalPalette(primary);
        var hue = _palettes[Primary].Hue;
        _palettes[Secondary] = new TonalPalette(hue, 16);
        _palettes[Tertiary] = new TonalPalette(hue + 60, 24);
        _palettes[Neutral] = new TonalPalette(hue, 4);
        _palettes[NeutralVariant] = new TonalPalette(hue, 8);
        switch (colorMode)
        {
            case ColorMode.Bright:
                ChangeColor(true);
                SystemEventRegistration(false);
                break;

            case ColorMode.Dark:
                ChangeColor(false);
                SystemEventRegistration(false);
                break;

            case ColorMode.Both:
                ChangeColor(IsThemeLight());
                SystemEventRegistration(true);
                break;
        }
    }

    public static void ApplyColors(Color primary, Color secondary, Color tertiary, ColorMode colorMode = ColorMode.Both)
    {
        _isWindowsColor = false;
        _palettes[Primary] = new TonalPalette(primary);
        _palettes[Secondary] = new TonalPalette(secondary);
        _palettes[Tertiary] = new TonalPalette(tertiary);
        var hue = _palettes[Primary].Hue;
        _palettes[Neutral] = new TonalPalette(hue, 4);
        _palettes[NeutralVariant] = new TonalPalette(hue, 8);
        switch (colorMode)
        {
            case ColorMode.Bright:
                ChangeColor(true);
                SystemEventRegistration(false);
                break;

            case ColorMode.Dark:
                ChangeColor(false);
                SystemEventRegistration(false);
                break;

            case ColorMode.Both:
                ChangeColor(IsThemeLight());
                SystemEventRegistration(true);
                break;
        }
    }

    public static void ApplyColorSet(string xamlPath)
    {
        _isWindowsColor = false;
        var res = new ResourceDictionary() { Source = new Uri(xamlPath, UriKind.RelativeOrAbsolute) };
        foreach (var item in res.Keys) _appResource![item] = res[item];
        SystemEventRegistration(false);
    }

    public static void ApplyColorSet(Dictionary<string, object> collection)
    {
        _isWindowsColor = false;
        if (collection != null)
        {
            foreach (var item in collection)
            {
                _appResource![item.Key] = item.Value;
            }
        }
        SystemEventRegistration(false);
    }

    private static void SystemEventRegistration(bool subscribe)
    {
        if (subscribe)
        {
            if (!_SystemEventRegistered)
            {
                _SystemEventRegistered = true;
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            }
        }
        else
        {
            if (_SystemEventRegistered)
            {
                _SystemEventRegistered = false;
                SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
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
        _palettes[Primary] = new TonalPalette(Color.FromRgb(0, 133, 255));
        var hue = _palettes[Primary].Hue;
        _palettes[Secondary] = new TonalPalette(hue, 16);
        _palettes[Tertiary] = new TonalPalette(hue + 60, 24);
        _palettes[Neutral] = new TonalPalette(hue, 4);
        _palettes[NeutralVariant] = new TonalPalette(hue, 8);
        ChangeColor(IsThemeLight());
    }

    private static void ChangeColor(bool isLight)
    {
        if (_isWindowsColor)
        {
            var currentColor = GetAccentColor();
            ResetColor(currentColor);
            Debug.WriteLine(currentColor);
            Debug.WriteLine(_palettes[Primary].Hue);
            Debug.WriteLine(_palettes[Secondary].Hue);
            Debug.WriteLine(_palettes[Primary].Chroma);
            Debug.WriteLine(_palettes[Secondary].Chroma);
            Debug.WriteLine(_palettes[Primary][40]);
            Debug.WriteLine(_palettes[Secondary][40]);
        }

        if (isLight) SetBright();
        else SetDark();

        static void ResetColor(Color primary)
        {
            _palettes[Primary] = new TonalPalette(primary);
            var hue = _palettes[Primary].Hue;
            _palettes[Secondary] = new TonalPalette(hue, 16);
            _palettes[Tertiary] = new TonalPalette(hue + 60, 24);
            _palettes[Neutral] = new TonalPalette(hue, 4);
            _palettes[NeutralVariant] = new TonalPalette(hue, 8);
        }
    }

    private static void SetBright()
    {
        _appResource!["Color.Primary"] = _palettes[Primary][40];
        _appResource!["Color.On.Primary"] = _palettes[Primary][100];
        _appResource!["Color.Primary.Container"] = _palettes[Primary][90];
        _appResource!["Color.On.Primary.Container"] = _palettes[Primary][10];
        _appResource!["Color.Secondary"] = _palettes[Secondary][40];
        _appResource!["Color.On.Secondary"] = _palettes[Secondary][100];
        _appResource!["Color.Secondary.Container"] = _palettes[Secondary][90];
        _appResource!["Color.On.Secondary.Container"] = _palettes[Secondary][10];
        _appResource!["Color.Tertiary"] = _palettes[Tertiary][40];
        _appResource!["Color.On.Tertiary"] = _palettes[Tertiary][100];
        _appResource!["Color.Tertiary.Container"] = _palettes[Tertiary][90];
        _appResource!["Color.On.Tertiary.Container"] = _palettes[Tertiary][10];
        _appResource!["Color.Error"] = _palettes[Error][40];
        _appResource!["Color.On.Error"] = _palettes[Error][100];
        _appResource!["Color.Error.Container"] = _palettes[Error][90];
        _appResource!["Color.On.Error.Container"] = _palettes[Error][10];
        _appResource!["Color.Primary.Fixed"] = _palettes[Primary][90];
        _appResource!["Color.Primary.Fixed.Dim"] = _palettes[Primary][80];
        _appResource!["Color.On.Primary.Fixed"] = _palettes[Primary][10];
        _appResource!["Color.On.Primary.Fixed.Variant"] = _palettes[Primary][30];
        _appResource!["Color.Secondary.Fixed"] = _palettes[Secondary][90];
        _appResource!["Color.Secondary.Fixed.Dim"] = _palettes[Secondary][80];
        _appResource!["Color.On.Secondary.Fixed"] = _palettes[Secondary][10];
        _appResource!["Color.On.Secondary.Fixed.Variant"] = _palettes[Secondary][30];
        _appResource!["Color.Tertiary.Fixed"] = _palettes[Tertiary][90];
        _appResource!["Color.Tertiary.Fixed.Dim"] = _palettes[Tertiary][80];
        _appResource!["Color.On.Tertiary.Fixed"] = _palettes[Tertiary][10];
        _appResource!["Color.On.Tertiary.Fixed.Variant"] = _palettes[Tertiary][30];
        _appResource!["Color.Surface.Dim"] = _palettes[Neutral][87];
        _appResource!["Color.Surface"] = _palettes[Neutral][98];
        _appResource!["Color.Surface.Bright"] = _palettes[Neutral][98];
        _appResource!["Color.Surface.Container.Lowest"] = _palettes[Neutral][100];
        _appResource!["Color.Surface.Container.Low"] = _palettes[Neutral][96];
        _appResource!["Color.Surface.Container"] = _palettes[Neutral][94];
        _appResource!["Color.Surface.Container.High"] = _palettes[Neutral][92];
        _appResource!["Color.Surface.Container.Highest"] = _palettes[Neutral][90];
        _appResource!["Color.On.Surface"] = _palettes[Neutral][10];
        _appResource!["Color.On.Surface.Variant"] = _palettes[NeutralVariant][30];
        _appResource!["Color.Outline"] = _palettes[NeutralVariant][50];
        _appResource!["Color.Outline.Variant"] = _palettes[NeutralVariant][80];
        _appResource!["Color.Inverse.Surface"] = _palettes[Neutral][20];
        _appResource!["Color.Inverse.On.Surface"] = _palettes[Neutral][95];
        _appResource!["Color.Inverse.Primary"] = _palettes[Primary][80];
        _appResource!["Color.Scrim"] = _palettes[Neutral][0];
        _appResource!["Color.Shadow"] = _palettes[Neutral][0];

        _appResource!["Brush.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][40] };
        _appResource!["Brush.On.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][100] };
        _appResource!["Brush.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _appResource!["Brush.On.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _appResource!["Brush.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][40] };
        _appResource!["Brush.On.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][100] };
        _appResource!["Brush.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _appResource!["Brush.On.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _appResource!["Brush.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][40] };
        _appResource!["Brush.On.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][100] };
        _appResource!["Brush.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _appResource!["Brush.On.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _appResource!["Brush.Error"] = new SolidColorBrush() { Color = _palettes[Error][40] };
        _appResource!["Brush.On.Error"] = new SolidColorBrush() { Color = _palettes[Error][100] };
        _appResource!["Brush.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][90] };
        _appResource!["Brush.On.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][10] };
        _appResource!["Brush.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _appResource!["Brush.Primary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _appResource!["Brush.On.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _appResource!["Brush.On.Primary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _appResource!["Brush.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _appResource!["Brush.Secondary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _appResource!["Brush.On.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _appResource!["Brush.On.Secondary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _appResource!["Brush.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _appResource!["Brush.Tertiary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _appResource!["Brush.On.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _appResource!["Brush.On.Tertiary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _appResource!["Brush.Surface.Dim"] = new SolidColorBrush() { Color = _palettes[Neutral][87] };
        _appResource!["Brush.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][98] };
        _appResource!["Brush.Surface.Bright"] = new SolidColorBrush() { Color = _palettes[Neutral][98] };
        _appResource!["Brush.Surface.Container.Lowest"] = new SolidColorBrush() { Color = _palettes[Neutral][100] };
        _appResource!["Brush.Surface.Container.Low"] = new SolidColorBrush() { Color = _palettes[Neutral][96] };
        _appResource!["Brush.Surface.Container"] = new SolidColorBrush() { Color = _palettes[Neutral][94] };
        _appResource!["Brush.Surface.Container.High"] = new SolidColorBrush() { Color = _palettes[Neutral][92] };
        _appResource!["Brush.Surface.Container.Highest"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _appResource!["Brush.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][10] };
        _appResource!["Brush.On.Surface.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][30] };
        _appResource!["Brush.Outline"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][50] };
        _appResource!["Brush.Outline.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][80] };
        _appResource!["Brush.Inverse.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][20] };
        _appResource!["Brush.Inverse.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][95] };
        _appResource!["Brush.Inverse.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _appResource!["Brush.Scrim"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
        _appResource!["Brush.Shadow"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
    }

    private static void SetDark()
    {
        _appResource!["Color.Primary"] = _palettes[Primary][80];
        _appResource!["Color.On.Primary"] = _palettes[Primary][20];
        _appResource!["Color.Primary.Container"] = _palettes[Primary][30];
        _appResource!["Color.On.Primary.Container"] = _palettes[Primary][90];
        _appResource!["Color.Secondary"] = _palettes[Secondary][80];
        _appResource!["Color.On.Secondary"] = _palettes[Secondary][20];
        _appResource!["Color.Secondary.Container"] = _palettes[Secondary][30];
        _appResource!["Color.On.Secondary.Container"] = _palettes[Secondary][90];
        _appResource!["Color.Tertiary"] = _palettes[Tertiary][80];
        _appResource!["Color.On.Tertiary"] = _palettes[Tertiary][20];
        _appResource!["Color.Tertiary.Container"] = _palettes[Tertiary][30];
        _appResource!["Color.On.Tertiary.Container"] = _palettes[Tertiary][90];
        _appResource!["Color.Error"] = _palettes[Error][80];
        _appResource!["Color.On.Error"] = _palettes[Error][20];
        _appResource!["Color.Error.Container"] = _palettes[Error][30];
        _appResource!["Color.On.Error.Container"] = _palettes[Error][90];
        _appResource!["Color.Primary.Fixed"] = _palettes[Primary][90];
        _appResource!["Color.Primary.Fixed.Dim"] = _palettes[Primary][80];
        _appResource!["Color.On.Primary.Fixed"] = _palettes[Primary][10];
        _appResource!["Color.On.Primary.Fixed.Variant"] = _palettes[Primary][30];
        _appResource!["Color.Secondary.Fixed"] = _palettes[Secondary][90];
        _appResource!["Color.Secondary.Fixed.Dim"] = _palettes[Secondary][80];
        _appResource!["Color.On.Secondary.Fixed"] = _palettes[Secondary][10];
        _appResource!["Color.On.Secondary.Fixed.Variant"] = _palettes[Secondary][30];
        _appResource!["Color.Tertiary.Fixed"] = _palettes[Tertiary][90];
        _appResource!["Color.Tertiary.Fixed.Dim"] = _palettes[Tertiary][80];
        _appResource!["Color.On.Tertiary.Fixed"] = _palettes[Tertiary][10];
        _appResource!["Color.On.Tertiary.Fixed.Variant"] = _palettes[Tertiary][30];
        _appResource!["Color.Surface.Dim"] = _palettes[Neutral][6];
        _appResource!["Color.Surface"] = _palettes[Neutral][6];
        _appResource!["Color.Surface.Bright"] = _palettes[Neutral][24];
        _appResource!["Color.Surface.Container.Lowest"] = _palettes[Neutral][4];
        _appResource!["Color.Surface.Container.Low"] = _palettes[Neutral][10];
        _appResource!["Color.Surface.Container"] = _palettes[Neutral][12];
        _appResource!["Color.Surface.Container.High"] = _palettes[Neutral][17];
        _appResource!["Color.Surface.Container.Highest"] = _palettes[Neutral][22];
        _appResource!["Color.On.Surface"] = _palettes[Neutral][90];
        _appResource!["Color.On.Surface.Variant"] = _palettes[NeutralVariant][80];
        _appResource!["Color.Outline"] = _palettes[NeutralVariant][60];
        _appResource!["Color.Outline.Variant"] = _palettes[NeutralVariant][30];
        _appResource!["Color.Inverse.Surface"] = _palettes[Neutral][90];
        _appResource!["Color.Inverse.On.Surface"] = _palettes[Neutral][20];
        _appResource!["Color.Inverse.Primary"] = _palettes[Primary][40];
        _appResource!["Color.Scrim"] = _palettes[Neutral][0];
        _appResource!["Color.Shadow"] = _palettes[Neutral][0];

        _appResource!["Brush.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _appResource!["Brush.On.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][20] };
        _appResource!["Brush.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _appResource!["Brush.On.Primary.Container"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _appResource!["Brush.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _appResource!["Brush.On.Secondary"] = new SolidColorBrush() { Color = _palettes[Secondary][20] };
        _appResource!["Brush.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _appResource!["Brush.On.Secondary.Container"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _appResource!["Brush.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _appResource!["Brush.On.Tertiary"] = new SolidColorBrush() { Color = _palettes[Tertiary][20] };
        _appResource!["Brush.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _appResource!["Brush.On.Tertiary.Container"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _appResource!["Brush.Error"] = new SolidColorBrush() { Color = _palettes[Error][80] };
        _appResource!["Brush.On.Error"] = new SolidColorBrush() { Color = _palettes[Error][20] };
        _appResource!["Brush.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][30] };
        _appResource!["Brush.On.Error.Container"] = new SolidColorBrush() { Color = _palettes[Error][90] };
        _appResource!["Brush.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][90] };
        _appResource!["Brush.Primary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Primary][80] };
        _appResource!["Brush.On.Primary.Fixed"] = new SolidColorBrush() { Color = _palettes[Primary][10] };
        _appResource!["Brush.On.Primary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Primary][30] };
        _appResource!["Brush.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][90] };
        _appResource!["Brush.Secondary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Secondary][80] };
        _appResource!["Brush.On.Secondary.Fixed"] = new SolidColorBrush() { Color = _palettes[Secondary][10] };
        _appResource!["Brush.On.Secondary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Secondary][30] };
        _appResource!["Brush.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][90] };
        _appResource!["Brush.Tertiary.Fixed.Dim"] = new SolidColorBrush() { Color = _palettes[Tertiary][80] };
        _appResource!["Brush.On.Tertiary.Fixed"] = new SolidColorBrush() { Color = _palettes[Tertiary][10] };
        _appResource!["Brush.On.Tertiary.Fixed.Variant"] = new SolidColorBrush() { Color = _palettes[Tertiary][30] };
        _appResource!["Brush.Surface.Dim"] = new SolidColorBrush() { Color = _palettes[Neutral][6] };
        _appResource!["Brush.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][6] };
        _appResource!["Brush.Surface.Bright"] = new SolidColorBrush() { Color = _palettes[Neutral][24] };
        _appResource!["Brush.Surface.Container.Lowest"] = new SolidColorBrush() { Color = _palettes[Neutral][4] };
        _appResource!["Brush.Surface.Container.Low"] = new SolidColorBrush() { Color = _palettes[Neutral][10] };
        _appResource!["Brush.Surface.Container"] = new SolidColorBrush() { Color = _palettes[Neutral][12] };
        _appResource!["Brush.Surface.Container.High"] = new SolidColorBrush() { Color = _palettes[Neutral][17] };
        _appResource!["Brush.Surface.Container.Highest"] = new SolidColorBrush() { Color = _palettes[Neutral][22] };
        _appResource!["Brush.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _appResource!["Brush.On.Surface.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][80] };
        _appResource!["Brush.Outline"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][60] };
        _appResource!["Brush.Outline.Variant"] = new SolidColorBrush() { Color = _palettes[NeutralVariant][30] };
        _appResource!["Brush.Inverse.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][90] };
        _appResource!["Brush.Inverse.On.Surface"] = new SolidColorBrush() { Color = _palettes[Neutral][20] };
        _appResource!["Brush.Inverse.Primary"] = new SolidColorBrush() { Color = _palettes[Primary][40] };
        _appResource!["Brush.Scrim"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
        _appResource!["Brush.Shadow"] = new SolidColorBrush() { Color = _palettes[Neutral][0] };
    }

    private static bool IsThemeLight()
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
}