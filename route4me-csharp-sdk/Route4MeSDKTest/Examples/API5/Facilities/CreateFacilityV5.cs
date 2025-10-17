using Route4MeSDK.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Facilities;
using Route4MeSDKLibrary.QueryTypes.V5.Facilities;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of creating a new facility using the API 5 endpoint.
        /// </summary>
        public void CreateFacilityV5()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var request = new FacilityCreateRequest
            {
                FacilityAlias = "Test facility",
                Address = "123 Main Street",
                Status = FacilityStatus.Active,
                Coordinates = new FacilityCoordinates {
                    Latitude = 40.6892f,
                    Longitude = -74.0445f
                }
            };

            var facility = route4Me.CreateFacility(request, out ResultResponse resultResponse);

            if (facility != null && facility.GetType() == typeof(FacilityResource))
            {
                facilitiesToRemove = new System.Collections.Generic.List<string> {
                    facility.FacilityId
                };
            }

            PrintTestFacilitiesV5(facility, resultResponse);

            RemoveTestFacilitiesV5();
        }
    }
}
