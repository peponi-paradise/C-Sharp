using System.Drawing;

namespace ToneTest;

internal static class Program
{
    public static TonalPalette Primary { get; set; } = null!;
    public static TonalPalette Secondary { get; set; } = null!;
    public static TonalPalette Tertiary { get; set; } = null!;
    public static TonalPalette Neutral { get; set; } = null!;
    public static TonalPalette NeutralVariant { get; set; } = null!;

    private static void Main(string[] args)
    {
        ViewingConditions c = ViewingConditions.Default;
        Console.WriteLine(c.Nc);
        Console.WriteLine(c.Ncb);
        Console.WriteLine(c.Z);
        Console.WriteLine(1 / 20.0 * 1.0169191804458755);

        Console.WriteLine("75,75,75");

        FromRGB(Color.FromArgb(0x009ABB));

        Console.WriteLine($"Primary : {Color.FromArgb((int)Primary[40])}");
        Console.WriteLine($"Secondary : {Color.FromArgb((int)Secondary[40])}");
        Console.WriteLine($"Tertiary : {Color.FromArgb((int)Tertiary[40])}");
        Console.WriteLine($"Neutral : {Color.FromArgb((int)Neutral[40])}");
        Console.WriteLine($"NeutralVariant : {Color.FromArgb((int)NeutralVariant[40])}");
    }

    private static void FromRGB(Color color)
    {
        Hct hct = Hct.FromInt((uint)color.ToArgb());
        double hue = hct.Hue;

        Primary = new(hue, 36);
        Secondary = new(hue, 16);
        Tertiary = new(hue + 60, 24);
        Neutral = new(hue, 4);
        NeutralVariant = new(hue, 8);
    }
}

public class TonalPalette
{
    private readonly Dictionary<uint, uint> _cache = new();
    private readonly double _hue;
    private readonly double _chroma;

    /// <summary>Creates tones using the HCT hue and chroma from a color.</summary>
    /// <param name="argb">ARGB representation of a color.</param>
    /// <returns>Tones matching that color's hue and chroma.</returns>
    public static TonalPalette FromInt(uint argb)
    {
        Hct hct = Hct.FromInt(argb);
        return FromHueAndChroma(hct.Hue, hct.Chroma);
    }

    /// <summary>Creates tones from a defined HCT hue and chroma.</summary>
    /// <param name="hue">HCT hue</param>
    /// <param name="chroma">HCT chroma</param>
    /// <returns>Tones matching hue and chroma.</returns>
    public static TonalPalette FromHueAndChroma(double hue, double chroma) => new(hue, chroma);

    public TonalPalette(double hue, double chroma)
    {
        _hue = hue;
        _chroma = chroma;
    }

    /// <summary>Creates an ARGB color with HCT hue and chroma of this TonalPalette instance, and the provided HCT tone.</summary>
    /// <param name="tone">HCT tone, measured from 0 to 100.</param>
    /// <returns>ARGB representation of a color with that tone.</returns>
    public uint Tone(uint tone)
        => _cache.TryGetValue(tone, out uint value)
            ? value
            : _cache[tone] = Hct.From(_hue, _chroma, tone).ToInt();

    /// <summary>Creates an ARGB color with HCT hue and chroma of this TonalPalette instance, and the provided HCT tone.</summary>
    /// <param name="tone">HCT tone, measured from 0 to 100.</param>
    /// <returns>ARGB representation of a color with that tone.</returns>
    public uint this[uint tone] => Tone(tone);
}

public class Hct
{
    private double hue;
    private double chroma;
    private double tone;
    private uint argb;

    /// <summary>
    /// Create an HCT color from hue, chroma, and tone.
    /// </summary>
    /// <param name="hue">0 ≤ hue &lt; 360; invalid values are corrected.</param>
    /// <param name="chroma">
    /// 0 ≤ chroma &lt; ?; Informally, colorfulness. The color returned may be lower than
    /// the requested chroma. Chroma has a different maximum for any given hue and tone.
    /// </param>
    /// <param name="tone">0 ≤ tone ≤ 100; invalid values are corrected.</param>
    /// <returns>HCT representation of a color in default viewing conditions.</returns>
    public static Hct From(double hue, double chroma, double tone)
    {
        uint argb = CamSolver.SolveToInt(hue, chroma, tone);
        return new(argb);
    }

    /// <summary>
    /// Create an HCT color from a color.
    /// </summary>
    /// <param name="argb">ARGB representation of a color</param>
    /// <returns>HCT representation of a color in default viewing conditions</returns>
    public static Hct FromInt(uint argb) => new(argb);

    private Hct(uint argb)
    {
        SetInternalState(argb);
    }

    /// <summary>
    /// The hue of this color.
    /// </summary>
    /// <remarks>
    /// 0 ≤ Hue &lt; 360; invalid values are corrected.<br/>
    /// After setting hue, the color is mapped from HCT to the more
    /// limited sRGB gamut for display. This will change its ARGB/integer
    /// representation. If the HCT color is outside of the sRGB gamut, chroma
    /// will decrease until it is inside the gamut.
    /// </remarks>
    public double Hue
    {
        get => hue;
        set => SetInternalState(CamSolver.SolveToInt(value, chroma, tone));
    }

    /// <summary>
    /// The chroma of this color.
    /// </summary>
    /// <remarks>
    /// 0 ≤ chroma &lt; ?<br/>
    /// After setting chroma, the color is mapped from HCT to the more
    /// limited sRGB gamut for display. This will change its ARGB/integer
    /// representation. If the HCT color is outside of the sRGB gamut, chroma
    /// will decrease until it is inside the gamut.
    /// </remarks>
    public double Chroma
    {
        get => chroma;
        set => SetInternalState(CamSolver.SolveToInt(hue, value, tone));
    }

    /// <summary>
    /// Lightness. Ranges from 0 to 100.
    /// </summary>
    /// <remarks>
    /// 0 ≤ tone ≤ 100; invalid values are corrected.<br/>
    /// After setting tone, the color is mapped from HCT to the more
    /// limited sRGB gamut for display. This will change its ARGB/integer
    /// representation. If the HCT color is outside of the sRGB gamut, chroma
    /// will decrease until it is inside the gamut.
    /// </remarks>
    public double Tone
    {
        get => tone;
        set => SetInternalState(CamSolver.SolveToInt(hue, chroma, value));
    }

    public uint ToInt() => argb;

    private void SetInternalState(uint argb)
    {
        this.argb = argb;
        Cam16 cam = Cam16.FromInt(argb);
        hue = cam.Hue;
        chroma = cam.Chroma;
        tone = ColorUtils.LStarFromArgb(argb);
    }
}

public static class CamSolver
{
    private static readonly double[][] ScaledDiscountFromLinrgb = new double[][]
    {
    new[] { 0.001200833568784504, 0.002389694492170889, 0.0002795742885861124 },
    new[] { 0.0005891086651375999, 0.0029785502573438758, 0.0003270666104008398 },
    new[] { 0.00010146692491640572, 0.0005364214359186694, 0.0032979401770712076 },
    };

    private static readonly double[][] LinrgbFromScaledDiscount = new double[][]
    {
    new[] { 1373.2198709594231, -1100.4251190754821, -7.278681089101213 },
    new[] { -271.815969077903, 559.6580465940733, -32.46047482791194 },
    new[] { 1.9622899599665666, -57.173814538844006, 308.7233197812385 },
    };

    private static readonly double[] YFromLinrgb = new double[] { 0.2126, 0.7152, 0.0722 };

