using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Route4MeDbLibrary
{
    public static class Utils
    {
        public static string GetRoute4MeDbBaseDir(string curPath)
        {
            var dirinfo = new DirectoryInfo(curPath);

            while (dirinfo.Name != "Route4MeDB")
            {
                curPath = dirinfo.Parent.FullName;
                dirinfo = new DirectoryInfo(curPath);
            }

            return curPath;
        }

        public static long ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if (date < origin) date = new DateTime(1970, 1, 1, date.Hour, date.Minute, date.Second);
            TimeSpan diff = date - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

        public static string Description(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetDescription();
        }

        /// <summary>
        /// Converts a standard type object to standard property type 
        /// (e.g. if first is Long type, second: Int32, converts to Int32)
        /// </summary>
        /// <param name="convertObject">An object to be converted to the target object type</param>
        /// <param name="targetProperty">A property with the standard type</param>
        /// <returns>Converted object to the target standard type</returns>
        public static object ConvertObjectToPropertyType(object value, PropertyInfo targetProperty)
        {
            Type destinationType = targetProperty?.PropertyType ?? null;

            if (Nullable.GetUnderlyingType(targetProperty.PropertyType) != null) destinationType = Nullable.GetUnderlyingType(targetProperty.PropertyType);

            Type convertObjectType = value?.GetType() ?? null;

            if (destinationType == null || convertObjectType == null) return null;

            // Non-standard object isn't converted
            if (targetProperty.PropertyType == typeof(object)) return null;

            object result = null;

            if (value is IConvertible)
            {
                try
                {
                    if (destinationType == typeof(Boolean))
                    {
                        result = ((IConvertible)value).ToBoolean(CultureInfo.CurrentCulture);
                    } 
                    else if (destinationType == typeof(Byte))
                    {
                        result = ((IConvertible)value).ToByte(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Char))
                    {
                        result = ((IConvertible)value).ToChar(CultureInfo.CurrentCulture);
                    } 
                    else if (destinationType == typeof(DateTime))
                    {
                        result = ((IConvertible)value).ToDateTime(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Decimal))
                    {
                        result = ((IConvertible)value).ToDecimal(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Double))
                    {
                        result = ((IConvertible)value).ToDouble(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Int16))
                    {
                        result = ((IConvertible)value).ToInt16(CultureInfo.CurrentCulture);
                        return true;
                    }
                    else if (destinationType == typeof(Int32))
                    {
                        result = ((IConvertible)value).ToInt32(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Int64))
                    {
                        result = ((IConvertible)value).ToInt64(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(SByte))
                    {
                        result = ((IConvertible)value).ToSByte(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(Single))
                    {
                        result = ((IConvertible)value).ToSingle(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt16))
                    {
                        result = ((IConvertible)value).ToUInt16(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt32))
                    {
                        result = ((IConvertible)value).ToUInt32(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(UInt64))
                    {
                        result = ((IConvertible)value).ToUInt64(CultureInfo.CurrentCulture);
                    }
                    else if (destinationType == typeof(String))
                    {
                        result = ((IConvertible)value).ToString(CultureInfo.CurrentCulture);
                    }
                }
                catch
                {
                    return result;
                }
            }

            return result;
        }
    }
}
