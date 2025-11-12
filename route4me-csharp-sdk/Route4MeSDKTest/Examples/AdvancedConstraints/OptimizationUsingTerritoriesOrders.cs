using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates the optimizing process of the routes 
        /// using orders from the specified territories.
        /// </summary>
        public void OptimizationUsingTerritoriesOrders()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = 46800,
                RouteName = "Single Depot, Multiple Driver - 3 Territories Order IDs",
                TravelMode = TravelMode.Driving.Description(),
                DeviceType = DeviceType.Web.Description(),
                UseMixedPickupDeliveryDemands = false
            };


            #region Advanced Constraints

            var territoryZones = GetTerritoriesWithMaxOrders(3);

            var depotOrder = route4Me.GetOrderByID(
                new OrderParameters()
                {
                    order_id = territoryZones[0].Orders[0].ToString()
                }, out _);

            if ((territoryZones?.Length ?? 0) < 3)
            {
                Console.WriteLine("Can not retrieve 3 territories with an array of the order IDs.");
            }

            var advancedConstraints1 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 3,
                AvailableTimeWindows = new List<int[]>()
                {
                    new int[] { 46800, 57600 }
                },
                Tags = new string[] { territoryZones[0].TerritoryId }
            };

            var advancedConstraints2 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 4,
                AvailableTimeWindows = new List<int[]>()
                {
                    new int[] { 46800, 61200 }
                },
                Tags = new string[] { territoryZones[1].TerritoryId }
            };

            var advancedConstraints3 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 3,
                AvailableTimeWindows = new List<int[]>()
                {
                    new int[] { 46800, 64800 }
                },
                Tags = new string[] { territoryZones[2].TerritoryId }
            };

            var advancedConstraints = new DataTypes.V5.RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2,
                advancedConstraints3
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>();

            foreach (var territoryZone in territoryZones)
            {
                foreach (int orderId in territoryZone.Orders)
                {
                    addresses.Add(new Address()
                    {
                        OrderId = orderId,
                        Tags = new string[] { territoryZone.TerritoryId }
                    });
                }
            }

            // Specify the optimization parameters
            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,

                Depots = new Address[]
                {
                    new Address()
                    {
                        AddressString = depotOrder.Address1,
                        Latitude = depotOrder.CachedLat,
                        Longitude = depotOrder.CachedLng
                    }
                },
                Addresses = addresses.ToArray()
            };

            // Send a request to the server
            var dataObject = route4Me.RunOptimization(optimizationParameters, out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject, errorString);

            //RemoveTestOptimizations();
        }
    }
}