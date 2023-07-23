using static UnitConversion.UnitConversion;

namespace TemperatureConversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double temperature_Kelvin = 400;
            var converted = UnitConversion.UnitConversion.TemperatureConversion(temperature_Kelvin, TemperatureUnit.Kelvin, TemperatureUnit.Fahrenheit);
        }
    }
}