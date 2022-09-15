using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating an optimization 
        /// with multi-depot, multi-driver options and capcity constraint.
        /// </summary>
        public void MultipleDepotMultipleDriverCapacity()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the addresses
            var addresses = new Address[]
            {
                #region Addresses

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
                    Time = 300,
                    Weight = 4797.0,
                    Cube = 627.0
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    Weight = 3836.0,
                    Cube = 711.0
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300,
                    Weight = 2470.0,
                    Cube = 978.0
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    Weight = 3922.0,
                    Cube = 353.0
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300,
                    Weight = 2879.0,
                    Cube = 591.0
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    Weight = 4437.0,
                    Cube = 702.0
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300,
                    Weight = 3369.0,
                    Cube = 707.0
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    Weight = 4051.0,
                    Cube = 295.0
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300,
                    Weight = 4775.0,
                    Cube = 754.0
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300,
                    Weight = 1693.0,
                    Cube = 660.0
                },
                new Address()
                {
                    AddressString = "1661 W HILL ST, Louisville, KY, 40210",
                    Latitude = 38.229584,
                    Longitude = -85.783966,
                    Time = 300,
                    Weight = 2204.0,
                    Cube = 143.0
                },
                new Address()
                {
                    AddressString = "3222 KINGSWOOD WAY, Louisville, KY, 40216",
                    Latitude = 38.210606,
                    Longitude = -85.822594,
                    Time = 300,
                    Weight = 2336.0,
                    Cube = 809.0
                },
                new Address()
                {
                    AddressString = "1922 PALATKA RD, Louisville, KY, 40214",
                    Latitude = 38.153767,
                    Longitude = -85.796783,
                    Time = 300,
                    Weight = 4840.0,
                    Cube = 974.0
                },
                new Address()
                {
                    AddressString = "1314 SOUTH 26TH STREET, Louisville, KY, 40210",
                    Latitude = 38.235847,
                    Longitude = -85.796852,
                    Time = 300,
                    Weight = 3314.0,
                    Cube = 414.0
                },
                new Address()
                {
                    AddressString = "2135 MCCLOSKEY AVENUE, Louisville, KY, 40210",
                    Latitude = 38.218662,
                    Longitude = -85.789032,
                    Time = 300,
                    Weight = 4281.0,
                    Cube = 176.0
                },
                new Address()
                {
                    AddressString = "1409 PHYLLIS AVE, Louisville, KY, 40215",
                    Latitude = 38.206154,
                    Longitude = -85.781387,
                    Time = 300,
                    Weight = 2804.0,
                    Cube = 792.0
                },
                new Address()
                {
                    AddressString = "4504 SUNFLOWER AVE, Louisville, KY, 40216",
                    Latitude = 38.187511,
                    Longitude = -85.839149,
                    Time = 300,
                    Weight = 3639.0,
                    Cube = 988.0
                },
                new Address()
                {
                    AddressString = "2512 GREENWOOD AVE, Louisville, KY, 40210",
                    Latitude = 38.241405,
                    Longitude = -85.795059,
                    Time = 300,
                    Weight = 2277.0,
                    Cube = 346.0
                },
                new Address()
                {
                    AddressString = "5500 WILKE FARM AVE, Louisville, KY, 40216",
                    Latitude = 38.166065,
                    Longitude = -85.863319,
                    Time = 300,
                    Weight = 4358.0,
                    Cube = 506.0
                },
                new Address()
                {
                    AddressString = "3640 LENTZ AVE, Louisville, KY, 40215",
                    Latitude = 38.193283,
                    Longitude = -85.786201,
                    Time = 300,
                    Weight = 3100.0,
                    Cube = 430.0
                },
                new Address()
                {
                    AddressString = "1020 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.17952,
                    Longitude = -85.780037,
                    Time = 300,
                    Weight = 4658.0,
                    Cube = 956.0
                },
                new Address()
                {
                    AddressString = "123 NORTH 40TH ST, Louisville, KY, 40212",
                    Latitude = 38.26498,
                    Longitude = -85.814156,
                    Time = 300,
                    Weight = 3922.0,
                    Cube = 517.0
                },
                new Address()
                {
                    AddressString = "7315 ST ANDREWS WOODS CIRCLE UNIT 104, Louisville, KY, 40214",
                    Latitude = 38.151072,
                    Longitude = -85.802867,
                    Time = 300,
                    Weight = 4530.0,
                    Cube = 485.0
                },
                new Address()
                {
                    AddressString = "3210 POPLAR VIEW DR, Louisville, KY, 40216",
                    Latitude = 38.182594,
                    Longitude = -85.849937,
                    Time = 300,
                    Weight = 2107.0,
                    Cube = 607.0
                },
                new Address()
                {
                    AddressString = "4519 LOUANE WAY, Louisville, KY, 40216",
                    Latitude = 38.1754,
                    Longitude = -85.811447,
                    Time = 300,
                    Weight = 1360.0,
                    Cube = 167.0
                },
                new Address()
                {
                    AddressString = "6812 MANSLICK RD, Louisville, KY, 40214",
                    Latitude = 38.161839,
                    Longitude = -85.798279,
                    Time = 300,
                    Weight = 1153.0,
                    Cube = 938.0
                },
                new Address()
                {
                    AddressString = "1524 HUNTOON AVENUE, Louisville, KY, 40215",
                    Latitude = 38.172031,
                    Longitude = -85.788353,
                    Time = 300,
                    Weight = 1578.0,
                    Cube = 480.0
                },
                new Address()
                {
                    AddressString = "1307 LARCHMONT AVE, Louisville, KY, 40215",
                    Latitude = 38.209663,
                    Longitude = -85.779816,
                    Time = 300,
                    Weight = 2936.0,
                    Cube = 791.0
                },
                new Address()
                {
                    AddressString = "434 N 26TH STREET #2, Louisville, KY, 40212",
                    Latitude = 38.26844,
                    Longitude = -85.791962,
                    Time = 300,
                    Weight = 3085.0,
                    Cube = 603.0
                },
                new Address()
                {
                    AddressString = "678 WESTLAWN ST, Louisville, KY, 40211",
                    Latitude = 38.250397,
                    Longitude = -85.80629,
                    Time = 300,
                    Weight = 2538.0,
                    Cube = 600.0
                },
                new Address()
                {
                    AddressString = "2308 W BROADWAY, Louisville, KY, 40211",
                    Latitude = 38.248882,
                    Longitude = -85.790421,
                    Time = 300,
                    Weight = 4804.0,
                    Cube = 971.0
                },
                new Address()
                {
                    AddressString = "2332 WOODLAND AVE, Louisville, KY, 40210",
                    Latitude = 38.233579,
                    Longitude = -85.794257,
                    Time = 300,
                    Weight = 4565.0,
                    Cube = 843.0
                },
                new Address()
                {
                    AddressString = "1706 WEST ST. CATHERINE, Louisville, KY, 40210",
                    Latitude = 38.239697,
                    Longitude = -85.783928,
                    Time = 300,
                    Weight = 1315.0,
                    Cube = 202.0
                },
                new Address()
                {
                    AddressString = "1699 WATHEN LN, Louisville, KY, 40216",
                    Latitude = 38.216465,
                    Longitude = -85.792397,
                    Time = 300,
                    Weight = 3045.0,
                    Cube = 828.0
                },
                new Address()
                {
                    AddressString = "2416 SUNSHINE WAY, Louisville, KY, 40216",
                    Latitude = 38.186245,
                    Longitude = -85.831787,
                    Time = 300,
                    Weight = 2355.0,
                    Cube = 663.0
                },
                new Address()
                {
                    AddressString = "6925 MANSLICK RD, Louisville, KY, 40214",
                    Latitude = 38.158466,
                    Longitude = -85.798355,
                    Time = 300,
                    Weight = 4374.0,
                    Cube = 882.0
                },
                new Address()
                {
                    AddressString = "2707 7TH ST, Louisville, KY, 40215",
                    Latitude = 38.212438,
                    Longitude = -85.785082,
                    Time = 300,
                    Weight = 2829.0,
                    Cube = 304.0
                },
                new Address()
                {
                    AddressString = "2014 KENDALL LN, Louisville, KY, 40216",
                    Latitude = 38.179394,
                    Longitude = -85.826668,
                    Time = 300,
                    Weight = 4210.0,
                    Cube = 501.0
                },
                new Address()
                {
                    AddressString = "612 N 39TH ST, Louisville, KY, 40212",
                    Latitude = 38.273354,
                    Longitude = -85.812012,
                    Time = 300,
                    Weight = 3077.0,
                    Cube = 937.0
                },
                new Address()
                {
                    AddressString = "2215 ROWAN ST, Louisville, KY, 40212",
                    Latitude = 38.261703,
                    Longitude = -85.786781,
                    Time = 300,
                    Weight = 2874.0,
                    Cube = 337.0
                },
                new Address()
                {
                    AddressString = "1826 W. KENTUCKY ST, Louisville, KY, 40210",
                    Latitude = 38.241611,
                    Longitude = -85.78653,
                    Time = 300,
                    Weight = 1238.0,
                    Cube = 90.0
                },
                new Address()
                {
                    AddressString = "1810 GREGG AVE, Louisville, KY, 40210",
                    Latitude = 38.224716,
                    Longitude = -85.796211,
                    Time = 300,
                    Weight = 3473.0,
                    Cube = 716.0
                },
                new Address()
                {
                    AddressString = "4103 BURRRELL DRIVE, Louisville, KY, 40216",
                    Latitude = 38.191753,
                    Longitude = -85.825836,
                    Time = 300,
                    Weight = 1880.0,
                    Cube = 134.0
                },
                new Address()
                {
                    AddressString = "359 SOUTHWESTERN PKWY, Louisville, KY, 40212",
                    Latitude = 38.259903,
                    Longitude = -85.823463,
                    Time = 300,
                    Weight = 2857.0,
                    Cube = 415.0
                },
                new Address()
                {
                    AddressString = "2407 W CHESTNUT ST, Louisville, KY, 40211",
                    Latitude = 38.252781,
                    Longitude = -85.792109,
                    Time = 300,
                    Weight = 3452.0,
                    Cube = 109.0
                },
                new Address()
                {
                    AddressString = "225 S 22ND ST, Louisville, KY, 40212",
                    Latitude = 38.257616,
                    Longitude = -85.786658,
                    Time = 300,
                    Weight = 1469.0,
                    Cube = 170.0
                },
                new Address()
                {
                    AddressString = "1404 MCCOY AVE, Louisville, KY, 40215",
                    Latitude = 38.202122,
                    Longitude = -85.786072,
                    Time = 300,
                    Weight = 1664.0,
                    Cube = 846.0
                },
                new Address()
                {
                    AddressString = "117 FOUNT LANDING CT, Louisville, KY, 40212",
                    Latitude = 38.270061,
                    Longitude = -85.799438,
                    Time = 300,
                    Weight = 4260.0,
                    Cube = 47.0
                },
                new Address()
                {
                    AddressString = "5504 SHOREWOOD DRIVE, Louisville, KY, 40214",
                    Latitude = 38.145851,
                    Longitude = -85.7798,
                    Time = 300,
                    Weight = 4559.0,
                    Cube = 683.0
                }

                #endregion
            };

            // Set parameters
            var parameters = new RouteParameters()
            {
                RouteName = "Multiple Depot, Multiple Driver Capacity Constraints",
                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 13 * 60 * 60,
                Optimize = Optimize.Distance.Description(),
                VehicleCapacity = 100,
                VehicleMaxCargoWeight = 40000.0,
                VehicleMaxCargoVolume = 5000.0,
                DistanceUnit = DistanceUnit.MI.Description(),
                TravelMode = TravelMode.Driving.Description(),
                RouteMaxDuration = 24 * 60 * 60,
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                DeviceType = DeviceType.Web.Description(),
                Parts = 20,
                UseMixedPickupDeliveryDemands = false
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses,
                Parameters = parameters
            };

            // Run the query
            DataObject dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out string errorString);

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