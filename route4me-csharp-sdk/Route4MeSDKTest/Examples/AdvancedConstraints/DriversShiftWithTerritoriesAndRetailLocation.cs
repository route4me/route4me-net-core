using System.Collections.Generic;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Retail Location based of address position id
        /// </summary>
        public void DriversShiftWithTerritoriesAndRetailLocation()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParameters = new RouteParameters()
            {
                RT = true,
                AlgorithmType = AlgorithmType.ADVANCED_CVRP_TW,
                Parts = 20,
                Metric = Metric.Matrix,
                RouteMaxDuration = 86400,
                VehicleCapacity = 100,
                VehicleMaxDistanceMI = 10000,
                RouteName = "Drivers Shift - 3 Territories - Retail Location",
                Optimize = Optimize.Distance.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                OverrideAddresses = new OverrideAddresses()
                {
                    Time = 90
                }
            };

            #region Advanced Constraints

            var advancedConstraints1 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 20,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 46800, 61200 }
                },
                Tags = new string[]
                {
                    "TERRITORY 01"
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        Alias = "RETAIL LOCATION",
                        AddressString = "RETAIL LOCATION",
                        Latitude = 25.8741751,
                        Longitude = -80.1288583,
                        Time = 300
                    }
                },
                Group = "part_time"
            };

            var advancedConstraints2 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 20,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 46800, 61200 }
                },
                Tags = new string[]
                {
                    "TERRITORY 02"
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        Alias = "RETAIL LOCATION",
                        AddressString = "RETAIL LOCATION",
                        Latitude = 25.8741751,
                        Longitude = -80.1288583,
                        Time = 300
                    }
                },
                Group = "part_time"
            };

            var advancedConstraints3 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 20,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 46800, 61200 }
                },
                Tags = new string[]
                {
                    "TERRITORY 03"
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        Alias = "RETAIL LOCATION",
                        AddressString = "RETAIL LOCATION",
                        Latitude = 25.8741751,
                        Longitude = -80.1288583,
                        Time = 300
                    }
                },
                Group = "part_time"
            };

            var advancedConstraints4 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 20,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 46800, 75600 }
                },
                Tags = new string[]
                {
                    "TERRITORY 01"
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        Alias = "RETAIL LOCATION",
                        AddressString = "RETAIL LOCATION",
                        Latitude = 25.8741751,
                        Longitude = -80.1288583,
                        Time = 300
                    }
                },
                Group = "full_time"
            };

            var advancedConstraints5 = new DataTypes.V5.RouteAdvancedConstraints()
            {
                MembersCount = 20,
                AvailableTimeWindows = new List<int[]>
                {
                    new int[] { 46800, 75600 }
                },
                Tags = new string[]
                {
                    "TERRITORY 03"
                },
                LocationSequencePattern = new object[]
                {
                    "",
                    new Address()
                    {
                        Alias = "RETAIL LOCATION",
                        AddressString = "RETAIL LOCATION",
                        Latitude = 25.8741751,
                        Longitude = -80.1288583,
                        Time = 300
                    }
                },
                Group = "full_time"
            };

            var advancedConstraints = new DataTypes.V5.RouteAdvancedConstraints[]
            {
                advancedConstraints1,
                advancedConstraints2,
                advancedConstraints3,
                advancedConstraints4,
                advancedConstraints5
            };

            #endregion

            routeParameters.AdvancedConstraints = advancedConstraints;

            var addresses = new List<Address>()
            {
                new Address()
                {
                    Alias = "2505",
                    AddressString = "2505",
                    Latitude = 25.767596,
                    Longitude = -80.226998,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2687",
                    AddressString = "2687",
                    Latitude = 25.786497,
                    Longitude = -80.207408,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "472",
                    AddressString = "472",
                    Latitude = 25.66043,
                    Longitude = -80.417161,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1235",
                    AddressString = "1235",
                    Latitude = 25.688111,
                    Longitude = -80.456527,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2849",
                    AddressString = "2849",
                    Latitude = 25.839934,
                    Longitude = -80.189969,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "887",
                    AddressString = "887",
                    Latitude = 25.755872,
                    Longitude = -80.419184,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2584",
                    AddressString = "2584",
                    Latitude = 25.720941,
                    Longitude = -80.289537,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2750",
                    AddressString = "2750",
                    Latitude = 25.837605,
                    Longitude = -80.294638,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1018",
                    AddressString = "1018",
                    Latitude = 25.693624,
                    Longitude = -80.26164,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "714",
                    AddressString = "714",
                    Latitude = 25.853241,
                    Longitude = -80.205793,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1220",
                    AddressString = "1220",
                    Latitude = 25.463502,
                    Longitude = -80.456949,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1360",
                    AddressString = "1360",
                    Latitude = 25.712858,
                    Longitude = -80.271239,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3818",
                    AddressString = "3818",
                    Latitude = 25.900222,
                    Longitude = -80.22482,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2717",
                    AddressString = "2717",
                    Latitude = 25.894207,
                    Longitude = -80.345417,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "591",
                    AddressString = "591",
                    Latitude = 25.706562,
                    Longitude = -80.412644,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2156",
                    AddressString = "2156",
                    Latitude = 25.525801,
                    Longitude = -80.401086,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1339",
                    AddressString = "1339",
                    Latitude = 25.630437,
                    Longitude = -80.43103,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "797",
                    AddressString = "797",
                    Latitude = 25.868294,
                    Longitude = -80.302895,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2634",
                    AddressString = "2634",
                    Latitude = 25.935107,
                    Longitude = -80.260117,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2731",
                    AddressString = "2731",
                    Latitude = 25.920058,
                    Longitude = -80.229779,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1787",
                    AddressString = "1787",
                    Latitude = 25.825283,
                    Longitude = -80.189021,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1280",
                    AddressString = "1280",
                    Latitude = 25.730165,
                    Longitude = -80.441235,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1807",
                    AddressString = "1807",
                    Latitude = 25.748351,
                    Longitude = -80.250563,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1554",
                    AddressString = "1554",
                    Latitude = 25.478459,
                    Longitude = -80.470336,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1453",
                    AddressString = "1453",
                    Latitude = 25.812783,
                    Longitude = -80.351845,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3608",
                    AddressString = "3608",
                    Latitude = 25.786452,
                    Longitude = -80.323251,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3407",
                    AddressString = "3407",
                    Latitude = 25.659019,
                    Longitude = -80.415279,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1886",
                    AddressString = "1886",
                    Latitude = 25.919989,
                    Longitude = -80.356243,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1695",
                    AddressString = "1695",
                    Latitude = 25.718286,
                    Longitude = -80.460965,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1174",
                    AddressString = "1174",
                    Latitude = 25.685317,
                    Longitude = -80.383784,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1991",
                    AddressString = "1991",
                    Latitude = 25.933406,
                    Longitude = -80.169637,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3444",
                    AddressString = "3444",
                    Latitude = 25.883938,
                    Longitude = -80.193806,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2057",
                    AddressString = "2057",
                    Latitude = 25.85603,
                    Longitude = -80.187272,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1025",
                    AddressString = "1025",
                    Latitude = 25.466222,
                    Longitude = -80.420452,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3745",
                    AddressString = "3745",
                    Latitude = 25.944004,
                    Longitude = -80.150793,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3491",
                    AddressString = "3491",
                    Latitude = 25.468136,
                    Longitude = -80.427913,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3763",
                    AddressString = "3763",
                    Latitude = 25.666494,
                    Longitude = -80.491322,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1282",
                    AddressString = "1282",
                    Latitude = 25.771978,
                    Longitude = -80.414949,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1331",
                    AddressString = "1331",
                    Latitude = 25.67895,
                    Longitude = -80.313754,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2366",
                    AddressString = "2366",
                    Latitude = 25.503359,
                    Longitude = -80.401658,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "50",
                    AddressString = "50",
                    Latitude = 25.624761,
                    Longitude = -80.417657,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1245",
                    AddressString = "1245",
                    Latitude = 25.678752,
                    Longitude = -80.459988,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "824",
                    AddressString = "824",
                    Latitude = 25.668055,
                    Longitude = -80.401825,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3013",
                    AddressString = "3013",
                    Latitude = 25.621031,
                    Longitude = -80.419136,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1908",
                    AddressString = "1908",
                    Latitude = 25.514841,
                    Longitude = -80.422449,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1500",
                    AddressString = "1500",
                    Latitude = 25.484149,
                    Longitude = -80.486158,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "875",
                    AddressString = "875",
                    Latitude = 25.504744,
                    Longitude = -80.414766,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "358",
                    AddressString = "358",
                    Latitude = 25.712526,
                    Longitude = -80.442093,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3663",
                    AddressString = "3663",
                    Latitude = 25.635575,
                    Longitude = -80.397669,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "818",
                    AddressString = "818",
                    Latitude = 25.52749,
                    Longitude = -80.415321,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3428",
                    AddressString = "3428",
                    Latitude = 25.625566,
                    Longitude = -80.355939,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "247",
                    AddressString = "247",
                    Latitude = 25.704969,
                    Longitude = -80.409955,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1549",
                    AddressString = "1549",
                    Latitude = 25.889093,
                    Longitude = -80.237312,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2407",
                    AddressString = "2407",
                    Latitude = 25.815763,
                    Longitude = -80.226706,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3050",
                    AddressString = "3050",
                    Latitude = 25.693771,
                    Longitude = -80.337638,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "843",
                    AddressString = "843",
                    Latitude = 25.838432,
                    Longitude = -80.373232,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2860",
                    AddressString = "2860",
                    Latitude = 25.711994,
                    Longitude = -80.348498,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3601",
                    AddressString = "3601",
                    Latitude = 25.766257,
                    Longitude = -80.254434,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2015",
                    AddressString = "2015",
                    Latitude = 25.795945,
                    Longitude = -80.198697,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1959",
                    AddressString = "1959",
                    Latitude = 25.752685,
                    Longitude = -80.228505,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3671",
                    AddressString = "3671",
                    Latitude = 25.726531,
                    Longitude = -80.250194,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1804",
                    AddressString = "1804",
                    Latitude = 25.690688,
                    Longitude = -80.318216,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "64",
                    AddressString = "64",
                    Latitude = 25.790691,
                    Longitude = -80.226054,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3759",
                    AddressString = "3759",
                    Latitude = 25.756774,
                    Longitude = -80.365051,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3794",
                    AddressString = "3794",
                    Latitude = 25.699597,
                    Longitude = -80.348233,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "565",
                    AddressString = "565",
                    Latitude = 25.816895,
                    Longitude = -80.37512,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1952",
                    AddressString = "1952",
                    Latitude = 25.754493,
                    Longitude = -80.342664,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1165",
                    AddressString = "1165",
                    Latitude = 25.824062,
                    Longitude = -80.33434,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3767",
                    AddressString = "3767",
                    Latitude = 25.738514,
                    Longitude = -80.325509,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3882",
                    AddressString = "3882",
                    Latitude = 25.863556,
                    Longitude = -80.326052,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3178",
                    AddressString = "3178",
                    Latitude = 25.761851,
                    Longitude = -80.376687,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "586",
                    AddressString = "586",
                    Latitude = 25.757977,
                    Longitude = -80.226683,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3230",
                    AddressString = "3230",
                    Latitude = 25.779132,
                    Longitude = -80.244306,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "925",
                    AddressString = "925",
                    Latitude = 25.732188,
                    Longitude = -80.400231,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2495",
                    AddressString = "2495",
                    Latitude = 25.786012,
                    Longitude = -80.241707,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3971",
                    AddressString = "3971",
                    Latitude = 25.820501,
                    Longitude = -80.24979,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3841",
                    AddressString = "3841",
                    Latitude = 25.768455,
                    Longitude = -80.354086,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3262",
                    AddressString = "3262",
                    Latitude = 25.734819,
                    Longitude = -80.268536,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "640",
                    AddressString = "640",
                    Latitude = 25.672768,
                    Longitude = -80.275971,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1451",
                    AddressString = "1451",
                    Latitude = 25.699311,
                    Longitude = -80.300853,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3324",
                    AddressString = "3324",
                    Latitude = 25.726846,
                    Longitude = -80.350281,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2184",
                    AddressString = "2184",
                    Latitude = 25.769002,
                    Longitude = -80.404676,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "379",
                    AddressString = "379",
                    Latitude = 25.762656,
                    Longitude = -80.1484,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2943",
                    AddressString = "2943",
                    Latitude = 25.755583,
                    Longitude = -80.318472,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "161",
                    AddressString = "161",
                    Latitude = 25.803613,
                    Longitude = -80.363973,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1094",
                    AddressString = "1094",
                    Latitude = 25.734979,
                    Longitude = -80.234887,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3931",
                    AddressString = "3931",
                    Latitude = 25.753538,
                    Longitude = -80.239045,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3424",
                    AddressString = "3424",
                    Latitude = 25.842973,
                    Longitude = -80.301121,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3986",
                    AddressString = "3986",
                    Latitude = 25.685397,
                    Longitude = -80.292975,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "890",
                    AddressString = "890",
                    Latitude = 25.849912,
                    Longitude = -80.316067,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3482",
                    AddressString = "3482",
                    Latitude = 25.801243,
                    Longitude = -80.212339,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2720",
                    AddressString = "2720",
                    Latitude = 25.715483,
                    Longitude = -80.350101,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3938",
                    AddressString = "3938",
                    Latitude = 25.885392,
                    Longitude = -80.357073,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2399",
                    AddressString = "2399",
                    Latitude = 25.749771,
                    Longitude = -80.31706,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2278",
                    AddressString = "2278",
                    Latitude = 25.743077,
                    Longitude = -80.300602,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1729",
                    AddressString = "1729",
                    Latitude = 25.77576,
                    Longitude = -80.273512,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3805",
                    AddressString = "3805",
                    Latitude = 25.704619,
                    Longitude = -80.355805,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3336",
                    AddressString = "3336",
                    Latitude = 25.740141,
                    Longitude = -80.247248,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1797",
                    AddressString = "1797",
                    Latitude = 25.749367,
                    Longitude = -80.250666,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3414",
                    AddressString = "3414",
                    Latitude = 25.825219,
                    Longitude = -80.363143,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "831",
                    AddressString = "831",
                    Latitude = 25.672391,
                    Longitude = -80.319963,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "263",
                    AddressString = "263",
                    Latitude = 25.766885,
                    Longitude = -80.324348,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1680",
                    AddressString = "1680",
                    Latitude = 25.832086,
                    Longitude = -80.364079,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3040",
                    AddressString = "3040",
                    Latitude = 25.796907,
                    Longitude = -80.204688,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3207",
                    AddressString = "3207",
                    Latitude = 25.734676,
                    Longitude = -80.375237,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "562",
                    AddressString = "562",
                    Latitude = 25.760616,
                    Longitude = -80.208228,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1626",
                    AddressString = "1626",
                    Latitude = 25.758186,
                    Longitude = -80.364412,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2316",
                    AddressString = "2316",
                    Latitude = 25.745408,
                    Longitude = -80.373365,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3150",
                    AddressString = "3150",
                    Latitude = 25.829079,
                    Longitude = -80.318713,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1929",
                    AddressString = "1929",
                    Latitude = 25.762886,
                    Longitude = -80.415113,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2951",
                    AddressString = "2951",
                    Latitude = 25.725761,
                    Longitude = -80.317414,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3330",
                    AddressString = "3330",
                    Latitude = 25.746921,
                    Longitude = -80.346266,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "923",
                    AddressString = "923",
                    Latitude = 25.766603,
                    Longitude = -80.240778,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "858",
                    AddressString = "858",
                    Latitude = 25.734553,
                    Longitude = -80.24883,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2283",
                    AddressString = "2283",
                    Latitude = 25.794811,
                    Longitude = -80.322695,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1024",
                    AddressString = "1024",
                    Latitude = 25.76627,
                    Longitude = -80.309128,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1590",
                    AddressString = "1590",
                    Latitude = 25.802953,
                    Longitude = -80.335483,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1093",
                    AddressString = "1093",
                    Latitude = 25.771181,
                    Longitude = -80.411964,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "339",
                    AddressString = "339",
                    Latitude = 25.463126,
                    Longitude = -80.486494,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2702",
                    AddressString = "2702",
                    Latitude = 25.487894,
                    Longitude = -80.474232,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1570",
                    AddressString = "1570",
                    Latitude = 25.690364,
                    Longitude = -80.403592,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3956",
                    AddressString = "3956",
                    Latitude = 25.416386,
                    Longitude = -80.501924,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3307",
                    AddressString = "3307",
                    Latitude = 25.569025,
                    Longitude = -80.354732,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2744",
                    AddressString = "2744",
                    Latitude = 25.458224,
                    Longitude = -80.445403,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "256",
                    AddressString = "256",
                    Latitude = 25.542705,
                    Longitude = -80.366437,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "731",
                    AddressString = "731",
                    Latitude = 25.54599,
                    Longitude = -80.530794,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1785",
                    AddressString = "1785",
                    Latitude = 25.634564,
                    Longitude = -80.3272,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "155",
                    AddressString = "155",
                    Latitude = 25.637642,
                    Longitude = -80.303665,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2213",
                    AddressString = "2213",
                    Latitude = 25.673454,
                    Longitude = -80.360787,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1957",
                    AddressString = "1957",
                    Latitude = 25.535058,
                    Longitude = -80.395323,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1432",
                    AddressString = "1432",
                    Latitude = 25.58897,
                    Longitude = -80.379412,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1616",
                    AddressString = "1616",
                    Latitude = 25.481877,
                    Longitude = -80.49044,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2789",
                    AddressString = "2789",
                    Latitude = 25.579634,
                    Longitude = -80.344699,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2459",
                    AddressString = "2459",
                    Latitude = 25.941963,
                    Longitude = -80.301709,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1986",
                    AddressString = "1986",
                    Latitude = 25.844346,
                    Longitude = -80.259847,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1130",
                    AddressString = "1130",
                    Latitude = 25.950286,
                    Longitude = -80.318674,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2899",
                    AddressString = "2899",
                    Latitude = 25.827841,
                    Longitude = -80.217117,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2099",
                    AddressString = "2099",
                    Latitude = 25.787751,
                    Longitude = -80.136798,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2053",
                    AddressString = "2053",
                    Latitude = 25.864537,
                    Longitude = -80.305609,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1014",
                    AddressString = "1014",
                    Latitude = 25.965271,
                    Longitude = -80.148076,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "195",
                    AddressString = "195",
                    Latitude = 25.958247,
                    Longitude = -80.160705,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3926",
                    AddressString = "3926",
                    Latitude = 25.858772,
                    Longitude = -80.196992,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2028",
                    AddressString = "2028",
                    Latitude = 25.806216,
                    Longitude = -80.217315,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "685",
                    AddressString = "685",
                    Latitude = 25.946074,
                    Longitude = -80.150152,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2808",
                    AddressString = "2808",
                    Latitude = 25.927147,
                    Longitude = -80.341343,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2656",
                    AddressString = "2656",
                    Latitude = 25.880505,
                    Longitude = -80.313985,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1780",
                    AddressString = "1780",
                    Latitude = 25.879322,
                    Longitude = -80.326792,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1081",
                    AddressString = "1081",
                    Latitude = 25.90851,
                    Longitude = -80.236548,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3403",
                    AddressString = "3403",
                    Latitude = 25.873804,
                    Longitude = -80.307824,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2183",
                    AddressString = "2183",
                    Latitude = 25.911261,
                    Longitude = -80.20384,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3899",
                    AddressString = "3899",
                    Latitude = 25.834144,
                    Longitude = -80.214514,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2923",
                    AddressString = "2923",
                    Latitude = 25.855386,
                    Longitude = -80.245007,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "993",
                    AddressString = "993",
                    Latitude = 25.95998,
                    Longitude = -80.174725,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2411",
                    AddressString = "2411",
                    Latitude = 25.855359,
                    Longitude = -80.173704,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1361",
                    AddressString = "1361",
                    Latitude = 25.925068,
                    Longitude = -80.15309,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "902",
                    AddressString = "902",
                    Latitude = 25.892347,
                    Longitude = -80.133856,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "680",
                    AddressString = "680",
                    Latitude = 25.880568,
                    Longitude = -80.212081,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3167",
                    AddressString = "3167",
                    Latitude = 25.867526,
                    Longitude = -80.273279,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3337",
                    AddressString = "3337",
                    Latitude = 25.89841,
                    Longitude = -80.173683,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2409",
                    AddressString = "2409",
                    Latitude = 25.85616,
                    Longitude = -80.224444,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "560",
                    AddressString = "560",
                    Latitude = 25.93676,
                    Longitude = -80.196433,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3301",
                    AddressString = "3301",
                    Latitude = 25.805258,
                    Longitude = -80.212974,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1930",
                    AddressString = "1930",
                    Latitude = 25.917128,
                    Longitude = -80.20415,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "627",
                    AddressString = "627",
                    Latitude = 25.952842,
                    Longitude = -80.277014,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "288",
                    AddressString = "288",
                    Latitude = 25.879066,
                    Longitude = -80.183581,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3621",
                    AddressString = "3621",
                    Latitude = 25.910919,
                    Longitude = -80.348655,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1661",
                    AddressString = "1661",
                    Latitude = 25.894898,
                    Longitude = -80.355958,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1320",
                    AddressString = "1320",
                    Latitude = 25.971129,
                    Longitude = -80.196883,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "917",
                    AddressString = "917",
                    Latitude = 25.94803,
                    Longitude = -80.292608,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1614",
                    AddressString = "1614",
                    Latitude = 25.775417,
                    Longitude = -80.13707,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1767",
                    AddressString = "1767",
                    Latitude = 25.846028,
                    Longitude = -80.123434,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "20",
                    AddressString = "20",
                    Latitude = 25.96843,
                    Longitude = -80.13811,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1561",
                    AddressString = "1561",
                    Latitude = 25.826723,
                    Longitude = -80.248755,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3395",
                    AddressString = "3395",
                    Latitude = 25.841752,
                    Longitude = -80.207566,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "194",
                    AddressString = "194",
                    Latitude = 25.633458,
                    Longitude = -80.36623,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2339",
                    AddressString = "2339",
                    Latitude = 25.969538,
                    Longitude = -80.1932,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1402",
                    AddressString = "1402",
                    Latitude = 25.72308,
                    Longitude = -80.444812,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "110",
                    AddressString = "110",
                    Latitude = 25.657503,
                    Longitude = -80.438148,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "739",
                    AddressString = "739",
                    Latitude = 25.792532,
                    Longitude = -80.154341,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "138",
                    AddressString = "138",
                    Latitude = 25.794866,
                    Longitude = -80.12856,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1909",
                    AddressString = "1909",
                    Latitude = 25.637132,
                    Longitude = -80.437514,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1003",
                    AddressString = "1003",
                    Latitude = 25.945725,
                    Longitude = -80.211682,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1828",
                    AddressString = "1828",
                    Latitude = 25.762353,
                    Longitude = -80.370667,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2939",
                    AddressString = "2939",
                    Latitude = 25.971078,
                    Longitude = -80.200512,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "453",
                    AddressString = "453",
                    Latitude = 25.464372,
                    Longitude = -80.524332,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3241",
                    AddressString = "3241",
                    Latitude = 25.751185,
                    Longitude = -80.302878,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1706",
                    AddressString = "1706",
                    Latitude = 25.851668,
                    Longitude = -80.232909,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2340",
                    AddressString = "2340",
                    Latitude = 25.962961,
                    Longitude = -80.199049,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2241",
                    AddressString = "2241",
                    Latitude = 25.909147,
                    Longitude = -80.226061,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1903",
                    AddressString = "1903",
                    Latitude = 25.709181,
                    Longitude = -80.285848,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3647",
                    AddressString = "3647",
                    Latitude = 25.618706,
                    Longitude = -80.317525,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2271",
                    AddressString = "2271",
                    Latitude = 25.578331,
                    Longitude = -80.390219,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2675",
                    AddressString = "2675",
                    Latitude = 25.552568,
                    Longitude = -80.374517,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "132",
                    AddressString = "132",
                    Latitude = 25.601348,
                    Longitude = -80.388065,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2349",
                    AddressString = "2349",
                    Latitude = 25.559866,
                    Longitude = -80.400218,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2555",
                    AddressString = "2555",
                    Latitude = 25.625891,
                    Longitude = -80.434198,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3572",
                    AddressString = "3572",
                    Latitude = 25.916935,
                    Longitude = -80.168832,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "770",
                    AddressString = "770",
                    Latitude = 25.680304,
                    Longitude = -80.33862,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3206",
                    AddressString = "3206",
                    Latitude = 25.745084,
                    Longitude = -80.265924,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "646",
                    AddressString = "646",
                    Latitude = 25.763829,
                    Longitude = -80.400425,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2958",
                    AddressString = "2958",
                    Latitude = 25.751368,
                    Longitude = -80.334104,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1634",
                    AddressString = "1634",
                    Latitude = 25.797373,
                    Longitude = -80.208759,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1355",
                    AddressString = "1355",
                    Latitude = 25.707983,
                    Longitude = -80.320925,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "1790",
                    AddressString = "1790",
                    Latitude = 25.952782,
                    Longitude = -80.20827,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1567",
                    AddressString = "1567",
                    Latitude = 25.899359,
                    Longitude = -80.149139,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1359",
                    AddressString = "1359",
                    Latitude = 25.945685,
                    Longitude = -80.324524,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1335",
                    AddressString = "1335",
                    Latitude = 25.907162,
                    Longitude = -80.183916,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1473",
                    AddressString = "1473",
                    Latitude = 25.921492,
                    Longitude = -80.396818,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "1414",
                    AddressString = "1414",
                    Latitude = 25.672442,
                    Longitude = -80.387422,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3867",
                    AddressString = "3867",
                    Latitude = 25.927522,
                    Longitude = -80.319546,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "3605",
                    AddressString = "3605",
                    Latitude = 25.948107,
                    Longitude = -80.2545,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2645",
                    AddressString = "2645",
                    Latitude = 25.457214,
                    Longitude = -80.483918,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "513",
                    AddressString = "513",
                    Latitude = 25.853753,
                    Longitude = -80.191203,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2460",
                    AddressString = "2460",
                    Latitude = 25.676155,
                    Longitude = -80.321218,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "701",
                    AddressString = "701",
                    Latitude = 25.904623,
                    Longitude = -80.343929,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "617",
                    AddressString = "617",
                    Latitude = 25.768295,
                    Longitude = -80.290599,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3797",
                    AddressString = "3797",
                    Latitude = 25.65354,
                    Longitude = -80.34634,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1864",
                    AddressString = "1864",
                    Latitude = 25.889653,
                    Longitude = -80.2313,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2763",
                    AddressString = "2763",
                    Latitude = 25.654347,
                    Longitude = -80.338129,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3703",
                    AddressString = "3703",
                    Latitude = 25.853331,
                    Longitude = -80.292184,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "474",
                    AddressString = "474",
                    Latitude = 25.617776,
                    Longitude = -80.418907,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2503",
                    AddressString = "2503",
                    Latitude = 25.669214,
                    Longitude = -80.277655,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "267",
                    AddressString = "267",
                    Latitude = 25.669394,
                    Longitude = -80.386716,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "2066",
                    AddressString = "2066",
                    Latitude = 25.52106,
                    Longitude = -80.41158,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3990",
                    AddressString = "3990",
                    Latitude = 25.75582,
                    Longitude = -80.256201,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2629",
                    AddressString = "2629",
                    Latitude = 25.751941,
                    Longitude = -80.243426,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3775",
                    AddressString = "3775",
                    Latitude = 25.621006,
                    Longitude = -80.318392,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "3166",
                    AddressString = "3166",
                    Latitude = 25.84736,
                    Longitude = -80.368358,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2765",
                    AddressString = "2765",
                    Latitude = 25.761182,
                    Longitude = -80.218515,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "2761",
                    AddressString = "2761",
                    Latitude = 25.769437,
                    Longitude = -80.416204,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "464",
                    AddressString = "464",
                    Latitude = 25.890699,
                    Longitude = -80.184342,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "965",
                    AddressString = "965",
                    Latitude = 25.913799,
                    Longitude = -80.185378,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2997",
                    AddressString = "2997",
                    Latitude = 25.83706,
                    Longitude = -80.124102,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "294",
                    AddressString = "294",
                    Latitude = 25.662326,
                    Longitude = -80.288628,
                    Tags = new string[]
                    {
                        "TERRITORY 01"
                    }
                },
                new Address()
                {
                    Alias = "3691",
                    AddressString = "3691",
                    Latitude = 25.485601,
                    Longitude = -80.421705,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    Alias = "1342",
                    AddressString = "1342",
                    Latitude = 25.903451,
                    Longitude = -80.171619,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "564",
                    AddressString = "564",
                    Latitude = 25.946412,
                    Longitude = -80.162314,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "852",
                    AddressString = "852",
                    Latitude = 25.862815,
                    Longitude = -80.275828,
                    Tags = new string[]
                    {
                        "TERRITORY 03"
                    }
                },
                new Address()
                {
                    Alias = "2543",
                    AddressString = "2543",
                    Latitude = 25.6776,
                    Longitude = -80.414642,
                    Tags = new string[]
                    {
                        "TERRITORY 02"
                    }
                },
                new Address()
                {
                    AddressString = "DEPOT",
                    Latitude = 25.723025,
                    Longitude = -80.452883,
                    Tags = new string[]
                    {
                        ""
                    },
                    IsDepot = true,
                    Time = 0
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