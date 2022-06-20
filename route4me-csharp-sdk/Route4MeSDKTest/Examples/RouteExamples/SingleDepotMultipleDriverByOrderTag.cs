using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        List<string> ordersToRemove;

        /// <summary>
        /// The example refers to the process of creating an optimization 
        /// with single-depot, multi-driver by order custom data.
        /// </summary>
        public async void SingleDepotMultipleDriverByOrderTag()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            CreateOrders();

            var filterParams = new OrderFilterParameters()
            {
                Offset = 0,
                Limit = 10,
                Filter = new FilterDetails()
                {
                    Terms = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                }
            };

            var filteringResult = await route4Me.FilterOrdersAsync(filterParams);

            if ((filteringResult.Item1?.Length ?? 0)<1)
            {
                Console.WriteLine("Cannot create the orders");
                return;
            }


            // Prepare the addresses
            var addresses = new List<Address>();

            foreach (var order in filteringResult.Item1)
            {
                addresses.Add(new Address { OrderId = order.OrderId });
            }


            // Set parameters
            var parameters = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_MD,
                RouteName = "Single Depot, Multiple Driver, No Time Window",

                RouteDate = R4MeUtils.ConvertToUnixTimestamp(DateTime.UtcNow.Date.AddDays(1)),
                RouteTime = 60 * 60 * 7,
                RT = true,
                RouteMaxDuration = 86400,
                VehicleCapacity = 20,
                VehicleMaxDistanceMI = 99999,
                Parts = 4,

                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description(),
                Metric = Metric.Matrix
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses.ToArray(),
                Parameters = parameters
            };

            // Run the query
            var dataObjectResult = await route4Me.RunOptimizationAsync( optimizationParameters);

            OptimizationsToRemove = new List<string>()
            {
                dataObjectResult.Item1?.OptimizationProblemId ?? null
            };

            // Output the result
            PrintExampleOptimizationResult(dataObjectResult.Item1, dataObjectResult.Item2);

            RemoveTestOptimizations();

            RemoveOrders();
        }

        async void CreateOrders()
        {
            ordersToRemove = new List<string>();
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            // Prepare the orders
            var orders = new Order[]
            {
                new Order() {
                    Address1 = "new york, ny",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Manhatten Island NYC",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "503 W139 St, NY,NY",
                    CachedLat = 40.7109062,
                    CachedLng = -74.0091848,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "203 grand st, new york, ny",
                    CachedLat = 40.7188990,
                    CachedLng = -73.9967320,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "119 Church Street",
                    CachedLat = 40.7137757,
                    CachedLng = -74.0088238,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "new york ny",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "ny",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "broadway street, new york",
                    CachedLat = 40.7191551,
                    CachedLng = -74.0020849,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Ground Zero, Vesey-Liberty-Church-West Streets New York NY 10038",
                    CachedLat = 40.7233126,
                    CachedLng = -74.0116602,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "226 ilyssa way staten lsland ny",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "185 franklin st.",
                    CachedLat = 40.7192099,
                    CachedLng = -74.0097670,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "new york city,",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "11 e. broaway 11038",
                    CachedLat = 40.7132060,
                    CachedLng = -73.9974019,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Brooklyn Bridge, NY",
                    CachedLat = 40.7053804,
                    CachedLng = -73.9962503,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "World Trade Center Site, NY",
                    CachedLat = 40.7114980,
                    CachedLng = -74.0122990,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "New York Stock Exchange, NY",
                    CachedLat = 40.7074242,
                    CachedLng = -74.0116342,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Wall Street, NY",
                    CachedLat = 40.7079825,
                    CachedLng = -74.0079781,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Trinity Church, NY",
                    CachedLat = 40.7081426,
                    CachedLng = -74.0120511,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "World Financial Center, NY",
                    CachedLat = 40.7104750,
                    CachedLng = -74.0154930,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Federal Hall, NY",
                    CachedLat = 40.7073034,
                    CachedLng = -74.0102734,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Flatiron Building, NY",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "South Street Seaport, NY",
                    CachedLat = 40.7069210,
                    CachedLng = -74.0036380,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Rockefeller Center, NY",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "FAO Schwarz, NY",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Woolworth Building, NY",
                    CachedLat = 40.7123903,
                    CachedLng = -74.0083309,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Met Life Building, NY",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "SOHO/Tribeca, NY",
                    CachedLat = 40.7185650,
                    CachedLng = -74.0120170,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "MacyГўв‚¬в„ўs, NY",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "City Hall, NY, NY",
                    CachedLat = 40.7127047,
                    CachedLng = -74.0058663,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "Macy&amp;acirc;в‚¬в„ўs, NY",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "1452 potter blvd bayshore ny",
                    CachedLat = 40.7142691,
                    CachedLng = -74.0059729,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "55 Church St. New York, NY",
                    CachedLat = 40.7112320,
                    CachedLng = -74.0102680,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                },
                new Order() {
                    Address1 = "55 Church St, New York, NY",
                    CachedLat = 40.7112320,
                    CachedLng = -74.0102680,
                    ExtFieldCustomData = new Dictionary<string, string>()
                    {
                        {"CustomDataKey", "CustomDataValue" }
                    }
                }
            };

            foreach (Order order in orders)
            {
                var result = await route4Me.AddOrderAsync(order);

                if ((result?.Item1?.OrderId ?? null) != null) ordersToRemove.Add(order.OrderId.ToString());
            }
        }

        async void RemoveOrders()
        {
            var route4Me = new Route4MeManager(ActualApiKey);

            var result = await route4Me.RemoveOrdersAsync(ordersToRemove.ToArray());


        }
    }
}
