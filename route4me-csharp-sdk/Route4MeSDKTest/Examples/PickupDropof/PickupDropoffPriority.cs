using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example referes to the process of creating an optimization 
        /// with pickup/dropof and address priority. 
        /// </summary>
        public void PickupDropoffPriority()
        {
            // Note: use an API key with permission for pickup/dropoff operations.
            var route4Me = new Route4MeManager("E60AA4276C4A321FE4AF62D0705D346E");

            var routeParams = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteTime = 28800,
                Parts = 99,
                LeftTurn = 1,
                Uturn = 1,
                VehicleCapacity = 4,
                UseMixedPickupDeliveryDemands = false,
                RouteName = $"Pickup Dropoff Priority Example {DateTime.UtcNow}",
                Optimize = Optimize.Time.Description(),
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var addresses = new List<Address>()
            {
             #region Pikup Addresses
                new Address()
                {
                    AddressString = "455 S 4th St, Louisville, KY 40202",
                    Alias = "DEPOT",
                    IsDepot = true,
                    Latitude = 38.251698,
                    Longitude = -85.757308,
                    Time = 0
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0001",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 840*7,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0001" }
                    }
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0002",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0002" }
                    }
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0003",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0003" }
                    }
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0004",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0004" }
                    }
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0005",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0005" }
                    }
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0006",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0006" }
                    }
                },
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    Alias = "STORAGE FACILITY",
                    Pickup = "PD0007",
                    AddressStopType = AddressStopType.PickUp.Description(),
                    Priority = 1,
                    Pieces = 1,
                    Latitude = 38.141598,
                    Longitude = -85.793846,
                    Time = 0,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0007" }
                    }
                },

                #endregion

                #region Dropoff Addresses

                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Alias = "Customer 001",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0001",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0001" }
                    }
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Alias = "Customer 002",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0002",
                    Joint = true,
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0002" }
                    }
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Alias = "Customer 003",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0003",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0003" }
                    }
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Alias = "Customer 004",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0004",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0004" }
                    }
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Alias = "Customer 005",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0005",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0005" }
                    }
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Alias = "Customer 006",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0006",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0006" }
                    }
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Alias = "Customer 007",
                    AddressStopType= AddressStopType.Delivery.Description(),
                    Pieces = 1,
                    Dropoff = "PD0007",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 840,
                    CustomFields = new Dictionary<string, string>()
                    {
                        { "GOOD_TYPE", "TYPE0007" }
                    }
                }

                #endregion
            };

            var optParams = new OptimizationParameters()
            {
                Parameters = routeParams,
                Addresses = addresses.ToArray()
            };

            var dataObject = route4Me.RunOptimization(optParams, out string errorString);

            OptimizationsToRemove = new List<string>()
            {
                dataObject?.OptimizationProblemId ?? null
            };

            if (dataObject == null && dataObject.GetType() != typeof(DataObject))
            {
                Console.WriteLine(
                    "PickupDropoffJoint failed" +
                    Environment.NewLine +
                    "Cannot create the optimization. " +
                    Environment.NewLine +
                    errorString
                );

                return;
            }

            if ((dataObject?.Routes?.Length ?? 0) < 1)
            {
                Console.WriteLine("The optimization doesn't contain route");
                RemoveTestOptimizations();
                return;
            }

            var routeId = dataObject.Routes[0].RouteId;

            if ((routeId?.Length ?? 0) < 32)
            {
                Console.WriteLine("The route ID is not valid");
                RemoveTestOptimizations();
                return;
            }

            var routeQueryParameters = new RouteParametersQuery()
            {
                RouteId = routeId
            };

            var routePickDrop = route4Me.GetRoute(routeQueryParameters, out errorString);

            PrintExampleRouteResult(routePickDrop, errorString);

            RemoveTestOptimizations();
        }
    }
}
