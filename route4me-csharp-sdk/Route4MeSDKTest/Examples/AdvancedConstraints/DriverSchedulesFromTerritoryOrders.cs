using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Drivers Schedules with Orders from Territories created in Route4Me
        /// </summary>
        public void DriverSchedulesFromTerritoryOrders()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = (8 + 5) * 3600,
                Metric = Metric.Matrix,
                RouteName = "Single Depot, Multiple Driver - 3 Territories Order IDs",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                UseMixedPickupDeliveryDemands = false
            };

            var depots = new Address[]
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Latitude = 38.141598,
                    Longitude = -85.793846
                }
            };

            string terrId1 = "A34BA30C717D1194FC0230252DF0C45C";
            string terrId2 = "5D418084790C59C2D42B1C067094A459";
            string terrId3 = "0B40480FD15C5F85A26AD759007834F4";

            //routeParameters.Depots = depots;

            var territory1 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = terrId1,
                    Orders = 1
                },
                out _
            );

            var territory2 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = terrId2,
                    Orders = 1
                },
                out _
            );

            var territory3 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = terrId3,
                    Orders = 1
                },
                out _
            );

            #region Get orders from territories

            var addresses = new List<Address>();

            foreach (var orderID in territory1.Orders)
            {
                addresses.Add(
                    new Address()
                    {
                        OrderId = orderID,
                        Tags = new string[] { territory1.TerritoryId }
                    }
                );
            }

            foreach (var orderID in territory2.Orders)
            {
                addresses.Add(
                    new Address()
                    {
                        OrderId = orderID,
                        Tags = new string[] { territory2.TerritoryId }
                    }
                );
            }

            foreach (var orderID in territory3.Orders)
            {
                addresses.Add(
                    new Address()
                    {
                        OrderId = orderID,
                        Tags = new string[] { territory3.TerritoryId }
                    }
                );
            }

            #endregion

            #region Advanced Constraints

            var advancedConstraints = new List<DataTypes.V5.RouteAdvancedConstraints>();

            if ((territory1.Orders?.Length ?? 0) > 0)
            {
                advancedConstraints.Add(
                    new DataTypes.V5.RouteAdvancedConstraints()
                    {
                        MembersCount = 3,
                        AvailableTimeWindows = new List<int[]>
                        {
                            new int[] { 46800, 57600 }
                        },
                        Tags = new string[] { terrId1 }
                    }
                );
            }

            if ((territory2.Orders?.Length ?? 0) > 0)
            {
                advancedConstraints.Add(
                    new DataTypes.V5.RouteAdvancedConstraints()
                    {
                        MembersCount = 4,
                        AvailableTimeWindows = new List<int[]>
                        {
                            new int[] { 46800, 61200 }
                        },
                        Tags = new string[] { terrId2 }
                    }
                );
            }

            if ((territory3.Orders?.Length ?? 0) > 0)
            {
                advancedConstraints.Add(
                    new DataTypes.V5.RouteAdvancedConstraints()
                    {
                        MembersCount = 3,
                        AvailableTimeWindows = new List<int[]>
                        {
                            new int[] { 46800, 64800 }
                        },
                        Tags = new string[] { terrId3 }
                    }
                );
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray(),
                Depots = depots
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