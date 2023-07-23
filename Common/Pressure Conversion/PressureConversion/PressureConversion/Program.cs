using static UnitConversion.UnitConversion;

namespace PressureConversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double pressure_Pa = 101325;
            var pressure_Torr = UnitConversion.UnitConversion.PressureConversion(pressure_Pa, PressureUnit.Pa, PressureUnit.Torr);
        }
    }
}