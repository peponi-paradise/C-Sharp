using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace WPFThemeBuilder;

public class TonalPalette
{
    public readonly double Hue;
    public readonly double Chroma;

    private readonly Dictionary<uint, uint> _cache = new();

    public TonalPalette()
    {
    }

    public TonalPalette(Color color)
    {
        var data = HCT.GetHueAndChroma(color);
        Hue = data.Hue;
        Chroma = data.Chroma > 48 ? 48 : data.Chroma;
    }

    public TonalPalette(double hue, double chroma)
    {
        Hue = hue;
        Chroma = chroma;
    }

    /// <summary>Creates an ARGB color with HCT hue and chroma of this TonalPalette instance, and the provided HCT tone.</summary>
    /// <param name="tone">HCT tone, measured from 0 to 100.</param>
    /// <returns>ARGB representation of a color with that tone.</returns>
    public uint Tone(uint tone)
        => _cache.TryGetValue(tone, out uint value)
            ? value
            : _cache[tone] = HCT.ToARGB(Hue, Chroma, tone);

    /// <summary>Creates an ARGB color with HCT hue and chroma of this TonalPalette instance, and the provided HCT tone.</summary>
    /// <param name="tone">HCT tone, measured from 0 to 100.</param>
    /// <returns>ARGB representation of a color with that tone.</returns>
    public Color this[uint tone] => Tone(tone).ToColor();
}

internal static class ColorEx
{
    public static Color ToColor(this uint value)
    {
        var color = System.Drawing.Color.FromArgb((int)value);
        return Color.FromRgb(color.R, color.G, color.B);
    }
}