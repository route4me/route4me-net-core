using System;
using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.FastProcessing;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {

        public void uploaLargeContactsCsvFile()
        {
            var fastProcessing = new FastBulkGeocoding(ActualApiKey, false)
            {
                ChankPause = 1000,
                CsvChankSize = 500
            };
            //var lsGeocodedAddressTotal = new List<AddressGeocoded>();
            var lsAddresses = new List<string>();

            var ab = new AddressBookContact();

            var csvAddressMapping = new Dictionary<string, string>()
            {
                {"Alias", R4MeUtils.GetPropertyName(() => ab.AddressAlias)},
                {"Address", R4MeUtils.GetPropertyName(() => ab.Address1)},
                {"City", R4MeUtils.GetPropertyName(() => ab.AddressCity)},
                {"State", R4MeUtils.GetPropertyName(() => ab.AddressStateId)},
                {"Zip", R4MeUtils.GetPropertyName(() => ab.AddressZip)},
                {"Lat", R4MeUtils.GetPropertyName(() => ab.CachedLat)},
                {"Lng", R4MeUtils.GetPropertyName(() => ab.CachedLng)},
                {"Time", R4MeUtils.GetPropertyName(() => ab.ServiceTime)},
                {"Time_window_start", R4MeUtils.GetPropertyName(() => ab.LocalTimeWindowStart)},
                {"Time_window_end", R4MeUtils.GetPropertyName(() => ab.LocalTimeWindowEnd)}
            };

            FastFileReading.csvAddressMapping = csvAddressMapping;

            Console.WriteLine("Start: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            var stPath = AppDomain.CurrentDomain.BaseDirectory;
            fastProcessing.uploadLargeContactsCsvFile(stPath + @"Data\CSV\30k_geocoded.csv", out string errorString);

            Console.WriteLine("End: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        }

    }
}
