using System;
using NUnit.Framework;
using Route4MeSDK;
using System.Collections.Generic;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;
using Route4MeSDKUnitTest.Types;

namespace Route4MeSDKUnitTest.Tests
{
    [TestFixture]
    internal class OptimizationTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        [Test]
        public void RunOptimizationWithoutAlgorithmTypeBeingSetTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

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

                new()
                {
                    AddressString = "1011 Madison Ave New York, NY 10075",
                    Alias = "Yigal Azrou'l",
                    Latitude = 40.7772129,
                    Longitude = -73.9669,
                    Time = 0
                },

                new()
                {
                    AddressString = "440 Columbus Ave New York, NY 10024",
                    Alias = "Frank Stella Clothier",
                    Latitude = 40.7808364,
                    Longitude = -73.9732729,
                    Time = 0
                },

                new()
                {
                    AddressString = "324 Columbus Ave #1 New York, NY 10023",
                    Alias = "Liana",
                    Latitude = 40.7803123,
                    Longitude = -73.9793079,
                    Time = 0
                },

                new()
                {
                    AddressString = "110 W End Ave New York, NY 10023",
                    Alias = "Toga Bike Shop",
                    Latitude = 40.7753077,
                    Longitude = -73.9861529,
                    Time = 0
                },

                new()
                {
                    AddressString = "555 W 57th St New York, NY 10019",
                    Alias = "BMW of Manhattan",
                    Latitude = 40.7718005,
                    Longitude = -73.9897716,
                    Time = 0
                },

                new()
                {
                    AddressString = "57 W 57th St New York, NY 10019",
                    Alias = "Verizon Wireless",
                    Latitude = 40.7558695,
                    Longitude = -73.9862019,
                    Time = 0
                },

                #endregion
            };

            var parameters = new RouteParameters()
            {
                RouteStartDateLocal = "2024-07-16"
            };

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses.ToArray(),

                Parameters = parameters
            };

            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.That(dataObject, Is.Not.Null);

            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
        }

        [Test]
        public void RunOptimizationWithSingleFacilityIdTest()
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

            var optimizationParameters = new OptimizationParameters()
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

            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
            route4MeV5.FacilityManager.DeleteFacility(facility.FacilityId, out _);
        }

        [Test]
        public void RunOptimizationWithMultipleFacilityIdsTest()
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

            var optimizationParameters = new OptimizationParameters()
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

            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
            route4MeV5.FacilityManager.DeleteFacility(facility1.FacilityId, out _);
            route4MeV5.FacilityManager.DeleteFacility(facility2.FacilityId, out _);
        }

        [Test]
        public void RunOptimizationWithoutFacilityIdsFallbackToProfileTest()
        {
            var route4Me = new Route4MeManager(c_ApiKey);

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

            var optimizationParameters = new OptimizationParameters()
            {
                Addresses = addresses.ToArray(),
                Parameters = parameters,
                OptimizationProfileId = "test-profile-id"
            };

            var dataObject = route4Me.RunOptimization(
                optimizationParameters,
                out var errorString);

            Assert.That(dataObject, Is.Not.Null, $"Optimization failed: {errorString}");

            route4Me.RemoveOptimization(new[] { dataObject.OptimizationProblemId }, out errorString);
        }

        [Test]
        public void RunOptimizationFacilityIdsOverridesProfileTest()
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

            var optimizationParameters = new OptimizationParameters()
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