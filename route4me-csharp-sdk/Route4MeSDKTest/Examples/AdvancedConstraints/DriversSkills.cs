using System;
using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Driver's Skills
        /// </summary>
        [Obsolete]
        public void DriversSkills()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var driverIDs = new List<int>();
            int? rootMemberId = null;

            var users = route4Me.GetUsers(new GenericParameters(), out _);

            if ((users?.Results.Length ?? 0) > 0)
            {
                rootMemberId = Convert.ToInt32(users.Results[0].MemberId);
            }


            foreach (var user in users.Results)
            {
                if (user.MemberType == Route4MeSDK.DataTypes.V5.MemberTypes.Driver.Description())
                {
                    driverIDs.Add(Convert.ToInt32(user.MemberId));
                    if (driverIDs.Count > 2) break;
                }
            }

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = 0,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Automatic Driver's Skills Example - Single Depot, Multiple Driver",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var skills1 = new List<string>() { "Class A CDL" };
            var skills2 = new List<string>() { "Class B CDL" };

            var route4meMembersID = driverIDs.Count > 0
                ? driverIDs.ToArray()
                : new int[] { (int)rootMemberId };

            #region Advanced Constraints

            var advancedConstraints1 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MaximumCapacity = 200,
                Route4meMembersId = route4meMembersID,
                AvailableTimeWindows = new List<int[]> { new int[] { 25200, 75000 } }
            };

            var advancedConstraints2 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MaximumCapacity = 500,
                Route4meMembersId = route4meMembersID,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 45200, 85000 }
                }
            };

            var advancedConstraints = new DataTypes.V5.RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300,
                    Tags = skills1.ToArray()
                },

                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300,
                    Tags = skills1.ToArray()
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300,
                    Tags = skills1.ToArray()
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300,
                    Tags = skills1.ToArray()
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    TimeWindowStart = 45200,
                    TimeWindowEnd = 55000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    TimeWindowStart = 62000,
                    TimeWindowEnd = 85000,
                    Time = 300,
                    Tags = skills2.ToArray()
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    TimeWindowStart = 62000,
                    TimeWindowEnd = 85000,
                    Time = 300,
                    Tags = skills2.ToArray()
                }
            };

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