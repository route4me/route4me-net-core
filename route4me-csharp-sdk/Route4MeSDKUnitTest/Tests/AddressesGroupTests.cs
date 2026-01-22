using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary;

using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    public class AddressesGroupTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;
        private TestDataRepository _tdr;
        private List<string> _lsOptimizationIDs;

        [OneTimeSetUp]
        public void AddressGroupInitialize()
        {
            _lsOptimizationIDs = new List<string>();

            _tdr = new TestDataRepository();

            var result = _tdr.RunSingleDriverRoundTrip();

            Assert.IsTrue(
                result,
                "Single Driver Round Trip generation failed.");

            Assert.IsTrue(
                _tdr.SDRT_route.Addresses.Length > 0,
                "The route has no addresses.");

            _lsOptimizationIDs.Add(_tdr.SDRT_optimization_problem_id);
        }

        [Test]
        public void GetAddressTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var routeIdToMoveTo = _tdr.SDRT_route_id;
            Assert.IsNotNull(
                routeIdToMoveTo,
                "routeId_SingleDriverRoundTrip is null.");

            var addressId = _tdr.DataObjectSDRT != null &&
                            _tdr.DataObjectSDRT.Routes != null &&
                            _tdr.DataObjectSDRT.Routes.Length > 0 &&
                            _tdr.DataObjectSDRT.Routes[0].Addresses.Length > 1 &&
                            _tdr.DataObjectSDRT.Routes[0].Addresses[1].RouteDestinationId != null
                ? _tdr.DataObjectSDRT.Routes[0].Addresses[1].RouteDestinationId.Value
                : 0;

            var addressParameters = new AddressParameters
            {
                RouteId = routeIdToMoveTo,
                RouteDestinationId = addressId,
                Notes = true
            };

            // Run the query
            var dataObject = route4Me.GetAddress(
                addressParameters,
                out var errorString);

            Assert.IsNotNull(dataObject, "GetAddressTest failed. " + errorString);
        }

        [Test]
        public void AddDestinationToOptimizationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            // Prepare the address that we are going to add to an existing route optimization
            var addresses = new[]
            {
                new Address
                {
                    AddressString = "717 5th Ave New York, NY 10021",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                }
            };

            // Optionally change any route parameters, such as maximum route duration, 
            // maximum cubic constraints, etc.
            var optimizationParameters = new OptimizationParameters
            {
                OptimizationProblemID = _tdr.SDRT_optimization_problem_id,
                Addresses = addresses,
                ReOptimize = true
            };

            // Execute the optimization to re-optimize and rebalance 
            // all the routes in this optimization
            var dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out var errorString);

            _tdr.SDRT_route_id = dataObject.Routes.Length > 0
                ? dataObject.Routes[0].RouteId
                : "";

            Assert.IsNotNull(
                _tdr.DataObjectSDRT,
                "AddDestinationToOptimization and reoptimized Test  failed. " + errorString);

            optimizationParameters.ReOptimize = false;
            dataObject = route4Me.UpdateOptimization(
                optimizationParameters,
                out errorString);

            _tdr.SDRT_route_id = dataObject.Routes.Length > 0
                ? dataObject.Routes[0].RouteId
                : "";

            Assert.IsNotNull(
                _tdr.DataObjectSDRT,
                "AddDestinationToOptimization and not reoptimized Test  failed. " + errorString);
        }

        [Test]
        public void RemoveDestinationFromOptimizationTest()
        {
            var destinationToRemove = _tdr.SDRT_route.Addresses[_tdr.SDRT_route.Addresses.Length - 2];

            var route4Me = new Route4MeManager(c_ApiKey);

            var OptimizationProblemId = _tdr.SDRT_optimization_problem_id;
            Assert.IsNotNull(OptimizationProblemId, "OptimizationProblemId is null.");

            var destinationId = destinationToRemove.RouteDestinationId != null
                ? Convert.ToInt32(destinationToRemove.RouteDestinationId)
                : -1;
            Assert.AreNotEqual(-1, "destinationId is null.");

            // Run the query
            var removed = route4Me.RemoveDestinationFromOptimization(
                OptimizationProblemId,
                destinationId,
                out var errorString);

            Assert.IsTrue(
                removed,
                "RemoveDestinationFromOptimizationTest failed. " + errorString);

            _tdr.SDRT_route.Addresses = _tdr.SDRT_route.Addresses.Where(s => s != destinationToRemove).ToArray();
        }

        [Test]
        public void AddRouteDestinationsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = _tdr.SDRT_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            // Prepare the addresses

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.143526,
                    Longitude = -83.240354,
                    Time = 0
                },

                new Address
                {
                    AddressString = "222 Blake Cir Milledgeville GA 31061",
                    Latitude = 33.177852,
                    Longitude = -83.263535,
                    Time = 0
                }
            };

            #endregion

            // Run the query
            var optimalPosition = true;

            var destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out var errorString);

            Assert.That(destinationIds, Is.InstanceOf<int[]>(), "AddRouteDestinationsTest failed. " + errorString);
        }

        [Test]
        public void AddRouteDestinationsWithNoAddressStringTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = _tdr.SDRT_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            // Prepare the addresses

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    Latitude = 30.143526,
                    Longitude = -87.240354,
                    Time = 0
                },

                new Address
                {
                    Latitude = 30.177852,
                    Longitude = -87.263535,
                    Time = 0
                }
            };

            #endregion

            // Run the query
            var optimalPosition = true;

            var destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out var errorString);

            Assert.That(destinationIds.Length, Is.EqualTo(2));
        }

        [Test]
        public void AddInvalidRouteDestinationsWithNoAddressStringTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = _tdr.SDRT_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            // Prepare the addresses

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                },

                new Address
                {
                }
            };

            #endregion

            // Run the query
            var optimalPosition = true;

            var destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out var errorString);

            Assert.That(errorString, Is.Not.Empty);
        }

        [Test]
        public void AddRouteDestinationInSpecificPositionTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = _tdr.SDRT_route_id;

            Assert.IsNotNull(route_id, "rote_id is null.");

            // Prepare the addresses

            #region Addresses

            Address[] addresses =
            {
                new Address
                {
                    AddressString = "146 Bill Johnson Rd NE Milledgeville GA 31061",
                    Latitude = 33.143526,
                    Longitude = -83.240354,
                    SequenceNo = 3,
                    Time = 0
                }
            };

            #endregion

            // Run the query
            var optimalPosition = false;

            var destinationIds = route4Me.AddRouteDestinations(
                route_id,
                addresses,
                optimalPosition,
                out var errorString);

            Assert.That(destinationIds, Is.InstanceOf<int[]>(), "AddRouteDestinationInSpecificPositionTest failed. " + errorString);
        }

        [Test]
        public void RemoveRouteDestinationTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var route_id = _tdr.SDRT_route_id;
            ;
            Assert.IsNotNull(route_id, "rote_id is null.");

            var destinationToRemove = _tdr.SDRT_route.Addresses[_tdr.SDRT_route.Addresses.Length - 2];

            // Run the query
            var deleted = route4Me.RemoveRouteDestination(
                route_id,
                (int)destinationToRemove.RouteDestinationId,
                out var errorString);

            Assert.IsTrue(deleted, "RemoveRouteDestinationTest failed. " + errorString);

            _tdr.SDRT_route.Addresses = _tdr.SDRT_route.Addresses.Where(s => s != destinationToRemove).ToArray();
        }

        [Test]
        public void MarkAddressAsMarkedAsDepartedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = _tdr.SDRT_route_id,
                RouteDestinationId = _tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(_tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsDeparted = true
            };

            // Run the query
            var resultAddress = route4Me.MarkAddressAsMarkedAsDeparted(
                aParams,
                out var errorString);

            Assert.IsNotNull(
                resultAddress,
                "MarkAddressAsMarkedAsDepartedTest. " + errorString);
        }

        [Test]
        public void MarkAddressAsMarkedAsVisitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = _tdr.SDRT_route_id,
                RouteDestinationId = _tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(_tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsVisited = true
            };

            // Run the query
            var resultAddress = route4Me.MarkAddressAsMarkedAsVisited(
                aParams,
                out var errorString);

            Assert.IsNotNull(
                resultAddress,
                "MarkAddressAsMarkedAsVisitedTest. " + errorString);
        }

        [Test]
        public void MarkAddressDepartedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var addressId = _tdr.SDRT_route.Addresses[1].RouteDestinationId != null
                ? Convert.ToInt32(_tdr.SDRT_route.Addresses[1].RouteDestinationId)
                : -1;

            var visitedParams = new AddressParameters
            {
                RouteId = _tdr.SDRT_route_id,
                AddressId = addressId,
                IsVisited = true
            };

            var visitedResult = route4Me.MarkAddressVisited(visitedParams, out var visitedError);

            Assert.That(
                visitedResult,
                Is.GreaterThan(0),
                "Failed to mark address as visited before departure. " + visitedError);

            var departedParams = new AddressParameters
            {
                RouteId = _tdr.SDRT_route_id,
                AddressId = addressId,
                IsDeparted = true
            };

            var departedResult = route4Me.MarkAddressDeparted(departedParams, out var departedError);

            Assert.That(
                departedResult,
                Is.GreaterThan(0),
                "MarkAddressDepartedTest failed. " + departedError);
        }

        [Test]
        public void MarkAddressVisitedTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

            var aParams = new AddressParameters
            {
                RouteId = _tdr.SDRT_route_id,
                AddressId = _tdr.SDRT_route.Addresses[0].RouteDestinationId != null
                    ? Convert.ToInt32(_tdr.SDRT_route.Addresses[0].RouteDestinationId)
                    : -1,
                IsVisited = true
            };

            // Run the query
            object oResult = route4Me.MarkAddressVisited(aParams, out var errorString);

            Assert.IsNotNull(oResult, "MarkAddressVisitedTest. " + errorString);
        }

        [Test]
        public void MarkAddressDepartedWithoutVisitingReturnsErrorWithEnabledErrorHandling()
        {
            var route4Me = new Route4MeManager(c_ApiKey);
            Route4MeConfig.UseImprovedErrorHandling = true;

            var addressId = _tdr.SDRT_route.Addresses[2].RouteDestinationId != null
                ? Convert.ToInt32(_tdr.SDRT_route.Addresses[2].RouteDestinationId)
                : -1;

            var departedParams = new AddressParameters
            {
                RouteId = _tdr.SDRT_route_id,
                AddressId = addressId,
                IsDeparted = true
            };

            var departedResult = route4Me.MarkAddressDeparted(departedParams, out var departedError);

            Assert.That(departedResult, Is.EqualTo(0), "Expected failure when departing without visiting first");
            Assert.That(departedError, Is.Not.Null.And.Not.Empty, "Error message should be provided on failure");

            // Log the error for debugging
            Console.WriteLine($"Error message received: {departedError}");
            Route4MeConfig.UseImprovedErrorHandling = false;
        }

        [OneTimeTearDown]
        public void AddressGroupCleanup()
        {
            var result = _tdr.RemoveOptimization(_lsOptimizationIDs.ToArray());

            Assert.IsTrue(
                result,
                "Removing of the testing optimization problem failed.");

            _tdr = null;
        }
    }
}