    private static readonly double[] CriticalPlanes = new[]
    {
    0.015176349177441876,
    0.045529047532325624,
    0.07588174588720938,
    0.10623444424209313,
    0.13658714259697685,
    0.16693984095186062,
    0.19729253930674434,
    0.2276452376616281,
    0.2579979360165119,
    0.28835063437139563,
    0.3188300904430532,
    0.350925934958123,
    0.3848314933096426,
    0.42057480301049466,
    0.458183274052838,
    0.4976837250274023,
    0.5391024159806381,
    0.5824650784040898,
    0.6277969426914107,
    0.6751227633498623,
    0.7244668422128921,
    0.775853049866786,
    0.829304845476233,
    0.8848452951698498,
    0.942497089126609,
    1.0022825574869039,
    1.0642236851973577,
    1.1283421258858297,
    1.1946592148522128,
    1.2631959812511864,
    1.3339731595349034,
    1.407011200216447,
    1.4823302800086415,
    1.5599503113873272,
    1.6398909516233677,
    1.7221716113234105,
    1.8068114625156377,
    1.8938294463134073,
    1.9832442801866852,
    2.075074464868551,
    2.1693382909216234,
    2.2660538449872063,
    2.36523901573795,
    2.4669114995532007,
    2.5710888059345764,
    2.6777882626779785,
    2.7870270208169257,
    2.898822059350997,
    3.0131901897720907,
    3.1301480604002863,
    3.2497121605402226,
    3.3718988244681087,
    3.4967242352587946,
    3.624204428461639,
    3.754355295633311,
    3.887192587735158,
    4.022731918402185,
    4.160988767090289,
    4.301978482107941,
    4.445716283538092,
    4.592217266055746,
    4.741496401646282,
    4.893568542229298,
    5.048448422192488,
    5.20615066083972,
    5.3666897647573375,
    5.5300801301023865,
    5.696336044816294,
    5.865471690767354,
    6.037501145825082,
    6.212438385869475,
    6.390297286737924,
    6.571091626112461,
    6.7548350853498045,
    6.941541251256611,
    7.131223617812143,
    7.323895587840543,
    7.5195704746346665,
    7.7182615035334345,
    7.919981813454504,
    8.124744458384042,
    8.332562408825165,
    8.543448553206703,
    8.757415699253682,
    8.974476575321063,
    9.194643831691977,
    9.417930041841839,
    9.644347703669503,
    9.873909240696694,
    10.106627003236781,
    10.342513269534024,
    10.58158024687427,
    10.8238400726681,
    11.069304815507364,
    11.317986476196008,
    11.569896988756009,
    11.825048221409341,
    12.083451977536606,
    12.345119996613247,
    12.610063955123938,
    12.878295467455942,
    13.149826086772048,
    13.42466730586372,
    13.702830557985108,
    13.984327217668513,
    14.269168601521828,
    14.55736596900856,
    14.848930523210871,
    15.143873411576273,
    15.44220572664832,
    15.743938506781891,
    16.04908273684337,
    16.35764934889634,
    16.66964922287304,
    16.985093187232053,
    17.30399201960269,
    17.62635644741625,
    17.95219714852476,
    18.281524751807332,
    18.614349837764564,
    18.95068293910138,
    19.290534541298456,
    19.633915083172692,
    19.98083495742689,
    20.331304511189067,
    20.685334046541502,
    21.042933821039977,
    21.404114048223256,
    21.76888489811322,
    22.137256497705877,
    22.50923893145328,
    22.884842241736916,
    23.264076429332462,
    23.6469514538663,
    24.033477234264016,
    24.42366364919083,
    24.817520537484558,
    25.21505769858089,
    25.61628489293138,
    26.021211842414342,
    26.429848230738664,
    26.842203703840827,
    27.258287870275353,
    27.678110301598522,
    28.10168053274597,
    28.529008062403893,
    28.96010235337422,
    29.39497283293396,
    29.83362889318845,
    30.276079891419332,
    30.722335150426627,
    31.172403958865512,
    31.62629557157785,
    32.08401920991837,
    32.54558406207592,
    33.010999283389665,
    33.4802739966603,
    33.953417292456834,
    34.430438229418264,
    34.911345834551085,
    35.39614910352207,
    35.88485700094671,
    36.37747846067349,
    36.87402238606382,
    37.37449765026789,
    37.87891309649659,
    38.38727753828926,
    38.89959975977785,
    39.41588851594697,
    39.93615253289054,
    40.460400508064545,
    40.98864111053629,
    41.520882981230194,
    42.05713473317016,
    42.597404951718396,
    43.141702194811224,
    43.6900349931913,
    44.24241185063697,
    44.798841244188324,
    45.35933162437017,
    45.92389141541209,
    46.49252901546552,
    47.065252796817916,
    47.64207110610409,
    48.22299226451468,
    48.808024568002054,
    49.3971762874833,
    49.9904556690408,
    50.587870934119984,
    51.189430279724725,
    51.79514187861014,
    52.40501387947288,
    53.0190544071392,
    53.637271562750364,
    54.259673423945976,
    54.88626804504493,
    55.517063457223934,
    56.15206766869424,
    56.79128866487574,
    57.43473440856916,
    58.08241284012621,
    58.734331877617365,
    59.39049941699807,
    60.05092333227251,
    60.715611475655585,
    61.38457167773311,
    62.057811747619894,
    62.7353394731159,
    63.417162620860914,
    64.10328893648692,
    64.79372614476921,
    65.48848194977529,
    66.18756403501224,
    66.89098006357258,
    67.59873767827808,
    68.31084450182222,
    69.02730813691093,
    69.74813616640164,
    70.47333615344107,
    71.20291564160104,
    71.93688215501312,
    72.67524319850172,
    73.41800625771542,
    74.16517879925733,
    74.9167682708136,
    75.67278210128072,
    76.43322770089146,
    77.1981124613393,
    77.96744375590167,
    78.74122893956174,
    79.51947534912904,
    80.30219030335869,
    81.08938110306934,
    81.88105503125999,
    82.67721935322541,
    83.4778813166706,
    84.28304815182372,
    85.09272707154808,
    85.90692527145302,
    86.72564993000343,
    87.54890820862819,
    88.3767072518277,
    89.2090541872801,
    90.04595612594655,
    90.88742016217518,
    91.73345337380438,
    92.58406282226491,
    93.43925555268066,
    94.29903859396902,
    95.16341895893969,
    96.03240364439274,
    96.9059996312159,
    97.78421388448044,
    98.6670533535366,
    99.55452497210776,
};

    /// <summary>Sanitizes a small enough angle in radians.</summary>
    /// <param name="angle">An angle in radians; must not deviate too much from 0.</param>
    /// <returns>A coterminal angle between 0 and 2pi.</returns>
    private static double SanitizeRadians(double angle)
    {
        return (angle + Math.PI * 8) % (Math.PI * 2);
    }

    /// <summary>Delinearizes an RGB component, returning a floating-point number.</summary>
    /// <param name="rgbComponent">0.0 &lt;= rgb_component &gt;= 100.0, represents linear R/G/B channel</param>
    /// <returns>0.0 &lt;= output &lt;= 255.0, color channel converted to regular RGB space</returns>
    private static double TrueDelinearized(double rgbComponent)
    {
        double normalized = rgbComponent / 100.0;
        double delinearized;
        if (normalized <= 0.0031308)
        {
            delinearized = normalized * 12.92;
        }
        else
        {
            delinearized = 1.055 * Math.Pow(normalized, 1.0 / 2.4) - 0.055;
        }
        return delinearized * 255.0;
    }

    private static double ChromaticAdaptation(double component)
    {
        double af = Math.Pow(Math.Abs(component), 0.42);
        return Math.Sign(component) * 400.0 * af / (af + 27.13);
    }

    /// <summary>Returns the hue of a linear RGB color in CAM16.</summary>
    /// <param name="linrgb">The linear RGB coordinates of a color.</param>
    /// <returns>The hue of the color in CAM16, in radians.</returns>
    private static double HueOf(double[] linrgb)
    {
        double[] scaledDiscount = MathUtils.MatrixMultiply(linrgb, ScaledDiscountFromLinrgb);
        double rA = ChromaticAdaptation(scaledDiscount[0]);
        double gA = ChromaticAdaptation(scaledDiscount[1]);
        double bA = ChromaticAdaptation(scaledDiscount[2]);
        // redness-greenness
        double a = (11.0 * rA + -12.0 * gA + bA) / 11.0;
        // yellowness-blueness
        double b = (rA + gA - 2.0 * bA) / 9.0;
        return Math.Atan2(b, a);
    }

    private static bool AreInCyclicOrder(double a, double b, double c)
    {
        double deltaAB = SanitizeRadians(b - a);
        double deltaAC = SanitizeRadians(c - a);
        return deltaAB < deltaAC;
    }

