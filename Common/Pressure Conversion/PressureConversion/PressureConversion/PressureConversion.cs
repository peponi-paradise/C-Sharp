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