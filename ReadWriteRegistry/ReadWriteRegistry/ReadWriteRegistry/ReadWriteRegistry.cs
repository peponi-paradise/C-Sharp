using Microsoft.Win32;
using System;

namespace Registry
{
    public static class Registry
    {
        static RegistryKey BaseKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Peponi");

        public static bool AppendRegistry(string name, string value)
        {
            try
            {
                BaseKey.SetValue(name, value);
            }
            catch (Exception e)
            {
                // Write exception log
                return false;
            }
            return true;
        }

        public static bool GetRegistry(string name, out string value)
        {
            value = string.Empty;
            try
            {
                value = (string)BaseKey.GetValue(name);
            }
            catch (Exception e)
            {
                // Write exception log
                return false;
            }
            return true;
        }
    }
}