    /// <summary>Solves the lerp equation.</summary>
    /// <param name="source">The starting number.</param>
    /// <param name="mid">The number in the middle.</param>
    /// <param name="target">The ending number.</param>
    /// <returns>A number t such that lerp(source, target, t) = mid.</returns>
    private static double Intercept(double source, double mid, double target)
    {
        return (mid - source) / (target - source);
    }

    private static double[] LerpPoint(double[] source, double t, double[] target)
    {
        return new[]
        {
        source[0] + (target[0] - source[0]) * t,
        source[1] + (target[1] - source[1]) * t,
        source[2] + (target[2] - source[2]) * t,
    };
    }

    /// <summary>Intersects a segment with a plane.</summary>
    /// <param name="source">The coordinates of point A.</param>
    /// <param name="coordinate">The R-, G-, or B-coordinate of the plane.</param>
    /// <param name="target">The coordinates of point B.</param>
    /// <param name="axis">The axis the plane is perpendicular with. (0: R, 1: G, 2: B)</param>
    /// <returns>The intersection point of the segment AB with the plane R=coordinate, G=coordinate, or
    private static double[] SetCoordinate(double[] source, double coordinate, double[] target, int axis)
    {
        double t = Intercept(source[axis], coordinate, target[axis]);
        return LerpPoint(source, t, target);
    }

    private static bool IsBounded(double x)
    {
        return 0.0 <= x && x <= 100.0;
    }

    /// <summary>Returns the intersections of the plane of constant y with the RGB cube.</summary>
    /// <param name="y">The Y value of the plane.</param>
    /// <returns>
    /// A list of points where the plane intersects with the edges of the RGB cube, in linear
    /// RGB coordinates.
    /// </returns>
    private static List<double[]> EdgePoints(double y)
    {
        double kR = YFromLinrgb[0];
        double kG = YFromLinrgb[1];
        double kB = YFromLinrgb[2];
        double[][] points = new double[][]
        {
        new[] {y / kR, 0.0, 0.0},
        new[] {(y - 100 * kB) / kR, 0.0, 100.0},
        new[] {(y - 100 * kG) / kR, 100.0, 0.0},
        new[] {(y - 100 * kB - 100 * kG) / kR, 100.0, 100.0},
        new[] {0.0, y / kG, 0.0},
        new[] {100.0, (y - 100 * kR) / kG, 0.0},
        new[] {0.0, (y - 100 * kB) / kG, 100.0},
        new[] {100.0, (y - 100 * kR - 100 * kB) / kG, 100.0},
        new[] {0.0, 0.0, y / kB},
        new[] {100.0, 0.0, (y - 100 * kR) / kB},
        new[] {0.0, 100.0, (y - 100 * kG) / kB},
        new[] {100.0, 100.0, (y - 100 * kR - 100 * kG) / kB},
        };
        List<double[]> ans = new();
        foreach (double[] point in points)
        {
            if (IsBounded(point[0]) && IsBounded(point[1]) && IsBounded(point[2]))
            {
                ans.Add(point);
            }
        }
        return ans;
    }

    /// <summary>Finds the segment containing the desired color.</summary>
    /// <param name="y">The Y value of the color.</param>
    /// <param name="targetHue">The hue of the color.</param>
    /// <returns>
    /// A list of two sets of linear RGB coordinates, each corresponding to an endpoint of the
    /// segment containing the desired color.
    /// </returns>
    private static double[][] BisectToSegment(double y, double targetHue)
    {
        List<double[]> vertices = EdgePoints(y);
        double[] left = vertices[0];
        double[] right = left;
        double leftHue = HueOf(left);
        double rightHue = leftHue;
        bool uncut = true;
        for (int i = 1; i < vertices.Count; i++)
        {
            double[] mid = vertices[i];
            double midHue = HueOf(mid);
            if (uncut || AreInCyclicOrder(leftHue, midHue, rightHue))
            {
                uncut = false;
                if (AreInCyclicOrder(leftHue, targetHue, midHue))
                {
                    right = mid;
                    rightHue = midHue;
                }
                else
                {
                    left = mid;
                    leftHue = midHue;
                }
            }
        }
        return new[] { left, right };
    }

    private static double[] Midpoint(double[] a, double[] b)
    {
        return new[]
        {
        (a[0] + b[0]) / 2, (a[1] + b[1]) / 2, (a[2] + b[2]) / 2,
    };
    }

    private static int CriticalPlaneBelow(double x)
    {
        return (int)Math.Floor(x - 0.5);
    }

    private static int CriticalPlaneAbove(double x)
    {
        return (int)Math.Ceiling(x - 0.5);
    }

    /// <summary>Finds a color with the given Y and hue on the boundary of the cube.</summary>
    /// <param name="y">The Y value of the color.</param>
    /// <param name="targetHue">The hue of the color.</param>
    /// <returns>The desired color, in linear RGB coordinates.</returns>
    private static double[] BisectToLimit(double y, double targetHue)
    {
        double[][] segment = BisectToSegment(y, targetHue);
        double[] left = segment[0];
        double leftHue = HueOf(left);
        double[] right = segment[1];
        for (int axis = 0; axis < 3; axis++)
        {
            if (left[axis] != right[axis])
            {
                int lPlane;
                int rPlane;
                if (left[axis] < right[axis])
                {
                    lPlane = CriticalPlaneBelow(TrueDelinearized(left[axis]));
                    rPlane = CriticalPlaneAbove(TrueDelinearized(right[axis]));
                }
                else
                {
                    lPlane = CriticalPlaneAbove(TrueDelinearized(left[axis]));
                    rPlane = CriticalPlaneBelow(TrueDelinearized(right[axis]));
                }
                for (int i = 0; i < 8; i++)
                {
                    if (Math.Abs(rPlane - lPlane) <= 1)
                    {
                        break;
                    }
                    else
                    {
                        int mPlane = (lPlane + rPlane) / 2;
                        double midPlaneCoordinate = CriticalPlanes[mPlane];
                        double[] mid = SetCoordinate(left, midPlaneCoordinate, right, axis);
                        double midHue = HueOf(mid);
                        if (AreInCyclicOrder(leftHue, targetHue, midHue))
                        {
                            right = mid;
                            rPlane = mPlane;
                        }
                        else
                        {
                            left = mid;
                            leftHue = midHue;
                            lPlane = mPlane;
                        }
                    }
                }
            }
        }
        return Midpoint(left, right);
    }

    private static double InverseChromaticAdaptation(double adapted)
    {
        double adaptedAbs = Math.Abs(adapted);
        double @base = Math.Max(0, 27.13 * adaptedAbs / (400.0 - adaptedAbs));
        return Math.Sign(adapted) * Math.Pow(@base, 1.0 / 0.42);
    }

