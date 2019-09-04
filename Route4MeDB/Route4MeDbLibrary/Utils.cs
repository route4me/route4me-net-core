using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
