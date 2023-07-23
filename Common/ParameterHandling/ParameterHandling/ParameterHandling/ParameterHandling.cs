using System;

namespace ParameterHandling
{
    public static class ParameterHandling
    {
        public static void CopyAllFieldsAndProperties<T>(in T willCopied, in T dataBase)
        {
            foreach (var fieldInfo in willCopied.GetType().GetFields())
            {
                foreach (var dataInfo in dataBase.GetType().GetFields())
                {
                    if (fieldInfo.Name == dataInfo.Name && fieldInfo.FieldType == dataInfo.FieldType) fieldInfo.SetValue(willCopied, dataInfo.GetValue(dataBase));
                }
            }
            foreach (var propertyInfo in willCopied.GetType().GetProperties())
            {
                foreach (var dataInfo in dataBase.GetType().GetProperties())
                {
                    if (propertyInfo.Name == dataInfo.Name && propertyInfo.PropertyType == dataInfo.PropertyType) propertyInfo.SetValue(willCopied, dataInfo.GetValue(dataBase));
                }
            }
        }

        public static bool GetParameter<T>(string paramName, ref T paramValue, object dataBase)
        {
            foreach (var fieldInfo in dataBase.GetType().GetFields())
            {
                if (fieldInfo.Name == paramName && fieldInfo.FieldType == typeof(T))
                {
                    paramValue = (T)Convert.ChangeType(fieldInfo.GetValue(dataBase), typeof(T));
                    return true;
                }
            }
            foreach (var propertyInfo in dataBase.GetType().GetProperties())
            {
                if (propertyInfo.Name == paramName && propertyInfo.PropertyType == typeof(T))
                {
                    paramValue = (T)Convert.ChangeType(propertyInfo.GetValue(dataBase), typeof(T));
                    return true;
                }
            }
            return false;
        }

        public static bool SetParameter<T>(string paramName, T paramValue, object dataBase)
        {
            foreach (var fieldInfo in dataBase.GetType().GetFields())
            {
                if (fieldInfo.Name == paramName && fieldInfo.FieldType == typeof(T))
                {
                    fieldInfo.SetValue(dataBase, paramValue);
                    return true;
                }
            }
            foreach (var propertyInfo in dataBase.GetType().GetProperties())
            {
                if (propertyInfo.Name == paramName && propertyInfo.PropertyType == typeof(T))
                {
                    propertyInfo.SetValue(dataBase, paramValue);
                    return true;
                }
            }
            return false;
        }
    }
}