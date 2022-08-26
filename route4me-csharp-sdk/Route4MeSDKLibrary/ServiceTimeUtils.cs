using System.Collections.Generic;

namespace Route4MeSDKLibrary
{
    public static class ServiceTimeUtils
    {
        public static long? GetServiceTimeByAddressType(string key, Dictionary<string, int> serviceTimeMap)
        {
            return serviceTimeMap.ContainsKey(key) ? serviceTimeMap[key] : (long?)null;
        }
    }
}
