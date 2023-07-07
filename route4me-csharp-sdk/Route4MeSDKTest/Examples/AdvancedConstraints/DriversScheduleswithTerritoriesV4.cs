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
        /// Drivers Schedules with Territories
        /// </summary>
        public async void DriversScheduleswithTerritoriesV4()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RT = true,
                Parts = 50,
                RouteName = "46 Drivers Schedules",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            DataTypes.V5.RouteAdvancedConstraints schedule;

            #region Advanced Constraints

            var advancedConstraints = new List<DataTypes.V5.RouteAdvancedConstraints>();

            var sScheduleFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/schedules.csv";

            using (TextReader reader = File.OpenText(sScheduleFile))
            {
                using (var csv = new CsvReader(reader))
                {
                    while (csv.Read())
                    {
                        var start = csv.GetField(0);
                        var end = csv.GetField(1);
                        var group = csv.GetField(2);

                        schedule = new DataTypes.V5.RouteAdvancedConstraints();

                        int timeStart = int.TryParse(start.ToString(), out _) ? int.Parse(start.ToString()) : -1;
                        int timeEnd = int.TryParse(end.ToString(), out _) ? int.Parse(end.ToString()) : -1;

                        schedule.MembersCount = 1;

                        schedule.AvailableTimeWindows = new List<int[]>
                        {
                            new int[] { timeStart, timeEnd }
                        };

                        advancedConstraints.Add(schedule);
                    }
                }
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            #region Prepare Addresses

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/locations.csv";

            var addresses = new List<Address>();
            int serviceTime = 120;

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

                        addresses.Add(
                            new Address()
                            {
                                AddressString = address,
                                Alias = address,
                                Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                                Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                                Time = serviceTime,
                                Group = group
                            }
                        );
                    }
                }
            }

            Address depot = new Address()
            {
                AddressString = "Depot",
                IsDepot = true,
                Latitude = 25.694341,
                Longitude = -80.166036,
                Time = 0
            };

            addresses.Add(depot);

            #endregion

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            var dataObject = await route4Me.RunOptimizationAsync(optimizationParameters);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.Item1?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObject.Item1, dataObject.Item2);

            RemoveTestOptimizations();
        }
    }
}