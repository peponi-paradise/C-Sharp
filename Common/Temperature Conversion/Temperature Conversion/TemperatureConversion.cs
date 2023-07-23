using System;

namespace UnitConversion
{
    public static class UnitConversion
    {
        public enum TemperatureUnit
        {
            Kelvin,
            Celsius,
            Fahrenheit,
        }

        public static T TemperatureConversion<T>(T value, TemperatureUnit previous, TemperatureUnit change)
        {
            switch (previous)
            {
                case TemperatureUnit.Kelvin:
                    switch (change)
                    {
                        case TemperatureUnit.Celsius:
                            return Temperature_K_To_Celsius(value);
                        case TemperatureUnit.Fahrenheit:
                            return Temperature_K_To_Fahrenheit(value);
                        default:
                            return value;
                    }
                case TemperatureUnit.Celsius:
                    switch (change)
                    {
                        case TemperatureUnit.Kelvin:
                            return Temperature_Celsius_To_K(value);
                        case TemperatureUnit.Fahrenheit:
                            return Temperature_Celsius_To_Fahrenheit(value);
                        default:
                            return value;
                    }
                case TemperatureUnit.Fahrenheit:
                    switch (change)
                    {
                        case TemperatureUnit.Kelvin:
                            return Temperature_Fahrenheit_To_K(value);
                        case TemperatureUnit.Celsius:
                            return Temperature_Fahrenheit_To_Celsius(value);
                        default:
                            return value;
                    }
                default:
                    return value;
            }
        }

        static T Temperature_K_To_Celsius<T>(T value) => (T)Convert.ChangeType((Convert.ToDouble(value) - 273.15), typeof(T));

        static T Temperature_K_To_Fahrenheit<T>(T value) => (T)Convert.ChangeType((Convert.ToDouble(Temperature_K_To_Celsius(value)) * 9 / 5 + 32), typeof(T));

        static T Temperature_Celsius_To_K<T>(T value) => (T)Convert.ChangeType((Convert.ToDouble(value) + 273.15), typeof(T));

        static T Temperature_Celsius_To_Fahrenheit<T>(T value) => (T)Convert.ChangeType((Convert.ToDouble(value) * 9 / 5 + 32), typeof(T));

        static T Temperature_Fahrenheit_To_Celsius<T>(T value) => (T)Convert.ChangeType((Convert.ToDouble(value) - 32) * 5 / 9, typeof(T));

        static T Temperature_Fahrenheit_To_K<T>(T value) => (T)Convert.ChangeType(Convert.ToDouble(Temperature_Fahrenheit_To_Celsius(value)) + 273.15, typeof(T));
    }
}