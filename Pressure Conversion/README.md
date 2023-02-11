<h1 id="title">C# - 압력 단위 변환</h1>

<h2 id="intro">Introduction</h2>

1. SW에서 물리량을 계산할 때 압력 단위 변환이 필요한 경우 사용하면 유용하다.
2. 현재 예시에는 파스칼, 토르, 프사이 간 압력 변환만 가능하다. 다른 단위가 필요한 경우 추가할 필요가 있다.

<br><br>

<h2 id="code">Code</h2>

```csharp
using System;

namespace UnitConversion
{
    public static class UnitConversion
    {
        public enum PressureUnit
        {
            Pa,
            Torr,
            Psi,
        }

        public static T PressureConversion<T>(T before, PressureUnit previous, PressureUnit change)
        {
            double multiplier = 0;
            switch (previous)
            {
                case PressureUnit.Pa:
                    switch (change)
                    {
                        case PressureUnit.Torr:
                            multiplier = 1 / 133.322;
                            break;
                        case PressureUnit.Psi:
                            multiplier = 1 / 6894.76;
                            break;
                    }
                    break;
                case PressureUnit.Torr:
                    switch (change)
                    {
                        case PressureUnit.Pa:
                            multiplier = 133.322;
                            break;
                        case PressureUnit.Psi:
                            multiplier = 1 / 51.7149;
                            break;
                    }
                    break;
                case PressureUnit.Psi:
                    switch (change)
                    {
                        case PressureUnit.Pa:
                            multiplier = 6894.76;
                            break;
                        case PressureUnit.Torr:
                            multiplier = 51.7149;
                            break;
                    }
                    break;
            }
            return (T)Convert.ChangeType((Convert.ToDouble(before) * multiplier), typeof(T));
        }
    }
}
```

<br><br>

<h2 id="example">Use example</h2>

```csharp
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
```

<br><br>