    /// <summary>Finds a color with the given hue, chroma, and Y.</summary>
    /// <param name="hueRadians">The desired hue in radians.</param>
    /// <param name="chroma">The desired chroma.</param>
    /// <param name="y">The desired Y.</param>
    /// <returns>The desired color as a hexadecimal integer, if found; 0 otherwise.</returns>
    private static uint FindResultByJ(double hueRadians, double chroma, double y)
    {
        // Initial estimate of j.
        double j = Math.Sqrt(y) * 11.0;
        // ===========================================================
        // Operations inlined from Cam16 to avoid repeated calculation
        // ===========================================================
        ViewingConditions viewingConditions = ViewingConditions.Default;
        double tInnerCoeff = 1 / Math.Pow(1.64 - Math.Pow(0.29, viewingConditions.N), 0.73);
        double eHue = 0.25 * (Math.Cos(hueRadians + 2.0) + 3.8);
        double p1 = eHue * (50000.0 / 13.0) * viewingConditions.Nc * viewingConditions.Ncb;
        double hSin = Math.Sin(hueRadians);
        double hCos = Math.Cos(hueRadians);
        for (int iterationRound = 0; iterationRound < 5; iterationRound++)
        {
            // ===========================================================
            // Operations inlined from Cam16 to avoid repeated calculation
            // ===========================================================
            double jNormalized = j / 100.0;
            double alpha = chroma == 0.0 || j == 0.0 ? 0.0 : chroma / Math.Sqrt(jNormalized);
            double t = Math.Pow(alpha * tInnerCoeff, 1.0 / 0.9);
            double ac =
                viewingConditions.Aw
                    * Math.Pow(jNormalized, 1.0 / viewingConditions.C / viewingConditions.Z);
            double p2 = ac / viewingConditions.Nbb;
            double gamma = 23.0 * (p2 + 0.305) * t / (23.0 * p1 + 11 * t * hCos + 108.0 * t * hSin);
            double a = gamma * hCos;
            double b = gamma * hSin;
            double rA = (460.0 * p2 + 451.0 * a + 288.0 * b) / 1403.0;
            double gA = (460.0 * p2 - 891.0 * a - 261.0 * b) / 1403.0;
            double bA = (460.0 * p2 - 220.0 * a - 6300.0 * b) / 1403.0;
            double rCScaled = InverseChromaticAdaptation(rA);
            double gCScaled = InverseChromaticAdaptation(gA);
            double bCScaled = InverseChromaticAdaptation(bA);
            double[] linrgb =
                MathUtils.MatrixMultiply(
                    new double[] { rCScaled, gCScaled, bCScaled }, LinrgbFromScaledDiscount);
            // ===========================================================
            // Operations inlined from Cam16 to avoid repeated calculation
            // ===========================================================
            if (linrgb[0] < 0 || linrgb[1] < 0 || linrgb[2] < 0)
            {
                return 0;
            }
            double kR = YFromLinrgb[0];
            double kG = YFromLinrgb[1];
            double kB = YFromLinrgb[2];
            double fnj = kR * linrgb[0] + kG * linrgb[1] + kB * linrgb[2];
            if (fnj <= 0)
            {
                return 0;
            }
            if (iterationRound == 4 || Math.Abs(fnj - y) < 0.002)
            {
                if (linrgb[0] > 100.01 || linrgb[1] > 100.01 || linrgb[2] > 100.01)
                {
                    return 0;
                }
                return ColorUtils.ArgbFromLinrgb(linrgb);
            }
            // Iterates with Newton method,
            // Using 2 * fn(j) / j as the approximation of fn'(j)
            j -= (fnj - y) * j / (2 * fnj);
        }
        return 0;
    }

    /// <summary>Finds an sRGB color with the given hue, chroma, and L*.</summary>
    /// <param name="hueDegrees">The desired hue, in degrees.</param>
    /// <param name="chroma">The desired chroma.</param>
    /// <param name="lstar">The desired L*.</param>
    /// <returns>
    /// A hexadecimal representing the sRGB color. The color has sufficiently close hue,
    /// chroma, and L* to the desired values, if possible; otherwise, the hue and L* will be
    /// sufficiently close, and chroma will be maximized.
    /// </returns>
    public static uint SolveToInt(double hueDegrees, double chroma, double lstar)
    {
        if (chroma < 0.0001 || lstar < 0.0001 || lstar > 99.9999)
        {
            return ColorUtils.ArgbFromLstar(lstar);
        }
        hueDegrees = MathUtils.SanitizeDegreesDouble(hueDegrees);
        double hueRadians = MathUtils.ToRadians(hueDegrees);
        double y = ColorUtils.YFromLstar(lstar);
        uint exactAnswer = FindResultByJ(hueRadians, chroma, y);
        if (exactAnswer != 0)
        {
            return exactAnswer;
        }
        double[] linrgb = BisectToLimit(y, hueRadians);
        return ColorUtils.ArgbFromLinrgb(linrgb);
    }

    /// <summary>Finds an sRGB color with the given hue, chroma, and L*.</summary>
    /// <param name="hueDegrees">The desired hue, in degrees.</param>
    /// <param name="chroma">The desired chroma.</param>
    /// <param name="lstar">The desired L*.</param>
    /// <returns>
    /// An CAM16 object representing the sRGB color. The color has sufficiently close hue,
    /// chroma, and L* to the desired values, if possible; otherwise, the hue and L* will be
    /// sufficiently close, and chroma will be maximized.
    /// </returns>
    public static Cam16 SolveToCam(double hueDegrees, double chroma, double lstar)
    {
        return Cam16.FromInt(SolveToInt(hueDegrees, chroma, lstar));
    }
}

public class Cam16
{
    // Transforms XYZ color space coordinates to 'cone'/'RGB' responses in CAM16.
    internal static readonly double[][] XyzToCam16Rgb =
    {
    new[] { 0.401288, 0.650173, -0.051461 },
    new[] { -0.250268, 1.204414, 0.045854 },
    new[] { -0.002079, 0.048952, 0.953127 }
};

    // Transforms 'cone'/'RGB' responses in CAM16 to XYZ color space coordinates.
    internal static readonly double[][] Cam16RgbToXyz =
    {
    new[] { 1.8620678, -1.0112547, 0.14918678 },
    new[] { 0.38752654, 0.62144744, -0.00897398 },
    new[] { -0.01584150, -0.03412294, 1.0499644 }
};

    /// <summary>Hue in CAM16</summary>
    public double Hue { get; set; }

    /// <summary>Chroma in CAM16</summary>
    public double Chroma { get; set; }

    /// <summary>Lightness in CAM16</summary>
    public double J { get; set; }

    /// <summary>Brightness in CAM16.</summary>
    /// <remarks>
    /// Prefer lightness, brightness is an absolute quantity. For example, a sheet of white paper is
    /// much brighter viewed in sunlight than in indoor light, but it is the lightest object under any
    /// lighting.
    /// </remarks>
    public double Q { get; set; }

    /// <summary>Colorfulness in CAM16.</summary>
    /// <remarks>
    /// Prefer chroma, colorfulness is an absolute quantity. For example, a yellow toy car is much
    /// more colorful outside than inside, but it has the same chroma in both environments.
    /// </remarks>
    public double M { get; set; }

    /// <summary>Saturation in CAM16.</summary>
    /// <remarks>
    /// Colorfulness in proportion to brightness. Prefer chroma, saturation measures colorfulness
    /// relative to the color's own brightness, where chroma is colorfulness relative to white.
    /// </remarks>
    public double S { get; set; }

    /// <summary>Lightness coordinate in CAM16-UCS</summary>
    public double Jstar { get; set; }

    /// <summary>a* coordinate in CAM16-UCS</summary>
    public double Astar { get; set; }

    /// <summary>b* coordinate in CAM16-UCS</summary>
    public double Bstar { get; set; }

    /// <summary>
    /// CAM16 instances also have coordinates in the CAM16-UCS space, called J*,
    /// a*, b*, or jstar, astar, bstar in code. CAM16-UCS is included in the CAM16
    /// specification, and should be used when measuring distances between colors.
    /// </summary>
    public double Distance(Cam16 other)
    {
        double dJ = Jstar - other.Jstar;
        double dA = Astar - other.Astar;
        double dB = Bstar - other.Bstar;
        double dEPrime = Math.Sqrt(dJ * dJ + dA * dA + dB * dB);
        double dE = 1.41 * Math.Pow(dEPrime, .63);
        return dE;
    }

    /// <summary>
    /// All of the CAM16 dimensions can be calculated from 3 of the dimensions, in the following
    /// combinations: - {j or q} and {c, m, or s} and hue - jstar, astar, bstar.
    /// Prefer using a static
    /// method that constructs from 3 of those dimensions. This constructor is intended for those
    /// methods to use to return all possible dimensions.
    /// </summary>
    /// <param name="hue">for example, red, orange, yellow, green, etc.</param>
    /// <param name="chroma">
    /// informally, colorfulness / color intensity. like saturation in HSL, except
    /// perceptually accurate.
    /// </param>
    /// <param name="j">lightness</param>
    /// <param name="q">brightness; ratio of lightness to white point's lightness</param>
    /// <param name="m">colorfulness</param>
    /// <param name="s">saturation; ratio of chroma to white point's chroma</param>
    /// <param name="jStar">CAM16-UCS J* coordinate</param>
    /// <param name="aStar">CAM16-UCS a* coordinate</param>
    /// <param name="bStar">CAM16-UCS b* coordinate</param>
    private Cam16(double hue, double chroma, double j, double q, double m, double s, double jStar, double aStar, double bStar)
    {
        Hue = hue;
        Chroma = chroma;
        J = j;
        Q = q;
        M = m;
        S = s;
        Jstar = jStar;
        Astar = aStar;
        Bstar = bStar;
    }

