using System.Collections.Generic;
using System.Linq;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void OptimizationWithProfile()
        {
            var optimizationProfileManagerV5 = new OptimizationProfileManagerV5(ActualApiKey);

            var optimizationProfiles = optimizationProfileManagerV5.GetOptimizationProfiles(out _);

            var optimizationProfile = optimizationProfiles.Items.FirstOrDefault();

            if (optimizationProfile != null)
            {
                var addresses = new List<Address>()
                {
                    #region Addresses

                    new Address()
                    {
                        AddressString = "754 5th Ave New York, NY 10019",
                        Alias = "Bergdorf Goodman",
                        IsDepot = true,
                        Latitude = 40.7636197,
                        Longitude = -73.9744388,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "717 5th Ave New York, NY 10022",
                        Alias = "Giorgio Armani",
                        Latitude = 40.7669692,
                        Longitude = -73.9693864,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "888 Madison Ave New York, NY 10014",
                        Alias = "Ralph Lauren Women's and Home",
                        Latitude = 40.7715154,
                        Longitude = -73.9669241,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "1011 Madison Ave New York, NY 10075",
                        Alias = "Yigal Azrou'l",
                        Latitude = 40.7772129,
                        Longitude = -73.9669,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "440 Columbus Ave New York, NY 10024",
                        Alias = "Frank Stella Clothier",
                        Latitude = 40.7808364,
                        Longitude = -73.9732729,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "324 Columbus Ave #1 New York, NY 10023",
                        Alias = "Liana",
                        Latitude = 40.7803123,
                        Longitude = -73.9793079,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "110 W End Ave New York, NY 10023",
                        Alias = "Toga Bike Shop",
                        Latitude = 40.7753077,
                        Longitude = -73.9861529,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "555 W 57th St New York, NY 10019",
                        Alias = "BMW of Manhattan",
                        Latitude = 40.7718005,
                        Longitude = -73.9897716,
                        Time = 0
                    },

                    new Address()
                    {
                        AddressString = "57 W 57th St New York, NY 10019",
                        Alias = "Verizon Wireless",
                        Latitude = 40.7558695,
                        Longitude = -73.9862019,
                        Time = 0
                    },

                    #endregion
                };

                var parameters = new RouteParameters()
                {
                   RouteStartDateLocal = "2024-08-16"
                };

                var optimizationParameters = new OptimizationParameters()
                {
                    Addresses = addresses.ToArray(),

                    Parameters = parameters,

                    OptimizationProfileId = optimizationProfile.Id
                };

                var route4Me = new Route4MeManager(ActualApiKey);

                var dataObject = route4Me.RunOptimization(
                    optimizationParameters,
                    out string errorString);

                PrintExampleOptimizationResult(dataObject, errorString);
            }
        }
    }
}