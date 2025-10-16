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
    }
}

