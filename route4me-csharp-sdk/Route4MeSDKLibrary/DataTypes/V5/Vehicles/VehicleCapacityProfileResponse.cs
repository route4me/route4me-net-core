using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Vehicle profile response data structure.
    /// </summary>
    [DataContract]
    public sealed class VehicleCapacityProfileResponse
    {
        /// <summary>
        ///     The data object containing a vehicle capacity profile.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleCapacityProfile Data { get; set; }
    }
}