    /// <summary>
    /// Create a CAM16 color from a color, assuming the color was viewed in default viewing conditions.
    /// </summary>
    /// <param name="argb">ARGB representation of a color.</param>
    public static Cam16 FromInt(uint argb) => FromIntInViewingConditions(argb, ViewingConditions.Default);

    /// <summary>
    /// Create a CAM16 color from a color in defined viewing conditions.
    /// </summary>
    /// <param name="argb">ARGB representation of a color.</param>
    /// <param name="viewingConditions">Information about the environment where the color was observed.</param>
    public static Cam16 FromIntInViewingConditions(uint argb, ViewingConditions viewingConditions)
    {
        // Transform ARGB int to XYZ
        uint red = (argb & 0x00ff0000) >> 16;
        uint green = (argb & 0x0000ff00) >> 8;
        uint blue = (argb & 0x000000ff);
        double redL = ColorUtils.Linearized(red);
        double greenL = ColorUtils.Linearized(green);
        double blueL = ColorUtils.Linearized(blue);
        double x = 0.41233895 * redL + 0.35762064 * greenL + 0.18051042 * blueL;
        double y = 0.2126 * redL + 0.7152 * greenL + 0.0722 * blueL;
        double z = 0.01932141 * redL + 0.11916382 * greenL + 0.95034478 * blueL;

        // Transform XYZ to 'cone'/'rgb' responses
        double[][] matrix = XyzToCam16Rgb;
        double rT = (x * matrix[0][0]) + (y * matrix[0][1]) + (z * matrix[0][2]);
        double gT = (x * matrix[1][0]) + (y * matrix[1][1]) + (z * matrix[1][2]);
        double bT = (x * matrix[2][0]) + (y * matrix[2][1]) + (z * matrix[2][2]);

        // Discount illuminant
        double rD = viewingConditions.RgbD[0] * rT;
        double gD = viewingConditions.RgbD[1] * gT;
        double bD = viewingConditions.RgbD[2] * bT;

        // Chromatic adaptation
        double rAF = Math.Pow(viewingConditions.Fl * Math.Abs(rD) / 100.0, 0.42);
        double gAF = Math.Pow(viewingConditions.Fl * Math.Abs(gD) / 100.0, 0.42);
        double bAF = Math.Pow(viewingConditions.Fl * Math.Abs(bD) / 100.0, 0.42);
        double rA = Math.Sign(rD) * 400.0 * rAF / (rAF + 27.13);
        double gA = Math.Sign(gD) * 400.0 * gAF / (gAF + 27.13);
        double bA = Math.Sign(bD) * 400.0 * bAF / (bAF + 27.13);

        // redness-greenness
        double a = (11.0 * rA + -12.0 * gA + bA) / 11.0;
        // yellowness-blueness
        double b = (rA + gA - 2.0 * bA) / 9.0;

        // auxiliary components
        double u = (20.0 * rA + 20.0 * gA + 21.0 * bA) / 20.0;
        double p2 = (40.0 * rA + 20.0 * gA + bA) / 20.0;

        // hue
        double atan2 = Math.Atan2(b, a);
        double atanDegrees = atan2 * 180.0 / Math.PI;
        double hue =
            atanDegrees < 0
                ? atanDegrees + 360.0
                : atanDegrees >= 360 ? atanDegrees - 360.0 : atanDegrees;
        double hueRadians = hue * Math.PI / 180.0;

        // achromatic response to color
        double ac = p2 * viewingConditions.Nbb;

        // CAM16 lightness and brightness
        double j = 100.0 * Math.Pow(
            ac / viewingConditions.Aw,
            viewingConditions.C * viewingConditions.Z);
        double q = 4.0
            / viewingConditions.C
            * Math.Sqrt(j / 100.0)
            * (viewingConditions.Aw + 4.0)
            * viewingConditions.FlRoot;

        // CAM16 chroma, colorfulness, and saturation.
        double huePrime = (hue < 20.14) ? hue + 360 : hue;
        double eHue = 0.25 * (Math.Cos(MathUtils.ToRadians(huePrime) + 2.0) + 3.8);
        double p1 = 50000.0 / 13.0 * eHue * viewingConditions.Nc * viewingConditions.Ncb;
        double t = p1 * MathUtils.Hypot(a, b) / (u + 0.305);
        double alpha = Math.Pow(1.64 - Math.Pow(0.29, viewingConditions.N), 0.73)
            * Math.Pow(t, 0.9);

        // CAM16 chroma, colorfulness, saturation
        double c = alpha * Math.Sqrt(j / 100.0);
        double m = c * viewingConditions.FlRoot;
        double s =
            50.0 * Math.Sqrt(alpha * viewingConditions.C / (viewingConditions.Aw + 4.0));

        // CAM16-UCS components
        double jstar = (1.0 + 100.0 * 0.007) * j / (1.0 + 0.007 * j);
        double mstar = 1.0 / 0.0228 * MathUtils.Log1p(0.0228 * m);
        double astar = mstar * Math.Cos(hueRadians);
        double bstar = mstar * Math.Sin(hueRadians);

        return new Cam16(hue, c, j, q, m, s, jstar, astar, bstar);
    }

    /// <param name="j">lightness</param>
    /// <param name="c">chroma</param>
    /// <param name="h">hue</param>
    public static Cam16 FromJch(double j, double c, double h) => FromJchInViewingConditions(j, c, h, ViewingConditions.Default);

    /// <param name="j">lightness</param>
    /// <param name="c">chroma</param>
    /// <param name="h">hue</param>
    /// <param name="viewingConditions">Information about the environment where the color was observed.</param>
    public static Cam16 FromJchInViewingConditions(
        double j, double c, double h, ViewingConditions viewingConditions)
    {
        double q =
            4.0
                / viewingConditions.C
                * Math.Sqrt(j / 100.0)
                * (viewingConditions.Aw + 4.0)
                * viewingConditions.FlRoot;
        double m = c * viewingConditions.FlRoot;
        double alpha = c / Math.Sqrt(j / 100.0);
        double s =
            50.0 * Math.Sqrt(alpha * viewingConditions.C / (viewingConditions.Aw + 4.0));

        double hueRadians = h * Math.PI / 180.0;
        double jstar = (1.0 + 100.0 * 0.007) * j / (1.0 + 0.007 * j);
        double mstar = 1.0 / 0.0228 * MathUtils.Log1p(0.0228 * m);
        double astar = mstar * Math.Cos(hueRadians);
        double bstar = mstar * Math.Sin(hueRadians);
        return new Cam16(h, c, j, q, m, s, jstar, astar, bstar);
    }

    /// <summary>
    /// Create a CAM16 color from CAM16-UCS coordinates.
    /// </summary>
    /// <param name="jstar">CAM16-UCS lightness.</param>
    /// <param name="astar">CAM16-UCS a dimension. Like a* in L*a*b*, it is a Cartesian coordinate on the Y axis.</param>
    /// <param name="bstar">CAM16-UCS b dimension. Like a* in L*a*b*, it is a Cartesian coordinate on the X axis.</param>
    public static Cam16 FromUcs(double jstar, double astar, double bstar) =>
        FromUcsInViewingConditions(jstar, astar, bstar, ViewingConditions.Default);

    /// <summary>
    /// Create a CAM16 color from CAM16-UCS coordinates in defined viewing conditions.
    /// </summary>
    /// <param name="jstar">CAM16-UCS lightness.</param>
    /// <param name="astar">CAM16-UCS a dimension. Like a* in L*a*b*, it is a Cartesian coordinate on the Y axis.</param>
    /// <param name="bstar">CAM16-UCS b dimension. Like a* in L*a*b*, it is a Cartesian coordinate on the X axis.</param>
    /// <param name="viewingConditions">Information about the environment where the color was observed.</param>
    public static Cam16 FromUcsInViewingConditions(
        double jstar, double astar, double bstar, ViewingConditions viewingConditions)
    {
        double m = MathUtils.Hypot(astar, bstar);
        double m2 = MathUtils.Expm1(m * 0.0228) / 0.0228;
        double c = m2 / viewingConditions.FlRoot;
        double h = Math.Atan2(bstar, astar) * (180.0 / Math.PI);
        if (h < 0.0)
        {
            h += 360.0;
        }
        double j = jstar / (1 - (jstar - 100) * 0.007);
        return FromJchInViewingConditions(j, c, h, viewingConditions);
    }

