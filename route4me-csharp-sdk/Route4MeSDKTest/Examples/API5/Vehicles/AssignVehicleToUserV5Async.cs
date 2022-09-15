using Route4MeSDK.DataTypes.V5;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// The example refers to the process of temporary assigning a member to a vehicle 
        /// asynchronously using the API 5 endpoint.
        /// Note: the example requires an API key with special feature.
        /// </summary>
        public async void AssignVehicleToUserV5Async()
        {
            var route4Me = new Route4MeManagerV5(ActualApiKey);

            var randomMember = GetRandomTeamMember();

            var vehicleParams = new VehicleTemporary()
            {
                VehicleId = "68F3545D3DA82DBF007B20FA9A1875EB",
                ForceAssignment = false,
                AssignedMemberId = randomMember.MemberId.ToString(),
                ExpiresAt = R4MeUtils.ConvertToUnixTimestamp(DateTime.Now + (new TimeSpan(2, 0, 0, 0))).ToString()
            };

            var result = await route4Me.CreateTemporaryVehicleAsync(vehicleParams);

            PrintTestVehcilesV5(result.Item1, result.Item2);
        }
    }
}