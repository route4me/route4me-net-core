using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Response for retrieved list of the vehicles.
    /// </summary>
    [DataContract]
    public sealed class VehiclesResponse : GenericParameters
    {
        /// <summary>
        ///     Vehicle data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Vehicle[] Data { get; set; }

        /// <summary>
        ///     Total found vehicles
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int Total { get; set; }

        /// <summary>
        ///     An array of the error messages.
        /// </summary>
        [DataMember(Name = "error", EmitDefaultValue = false)]
        public string[] Error { get; set; }
    }
}