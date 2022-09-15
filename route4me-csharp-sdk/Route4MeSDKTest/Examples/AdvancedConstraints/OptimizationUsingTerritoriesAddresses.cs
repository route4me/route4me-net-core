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
        /// using contacts from the specified territories and different depots.
        /// </summary>
        public void OptimizationUsingTerritoriesAddresses()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = 46800,
                RouteName = "Single Depot, Multiple Driver - 3 Territories Address IDs",
                TravelMode = TravelMode.Driving.Description(),
                DeviceType = DeviceType.Web.Description(),
                UseMixedPickupDeliveryDemands = false
            };


            #region Advanced Constraints

            var territoryZones = GetTerritoriesWithMaxContacts(3);

            var depotContact = route4Me.GetAddressBookLocation(
                new AddressBookParameters()
                {
                    AddressId = territoryZones[0].Addresses[0].ToString()
                }, out _, out _)[0];

            if ((territoryZones?.Length ?? 0) < 3)
            {
                Console.WriteLine("Can not retrieve 3 territories with an array of the contact IDs.");
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
                foreach (int contactId in territoryZone.Addresses)
                {
                    addresses.Add(new Address()
                    {
                        ContactId = contactId,
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
                        AddressString = depotContact.Address1,
                        Latitude = depotContact.CachedLat,
                        Longitude = depotContact.CachedLng
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