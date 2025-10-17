using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Route4MeSdkV5UnitTest.V5.Facilities
{
    [TestFixture]
    public class FacilityTests
    {
        private static readonly string c_ApiKey = ApiKeys.ActualApiKey;

        private static List<FacilityResource> lsCreatedFacilities;

        [OneTimeSetUp]
        public void Setup() {
            lsCreatedFacilities = new List<FacilityResource>();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var route4Me = new Route4MeManagerV5(c_ApiKey);

            if (lsCreatedFacilities?.Any() ?? false)
            {
                var facilityIDs = lsCreatedFacilities
                    .Where(x => x != null && x.FacilityId != null)
                    .Select(x => x.FacilityId);

                foreach (var facilityId in facilityIDs)
                {
                    var removed = route4Me.DeleteFacility(facilityId, out ResultResponse resultResponse);

                    Console.WriteLine(
                        ((removed?.FacilityId ?? null) != null)
                        ? $"The facility {facilityId} removed"
                        : $"Cannot remove the facility {facilityId}"
                     );
                }
            }
        }

        [Test, Order(1)]
        public void CreateFacilityTest()
        {
            var route4me = new Route4MeManagerV5(c_ApiKey);

            var request = new FacilityCreateRequest()
            {
                FacilityAlias = "New Test Facility",
                Address = "123 Main Street",
                Status = FacilityStatus.Active,
                Coordinates = new FacilityCoordinates
                {
                    Latitude = 40.6892f,
                    Longitude = -74.0445f
                },
                FacilityTypes = new FacilityTypeAssignmentResource[]
                {
                    new FacilityTypeAssignmentResource
                    {
                        FacilityTypeId = 1,
                        IsDefault = true
                    }
                }
            };

            var facility = route4me.CreateFacility(request, out ResultResponse resultResponse);

            if ((facility?.FacilityId ?? null) != null) lsCreatedFacilities.Add(facility);

            Assert.NotNull(facility);
        }

        [Test, Order(2)]
        public void GetFacilities()
        {
            var route4me = new Route4MeManagerV5(c_ApiKey);

            var facilityParams = new FacilityGetParameters()
            {
                Page = 1,
                PerPage = 10
            };

            var facilities = route4me.GetFacilities(facilityParams, out ResultResponse resultResponse);

            Assert.NotNull(facilities);
            Assert.That(facilities.GetType(), Is.EqualTo(typeof(FacilitiesPaginateResource)));
        }

        [Test, Order(3)]
        public void GetFacilityTypes()
        {
            var route4me = new Route4MeManagerV5(c_ApiKey);

            var types = route4me.GetFacilityTypes(out ResultResponse resultResponse);

            Assert.NotNull(types);
            Assert.That(types.GetType(), Is.EqualTo(typeof(FacilityTypeCollectionResource)));
        }
    }
}
