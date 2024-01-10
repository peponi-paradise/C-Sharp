using System.Windows.Media;

namespace WPFThemeBuilder;

public static class HCT
{
    private static readonly double[][] XyzToCam16Rgb =
   {
    new[] { 0.401288, 0.650173, -0.051461 },
    new[] { -0.250268, 1.204414, 0.045854 },
    new[] { -0.002079, 0.048952, 0.953127 }
};

    public static double[] WhitePointD65 { get; } = { 95.047, 100, 108.883 };

    public static double GetHue(Color color)
    {
        double redL = color.R.ToLinear();
        double greenL = color.G.ToLinear();
        double blueL = color.B.ToLinear();

        double x = 0.41233895 * redL + 0.35762064 * greenL + 0.18051042 * blueL;
        double y = 0.2126 * redL + 0.7152 * greenL + 0.0722 * blueL;
        double z = 0.01932141 * redL + 0.11916382 * greenL + 0.95034478 * blueL;

        double rD = 1.0211777027575202 * (x * XyzToCam16Rgb[0][0]) + (y * XyzToCam16Rgb[0][1]) + (z * XyzToCam16Rgb[0][2]);
        double gD = 0.9863077294280123 * (x * XyzToCam16Rgb[1][0]) + (y * XyzToCam16Rgb[1][1]) + (z * XyzToCam16Rgb[1][2]);
        double bD = 0.9339605082802299 * (x * XyzToCam16Rgb[2][0]) + (y * XyzToCam16Rgb[2][1]) + (z * XyzToCam16Rgb[2][2]);

        double rAF = Math.Pow(0.3884814537800353 * Math.Abs(rD) / 100.0, 0.42);
        double gAF = Math.Pow(0.3884814537800353 * Math.Abs(gD) / 100.0, 0.42);
        double bAF = Math.Pow(0.3884814537800353 * Math.Abs(bD) / 100.0, 0.42);

        double rA = Math.Sign(rD) * 400.0 * rAF / (rAF + 27.13);
        double gA = Math.Sign(gD) * 400.0 * gAF / (gAF + 27.13);
        double bA = Math.Sign(bD) * 400.0 * bAF / (bAF + 27.13);

        // redness-greenness
        double a = (11.0 * rA + -12.0 * gA + bA) / 11.0;
        // yellowness-blueness
        double b = (rA + gA - 2.0 * bA) / 9.0;

        // hue
        double atanDegrees = Math.Atan2(b, a) * 180.0 / Math.PI;
        return
            atanDegrees < 0
                ? atanDegrees + 360.0
                : atanDegrees >= 360 ? atanDegrees - 360.0 : atanDegrees;
    }

    public static uint ToARGB(double hue, double chroma, double tone)
    {
        if (chroma < 0.0001 || tone < 0.0001 || tone > 99.9999)
        {
            return ColorUtils.ArgbFromLstar(tone);
        }
        hueDegrees = MathUtils.SanitizeDegreesDouble(hueDegrees);
        double hueRadians = MathUtils.ToRadians(hueDegrees);
        double y = ColorUtils.YFromLstar(tone);
        uint exactAnswer = FindResultByJ(hueRadians, chroma, y);
        if (exactAnswer != 0)
        {
            return exactAnswer;
        }
        double[] linrgb = BisectToLimit(y, hueRadians);
        return ColorUtils.ArgbFromLinrgb(linrgb);
    }

    private static uint ARGBFromTone(double tone)
    {
        double fy = (tone + 16.0) / 116.0;
        double fz = fy;
        double fx = fy;
        double kappa = 24389.0 / 27.0;
        double epsilon = 216.0 / 24389.0;
        bool lExceedsEpsilonKappa = tone > 8.0;
        double y = lExceedsEpsilonKappa ? fy * fy * fy : tone / kappa;
        bool cubeExceedEpsilon = fy * fy * fy > epsilon;
        double x = cubeExceedEpsilon ? fx * fx * fx : tone / kappa;
        double z = cubeExceedEpsilon ? fz * fz * fz : tone / kappa;
        double[] whitePoint = WhitePointD65;
        return ArgbFromXyz(x * whitePoint[0], y * whitePoint[1], z * whitePoint[2]);
    }

    private static double ToLinear(this byte channel)
    {
        double normalized = channel / 255.0;
        if (normalized <= 0.040449936) return normalized / 12.92 * 100.0;
        else return Math.Pow((normalized + 0.055) / 1.055, 2.4) * 100.0;
    }
}