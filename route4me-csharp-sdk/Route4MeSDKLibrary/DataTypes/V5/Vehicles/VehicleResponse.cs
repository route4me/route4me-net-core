using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response for the endpoint /vehicles/license
    /// </summary>
    [DataContract]
    public sealed class VehicleResponse
    {
        /// <summary>
        ///     Vehicle data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public DataVehicle Data { get; set; }
    }

    [DataContract]
    public class DataVehicle
    {
        /// <summary>
        ///     The vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle", EmitDefaultValue = false)]
        public VehicleBase Vehicle { get; set; }
    }
}