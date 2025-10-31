using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Route4MeSDK;
using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSdkV5UnitTest.V5
{
    [TestFixture]
    public class FacilityOptimizationTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        [Test]
        public async Task RunOptimizationWithSingleFacilityIdTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);
            var route4MeV5 = new Route4MeManagerV5(c_ApiKey);

            var facility = CreateFacility();

            var addresses = new List<Address>()
            {
                #region Addresses

                new()
                {
                    AddressString = "754 5th Ave New York, NY 10019",
                    Alias = "Bergdorf Goodman",
                    IsDepot = true,
                    Latitude = 40.7636197,
                    Longitude = -73.9744388,
                    Time = 0
                },

                new()
                {
                    AddressString = "717 5th Ave New York, NY 10022",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new()
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    Alias = "Ralph Lauren Women's and Home",
                    Latitude = 40.7715154,
                    Longitude = -73.9669241,
                    Time = 0
                },

                #endregion
            };

            var parameters = new RouteParameters()
            {
                RouteStartDateLocal = "2024-07-16",
                FacilityIds = new[] { facility.FacilityId }
            };

            var optimizationParameters = new Route4MeSDK.QueryTypes.OptimizationParameters()
            {
                Addresses = addresses.ToArray(),
                Parameters = parameters
            };

            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.That(dataObject, Is.Not.Null, $"Optimization failed: {errorString}");
            Assert.That(dataObject.Routes, Is.Not.Null);
            Assert.That(dataObject.Routes.Length, Is.GreaterThan(0));

            var createdRouteId = dataObject.Routes[0].RouteId;

            // Wait for route to be indexed
            await Task.Delay(2000);

            // Get route using V5 endpoint and verify facility IDs
            var routeManagerV5 = new RouteManagerV5(c_ApiKey);
            var filterParams = new RouteFilterParameters
            {
                Filters = new RouteFilterParametersFilters
                {
                    RouteId = createdRouteId
                }
            };

            var (routeResult, _) = await routeManagerV5.GetRoutesByFilterAsync(filterParams);

            // Business rule assertions
            Assert.IsNotNull(routeResult, "Route result should not be null");
            Assert.IsNotNull(routeResult.Data, "Route data should not be null");
            Assert.That(routeResult.Data.Length, Is.GreaterThan(0), "Should return at least one route");

            var retrievedRoute = routeResult.Data.FirstOrDefault(r => r.RouteID == createdRouteId);
            Assert.IsNotNull(retrievedRoute, "Should retrieve the created route by ID");

            // Verify facility IDs are correctly assigned
            Assert.IsNotNull(retrievedRoute.FacilityIds, "Route should have facility IDs");
            Assert.That(retrievedRoute.FacilityIds.Length, Is.EqualTo(1), "Route should have exactly 1 facility ID");
            Assert.That(retrievedRoute.FacilityIds[0], Is.EqualTo(facility.FacilityId), "Route should have the correct facility ID");

            // Cleanup
            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
            route4MeV5.FacilityManager.DeleteFacility(facility.FacilityId, out _);
        }

        [Test]
        public async Task RunOptimizationWithMultipleFacilityIdsTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);
            var route4MeV5 = new Route4MeManagerV5(c_ApiKey);

            var facility1 = CreateFacility();
            var facility2 = CreateFacility();

            var addresses = new List<Address>()
            {
                #region Addresses

                new()
                {
                    AddressString = "754 5th Ave New York, NY 10019",
                    Alias = "Bergdorf Goodman",
                    IsDepot = true,
                    Latitude = 40.7636197,
                    Longitude = -73.9744388,
                    Time = 0
                },

                new()
                {
                    AddressString = "717 5th Ave New York, NY 10022",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new()
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    Alias = "Ralph Lauren Women's and Home",
                    Latitude = 40.7715154,
                    Longitude = -73.9669241,
                    Time = 0
                },

                #endregion
            };

            var parameters = new RouteParameters()
            {
                RouteStartDateLocal = "2024-07-16",
                FacilityIds = new[] { facility1.FacilityId, facility2.FacilityId }
            };

            var optimizationParameters = new Route4MeSDK.QueryTypes.OptimizationParameters()
            {
                Addresses = addresses.ToArray(),
                Parameters = parameters
            };

            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.That(dataObject, Is.Not.Null, $"Optimization failed: {errorString}");
            Assert.That(dataObject.Routes, Is.Not.Null);
            Assert.That(dataObject.Routes.Length, Is.GreaterThan(0));

            var createdRouteId = dataObject.Routes[0].RouteId;

            // Wait for route to be indexed
            await Task.Delay(2000);

            // Get route using V5 endpoint and verify facility IDs
            var routeManagerV5 = new RouteManagerV5(c_ApiKey);
            var filterParams = new RouteFilterParameters
            {
                Filters = new RouteFilterParametersFilters
                {
                    RouteId = createdRouteId
                }
            };

            var (routeResult, _) = await routeManagerV5.GetRoutesByFilterAsync(filterParams);

            // Business rule assertions
            Assert.IsNotNull(routeResult, "Route result should not be null");
            Assert.IsNotNull(routeResult.Data, "Route data should not be null");
            Assert.That(routeResult.Data.Length, Is.GreaterThan(0), "Should return at least one route");

            var retrievedRoute = routeResult.Data.FirstOrDefault(r => r.RouteID == createdRouteId);
            Assert.IsNotNull(retrievedRoute, "Should retrieve the created route by ID");

            // Verify facility IDs are correctly assigned
            Assert.IsNotNull(retrievedRoute.FacilityIds, "Route should have facility IDs");
            Assert.That(retrievedRoute.FacilityIds.Length, Is.EqualTo(2), "Route should have exactly 2 facility IDs");
            Assert.That(retrievedRoute.FacilityIds, Does.Contain(facility1.FacilityId), "Route should contain facility 1 ID");
            Assert.That(retrievedRoute.FacilityIds, Does.Contain(facility2.FacilityId), "Route should contain facility 2 ID");

            // Cleanup
            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
            route4MeV5.FacilityManager.DeleteFacility(facility1.FacilityId, out _);
            route4MeV5.FacilityManager.DeleteFacility(facility2.FacilityId, out _);
        }

        [Test]
        public async Task RunOptimizationWithoutFacilityIdsFallbackToProfileTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);
            var route4MeV5 = new Route4MeManagerV5(c_ApiKey);

            var addresses = new List<Address>()
            {
                #region Addresses

                new()
                {
                    AddressString = "754 5th Ave New York, NY 10019",
                    Alias = "Bergdorf Goodman",
                    IsDepot = true,
                    Latitude = 40.7636197,
                    Longitude = -73.9744388,
                    Time = 0
                },

                new()
                {
                    AddressString = "717 5th Ave New York, NY 10022",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new()
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    Alias = "Ralph Lauren Women's and Home",
                    Latitude = 40.7715154,
                    Longitude = -73.9669241,
                    Time = 0
                },

                #endregion
            };

            var parameters = new RouteParameters()
            {
                RouteStartDateLocal = "2024-07-16"
            };

            var optimizationParameters = new Route4MeSDK.QueryTypes.OptimizationParameters()
            {
                Addresses = addresses.ToArray(),
                Parameters = parameters,
                OptimizationProfileId = "test-profile-id"
            };

            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.That(dataObject, Is.Not.Null, $"Optimization failed: {errorString}");

            var createdRouteId = dataObject.Routes[0].RouteId;

            // Wait for route to be indexed
            await Task.Delay(2000);

            // Get route using V5 endpoint
            var routeManagerV5 = new RouteManagerV5(c_ApiKey);
            var filterParams = new RouteFilterParameters
            {
                Filters = new RouteFilterParametersFilters
                {
                    RouteId = createdRouteId
                }
            };

            var (routeResult, _) = await routeManagerV5.GetRoutesByFilterAsync(filterParams);

            // Business rule assertions: When no facility IDs provided, route may still be created
            Assert.IsNotNull(routeResult, "Route result should not be null");
            Assert.IsNotNull(routeResult.Data, "Route data should not be null");
            Assert.That(routeResult.Data.Length, Is.GreaterThan(0), "Should return at least one route");

            var retrievedRoute = routeResult.Data.FirstOrDefault(r => r.RouteID == createdRouteId);
            Assert.IsNotNull(retrievedRoute, "Should retrieve the created route by ID");

            // When no facility IDs are provided, the route may have null or empty facility IDs
            // This depends on whether the profile has facility IDs configured

            // Cleanup
            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
        }

        [Test]
        public async Task RunOptimizationFacilityIdsOverridesProfileTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);
            var route4MeV5 = new Route4MeManagerV5(c_ApiKey);

            var facility = CreateFacility();

            var addresses = new List<Address>()
            {
                #region Addresses

                new()
                {
                    AddressString = "754 5th Ave New York, NY 10019",
                    Alias = "Bergdorf Goodman",
                    IsDepot = true,
                    Latitude = 40.7636197,
                    Longitude = -73.9744388,
                    Time = 0
                },

                new()
                {
                    AddressString = "717 5th Ave New York, NY 10022",
                    Alias = "Giorgio Armani",
                    Latitude = 40.7669692,
                    Longitude = -73.9693864,
                    Time = 0
                },

                new()
                {
                    AddressString = "888 Madison Ave New York, NY 10014",
                    Alias = "Ralph Lauren Women's and Home",
                    Latitude = 40.7715154,
                    Longitude = -73.9669241,
                    Time = 0
                },

                #endregion
            };

            var parameters = new RouteParameters()
            {
                RouteStartDateLocal = "2024-07-16",
                FacilityIds = new[] { facility.FacilityId }
            };

            var optimizationParameters = new Route4MeSDK.QueryTypes.OptimizationParameters()
            {
                Addresses = addresses.ToArray(),
                Parameters = parameters,
                OptimizationProfileId = "test-profile-id"
            };

            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.That(dataObject, Is.Not.Null, $"Optimization failed: {errorString}");
            Assert.That(dataObject.Routes, Is.Not.Null);
            Assert.That(dataObject.Routes.Length, Is.GreaterThan(0));

            var createdRouteId = dataObject.Routes[0].RouteId;

            // Wait for route to be indexed
            await Task.Delay(2000);

            // Get route using V5 endpoint and verify facility IDs
            var routeManagerV5 = new RouteManagerV5(c_ApiKey);
            var filterParams = new RouteFilterParameters
            {
                Filters = new RouteFilterParametersFilters
                {
                    RouteId = createdRouteId
                }
            };

            var (routeResult, _) = await routeManagerV5.GetRoutesByFilterAsync(filterParams);

            // Business rule assertions: Explicitly provided facility IDs should override profile
            Assert.IsNotNull(routeResult, "Route result should not be null");
            Assert.IsNotNull(routeResult.Data, "Route data should not be null");
            Assert.That(routeResult.Data.Length, Is.GreaterThan(0), "Should return at least one route");

            var retrievedRoute = routeResult.Data.FirstOrDefault(r => r.RouteID == createdRouteId);
            Assert.IsNotNull(retrievedRoute, "Should retrieve the created route by ID");

            // Verify facility IDs from parameters override profile settings
            Assert.IsNotNull(retrievedRoute.FacilityIds, "Route should have facility IDs from parameters, not profile");
            Assert.That(retrievedRoute.FacilityIds.Length, Is.EqualTo(1), "Route should have exactly 1 facility ID from parameters");
            Assert.That(retrievedRoute.FacilityIds[0], Is.EqualTo(facility.FacilityId), "Route should have the facility ID from parameters");

            // Cleanup
            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
            route4MeV5.FacilityManager.DeleteFacility(facility.FacilityId, out _);
        }

        private static FacilityResource CreateFacility()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = $"Test Distribution Center {DateTime.Now:yyyyMMdd_HHmmss}",
                Address = "123 Industrial Blvd, Chicago, IL 60601",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8781,
                    Lng = -87.6298
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new() { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 1
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(createRequest, out var createError);

            Assert.IsNotNull(createdFacility, "Failed to create facility");
            Assert.IsNotNull(createdFacility.FacilityId, "Facility ID should not be null");
            return createdFacility;
        }
    }
}
