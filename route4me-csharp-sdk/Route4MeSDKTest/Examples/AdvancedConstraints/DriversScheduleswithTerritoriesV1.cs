using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Drivers Schedules with Territories
        /// </summary>
        public void DriversScheduleswithTerritoriesV1()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                RouteTime = 0,
                RT = true,
                Parts = 20,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Drivers Schedules - 3 Territories",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var zone1 = new List<string>() { "ZONE 01" };
            var zone2 = new List<string>() { "ZONE 02" };
            var zone3 = new List<string>() { "ZONE 03" };

            var route4meMembersID = new List<int>() { 1459842, 1481916, 1481918 };

            #region Advanced Constraints

            var advancedConstraints1 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                Tags = zone1.ToArray(),
                MembersCount = 3,
                AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (11 + 5) * 3600 } }
            };

            var advancedConstraints2 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                Tags = zone2.ToArray(),
                MembersCount = 4,
                AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (12 + 5) * 3600 } }
            };

            var advancedConstraints3 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                Tags = zone3.ToArray(),
                MembersCount = 3,
                AvailableTimeWindows = new List<int[]> { new int[] { (8 + 5) * 3600, (13 + 5) * 3600 } }
            };

            var advancedConstraints = new DataTypes.V5.RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2,
                advancedConstraints3
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            int serviceTime = 300;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "2158",
                    IsDepot = false,
                    Latitude = 25.603049,
                    Longitude = -80.348022,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "253",
                    IsDepot = false,
                    Latitude = 25.618737,
                    Longitude = -80.329138,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1527",
                    IsDepot = false,
                    Latitude = 25.660645,
                    Longitude = -80.284289,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1889",
                    IsDepot = false,
                    Latitude = 25.816771,
                    Longitude = -80.265282,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3998",
                    IsDepot = false,
                    Latitude = 25.830834,
                    Longitude = -80.336474,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1988",
                    IsDepot = false,
                    Latitude = 25.934509,
                    Longitude = -80.216283,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3566",
                    IsDepot = false,
                    Latitude = 25.826221,
                    Longitude = -80.247753,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2659",
                    IsDepot = false,
                    Latitude = 25.60218,
                    Longitude = -80.384538,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2477",
                    IsDepot = false,
                    Latitude = 25.679245,
                    Longitude = -80.281254,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3968",
                    IsDepot = false,
                    Latitude = 25.655636,
                    Longitude = -80.350484,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1700",
                    IsDepot = false,
                    Latitude = 25.871786,
                    Longitude = -80.341298,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1804",
                    IsDepot = false,
                    Latitude = 25.690688,
                    Longitude = -80.318216,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "209",
                    IsDepot = false,
                    Latitude = 25.893571,
                    Longitude = -80.20119,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "834",
                    IsDepot = false,
                    Latitude = 25.951618,
                    Longitude = -80.29993,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1530",
                    IsDepot = false,
                    Latitude = 25.818694,
                    Longitude = -80.354931,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1696",
                    IsDepot = false,
                    Latitude = 25.748019,
                    Longitude = -80.243968,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1596",
                    IsDepot = false,
                    Latitude = 25.834085,
                    Longitude = -80.193554,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3563",
                    IsDepot = false,
                    Latitude = 25.690451,
                    Longitude = -80.272227,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3622",
                    IsDepot = false,
                    Latitude = 25.602187,
                    Longitude = -80.41193,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1805",
                    IsDepot = false,
                    Latitude = 25.780564,
                    Longitude = -80.415264,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1655",
                    IsDepot = false,
                    Latitude = 25.779567,
                    Longitude = -80.356258,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1533",
                    IsDepot = false,
                    Latitude = 25.459839,
                    Longitude = -80.44416,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "269",
                    IsDepot = false,
                    Latitude = 25.777716,
                    Longitude = -80.25451,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1238",
                    IsDepot = false,
                    Latitude = 25.821602,
                    Longitude = -80.12694,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3312",
                    IsDepot = false,
                    Latitude = 25.894716,
                    Longitude = -80.33056,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3989",
                    IsDepot = false,
                    Latitude = 25.553594,
                    Longitude = -80.374832,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "457",
                    IsDepot = false,
                    Latitude = 25.636562,
                    Longitude = -80.451262,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3105",
                    IsDepot = false,
                    Latitude = 25.737308,
                    Longitude = -80.43438,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3317",
                    IsDepot = false,
                    Latitude = 25.752353,
                    Longitude = -80.215284,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3589",
                    IsDepot = false,
                    Latitude = 25.877066,
                    Longitude = -80.22757,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3313",
                    IsDepot = false,
                    Latitude = 25.93445,
                    Longitude = -80.257547,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "385",
                    IsDepot = false,
                    Latitude = 25.902516,
                    Longitude = -80.254678,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2601",
                    IsDepot = false,
                    Latitude = 25.85515,
                    Longitude = -80.219475,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "602",
                    IsDepot = false,
                    Latitude = 25.513304,
                    Longitude = -80.387233,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2710",
                    IsDepot = false,
                    Latitude = 25.626475,
                    Longitude = -80.428484,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1187",
                    IsDepot = false,
                    Latitude = 25.781259,
                    Longitude = -80.402599,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2155",
                    IsDepot = false,
                    Latitude = 25.760206,
                    Longitude = -80.330144,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "990",
                    IsDepot = false,
                    Latitude = 25.9182,
                    Longitude = -80.352967,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2810",
                    IsDepot = false,
                    Latitude = 25.763907,
                    Longitude = -80.293502,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3294",
                    IsDepot = false,
                    Latitude = 25.576745,
                    Longitude = -80.380201,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3578",
                    IsDepot = false,
                    Latitude = 25.441741,
                    Longitude = -80.454027,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1040",
                    IsDepot = false,
                    Latitude = 25.741545,
                    Longitude = -80.320633,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2184",
                    IsDepot = false,
                    Latitude = 25.769002,
                    Longitude = -80.404676,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "842",
                    IsDepot = false,
                    Latitude = 25.705431,
                    Longitude = -80.398938,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1668",
                    IsDepot = false,
                    Latitude = 25.660366,
                    Longitude = -80.376896,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2603",
                    IsDepot = false,
                    Latitude = 25.660645,
                    Longitude = -80.284289,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "1304",
                    IsDepot = false,
                    Latitude = 25.935256,
                    Longitude = -80.176192,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3281",
                    IsDepot = false,
                    Latitude = 25.962562,
                    Longitude = -80.250286,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2855",
                    IsDepot = false,
                    Latitude = 25.781819,
                    Longitude = -80.235649,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2518",
                    IsDepot = false,
                    Latitude = 25.632515,
                    Longitude = -80.368998,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "46",
                    IsDepot = false,
                    Latitude = 25.741641,
                    Longitude = -80.221332,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3185",
                    IsDepot = false,
                    Latitude = 25.945872,
                    Longitude = -80.310623,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3309",
                    IsDepot = false,
                    Latitude = 25.761921,
                    Longitude = -80.368253,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2586",
                    IsDepot = false,
                    Latitude = 25.792323,
                    Longitude = -80.336024,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "237",
                    IsDepot = false,
                    Latitude = 25.749872,
                    Longitude = -80.393815,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2192",
                    IsDepot = false,
                    Latitude = 25.94228,
                    Longitude = -80.174436,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2887",
                    IsDepot = false,
                    Latitude = 25.753024,
                    Longitude = -80.232491,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3485",
                    IsDepot = false,
                    Latitude = 25.547749,
                    Longitude = -80.375777,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3832",
                    IsDepot = false,
                    Latitude = 25.489671,
                    Longitude = -80.419657,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1393",
                    IsDepot = false,
                    Latitude = 25.872401,
                    Longitude = -80.295227,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "781",
                    IsDepot = false,
                    Latitude = 25.912158,
                    Longitude = -80.204096,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2165",
                    IsDepot = false,
                    Latitude = 25.745813,
                    Longitude = -80.275891,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "537",
                    IsDepot = false,
                    Latitude = 25.843267,
                    Longitude = -80.373141,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "235",
                    IsDepot = false,
                    Latitude = 25.877239,
                    Longitude = -80.222824,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1175",
                    IsDepot = false,
                    Latitude = 25.924446,
                    Longitude = -80.162018,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2235",
                    IsDepot = false,
                    Latitude = 25.850434,
                    Longitude = -80.183362,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2775",
                    IsDepot = false,
                    Latitude = 25.647769,
                    Longitude = -80.410684,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1556",
                    IsDepot = false,
                    Latitude = 25.457798,
                    Longitude = -80.483813,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3233",
                    IsDepot = false,
                    Latitude = 25.593026,
                    Longitude = -80.382412,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3534",
                    IsDepot = false,
                    Latitude = 25.867923,
                    Longitude = -80.24087,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3205",
                    IsDepot = false,
                    Latitude = 25.656392,
                    Longitude = -80.291358,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "2893",
                    IsDepot = false,
                    Latitude = 25.867024,
                    Longitude = -80.201303,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1555",
                    IsDepot = false,
                    Latitude = 25.776622,
                    Longitude = -80.415111,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3218",
                    IsDepot = false,
                    Latitude = 25.832436,
                    Longitude = -80.280374,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "899",
                    IsDepot = false,
                    Latitude = 25.855764,
                    Longitude = -80.187256,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1027",
                    IsDepot = false,
                    Latitude = 25.735087,
                    Longitude = -80.259917,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3448",
                    IsDepot = false,
                    Latitude = 25.84728,
                    Longitude = -80.266024,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "884",
                    IsDepot = false,
                    Latitude = 25.480335,
                    Longitude = -80.458004,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "448",
                    IsDepot = false,
                    Latitude = 25.684473,
                    Longitude = -80.451831,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3643",
                    IsDepot = false,
                    Latitude = 25.677524,
                    Longitude = -80.425454,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1952",
                    IsDepot = false,
                    Latitude = 25.754493,
                    Longitude = -80.342664,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3507",
                    IsDepot = false,
                    Latitude = 25.874399,
                    Longitude = -80.345727,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "3520",
                    IsDepot = false,
                    Latitude = 25.483806,
                    Longitude = -80.428498,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1611",
                    IsDepot = false,
                    Latitude = 25.713302,
                    Longitude = -80.440269,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1402",
                    IsDepot = false,
                    Latitude = 25.72308,
                    Longitude = -80.444812,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1211",
                    IsDepot = false,
                    Latitude = 25.699226,
                    Longitude = -80.422237,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1161",
                    IsDepot = false,
                    Latitude = 25.835215,
                    Longitude = -80.252216,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1274",
                    IsDepot = false,
                    Latitude = 25.888309,
                    Longitude = -80.344764,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "1341",
                    IsDepot = false,
                    Latitude = 25.716966,
                    Longitude = -80.438179,
                    Time = serviceTime,
                    Tags = zone2.ToArray()
                },
                new Address()
                {
                    AddressString = "2946",
                    IsDepot = false,
                    Latitude = 25.530972,
                    Longitude = -80.448924,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "813",
                    IsDepot = false,
                    Latitude = 25.488095,
                    Longitude = -80.450334,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "3862",
                    IsDepot = false,
                    Latitude = 25.954786,
                    Longitude = -80.16335,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "759",
                    IsDepot = false,
                    Latitude = 25.555122,
                    Longitude = -80.335284,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "52",
                    IsDepot = false,
                    Latitude = 25.927916,
                    Longitude = -80.268189,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "909",
                    IsDepot = false,
                    Latitude = 25.832815,
                    Longitude = -80.217156,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "2768",
                    IsDepot = false,
                    Latitude = 25.835259,
                    Longitude = -80.223997,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3967",
                    IsDepot = false,
                    Latitude = 25.630732,
                    Longitude = -80.366065,
                    Time = serviceTime,
                    Tags = zone1.ToArray()
                },
                new Address()
                {
                    AddressString = "1974",
                    IsDepot = false,
                    Latitude = 25.931024,
                    Longitude = -80.217991,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
                },
                new Address()
                {
                    AddressString = "3147",
                    IsDepot = false,
                    Latitude = 25.921095,
                    Longitude = -80.261839,
                    Time = serviceTime,
                    Tags = zone3.ToArray()
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