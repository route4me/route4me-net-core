using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     For bulk operation request
    /// </summary>
    [DataContract]
    public sealed class Vehicles : GenericParameters
    {
        /// <summary>
        ///     To set an array of the vehicle for bulk operations.
        /// </summary>
        [DataMember(Name = "vehicles", EmitDefaultValue = false)]
        public Vehicle[] VehicleArray { get; set; }

    }
}