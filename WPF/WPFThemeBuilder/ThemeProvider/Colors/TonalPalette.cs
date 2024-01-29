/*
 * Copyright 2022 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Windows.Media;

namespace Peponi.WPF.ThemeProvider.Colors;

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
        //Chroma = data.Chroma > 48 ? 48 : data.Chroma;
        // 정확하게는 모르겠다. 원본 소스에서는 위와 같이 Chroma 값을 쓰나 Figma나 C# M3 nuget을 보면 Tonal spot을 사용한다.
        // 뭐가 맞는거지..? 일단 Figma 플러그인에 맞게 Tonal spot으로 사용
        Chroma = 36;
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