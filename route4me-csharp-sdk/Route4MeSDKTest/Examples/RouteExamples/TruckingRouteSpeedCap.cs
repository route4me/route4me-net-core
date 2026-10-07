using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples;

public sealed partial class Route4MeExamples
{
    /// <summary>
    /// The example refers to the process of creating a trucking optimization
    /// with a truck speed cap.
    /// </summary>
    public void TruckingRouteSpeedCap()
    {
        var route4Me = new Route4MeManager(ActualApiKey);

        // Prepare the addresses
        var addresses = new Address[]
        {
            #region Addresses

            new Address
            {
                AddressString = "754 5th Ave New York, NY 10019",
                Alias = "Bergdorf Goodman",
                IsDepot = true,
                Latitude = 40.7636197,
                Longitude = -73.9744388,
                Time = 0
            },

            new Address
            {
                AddressString = "1011 Madison Ave New York, NY 10075",
                Alias = "Yigal Azrou'l",
                Latitude = 40.7772129,
                Longitude = -73.9669,
                Time = 0
            },

            new Address
            {
                AddressString = "440 Columbus Ave New York, NY 10024",
                Alias = "Frank Stella Clothier",
                Latitude = 40.7808364,
                Longitude = -73.9732729,
                Time = 0
            },

            new Address
            {
                AddressString = "555 W 57th St New York, NY 10019",
                Alias = "BMW of Manhattan",
                Latitude = 40.7718005,
                Longitude = -73.9897716,
                Time = 0
            },

            #endregion
        };

        // Set parameters
        var parameters = new RouteParameters
        {
            AlgorithmType = AlgorithmType.TSP,
            RouteName = "Trucking Route With Speed Cap",

            RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
            RouteTime = 60 * 60 * 7,
            RouteMaxDuration = 86400,
            VehicleCapacity = 1,
            VehicleMaxDistanceMI = 10000,

            Optimize = Optimize.Time.Description(),
            DistanceUnit = DistanceUnit.MI.Description(),
            DeviceType = DeviceType.Web.Description(),
            TravelMode = TravelMode.Trucking.Description(),

            TruckHeightMeters = 4,
            TruckLengthMeters = 12,
            TruckWidthMeters = 3,
            // Always km/h (0 to 255), even when the distance unit is miles; 80 km/h is about 50 mph
            TruckSpeedCap = 80
        };

        var optimizationParameters = new OptimizationParameters
        {
            Addresses = addresses,
            Parameters = parameters
        };

        // Run the query
        var dataObject = route4Me.RunOptimization(
            optimizationParameters,
            out string errorString);

        OptimizationsToRemove = new List<string>();

        if (!string.IsNullOrEmpty(dataObject?.OptimizationProblemId))
            OptimizationsToRemove.Add(dataObject.OptimizationProblemId);

        try
        {
            PrintExampleOptimizationResult(dataObject, errorString);

            Console.WriteLine();

            Console.WriteLine($"TruckSpeedCap: {dataObject?.Parameters?.TruckSpeedCap}");
        }
        finally
        {
            RemoveTestOptimizations();
        }
    }
}