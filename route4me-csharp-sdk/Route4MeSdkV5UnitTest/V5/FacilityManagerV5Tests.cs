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
                FacilityAlias = "Test Distribution Center",
                Address = "123 Industrial Blvd, Chicago, IL 45678",
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
            Assert.AreEqual("Test Distribution Center", createdFacility.FacilityAlias, "Facility name mismatch");
            Assert.IsNull(createError, "Create facility should not return error");

            _createdFacilityId = createdFacility.FacilityId;

            var retrievedFacility = route4Me.FacilityManager.GetFacility(createdFacility.FacilityId, out var getError);

            Assert.IsNotNull(retrievedFacility, "Failed to retrieve facility");
            Assert.AreEqual(createdFacility.FacilityId, retrievedFacility.FacilityId, "Facility ID mismatch");
            Assert.AreEqual("Test Distribution Center", retrievedFacility.FacilityAlias, "Retrieved facility name mismatch");
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
            Assert.That(facilityTypes.Count, Is.GreaterThan(0), "Should return at least one facility type");
            Assert.IsNull(error, "Get facility types should not return error");

            if (facilityTypes.Count > 0)
            {
                var firstType = facilityTypes[0];
                Assert.That(firstType.FacilityTypeId, Is.GreaterThan(0), "Facility type ID should be greater than 0");
                Assert.IsNotNull(firstType.FacilityTypeAlias, "Facility type name should not be null");
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

            // Create a facility first
            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Facility for Update Test",
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
                Status = 1,
                ContactPersonFirstName = "John",
                ContactPersonLastName = "Doe"
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(createRequest, out var createError);
            Assert.IsNotNull(createdFacility, "Failed to create facility for update test");
            _createdFacilityId = createdFacility.FacilityId;

            // Update the facility
            var updateRequest = new FacilityUpdateRequest
            {
                FacilityAlias = "Updated Facility Name",
                Address = createdFacility.Address,
                Coordinates = new FacilityCoordinates { Lat = 41.8781, Lng = -87.6298 },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 2,
                ContactPersonFirstName = "Jane",
                ContactPersonLastName = "Smith",
                ContactPersonEmail = "jane.smith@example.com",
                ContactPersonPhone = "+1-555-0200"
            };

            var updatedFacility = route4Me.FacilityManager.UpdateFacility(
                createdFacility.FacilityId,
                updateRequest,
                out var updateError
            );

            Assert.IsNotNull(updatedFacility, "Failed to update facility");
            Assert.AreEqual("Updated Facility Name", updatedFacility.FacilityAlias, "Facility name was not updated");
            Assert.AreEqual(2, updatedFacility.Status, "Facility status was not updated");
            Assert.AreEqual("Jane", updatedFacility.ContactPersonFirstName, "Contact first name was not updated");
            Assert.AreEqual("Smith", updatedFacility.ContactPersonLastName, "Contact last name was not updated");
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

            // Create a facility first
            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Facility for Delete Test",
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

            // Delete the facility
            var deleteResult = route4Me.FacilityManager.DeleteFacility(facilityIdToDelete, out var deleteError);

            Assert.IsNotNull(deleteResult, "Delete operation should return result");
            Assert.IsNull(deleteError, "Delete facility should not return error");

            // Verify deletion by trying to get the facility
            System.Threading.Thread.Sleep(500); // Brief pause for API sync
            var deletedFacility = route4Me.FacilityManager.GetFacility(facilityIdToDelete, out var getError);

            // The facility should not be found or should return an error
            Assert.IsTrue(deletedFacility == null || getError != null, "Facility should not exist after deletion");
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
            Assert.That(facilities.PerPage, Is.EqualTo(5), "Per page should be 5");
            Assert.That(facilities.CurrentPage, Is.EqualTo(1), "Current page should be 1");
            
            if (facilities.Data != null && facilities.Data.Length > 0)
            {
                Assert.That(facilities.Data.Length, Is.LessThanOrEqualTo(5), "Should return at most 5 facilities per page");
            }
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
            Assert.That(allTypes.Count, Is.GreaterThan(0), "Should have at least one facility type");

            int validTypeId = allTypes[0].FacilityTypeId;

            // Now get specific type by ID
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

        /// <summary>
        /// Test 13: Facility with Contact Information
        /// Tests creating and retrieving facility with contact person fields
        /// </summary>
        [Test]
        public void FacilityWithContactInformationTest()
        {
            var route4Me = new Route4MeManagerV5(CApiKey);

            var createRequest = new FacilityCreateRequest
            {
                FacilityAlias = "Facility with Contacts",
                Address = "789 Contact Rd, Chicago, IL 60603",
                Coordinates = new FacilityCoordinates
                {
                    Lat = 41.8781,
                    Lng = -87.6298
                },
                FacilityTypes = new FacilityTypeAssignment[]
                {
                    new FacilityTypeAssignment { FacilityTypeId = 1, IsDefault = true }
                },
                Status = 1,
                ContactPersonFirstName = "Alice",
                ContactPersonLastName = "Johnson",
                ContactPersonEmail = "alice.johnson@example.com",
                ContactPersonPhone = "+1-555-0300"
            };

            var createdFacility = route4Me.FacilityManager.CreateFacility(createRequest, out var createError);

            Assert.IsNotNull(createdFacility, "Failed to create facility with contact information");
            Assert.AreEqual("Alice", createdFacility.ContactPersonFirstName, "Contact first name mismatch");
            Assert.AreEqual("Johnson", createdFacility.ContactPersonLastName, "Contact last name mismatch");
            Assert.AreEqual("alice.johnson@example.com", createdFacility.ContactPersonEmail, "Contact email mismatch");
            Assert.AreEqual("+1-555-0300", createdFacility.ContactPersonPhone, "Contact phone mismatch");
            Assert.IsNull(createError, "Create facility should not return error");

            _createdFacilityId = createdFacility.FacilityId;
        }
    }
}

