﻿using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Drivers Schedules with Address from Territories created in Route4Me
        /// </summary>
        public void AdvancedConstraintsExample13()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = (8 + 5) * 3600,
                Metric = Metric.Matrix,
                RouteName = "Single Depot, Multiple Driver - 3 Territories IDs",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),

            };

            var depots = new Address[]
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Latitude = 38.141598,
                    Longitude =-85.793846
                }
            };

            routeParameters.Depots = depots;

            var territory1 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = "A34BA30C717D1194FC0230252DF0C45C",
                    Addresses = 1
                },
                out _
            );

            var territory2 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = "5D418084790C59C2D42B1C067094A459",
                    Addresses = 1
                },
                out _
            );

            var territory3 = route4Me.GetTerritory(
                new TerritoryQuery()
                {
                    TerritoryId = "0B40480FD15C5F85A26AD759007834F4",
                    Addresses = 1
                },
                out _
            );

            #region Get Addresses from territories

            var addresses = new List<Address>();

            foreach (var addressID in territory1.Addresses)
            {
                addresses.Add(
                    new Address()
                    {
                        ContactId = addressID,
                        Tags = new string[] { territory1.TerritoryId }
                    }
                );
            }

            foreach (var addressID in territory2.Addresses)
            {
                addresses.Add(
                    new Address()
                    {
                        ContactId = addressID,
                        Tags = new string[] { territory2.TerritoryId }
                    }
                );
            }

            foreach (var addressID in territory3.Addresses)
            {
                addresses.Add(
                    new Address()
                    {
                        ContactId = addressID,
                        Tags = new string[] { territory3.TerritoryId }
                    }
                );
            }

            #endregion

            #region Advanced Constraints

            var advancedConstraints = new List<DataTypes.V5.RouteAdvancedConstraints>()
            {
                new DataTypes.V5.RouteAdvancedConstraints()
                {
                    Tags = new string[] { territory1.TerritoryId },
                    MembersCount = 3,
                    AvailableTimeWindows = new List<int[]>
                    {
                        new int[] { (8 + 5) * 3600, (11 + 5) * 3600 }
                    }
                },
                new DataTypes.V5.RouteAdvancedConstraints()
                {
                    Tags = new string[] { territory1.TerritoryId },
                    MembersCount = 4,
                    AvailableTimeWindows = new List<int[]>
                    {
                        new int[] { (8 + 5) * 3600, (12 + 5) * 3600 }
                    }
                },
                new DataTypes.V5.RouteAdvancedConstraints()
                {
                    Tags = new string[] { territory1.TerritoryId },
                    MembersCount = 3,
                    AvailableTimeWindows = new List<int[]>
                    {
                        new int[] { (8 + 5) * 3600, (13 + 5) * 3600 }
                    }
                }
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
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