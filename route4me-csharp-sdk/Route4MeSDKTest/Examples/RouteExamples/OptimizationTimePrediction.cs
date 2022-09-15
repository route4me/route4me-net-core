using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Time prediction of an optimization action.
        /// an optimization with round trip option.
        /// </summary>
        public void OptimizationTimePrediction()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            Address[] addresses = new Address[]
            {
                #region Addresses

                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0
                },

                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300
                },

                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteName = $"Single Depot, Multiple Driver {DateTime.Now}",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 0,
                RouteMaxDuration = (long)(3600 * 0.5),
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                Parts = 20,

                RT = true, // if true, a round trip route is generated.

                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            var optimizationPrediction = route4Me.GetOptimizationPrediction(
                optimizationParameters,
                out string errorString);
            // Output the result
            foreach (var predict1 in optimizationPrediction.TimePrediction)
            {
                Console.WriteLine($"Model: {predict1.Model}, Time: {predict1.Value}, Unit: {predict1.Unit}");
            }
        }
    }
}