    /// <summary>
    /// ARGB representation of the color. Assumes the color was viewed in default viewing conditions,
    /// which are near-identical to the default viewing conditions for sRGB.
    /// </summary>
    /// <returns></returns>
    public uint ToInt() => Viewed(ViewingConditions.Default);

    /// <summary>
    /// ARGB representation of the color, in defined viewing conditions.
    /// </summary>
    /// <param name="viewingConditions">Information about the environment where the color will be viewed.</param>
    /// <returns>ARGB representation of color</returns>
    public uint Viewed(ViewingConditions viewingConditions)
    {
        double alpha =
            (Chroma == 0.0 || J == 0.0)
                ? 0.0
                : Chroma / Math.Sqrt(J / 100.0);

        double t =

                Math.Pow(
                    alpha / Math.Pow(1.64 - Math.Pow(0.29, viewingConditions.N), 0.73), 1.0 / 0.9);
        double hRad = Hue * Math.PI / 180.0;

        double eHue = 0.25 * (Math.Cos(hRad + 2.0) + 3.8);
        double ac =
            viewingConditions.Aw
                *
                    Math.Pow(J / 100.0, 1.0 / viewingConditions.C / viewingConditions.Z);
        double p1 = eHue * (50000.0 / 13.0) * viewingConditions.Nc * viewingConditions.Ncb;
        double p2 = ac / viewingConditions.Nbb;

        double hSin = Math.Sin(hRad);
        double hCos = Math.Cos(hRad);

        double gamma = 23.0 * (p2 + 0.305) * t / (23.0 * p1 + 11.0 * t * hCos + 108.0 * t * hSin);
        double a = gamma * hCos;
        double b = gamma * hSin;
        double rA = (460.0 * p2 + 451.0 * a + 288.0 * b) / 1403.0;
        double gA = (460.0 * p2 - 891.0 * a - 261.0 * b) / 1403.0;
        double bA = (460.0 * p2 - 220.0 * a - 6300.0 * b) / 1403.0;

        double rCBase = Math.Max(0, (27.13 * Math.Abs(rA)) / (400.0 - Math.Abs(rA)));
        double rC =
            Math.Sign(rA)
                * (100.0 / viewingConditions.Fl)
                * Math.Pow(rCBase, 1.0 / 0.42);
        double gCBase = Math.Max(0, (27.13 * Math.Abs(gA)) / (400.0 - Math.Abs(gA)));
        double gC =
            Math.Sign(gA)
                * (100.0 / viewingConditions.Fl)
                * Math.Pow(gCBase, 1.0 / 0.42);
        double bCBase = Math.Max(0, (27.13 * Math.Abs(bA)) / (400.0 - Math.Abs(bA)));
        double bC =
            Math.Sign(bA)
                * (100.0 / viewingConditions.Fl)
                * Math.Pow(bCBase, 1.0 / 0.42);
        double rF = rC / viewingConditions.RgbD[0];
        double gF = gC / viewingConditions.RgbD[1];
        double bF = bC / viewingConditions.RgbD[2];

        double[][] matrix = Cam16RgbToXyz;
        double x = (rF * matrix[0][0]) + (gF * matrix[0][1]) + (bF * matrix[0][2]);
        double y = (rF * matrix[1][0]) + (gF * matrix[1][1]) + (bF * matrix[1][2]);
        double z = (rF * matrix[2][0]) + (gF * matrix[2][1]) + (bF * matrix[2][2]);

        return ColorUtils.ArgbFromXyz(x, y, z);
    }
}

public static class ColorUtils
{
    private static readonly double[][] SrgbToXyz =
    {
        new[] { 0.41233895, 0.35762064, 0.18051042 },
        new[] { 0.2126, 0.7152, 0.0722 },
        new[] { 0.01932141, 0.11916382, 0.95034478 }
    };

    private static readonly double[][] XyzToSrgb =
    {
        new[] { 3.2413774792388685, -1.5376652402851851, -0.49885366846268053 },
        new[] { -0.9691452513005321, 1.8758853451067872, 0.04156585616912061 },
        new[] { 0.05562093689691305, -0.20395524564742123, 1.0571799111220335 }
    };

    /// <summary>
    /// The standard white point; white on a sunny day.
    /// </summary>
    public static double[] WhitePointD65 { get; } = { 95.047, 100, 108.883 };

    /// <summary>Converts a color from RGB components to ARGB format.</summary>
    public static uint ArgbFromRgb(uint red, uint green, uint blue)
    {
        return (255u << 24) | ((red & 255) << 16) | ((green & 255) << 8) | (blue & 255);
    }

    /// <summary>Converts a color from ARGB components to ARGB format.</summary>
    public static uint ArgbFromComponents(uint alpha, uint red, uint green, uint blue)
    {
        return ((alpha & 255) << 24) | ((red & 255) << 16) | ((green & 255) << 8) | (blue & 255);
    }

    /// <summary>Converts a color from linear RGB components to ARGB format.</summary>
    public static uint ArgbFromLinrgb(double[] linrgb)
    {
        uint r = Delinearized(linrgb[0]);
        uint g = Delinearized(linrgb[1]);
        uint b = Delinearized(linrgb[2]);
        return ArgbFromRgb(r, g, b);
    }

    /// <summary>Returns the alpha component of a color in ARGB format.</summary>
    public static uint AlphaFromArgb(uint argb)
    {
        return (argb >> 24) & 255;
    }

    /// <summary>Returns the red component of a color in ARGB format.</summary>
    public static uint RedFromArgb(uint argb)
    {
        return (argb >> 16) & 255;
    }

    /// <summary>Returns the green component of a color in ARGB format.</summary>
    public static uint GreenFromArgb(uint argb)
    {
        return (argb >> 8) & 255;
    }

    /// <summary>Returns the blue component of a color in ARGB format.</summary>
    public static uint BlueFromArgb(uint argb)
    {
        return argb & 255;
    }

    /// <summary>Returns whether a color in ARGB format is opaque.</summary>
    public static bool IsOpaque(uint argb)
    {
        return AlphaFromArgb(argb) >= 255;
    }

    /// <summary>Converts a color from XYZ to ARGB.</summary>
    public static uint ArgbFromXyz(double x, double y, double z)
    {
        double[][] matrix = XyzToSrgb;
        double linearR = matrix[0][0] * x + matrix[0][1] * y + matrix[0][2] * z;
        double linearG = matrix[1][0] * x + matrix[1][1] * y + matrix[1][2] * z;
        double linearB = matrix[2][0] * x + matrix[2][1] * y + matrix[2][2] * z;
        uint r = Delinearized(linearR);
        uint g = Delinearized(linearG);
        uint b = Delinearized(linearB);
        return ArgbFromRgb(r, g, b);
    }

    /// <summary>Converts a color from ARGB to XYZ.</summary>
    public static double[] XyzFromArgb(uint argb)
    {
        double r = Linearized(RedFromArgb(argb));
        double g = Linearized(GreenFromArgb(argb));
        double b = Linearized(BlueFromArgb(argb));
        return MathUtils.MatrixMultiply(new double[] { r, g, b }, SrgbToXyz);
    }

    /// <summary>Converts a color represented in Lab color space into an ARGB integer.</summary>
    public static uint ArgbFromLab(double l, double a, double b)
    {
        double[] whitePoint = WhitePointD65;
        double fy = (l + 16.0) / 116.0;
        double fx = a / 500.0 + fy;
        double fz = fy - b / 200.0;
        double xNormalized = LabInvf(fx);
        double yNormalized = LabInvf(fy);
        double zNormalized = LabInvf(fz);
        double x = xNormalized * whitePoint[0];
        double y = yNormalized * whitePoint[1];
        double z = zNormalized * whitePoint[2];
        return ArgbFromXyz(x, y, z);
    }

