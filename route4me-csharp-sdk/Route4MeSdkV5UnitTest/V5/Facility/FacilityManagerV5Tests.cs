using System;

using NUnit.Framework;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSdkV5UnitTest.V5
{
    /// <summary>
    /// Smoke tests for Facility API V5 MVP
    /// </summary>
    [TestFixture]
    public class FacilityManagerV5Tests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;
        private string _createdFacilityId;
        private static readonly string TestRunId = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        [OneTimeTearDown]
        public void Cleanup()
        {
            // Clean up created facility
            if (!string.IsNullOrEmpty(_createdFacilityId))
            {
                var route4Me = new Route4MeManagerV5(CApiKey);
                route4Me.FacilityManager.DeleteFacility(_createdFacilityId, out _);
            }
        }

        /// <summary>
        /// Smoke Test 1: Create and Get Facility
        /// Tests CreateFacility and GetFacility methods
        /// </summary>
        [Test]
        public void CreateAndGetFacilityTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = $"Test Distribution Center {TestRunId}",
                Address = "123 Industrial Blvd, Chicago, IL 60601",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8781,
                    Lng = -87.6298
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 1
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(createRequest, out var createError);

            Assert.IsNotNull(createdFacility, "Failed to create facility");
            Assert.IsNotNull(createdFacility.FacilityId, "Facility ID should not be null");
            Assert.AreEqual($"Test Distribution Center {TestRunId}", createdFacility.FacilityAlias, "Facility name mismatch");
            Assert.IsNull(createError, "Create facility should not return error");

            _createdFacilityId = createdFacility.FacilityId;

            var retrievedFacility = route4Me.FacilityManager.GetFacility(createdFacility.FacilityId, out var getError);

            Assert.IsNotNull(retrievedFacility, "Failed to retrieve facility");
            Assert.AreEqual(createdFacility.FacilityId, retrievedFacility.FacilityId, "Facility ID mismatch");
            Assert.AreEqual($"Test Distribution Center {TestRunId}", retrievedFacility.FacilityAlias, "Retrieved facility name mismatch");
            Assert.AreEqual("123 Industrial Blvd, Chicago, IL 60601", retrievedFacility.Address, "Address mismatch");
            Assert.IsNull(getError, "Get facility should not return error");
        }

        /// <summary>
        /// Smoke Test 2: Get Facility Types
        /// Tests GetFacilityTypes method
        /// </summary>
        [Test]
        public void GetFacilityTypesTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var facilityTypes = route4Me.FacilityManager.GetFacilityTypes(out var error);

            Assert.IsNotNull(facilityTypes, "Facility types should not be null");
            Assert.IsNull(error, "Get facility types should not return error");

            if (facilityTypes.Data != null && facilityTypes.Data.Length > 0)
            {
                var firstType = facilityTypes.Data[0];
                Assert.That(firstType.FacilityTypeId, Is.GreaterThan(0), "Facility type ID should be greater than 0");
                Assert.IsNotNull(firstType.FacilityTypeAlias, "Facility type name should not be null");
            }
            else
            {
                Assert.Warn("No facility types found in the account. This test requires facility types to be configured.");
            }
        }

        /// <summary>
        /// Test 3: Update Facility
        /// Tests UpdateFacility method
        /// </summary>
        [Test]
        public void UpdateFacilityTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = $"Facility for Update Test {TestRunId}",
                Address = "123 Update St, Chicago, IL 60601",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8781,
                    Lng = -87.6298
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 1
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(createRequest, out var createError);
            Assert.IsNotNull(createdFacility, "Failed to create facility for update test");
            _createdFacilityId = createdFacility.FacilityId;

            var updateRequest = new FacilityUpdateRequest
            {
                FacilityAlias = $"Updated Facility Name {TestRunId}",
                Address = createdFacility.Address,
                Coordinates = new FacilityCoordinates { Lat = 41.8781, Lng = -87.6298 },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 2
            };

            var updatedFacility = route4Me.FacilityManager.UpdateFacility(
                createdFacility.FacilityId,
                updateRequest,
                out var updateError
            );

            Assert.IsNotNull(updatedFacility, "Failed to update facility");
            Assert.AreEqual($"Updated Facility Name {TestRunId}", updatedFacility.FacilityAlias, "Facility name was not updated");
            Assert.AreEqual(2, updatedFacility.Status, "Facility status was not updated");
            Assert.IsNull(updateError, "Update facility should not return error");
        }

        /// <summary>
        /// Test 4: Delete Facility
        /// Tests DeleteFacility method
        /// </summary>
        [Test]
        public void DeleteFacilityTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = $"Facility for Delete Test {TestRunId}",
                Address = "456 Delete Ave, Chicago, IL 60602",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8781,
                    Lng = -87.6298
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 1
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(createRequest, out var createError);
            Assert.IsNotNull(createdFacility, "Failed to create facility for delete test");

            string facilityIdToDelete = createdFacility.FacilityId;

            var deleteResult = route4Me.FacilityManager.DeleteFacility(facilityIdToDelete, out var deleteError);

            Assert.IsNotNull(deleteResult, "Delete operation should return result");
            Assert.IsNull(deleteError, "Delete facility should not return error");

        }

        /// <summary>
        /// Test 5: Get Paginated Facilities
        /// Tests GetFacilities with pagination
        /// </summary>
        [Test]
        public void GetPaginatedFacilitiesTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var parameters = new FacilityGetParameters
            {
                Page = 1,
                PerPage = 5
            };

            var facilities = route4Me.FacilityManager.GetFacilities(parameters, out var error);

            Assert.IsNotNull(facilities, "Facilities should not be null");
            Assert.IsNull(error, "Get facilities should not return error");

            Assert.IsNotNull(facilities.Data, "Facilities data should not be null");
            Assert.That(facilities.Data.Length, Is.LessThanOrEqualTo(5), "Should return at most 5 facilities per page");
            Assert.That(facilities.Total, Is.GreaterThanOrEqualTo(facilities.Data.Length), "Total should be >= data length");
        }

        /// <summary>
        /// Test 6: Error Handling - Invalid Facility ID
        /// Tests error handling for GetFacility with invalid ID
        /// </summary>
        [Test]
        public void GetFacilityWithInvalidIdTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.FacilityManager.GetFacility("", out var error);

            Assert.IsNull(result, "Result should be null for invalid ID");
            Assert.IsNotNull(error, "Should return error for invalid ID");
            Assert.IsFalse(error.Status, "Error status should be false");
        }

        /// <summary>
        /// Test 7: Error Handling - Null Create Request
        /// Tests error handling for CreateFacility with null request
        /// </summary>
        [Test]
        public void CreateFacilityWithNullRequestTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.FacilityManager.CreateFacility(null, out var error);

            Assert.IsNull(result, "Result should be null for null request");
            Assert.IsNotNull(error, "Should return error for null request");
            Assert.IsFalse(error.Status, "Error status should be false");
        }

        /// <summary>
        /// Test 8: Error Handling - Update Facility with Invalid ID
        /// Tests error handling for UpdateFacility with invalid ID
        /// </summary>
        [Test]
        public void UpdateFacilityWithInvalidIdTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var updateRequest = new FacilityUpdateRequest
            {
                FacilityAlias = "Test",
                Address = "Test Address",
                Coordinates = new FacilityCoordinates { Lat = 0, Lng = 0 },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1 }
                }
            };

            var result = route4Me.FacilityManager.UpdateFacility("", updateRequest, out var error);

            Assert.IsNull(result, "Result should be null for invalid ID");
            Assert.IsNotNull(error, "Should return error for invalid ID");
            Assert.IsFalse(error.Status, "Error status should be false");
        }

        /// <summary>
        /// Test 9: Error Handling - Update Facility with Null Request
        /// Tests error handling for UpdateFacility with null request
        /// </summary>
        [Test]
        public void UpdateFacilityWithNullRequestTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.FacilityManager.UpdateFacility("test-id", null, out var error);

            Assert.IsNull(result, "Result should be null for null request");
            Assert.IsNotNull(error, "Should return error for null request");
            Assert.IsFalse(error.Status, "Error status should be false");
        }

        /// <summary>
        /// Test 10: Error Handling - Delete Facility with Invalid ID
        /// Tests error handling for DeleteFacility with invalid ID
        /// </summary>
        [Test]
        public void DeleteFacilityWithInvalidIdTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.FacilityManager.DeleteFacility("", out var error);

            Assert.IsNull(result, "Result should be null for invalid ID");
            Assert.IsNotNull(error, "Should return error for invalid ID");
            Assert.IsFalse(error.Status, "Error status should be false");
        }

        /// <summary>
        /// Test 11: Get Facility Type by ID
        /// Tests GetFacilityType method
        /// </summary>
        [Test]
        public void GetFacilityTypeByIdTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            // First get all types to get a valid ID
            var allTypes = route4Me.FacilityManager.GetFacilityTypes(out var typesError);
            Assert.IsNotNull(allTypes, "Should be able to get facility types");

            if (allTypes.Data == null || allTypes.Data.Length == 0)
            {
                Assert.Ignore("No facility types found in the account. This test requires facility types to be configured.");
                return;
            }

            int validTypeId = allTypes.Data[0].FacilityTypeId;

            var specificType = route4Me.FacilityManager.GetFacilityType(validTypeId, out var error);

            Assert.IsNotNull(specificType, "Should be able to get facility type by ID");
            Assert.AreEqual(validTypeId, specificType.FacilityTypeId, "Returned type ID should match requested ID");
            Assert.IsNull(error, "Get facility type by ID should not return error");
        }

        /// <summary>
        /// Test 12: Error Handling - Get Facility Type with Invalid ID
        /// Tests error handling for GetFacilityType with invalid ID
        /// </summary>
        [Test]
        public void GetFacilityTypeWithInvalidIdTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var result = route4Me.FacilityManager.GetFacilityType(0, out var error);

            Assert.IsNull(result, "Result should be null for invalid ID");
            Assert.IsNotNull(error, "Should return error for invalid ID");
            Assert.IsFalse(error.Status, "Error status should be false");
        }

    }
}