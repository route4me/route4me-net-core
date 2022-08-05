using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example demonstrates the optimizing process of the routes 
        /// using orders from the specified territories and different depots.
        /// </summary>
        public void OptimizationUsingTerritoriesOrdersDifferentDepot()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = 46800,
                RouteName = "Multiple Depot, Multiple Driver - 3 Territories Order IDs",
                TravelMode = TravelMode.Driving.Description(),
                DeviceType = DeviceType.Web.Description(),
                UseMixedPickupDeliveryDemands = false
            };


            #region Advanced Constraints

            var territoryZones = GetTerritoriesWithMaxOrders(3);

            
            var order1 = route4Me.GetOrderByID(new OrderParameters() 
            { 
                order_id = territoryZones[0].Orders[0].ToString() 
            }, out _);

            var order2 = route4Me.GetOrderByID(new OrderParameters()
            {
                order_id = territoryZones[1].Orders[0].ToString()
            }, out _);

            var order3 = route4Me.GetOrderByID(new OrderParameters()
            {
                order_id = territoryZones[2].Orders[0].ToString()
            }, out _);


            if ((territoryZones?.Length ?? 0) < 3)
            {
                Console.WriteLine("Can not retrieve 3 territories with an array of the order IDs.");
            }

            var advancedConstraints1 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 1,
                AvailableTimeWindows = new List<int[]>()
                {
                    new int[] { 46800, 57600 }
                },
                Tags = new string[] { territoryZones[0].TerritoryId },
                DepotAddress = new DataTypes.V5.Address()
                {
                    Alias = order1.AddressAlias,
                    AddressString = order1.Address1,
                    Latitude = order1.CachedLat,
                    Longitude = order1.CachedLng,
                    Time = 0
                }
            };

            var advancedConstraints2 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 1,
                AvailableTimeWindows = new List<int[]>()
                {
                    new int[] { 46800, 61200 }
                },
                Tags = new string[] { territoryZones[1].TerritoryId },
                DepotAddress = new DataTypes.V5.Address()
                {
                    Alias = order2.AddressAlias,
                    AddressString = order2.Address1,
                    Latitude = order2.CachedLat,
                    Longitude = order2.CachedLng,
                    Time = 0
                }
            };

            var advancedConstraints3 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 1,
                AvailableTimeWindows = new List<int[]>()
                {
                    new int[] { 46800, 64800 }
                },
                Tags = new string[] { territoryZones[2].TerritoryId },
                DepotAddress = new DataTypes.V5.Address()
                {
                    Alias = order3.AddressAlias,
                    AddressString = order3.Address1,
                    Latitude = order3.CachedLat,
                    Longitude = order3.CachedLng,
                    Time = 0
                }
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
                        AddressString = order1.Address1,
                        Latitude = order1.CachedLat,
                        Longitude = order1.CachedLng
                    },
                    new Address()
                    {
                        AddressString = order2.Address1,
                        Latitude = order2.CachedLat,
                        Longitude = order2.CachedLng
                    },
                    new Address()
                    {
                        AddressString = order3.Address1,
                        Latitude = order3.CachedLat,
                        Longitude = order3.CachedLng
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

            RemoveTestOptimizations();
        }
    }
}