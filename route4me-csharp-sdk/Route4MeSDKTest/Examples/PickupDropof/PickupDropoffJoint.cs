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
        /// with pickup/dropof and joint addresses. 
        /// </summary>
        public void PickupDropoffJoint()
        {
            // Note: use an API key with permission for pickup/dropoff operations.
            var route4Me = new Route4MeManager(ActualApiKey);

            var routeParams = new RouteParameters()
            {
                AlgorithmType = AlgorithmType.CVRP_TW_SD,
                RouteTime = 28800,
                VehicleCapacity = 100,
                RouteName = $"Pickup Dropoff Joint Example {DateTime.UtcNow}",
                DistanceUnit = DistanceUnit.MI.Description(),
                DeviceType = DeviceType.Web.Description(),
                TravelMode = TravelMode.Driving.Description()
            };

            var addresses = new List<Address>()
            {
                new Address()
                {
                    AddressString = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    IsDepot = true,
                    Latitude = 38.141598,
                    Longitude = -85.793846
                },
                new Address()
                {
                    AddressString = "1407 MCCOY, Louisville, KY, 40215",
                    Alias = "Pickup - Customer 001",
                    Pickup = "PD0001",
                    Latitude = 38.202496,
                    Longitude = -85.786514,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4805 BELLEVUE AVE, Louisville, KY, 40215",
                    Alias = "Pickup - Customer 004",
                    Pickup = "PD0004",
                    Joint = true,
                    Latitude = 38.178844,
                    Longitude = -85.774864,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "730 CECIL AVENUE, Louisville, KY, 40211",
                    Alias = "Dropoff - Customer 003",
                    Dropoff = "PD0003",
                    Latitude = 38.248684,
                    Longitude = -85.821121,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "650 SOUTH 29TH ST UNIT 315, Louisville, KY, 40211",
                    Latitude = 38.251923,
                    Longitude = -85.800034,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4629 HILLSIDE DRIVE, Louisville, KY, 40216",
                    Alias = "Pickup - Customer 002",
                    Pickup = "PD0002",
                    Latitude = 38.176067,
                    Longitude = -85.824638,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "4738 BELLEVUE AVE, Louisville, KY, 40215",
                    Alias = "Pickup - Customer 003",
                    Pickup = "PD0003",
                    Latitude = 38.179806,
                    Longitude = -85.775558,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "318 SO. 39TH STREET, Louisville, KY, 40212",
                    Alias = "Dropoff - Customer 004",
                    Dropoff = "PD0004",
                    Latitude = 38.259335,
                    Longitude = -85.815094,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "1324 BLUEGRASS AVE, Louisville, KY, 40215",
                    Alias = "Dropoff - Customer 002",
                    Dropoff = "PD0002",
                    Latitude = 38.179253,
                    Longitude = -85.785118,
                    Time = 300
                },
                new Address()
                {
                    AddressString = "7305 ROYAL WOODS DR, Louisville, KY, 40214",
                    Alias = "Dropoff - Customer 001",
                    Dropoff = "PD0001",
                    Latitude = 38.162472,
                    Longitude = -85.792854,
                    Time = 300
                }
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