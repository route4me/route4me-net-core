using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates the process of generating a balanced route.
        /// </summary>
        public void BalancedRoute()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteName = $"Balanced Routes {DateTime.Now}",
                RouteTime = 0,
                Optimize = Optimize.Distance.Description(),
                VehicleCapacity = 9999,
                DistanceUnit = DistanceUnit.MI.Description(),
                TravelMode = TravelMode.Driving.Description(),
                RouteMaxDuration = 86400,
                DeviceType = DeviceType.Web.Description(),
                Parts = 3,
                UseMixedPickupDeliveryDemands = false,
                Balance = new Balance()
                {
                    Mode = BalanceModes.DestinationsCount.Description()
                }
            };

            var addresses = new List<Address>()
            {
                new Address()
                {
                  AddressString = "455 S 4th St, Louisville, KY 40202",
                  IsDepot = true,
                  Latitude = 38.251698,
                  Longitude = -85.757308,
                  TimeWindowStart = 28800,
                  TimeWindowEnd = 29400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                  Latitude = 38.141598,
                  Longitude = -85.793846,
                  TimeWindowStart = 29400,
                  TimeWindowEnd = 30000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1407 MCCOY, Louisville, KY, 40215",
                  Latitude = 38.202496,
                  Longitude = -85.786514,
                  TimeWindowStart = 30000,
                  TimeWindowEnd = 30600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                  Latitude = 38.178844,
                  Longitude = -85.774864,
                  TimeWindowStart = 30600,
                  TimeWindowEnd = 31200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                  Latitude = 38.248684,
                  Longitude = -85.821121,
                  TimeWindowStart = 31200,
                  TimeWindowEnd = 31800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                  Latitude = 38.251923,
                  Longitude = -85.800034,
                  TimeWindowStart = 31800,
                  TimeWindowEnd = 32400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                  Latitude = 38.176067,
                  Longitude = -85.824638,
                  TimeWindowStart = 32400,
                  TimeWindowEnd = 33000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                  Latitude = 38.179806,
                  Longitude = -85.775558,
                  TimeWindowStart = 33000,
                  TimeWindowEnd = 33600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                  Latitude = 38.259335,
                  Longitude = -85.815094,
                  TimeWindowStart = 33600,
                  TimeWindowEnd = 34200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                  Latitude = 38.179253,
                  Longitude = -85.785118,
                  TimeWindowStart = 34200,
                  TimeWindowEnd = 34800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                  Latitude = 38.162472,
                  Longitude = -85.792854,
                  TimeWindowStart = 34800,
                  TimeWindowEnd = 35400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1661 W HILL ST, Louisville, KY, 40210",
                  Latitude = 38.229584,
                  Longitude = -85.783966,
                  TimeWindowStart = 35400,
                  TimeWindowEnd = 36000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "3222 KINGSWOOD WAY, Louisville, KY, 40216",
                  Latitude = 38.210606,
                  Longitude = -85.822594,
                  TimeWindowStart = 36000,
                  TimeWindowEnd = 36600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1922 PALATKA RD, Louisville, KY, 40214",
                  Latitude = 38.153767,
                  Longitude = -85.796783,
                  TimeWindowStart = 36600,
                  TimeWindowEnd = 37200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1314 SOUTH 26TH STREET, Louisville, KY, 40210",
                  Latitude = 38.235847,
                  Longitude = -85.796852,
                  TimeWindowStart = 37200,
                  TimeWindowEnd = 37800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2135 MCCLOSKEY AVENUE, Louisville, KY, 40210",
                  Latitude = 38.218662,
                  Longitude = -85.789032,
                  TimeWindowStart = 37800,
                  TimeWindowEnd = 38400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1409 PHYLLIS AVE, Louisville, KY, 40215",
                  Latitude = 38.206154,
                  Longitude = -85.781387,
                  TimeWindowStart = 38400,
                  TimeWindowEnd = 39000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "4504 SUNFLOWER AVE, Louisville, KY, 40216",
                  Latitude = 38.187511,
                  Longitude = -85.839149,
                  TimeWindowStart = 39000,
                  TimeWindowEnd = 39600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2512 GREENWOOD AVE, Louisville, KY, 40210",
                  Latitude = 38.241405,
                  Longitude = -85.795059,
                  TimeWindowStart = 39600,
                  TimeWindowEnd = 40200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "5500 WILKE FARM AVE, Louisville, KY, 40216",
                  Latitude = 38.166065,
                  Longitude = -85.863319,
                  TimeWindowStart = 40200,
                  TimeWindowEnd = 40800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "3640 LENTZ AVE, Louisville, KY, 40215",
                  Latitude = 38.193283,
                  Longitude = -85.786201,
                  TimeWindowStart = 40800,
                  TimeWindowEnd = 41400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1020 BLUEGRASS AVE, Louisville, KY, 40215",
                  Latitude = 38.17952,
                  Longitude = -85.780037,
                  TimeWindowStart = 41400,
                  TimeWindowEnd = 42000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "123 NORTH 40TH ST, Louisville, KY, 40212",
                  Latitude = 38.26498,
                  Longitude = -85.814156,
                  TimeWindowStart = 42000,
                  TimeWindowEnd = 42600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "7315 ST ANDREWS WOODS CIRCLE UNIT 104, Louisville, KY, 40214",
                  Latitude = 38.151072,
                  Longitude = -85.802867,
                  TimeWindowStart = 42600,
                  TimeWindowEnd = 43200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "3210 POPLAR VIEW DR, Louisville, KY, 40216",
                  Latitude = 38.182594,
                  Longitude = -85.849937,
                  TimeWindowStart = 43200,
                  TimeWindowEnd = 43800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "4519 LOUANE WAY, Louisville, KY, 40216",
                  Latitude = 38.1754,
                  Longitude = -85.811447,
                  TimeWindowStart = 43800,
                  TimeWindowEnd = 44400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "6812 MANSLICK RD, Louisville, KY, 40214",
                  Latitude = 38.161839,
                  Longitude = -85.798279,
                  TimeWindowStart = 44400,
                  TimeWindowEnd = 45000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1524 HUNTOON AVENUE, Louisville, KY, 40215",
                  Latitude = 38.172031,
                  Longitude = -85.788353,
                  TimeWindowStart = 45000,
                  TimeWindowEnd = 45600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1307 LARCHMONT AVE, Louisville, KY, 40215",
                  Latitude = 38.209663,
                  Longitude = -85.779816,
                  TimeWindowStart = 45600,
                  TimeWindowEnd = 46200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "434 N 26TH STREET #2, Louisville, KY, 40212",
                  Latitude = 38.26844,
                  Longitude = -85.791962,
                  TimeWindowStart = 46200,
                  TimeWindowEnd = 46800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "678 WESTLAWN ST, Louisville, KY, 40211",
                  Latitude = 38.250397,
                  Longitude = -85.80629,
                  TimeWindowStart = 46800,
                  TimeWindowEnd = 47400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2308 W BROADWAY, Louisville, KY, 40211",
                  Latitude = 38.248882,
                  Longitude = -85.790421,
                  TimeWindowStart = 47400,
                  TimeWindowEnd = 48000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2332 WOODLAND AVE, Louisville, KY, 40210",
                  Latitude = 38.233579,
                  Longitude = -85.794257,
                  TimeWindowStart = 48000,
                  TimeWindowEnd = 48600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1706 WEST ST. CATHERINE, Louisville, KY, 40210",
                  Latitude = 38.239697,
                  Longitude = -85.783928,
                  TimeWindowStart = 48600,
                  TimeWindowEnd = 49200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1699 WATHEN LN, Louisville, KY, 40216",
                  Latitude = 38.216465,
                  Longitude = -85.792397,
                  TimeWindowStart = 49200,
                  TimeWindowEnd = 49800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2416 SUNSHINE WAY, Louisville, KY, 40216",
                  Latitude = 38.186245,
                  Longitude = -85.831787,
                  TimeWindowStart = 49800,
                  TimeWindowEnd = 50400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "6925 MANSLICK RD, Louisville, KY, 40214",
                  Latitude = 38.158466,
                  Longitude = -85.798355,
                  TimeWindowStart = 50400,
                  TimeWindowEnd = 51000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2707 7TH ST, Louisville, KY, 40215",
                  Latitude = 38.212438,
                  Longitude = -85.785082,
                  TimeWindowStart = 51000,
                  TimeWindowEnd = 51600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2014 KENDALL LN, Louisville, KY, 40216",
                  Latitude = 38.179394,
                  Longitude = -85.826668,
                  TimeWindowStart = 51600,
                  TimeWindowEnd = 52200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "612 N 39TH ST, Louisville, KY, 40212",
                  Latitude = 38.273354,
                  Longitude = -85.812012,
                  TimeWindowStart = 52200,
                  TimeWindowEnd = 52800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2215 ROWAN ST, Louisville, KY, 40212",
                  Latitude = 38.261703,
                  Longitude = -85.786781,
                  TimeWindowStart = 52800,
                  TimeWindowEnd = 53400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1826 W. KENTUCKY ST, Louisville, KY, 40210",
                  Latitude = 38.241611,
                  Longitude = -85.78653,
                  TimeWindowStart = 53400,
                  TimeWindowEnd = 54000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1810 GREGG AVE, Louisville, KY, 40210",
                  Latitude = 38.224716,
                  Longitude = -85.796211,
                  TimeWindowStart = 54000,
                  TimeWindowEnd = 54600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "4103 BURRRELL DRIVE, Louisville, KY, 40216",
                  Latitude = 38.191753,
                  Longitude = -85.825836,
                  TimeWindowStart = 54600,
                  TimeWindowEnd = 55200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "359 SOUTHWESTERN PKWY, Louisville, KY, 40212",
                  Latitude = 38.259903,
                  Longitude = -85.823463,
                  TimeWindowStart = 55200,
                  TimeWindowEnd = 55800,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "2407 W CHESTNUT ST, Louisville, KY, 40211",
                  Latitude = 38.252781,
                  Longitude = -85.792109,
                  TimeWindowStart = 55800,
                  TimeWindowEnd = 56400,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "225 S 22ND ST, Louisville, KY, 40212",
                  Latitude = 38.257616,
                  Longitude = -85.786658,
                  TimeWindowStart = 56400,
                  TimeWindowEnd = 57000,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "1404 MCCOY AVE, Louisville, KY, 40215",
                  Latitude = 38.202122,
                  Longitude = -85.786072,
                  TimeWindowStart = 57000,
                  TimeWindowEnd = 57600,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "117 FOUNT LANDING CT, Louisville, KY, 40212",
                  Latitude = 38.270061,
                  Longitude = -85.799438,
                  TimeWindowStart = 57600,
                  TimeWindowEnd = 58200,
                  Time = 300
                },
                new Address()
                {
                  AddressString = "5504 SHOREWOOD DRIVE, Louisville, KY, 40214",
                  Latitude = 38.145851,
                  Longitude = -85.7798,
                  TimeWindowStart = 58200,
                  TimeWindowEnd = 58800,
                  Time = 300
                }
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Redirect = false,
                Addresses = addresses.ToArray()
            };

            var dataObject = route4Me.RunOptimization(optimizationParameters, out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            RemoveTestOptimizations();
        }
    }
}