using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using CsvHelper;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Drivers Schedules with Territories
        /// </summary>
        public async void DriversScheduleswithTerritoriesV2()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RT = true,
                Parts = 30,
                RouteName = "Drivers Schedules - 3 Territories",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zone1 = new List<string>() { "ZONE 01" };
            var zone2 = new List<string>() { "ZONE 02" };
            var zone3 = new List<string>() { "ZONE 03" };

            DataTypes.V5.RouteAdvancedConstraints schedule;

            var zone = new List<string>();

            #region Advanced Constraints

            var advancedConstraints = new List<DataTypes.V5.RouteAdvancedConstraints>();

            for (int i = 0; i < 30; i++)
            {
                schedule = new DataTypes.V5.RouteAdvancedConstraints();
                switch (i % 3)
                {
                    case 0:
                        zone = zone1;
                        break;
                    case 1:
                        zone = zone2;
                        break;
                    case 2:
                        zone = zone3;
                        break;
                }

                schedule.Tags = zone.ToArray();
                schedule.MembersCount = 1;
                schedule.AvailableTimeWindows = new List<int[]>
                {
                    new int[] { (8 + 5) * 3600, (11 + 5) * 3600 }
                };

                advancedConstraints.Add(schedule);
            }

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints.ToArray();

            #region Prepare Addresses

            var sAddressFile = AppDomain.CurrentDomain.BaseDirectory + @"/Data/CSV/locations.csv";

            var addresses = new List<Address>();

            int count = 0;

            using (var reader = File.OpenText(sAddressFile))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
                    {
                        string[] tags = new string[0];

                        if (count > 0)
                        {
                            int remainder = -1;

                            Math.DivRem(count, 3, out remainder);

                            switch (remainder)
                            {
                                case 0:
                                    tags = zone1.ToArray();
                                    break;
                                case 1:
                                    tags = zone2.ToArray();
                                    break;
                                case 2:
                                    tags = zone3.ToArray();
                                    break;
                            }
                        }

                        var address = csv.GetField(0);
                        var lat = csv.GetField(1);
                        var lng = csv.GetField(2);
                        var group = csv.GetField(3);

                        addresses.Add(
                            new Address()
                            {
                                AddressString = address,
                                Latitude = double.TryParse(lat.ToString(), out _) ? Convert.ToDouble(lat) : 0,
                                Longitude = double.TryParse(lng.ToString(), out _) ? Convert.ToDouble(lng) : 0,
                                Group = group,
                                Tags = tags
                            }
                        );

                        count++;
                    }
                }
            }

            Address depot = new Address()
            {
                AddressString = "Depot",
                IsDepot = true,
                Latitude = 25.723025,
                Longitude = -80.452883,
                Time = 0
            };

            addresses.Add(depot);

            #endregion

            var optimizationParameters = new OptimizationParameters()
            {
                Parameters = routeParameters,
                Addresses = addresses.ToArray()
            };

            //var response = route4Me.RunOptimization(optimizationParameters, out string errorString);
            var result = await route4Me.RunOptimizationAsync(optimizationParameters);

            OptimizationsToRemove = new List<string>()
            {
                result?.Item1?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(result.Item1, result.Item2);

            RemoveTestOptimizations();
        }
    }
}