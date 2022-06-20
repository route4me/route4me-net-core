using CsvHelper;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Drivers Schedules with Territories and Retail Location
        /// </summary>
        public void DriversSchedulesWithTerritoriesAndRetailLocation()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,

                RT = true,
                Parts = 30,
                Metric = Metric.Matrix,
                RouteName = "Drivers Schedules - 3 Territories",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zones = new Dictionary<int, List<string>>()
            {
                { 0, new List<string>() { "ZONE 01" } },
                { 1, new List<string>() { "ZONE 02" } },
                { 2, new List<string>() { "ZONE 03" } }
            };

            #region Advanced Constraints

            var advancedConstraints = new List<DataTypes.V5.RouteAdvancedConstraints>();

            for (int i = 0; i < 30; i++)
            {
                advancedConstraints.Add(
                    new DataTypes.V5.RouteAdvancedConstraints()
                    {
                        Tags = zones[i % 3].ToArray(),
                        MembersCount = 1,
                        AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (11 + 5) * 3600 } },
                        LocationSequencePattern = new object[]
                        {
                            "",
                            new Address()
                            {
                                AddressString = "RETAIL LOCATION",
                                Alias = "RETAIL LOCATION",
                                Latitude = 25.8741751,
                                Longitude = -80.1288583,
                                Time = 300
                            }
                        }
                    }
                );
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/locations.csv";

            #region Prepare Addresses

            int serviceTime = 300;

            var addresses = new List<Address>();

            using (TextReader reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var address = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var lng = csv.GetField(2);
                        var group = csv.GetField(3);

                        int igroup = int.TryParse(group.ToString(), out _) ? Convert.ToInt32(group) : 0;

                        addresses.Add(
                            new Address()
                            {
                                AddressString = address,
                                Alias = address,
                                Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                                Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                                Group = group,
                                Time = serviceTime,
                                Tags = zones[igroup].ToArray()
                            }
                        );
                    }
                }
            }

            #endregion

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