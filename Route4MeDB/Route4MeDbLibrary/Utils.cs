using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
