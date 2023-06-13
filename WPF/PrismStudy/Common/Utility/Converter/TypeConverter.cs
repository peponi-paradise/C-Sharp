using System;

namespace Common.Utility.Converter;

public static class TypeConverter
{
    public static T? To<T>(object input) => To(input, default(T));

    private static T? To<T>(object input, T defaultValue)
    {
        var result = defaultValue;
        try
        {
            if (input == null || input == DBNull.Value) return result;

            if (typeof(T).IsEnum) result = (T)Enum.ToObject(typeof(T), To(input, Convert.ToInt32(defaultValue)));
            else result = (T)Convert.ChangeType(input, typeof(T));
        }
        catch
        {
        }

        return result;
    }
}