using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     The request for route status
    /// </summary>
    [DataContract]
    public class RouteStatusRequest : GenericParameters
    {
        /// <summary>
        ///     Route status see <seealso cref="RouteStatus"/> for the list of possible values
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        ///     The latitude.
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        ///     The longitude.
        /// </summary>
        [DataMember(Name = "lng")]
        public double Longitude { get; set; }

        /// <summary>
        ///     Event timestamp (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "event_timestamp")]
        public long? EventTimestamp { get; set; }
    }
}