    /// <summary>Converts a color from ARGB representation to L*a*b* representation.</summary>
    public static double[] LabFromArgb(uint argb)
    {
        double linearR = Linearized(RedFromArgb(argb));
        double linearG = Linearized(GreenFromArgb(argb));
        double linearB = Linearized(BlueFromArgb(argb));
        double[][] matrix = SrgbToXyz;
        double x = matrix[0][0] * linearR + matrix[0][1] * linearG + matrix[0][2] * linearB;
        double y = matrix[1][0] * linearR + matrix[1][1] * linearG + matrix[1][2] * linearB;
        double z = matrix[2][0] * linearR + matrix[2][1] * linearG + matrix[2][2] * linearB;
        double[] whitePoint = WhitePointD65;
        double xNormalized = x / whitePoint[0];
        double yNormalized = y / whitePoint[1];
        double zNormalized = z / whitePoint[2];
        double fx = LabF(xNormalized);
        double fy = LabF(yNormalized);
        double fz = LabF(zNormalized);
        double l = 116.0 * fy - 16;
        double a = 500.0 * (fx - fy);
        double b = 200.0 * (fy - fz);
        return new double[] { l, a, b };
    }

    /// <summary>Converts an L* value to an ARGB representation.</summary>
    /// <param name="l">L* in L*a*b*</param>
    /// <returns>ARGB representation of grayscale color with lightness matching L*</returns>
    public static uint ArgbFromLstar(double lstar)
    {
        double fy = (lstar + 16.0) / 116.0;
        double fz = fy;
        double fx = fy;
        double kappa = 24389.0 / 27.0;
        double epsilon = 216.0 / 24389.0;
        bool lExceedsEpsilonKappa = lstar > 8.0;
        double y = lExceedsEpsilonKappa ? fy * fy * fy : lstar / kappa;
        bool cubeExceedEpsilon = fy * fy * fy > epsilon;
        double x = cubeExceedEpsilon ? fx * fx * fx : lstar / kappa;
        double z = cubeExceedEpsilon ? fz * fz * fz : lstar / kappa;
        double[] whitePoint = WhitePointD65;
        return ArgbFromXyz(x * whitePoint[0], y * whitePoint[1], z * whitePoint[2]);
    }

    /// <summary>Computes the L* value of a color in ARGB representation.</summary>
    /// <param name="argb">ARGB representation of a color</param>
    /// <returns>L*, from L*a*b*, coordinate of the color</returns>
    public static double LStarFromArgb(uint argb)
    {
        double y = XyzFromArgb(argb)[1] / 100.0;
        double e = 216.0 / 24389.0;
        if (y <= e)
        {
            return 24389.0 / 27.0 * y;
        }
        else
        {
            double yIntermediate = Math.Pow(y, 1.0 / 3.0);
            return 116.0 * yIntermediate - 16.0;
        }
    }

    /// <summary>
    /// Converts an L* value to a Y value.
    /// </summary>
    /// <remarks>
    /// L* in L*a*b* and Y in XYZ measure the same quantity, luminance.
    /// L* measures perceptual luminance, a linear scale. Y in XYZ measures relative luminance, a
    /// logarithmic scale.
    /// </remarks>
    /// <param name="lstar">L* in L*a*b*</param>
    /// <returns>Y in XYZ</returns>
    public static double YFromLstar(double lstar)
    {
        double ke = 8.0;
        if (lstar > ke)
        {
            return Math.Pow((lstar + 16.0) / 116.0, 3.0) * 100.0;
        }
        else
        {
            return lstar / (24389.0 / 27.0) * 100.0;
        }
    }

    /// <summary>
    /// Linearizes an RGB component.
    /// </summary>
    /// <param name="rgbComponent">0 ≤ rgb_component ≤ 255, represents R/G/B channel</param>
    /// <returns>0.0 ≤ output ≤ 100.0, color channel converted to linear RGB space</returns>
    public static double Linearized(uint rgbComponent)
    {
        double normalized = rgbComponent / 255.0;
        if (normalized <= 0.040449936)
        {
            return normalized / 12.92 * 100.0;
        }
        else
        {
            return Math.Pow((normalized + 0.055) / 1.055, 2.4) * 100.0;
        }
    }

    /// <summary>
    /// Delinearizes an RGB component.
    /// </summary>
    /// <param name="rgbComponent">0.0 ≤ rgb_component ≤ 100.0, represents linear R/G/B channel</param>
    /// <returns>0 ≤ output ≤ 255, color channel converted to regular RGB space</returns>
    public static uint Delinearized(double rgbComponent)
    {
        double normalized = rgbComponent / 100.0;
        double delinearized;
        if (normalized <= 0.0031308)
        {
            delinearized = normalized * 12.92;
        }
        else
        {
            delinearized = 1.055 * Math.Pow(normalized, 1.0 / 2.4) - 0.055;
        }
        return (uint)MathUtils.ClampInt(0, 255, (int)Math.Round(delinearized * 255.0));
    }

    public static double LabF(double t)
    {
        const double e = 216.0 / 24389.0;
        const double kappa = 24389.0 / 27.0;
        if (t > e)
        {
            return Math.Pow(t, 1.0 / 3.0);
        }
        else
        {
            return (kappa * t + 16) / 116;
        }
    }

    public static double LabInvf(double ft)
    {
        double e = 216.0 / 24389.0;
        double kappa = 24389.0 / 27.0;
        double ft3 = ft * ft * ft;
        if (ft3 > e)
        {
            return ft3;
        }
        else
        {
            return (116 * ft - 16) / kappa;
        }
    }

    public static uint Add(this uint background, uint foreground, double foregroundAlpha)
    {
        uint a = (uint)(foregroundAlpha * 255);
        foreground &= (a << 24) | 0x00FFFFFF;
        return Add(background, foreground);
    }

    public static uint Add(this uint background, uint foreground)
    {
        DeconstructArgb(background,
            out float bgA,
            out float bgR,
            out float bgG,
            out float bgB);
        DeconstructArgb(foreground,
            out float fgA,
            out float fgR,
            out float fgG,
            out float fgB);

        float a = fgA + (bgA * (1 - fgA));

        float r = CompositeComponent(fgR, bgR, fgA, bgA, a);
        float g = CompositeComponent(fgG, bgG, fgA, bgA, a);
        float b = CompositeComponent(fgB, bgB, fgA, bgA, a);

        return ArgbFromComponents(
            (uint)(a * 255),
            (uint)(r * 255),
            (uint)(g * 255),
            (uint)(b * 255));
    }

    public static float CompositeComponent(float fgC, float bgC, float fgA, float bgA, float a)
    {
        if (a == 0) return 0;
        return ((fgC * fgA) + (bgC * bgA * (1 - fgA))) / a;
    }

    public static void DeconstructArgb(
        uint argb,
        out float a,
        out float r,
        out float g,
        out float b)
    {
        a = AlphaFromArgb(argb) / 255f;
        r = RedFromArgb(argb) / 255f;
        g = GreenFromArgb(argb) / 255f;
        b = BlueFromArgb(argb) / 255f;
    }
}

public static class MathUtils
{
    /// <summary>The linear interpolation function.</summary>
    /// <returns>
    /// <paramref name="start"/> if <paramref name="amount"/> = 0 and <paramref name="stop"/> if <paramref name="amount"/> = 1
    /// </returns>
    public static double Lerp(double start, double stop, double amount)
    {
        return (1.0 - amount) * start + amount * stop;
    }

    /// <summary>Clamps an integer between two integers.</summary>
    /// <returns>
    /// <paramref name="input"/> when <paramref name="min"/> ≤ <paramref name="input"/> ≤ <paramref name="max"/>,
    /// and either <paramref name="min"/> or <paramref name="max"/> otherwise.
    /// </returns>
    public static int ClampInt(int min, int max, int input)
    {
        if (input < min)
            return min;
        if (input > max)
            return max;
        return input;
    }

    /// <summary>Clamps an integer between two floating-point numbers.</summary>
    /// <returns>
    /// <paramref name="input"/> when <paramref name="min"/> ≤ <paramref name="input"/> ≤ <paramref name="max"/>,
    /// and either <paramref name="min"/> or <paramref name="max"/> otherwise.
    /// </returns>
    public static double ClampDouble(double min, double max, double input)
    {
        if (input < min)
            return min;
        if (input > max)
            return max;
        return input;
    }

