using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     Parameters for set stop route status
    /// </summary>
    [DataContract]
    public class SetRouteStopStatusParameters : GenericParameters
    {
        /// <summary>
        /// Destination IDs
        /// </summary>
        [DataMember(Name = "destination_ids", EmitDefaultValue = false)]
        public long[] DestinationIds { get; set; }

        /// <summary>
        /// Status see <seealso cref="RouteStopStatus"/>
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }
    }
}