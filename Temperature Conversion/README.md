<h1 id="title">C# - 온도 단위 변환</h1>

<h2 id="intro">Introduction</h2>

1. SW에서 물리량을 계산할 때 온도 단위 변환이 필요한 경우 사용하면 유용하다.
2. [C# - 압력 단위 변환](https://peponi-paradise.tistory.com/entry/C-%EC%95%95%EB%A0%A5-%EB%8B%A8%EC%9C%84-%EB%B3%80%ED%99%98)과 함께 한 클래스에 묶어 사용 가능하다.
3. 압력 변환과는 달리, 온도 변환은 개별 method로 정리하였다.

<br><br>

<h2 id="code">Code</h2>

```csharp
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
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
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
```

<br><br>