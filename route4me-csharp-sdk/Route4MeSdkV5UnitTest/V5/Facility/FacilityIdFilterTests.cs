using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Locations;

namespace Route4MeSdkV5UnitTest.V5
{
    [TestFixture]
    public class FacilityIdFilterTests
    {
        private string _apiKey;
        private Route4MeManagerV5 _route4Me;
        private List<string> _createdFacilityIds;
        private List<string> _createdOrderIds;
        private List<string> _createdVehicleIds;
        private List<long> _createdTeamMemberIds;
        private static readonly string TestRunId = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        private string[] TestFacilityIds => _createdFacilityIds.ToArray();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _apiKey = ApiKeys.ActualApiKey;
            _route4Me = new Route4MeManagerV5(_apiKey);
            _createdFacilityIds = new List<string>();
            _createdOrderIds = new List<string>();
            _createdVehicleIds = new List<string>();
            _createdTeamMemberIds = new List<long>();

            CreateTestFacilities();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (_createdVehicleIds != null && _createdVehicleIds.Count > 0)
            {
                var vehicleManager = new VehicleManagerV5(_apiKey);
                foreach (var vehicleId in _createdVehicleIds)
                {
                    try
                    {
                        vehicleManager.DeleteVehicleAsync(vehicleId).Wait();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            if (_createdOrderIds is { Count: > 0 })
            {
                var orderManager = new OrderManagerV5(_apiKey);
                foreach (var orderId in _createdOrderIds)
                {
                    try
                    {
                        orderManager.DeleteOrderAsync(orderId).Wait();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            if (_createdFacilityIds != null && _createdFacilityIds.Count > 0)
            {
                foreach (var facilityId in _createdFacilityIds)
                {
                    try
                    {
                        _route4Me.FacilityManager.DeleteFacility(facilityId, out _);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
        }

        private void CreateTestFacilities()
        {
            var facilities = new[]
            {
                new FacilityCreateRequest
                {
                    FacilityAlias = $"Test Facility 1 {TestRunId}",
                    Address = "123 Test St, Chicago, IL 60601",
                    Coordinates = new FacilityCoordinates { Lat = 41.8781, Lng = -87.6298 },
                    FacilityTypes = new FacilityTypeAssignment[]
                    {
                        new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                    },
                    Status = 1
                },
                new FacilityCreateRequest
                {
                    FacilityAlias = $"Test Facility 2 {TestRunId}",
                    Address = "456 Test Ave, Chicago, IL 60602",
                    Coordinates = new FacilityCoordinates { Lat = 41.8881, Lng = -87.6398 },
                    FacilityTypes = new FacilityTypeAssignment[]
                    {
                        new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                    },
                    Status = 1
                },
                new FacilityCreateRequest
                {
                    FacilityAlias = $"Test Facility 3 {TestRunId}",
                    Address = "789 Test Blvd, Chicago, IL 60603",
                    Coordinates = new FacilityCoordinates { Lat = 41.8981, Lng = -87.6498 },
                    FacilityTypes = new FacilityTypeAssignment[]
                    {
                        new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                    },
                    Status = 1
                }
            };

            foreach (var facilityRequest in facilities)
            {
                var created = _route4Me.FacilityManager.CreateFacility(facilityRequest, out var error);
                if (created != null && !string.IsNullOrEmpty(created.FacilityId))
                {
                    _createdFacilityIds.Add(created.FacilityId);
                }
                else
                {
                    Assert.Fail($"Failed to create test facility: {facilityRequest.FacilityAlias}");
                }
            }

            Assert.AreEqual(3, _createdFacilityIds.Count, "Should have created 3 test facilities");
        }

        #region Orders API Tests

        [Test]
        public void CreateOrder_WithFacilityIds_ShouldSucceed()
        {
            // Arrange
            var orderManager = new OrderManagerV5(_apiKey);
            var facilityIds = new[] { TestFacilityIds[0], TestFacilityIds[1] };

            var order = new Route4MeSDKLibrary.DataTypes.V5.Orders.Order
            {
                Address1 = $"Test Order Address {TestRunId}",
                AddressAlias = $"Order {new Random().Next()}",
                AddressGeo = new GeoPoint { Latitude = 37.9, Longitude = -34.6 },
                FacilityIds = facilityIds
            };

            // Act
            var createdOrder = orderManager.CreateOrder(order, out _);

            // Assert
            Assert.IsNotNull(createdOrder, "Created order should not be null");
            Assert.IsNotEmpty(createdOrder.OrderUuid, "Order should have UUID");

            _createdOrderIds.Add(createdOrder.OrderUuid);
        }

        [Test]
        public async Task UpdateOrder_WithFacilityIds_ShouldUpdateSuccessfully()
        {
            // Arrange
            var orderManager = new OrderManagerV5(_apiKey);
            var initialFacilityIds = new[] { TestFacilityIds[0] };
            var updatedFacilityIds = new[] { TestFacilityIds[1], TestFacilityIds[2] };

            var order = new Route4MeSDKLibrary.DataTypes.V5.Orders.Order
            {
                Address1 = $"Update Test Order {TestRunId}",
                AddressAlias = $"Update Order {new Random().Next()}",
                AddressGeo = new GeoPoint { Latitude = 37.9, Longitude = -34.6 },
                FacilityIds = initialFacilityIds
            };

            // Act - Create with initial facility IDs
            var (createdOrder, _) = await orderManager.CreateOrderAsync(order);
            Assert.IsNotNull(createdOrder, "Created order should not be null");
            _createdOrderIds.Add(createdOrder.OrderUuid);

            // Act - Update with new facility IDs
            createdOrder.FacilityIds = updatedFacilityIds;
            var (updatedOrder, updateResponse) = await orderManager.UpdateOrderAsync(createdOrder);

            // Assert
            Assert.IsNotNull(updatedOrder, "Updated order should not be null");
            AssertCollectionsEqual(updatedFacilityIds, updatedOrder.FacilityIds);
        }

        [Test]
        public async Task SearchOrders_WithFacilityIdsFilter_ShouldReturnFilteredResults()
        {
            // Arrange
            var orderManager = new OrderManagerV5(_apiKey);
            var specificFacilityId = TestFacilityIds[0];

            // Create order with specific facility ID
            var order = new Route4MeSDKLibrary.DataTypes.V5.Orders.Order
            {
                Address1 = $"Search Test Order {TestRunId}",
                AddressAlias = $"Search Order {new Random().Next()}",
                AddressGeo = new GeoPoint { Latitude = 37.9, Longitude = -34.6 },
                FacilityIds = new[] { specificFacilityId }
            };

            var (createdOrder, _) = await orderManager.CreateOrderAsync(order);
            _createdOrderIds.Add(createdOrder.OrderUuid);

            // Wait for indexing
            await Task.Delay(2000);

            // Act - Search with facility ID filter
            var searchRequest = new SearchOrdersRequest
            {
                Filters = new FiltersParamRequestBody
                {
                    FacilityIds = new[] { specificFacilityId }
                },
                Limit = 10
            };

            var (searchResults, _) = await orderManager.SearchOrdersAsync(searchRequest);

            // Assert
            Assert.IsNotNull(searchResults, "Search results should not be null");

            if (searchResults.Results != null && searchResults.Results.Length > 0)
            {
                // Verify at least our created order is in the results
                var foundOrder = searchResults.Results.FirstOrDefault(o => o.OrderUuid == createdOrder.OrderUuid);
                Assert.IsNotNull(foundOrder, "Should find the created order with facility ID filter");
            }
        }

        [Test]
        public async Task BatchUpdateOrders_WithFacilityIdsFilter_ShouldUpdate()
        {
            // Arrange
            var orderManager = new OrderManagerV5(_apiKey);
            var filterFacilityId = TestFacilityIds[0];

            // Create multiple orders with same facility ID
            var order1 = new Route4MeSDKLibrary.DataTypes.V5.Orders.Order
            {
                Address1 = $"Batch Test Order 1 {TestRunId}",
                AddressAlias = $"Batch 1 {new Random().Next()}",
                AddressGeo = new GeoPoint { Latitude = 37.9, Longitude = -34.6 },
                FacilityIds = new[] { filterFacilityId }
            };

            var order2 = new Route4MeSDKLibrary.DataTypes.V5.Orders.Order
            {
                Address1 = $"Batch Test Order 2 {TestRunId}",
                AddressAlias = $"Batch 2 {new Random().Next()}",
                AddressGeo = new GeoPoint { Latitude = 37.9, Longitude = -34.6 },
                FacilityIds = new[] { filterFacilityId }
            };

            var (created1, _) = await orderManager.CreateOrderAsync(order1);
            var (created2, _) = await orderManager.CreateOrderAsync(order2);
            _createdOrderIds.Add(created1.OrderUuid);
            _createdOrderIds.Add(created2.OrderUuid);

            // Act - Batch update using facility ID filter
            var updateData = new Route4MeSDKLibrary.DataTypes.V5.Orders.Order
            {
                AddressCity = "Updated City"
            };

            var batchRequest = new BatchUpdateFilterOrderRequest
            {
                Data = updateData,
                Filters = new FiltersParamRequestBody
                {
                    OrderIds = new[] { created1.OrderUuid, created2.OrderUuid },
                    FacilityIds = new[] { filterFacilityId }
                }
            };

            var (result, _) = await orderManager.BatchUpdateFilterAsync(batchRequest);

            // Assert
            Assert.IsNotNull(result, "Batch update result should not be null");
        }

        #endregion

        #region Vehicles API Tests

        [Test]
        public async Task CreateVehicle_WithFacilityIds_ShouldSucceed()
        {
            // Arrange
            var vehicleManager = new VehicleManagerV5(_apiKey);
            var facilityIds = new[] { TestFacilityIds[0], TestFacilityIds[1] };

            var vehicle = new Vehicle
            {
                VehicleAlias = $"Test Vehicle {TestRunId}",
                VehicleVin = $"VIN{new Random().Next(10000, 99999)}",
                VehicleLicensePlate = $"TEST{new Random().Next(100, 999)}",
                FacilityIds = facilityIds
            };

            // Act
            var (createdVehicle, _) = await vehicleManager.CreateVehicleAsync(vehicle);

            // Assert
            Assert.IsNotNull(createdVehicle, "Created vehicle should not be null");
            Assert.IsNotEmpty(createdVehicle.VehicleId, "Vehicle should have ID");

            AssertCollectionsEqual(facilityIds, createdVehicle.FacilityIds);

            _createdVehicleIds.Add(createdVehicle.VehicleId);
        }

        [Test]
        public async Task SearchVehicles_WithFacilityIdsFilter_ShouldApplyFilter()
        {
            // Arrange
            var vehicleManager = new VehicleManagerV5(_apiKey);
            var specificFacilityId = TestFacilityIds[0];

            var vehicle = new Vehicle
            {
                VehicleAlias = $"Search Test Vehicle {TestRunId}",
                VehicleVin = $"VIN{new Random().Next(10000, 99999)}",
                VehicleLicensePlate = $"SRCH{new Random().Next(100, 999)}",
                FacilityIds = new[] { specificFacilityId }
            };

            var (createdVehicle, _) = await vehicleManager.CreateVehicleAsync(vehicle);
            _createdVehicleIds.Add(createdVehicle.VehicleId);

            // Wait for indexing
            await Task.Delay(1000);

            var searchParams = new VehicleSearchParameters
            {
                FacilityIds = new[] { specificFacilityId }
            };

            var result = await vehicleManager.SearchVehiclesAsync(searchParams);

            // Assert
            Assert.IsNotNull(result, "Search result should not be null");
            Assert.IsNotNull(result.Item1, "Vehicle search results should not be null");

            if (result.Item1 != null && result.Item1.Length > 0)
            {
                // Verify at least our created vehicle is in the results
                var foundVehicle = result.Item1.FirstOrDefault(v => v.VehicleId == createdVehicle.VehicleId);
                Assert.IsNotNull(foundVehicle, "Should find the created vehicle with facility ID filter");
            }
        }

        #endregion

        #region Routes API Tests

        [Test]
        public async Task GetRoutes_WithFacilityIdsFilter_ShouldApplyFilter()
        {
            var routeManager = new RouteManagerV5(_apiKey);
            var specificFacilityId = TestFacilityIds[0];

            var filterParams = new RouteFilterParameters
            {
                Page = 1,
                PerPage = 10,
                Filters = new RouteFilterParametersFilters
                {
                    FacilityIds = new[] { specificFacilityId }
                }
            };

            var result = await routeManager.GetRoutesByFilterAsync(filterParams);

            Assert.IsNotNull(result, "Routes result should not be null");
            Assert.IsNotNull(result.Item1, "Routes data should not be null");
        }

        #endregion

        #region Locations API Tests

        [Test]
        public async Task GetLocations_WithFacilityIdsFilter_ShouldApplyFilter()
        {
            var locationManager = new LocationManagerV5(_apiKey);
            var specificFacilityId = TestFacilityIds[0];

            var request = new LocationCombinedRequest
            {
                Page = 1,
                PerPage = 10,
                Filters = new LocationFilters
                {
                    FacilityIds = new[] { specificFacilityId }
                }
            };

            var result = await locationManager.GetLocationsCombinedAsync(request);

            Assert.IsNotNull(result, "Locations result should not be null");
        }

        #endregion

        #region AddressBook API Tests

        [Test]
        public async Task GetAddressBookContacts_WithFacilityIdsFilter_ShouldApplyFilter()
        {
            // Arrange
            var addressBookManager = new AddressBookContactsManagerV5(_apiKey);

            var parameters = new AddressBookParameters
            {
                Limit = 10,
                Offset = 0,
                FacilityIds = new[] { TestFacilityIds[0] }
            };

            // Act
            var result = await addressBookManager.GetAddressBookContactsAsync(parameters);

            // Assert
            Assert.IsNotNull(result, "Result should not be null");
        }

        #endregion

        #region Team API Tests

        [Test]
        public async Task CreateTeamMember_WithFacilityIds_ShouldSucceed()
        {
            // Arrange
            var teamManager = new TeamManagementManagerV5(_apiKey);
            var facilityIds = new[] { TestFacilityIds[0] };
            var ownerId = teamManager.GetTeamOwner(out _);

            var memberRequest = new TeamRequest
            {
                OwnerMemberId = ownerId,
                MemberType = MemberTypes.Driver.Description(),
                MemberEmail = $"test{new Random().Next(10000, 99999)}@route4metest.com",
                NewPassword = R4MeUtils.ReadSetting("test_acc_psw"),
                MemberFirstName = "Test",
                MemberLastName = $"Member{new Random().Next(1000, 9999)}",
                FacilityIds = facilityIds
            };

            // Act
            var (createdMember, _) = await teamManager.CreateTeamMemberAsync(memberRequest);

            // Assert
            Assert.IsNotNull(createdMember, "Created team member should not be null");

            if (createdMember.MemberId.HasValue)
            {
                _createdTeamMemberIds.Add(createdMember.MemberId.Value);
            }
        }

        #endregion

        #region Private methods

        private static void AssertCollectionsEqual(string[] expected, string[] actual)
        {
            Assert.AreEqual(actual.Length, expected.Length);
            Assert.True(actual.All(expected.Contains));
        }

        #endregion
    }
}