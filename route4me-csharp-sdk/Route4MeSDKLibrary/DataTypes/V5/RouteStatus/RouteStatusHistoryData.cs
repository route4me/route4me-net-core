using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    [DataContract]
    public class RouteStatusHistoryData
    {
        /// <summary>
        ///     Route status data.
        /// </summary>
        [DataMember(Name = "route_status_history_id", EmitDefaultValue = false)]
        public string RouteStatusHistoryId { get; set; }

        /// <summary>
        ///     Route status data.
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Member id
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     Route status see <seealso cref="RouteStatus"/> for the list of possible values
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        ///     The latitude.
        /// </summary>
        [DataMember(Name = "lat")]
        public double? Latitude { get; set; }

        /// <summary>
        ///     The longitude.
        /// </summary>
        [DataMember(Name = "lng")]
        public double? Longitude { get; set; }

        /// <summary>
        ///     Timestamp (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "timestamp")]
        public long? StartedAt { get; set; }

        /// <summary>
        ///     Rollback timestamp (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "rollback_timestamp")]
        public long? TimestampTimestamp { get; set; }
    }
}