    /// <summary>Sanitizes a degree measure as an integer.</summary>
    /// <returns>A degree measure between 0 (inclusive) and 360 (exclusive).</returns>
    public static uint SanitizeDegreesInt(int degrees)
    {
        degrees %= 360;
        if (degrees < 0)
        {
            degrees += 360;
        }
        return (uint)degrees;
    }

    /// <summary>Sanitizes a degree measure as a floating-point number.</summary>
    /// <returns>A degree measure between 0.0 (inclusive) and 360.0 (exclusive).</returns>
    public static double SanitizeDegreesDouble(double degrees)
    {
        degrees %= 360.0;
        if (degrees < 0)
        {
            degrees += 360.0;
        }
        return degrees;
    }

    /// <summary>
    /// Sign of direction change needed to travel from one angle to another.
    /// </summary>
    /// <param name="from">The angle travel starts from, in degrees.</param>
    /// <param name="to">The angle travel ends at, in degrees.</param>
    /// <returns>
    /// -1 if decreasing <paramref name="from"/> leads to the shortest travel distance, 1 if increasing <paramref name="from"/> leads
    /// to the shortest travel distance.
    /// </returns>
    public static double RotationDirection(double from, double to)
    {
        double increasingDifference = SanitizeDegreesDouble(to - from);
        return increasingDifference <= 180.0 ? 1.0 : -1.0;
    }

    /// <summary>Distance of two points on a circle, represented using degrees.</summary>
    public static double DifferenceDegrees(double a, double b)
    {
        return 180.0 - Math.Abs(Math.Abs(a - b) - 180.0);
    }

    /// <summary>Multiplies a 1x3 row vector with a 3x3 matrix.</summary>
    public static double[] MatrixMultiply(double[] row, double[][] matrix)
    {
        double a = row[0] * matrix[0][0] + row[1] * matrix[0][1] + row[2] * matrix[0][2];
        double b = row[0] * matrix[1][0] + row[1] * matrix[1][1] + row[2] * matrix[1][2];
        double c = row[0] * matrix[2][0] + row[1] * matrix[2][1] + row[2] * matrix[2][2];
        return new double[] { a, b, c };
    }

    public static double ToRadians(double angdeg) => Math.PI / 180 * angdeg;

    public static double Hypot(double x, double y) => Math.Sqrt(x * x + y * y);

    public static double Log1p(double x) => Math.Log(1 + x);

    public static double Expm1(double x) => Math.Exp(x) - 1;

    /// <summary>
    /// Given a seed hue, and a mapping of hues to hue rotations, find which hues in the mapping the seed hue falls
    /// between, and add the hue rotation of the lower hue to the seed hue.
    /// </summary>
    /// <param name="seedHue">Hue of the seed color</param>
    /// <param name="hueAndRotations">
    /// List of pairs, where the first item in a pair is a hue, and the second item in
    /// the pair is a hue rotation that should be applied</param>
    /// <returns></returns>
    public static double RotateHue(double seedHue, params (int Hue, int Rotation)[] hueAndRotations)
    {
        for (int i = 0; i < hueAndRotations.Length - 1; i++)
        {
            double thisHue = hueAndRotations[i].Hue;
            double nextHue = hueAndRotations[i + 1].Hue;
            if (thisHue <= seedHue && seedHue < nextHue)
                return SanitizeDegreesDouble(seedHue + hueAndRotations[i].Rotation);
        }

        return seedHue;
    }
}

public class ViewingConditions
{
    /// <summary>
    /// sRGB-like viewing conditions.
    /// </summary>
    public static ViewingConditions Default { get; } = Make(
        new[]
        {
             ColorUtils.WhitePointD65[0],
             ColorUtils.WhitePointD65[1],
             ColorUtils.WhitePointD65[2]
        },
        200.0 / Math.PI * ColorUtils.YFromLstar(50.0) / 100.0,
        50.0,
        2.0,
        false);

    public double N { get; }
    public double Aw { get; }
    public double Nbb { get; }
    public double Ncb { get; }
    public double C { get; }
    public double Nc { get; }
    public double[] RgbD { get; }
    public double Fl { get; }
    public double FlRoot { get; }
    public double Z { get; }

    public static ViewingConditions Make(
        double[] whitePoint,
        double adaptingLuminance,
        double backgroundLstar,
        double surround,
        bool discountingIlluminant)
    {
        // Transform white point XYZ to 'cone'/'rgb' responses
        double[][] matrix = Cam16.XyzToCam16Rgb;
        double[] xyz = whitePoint;
        double rW = (xyz[0] * matrix[0][0]) + (xyz[1] * matrix[0][1]) + (xyz[2] * matrix[0][2]);
        double gW = (xyz[0] * matrix[1][0]) + (xyz[1] * matrix[1][1]) + (xyz[2] * matrix[1][2]);
        double bW = (xyz[0] * matrix[2][0]) + (xyz[1] * matrix[2][1]) + (xyz[2] * matrix[2][2]);
        double f = 0.8 + (surround / 10.0);
        double c =
            (f >= 0.9)
                ? MathUtils.Lerp(0.59, 0.69, ((f - 0.9) * 10.0))
                : MathUtils.Lerp(0.525, 0.59, ((f - 0.8) * 10.0));
        double d =
            discountingIlluminant
                ? 1.0
                : f * (1.0 - ((1.0 / 3.6) * Math.Exp((-adaptingLuminance - 42.0) / 92.0)));
        d = (d > 1.0) ? 1.0 : (d < 0.0) ? 0.0 : d;
        double nc = f;
        double[] rgbD =
            new double[] {
          d * (100.0 / rW) + 1.0 - d,
                d * (100.0 / gW) + 1.0 - d,
                d * (100.0 / bW) + 1.0 - d
            };
        double k = 1.0 / (5.0 * adaptingLuminance + 1.0);
        double k4 = k * k * k * k;
        double k4F = 1.0 - k4;
        double fl =
            (k4 * adaptingLuminance) + (0.1 * k4F * k4F * Math.Pow(5.0 * adaptingLuminance, 1.0 / 3.0));
        double n = (ColorUtils.YFromLstar(backgroundLstar) / whitePoint[1]);
        double z = 1.48 + Math.Sqrt(n);
        double nbb = 0.725 / Math.Pow(n, 0.2);
        double ncb = nbb;
        double[] rgbAFactors = new double[]
        {
             Math.Pow(fl * rgbD[0] * rW / 100.0, 0.42),
             Math.Pow(fl * rgbD[1] * gW / 100.0, 0.42),
             Math.Pow(fl * rgbD[2] * bW / 100.0, 0.42)
        };

        double[] rgbA = new[]
        {
            (400.0 * rgbAFactors[0]) / (rgbAFactors[0] + 27.13),
            (400.0 * rgbAFactors[1]) / (rgbAFactors[1] + 27.13),
            (400.0 * rgbAFactors[2]) / (rgbAFactors[2] + 27.13)
        };

        double aw = ((2.0 * rgbA[0]) + rgbA[1] + (0.05 * rgbA[2])) * nbb;
        return new ViewingConditions(n, aw, nbb, ncb, c, nc, rgbD, fl, Math.Pow(fl, 0.25), z);
    }

    /// <summary>
    /// Parameters are intermediate values of the CAM16 conversion process.
    /// Their names are shorthand for technical color science terminology,
    /// this class would not benefit from documenting them individually.
    /// A brief overview is available in the CAM16 specification,
    /// and a complete overview requires a color science textbook,
    /// osuch as Fairchild's Color Appearance Models.
    /// </summary>
    public ViewingConditions(
        double n,
        double aw,
        double nbb,
        double ncb,
        double c,
        double nc,
        double[] rgbD,
        double fl,
        double flRoot,
        double z)
    {
        N = n;
        Aw = aw;
        Nbb = nbb;
        Ncb = ncb;
        C = c;
        Nc = nc;
        RgbD = rgbD;
        Fl = fl;
        FlRoot = flRoot;
        Z = z;
    }
}