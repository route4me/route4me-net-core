using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     The response for route status request
    /// </summary>
    [DataContract]
    public class RouteStatusResponse
    {
        /// <summary>
        ///     Route status data.
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

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
        ///     Planned at (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "planned_at")]
        public long? PlannedAt { get; set; }

        /// <summary>
        ///     Started at (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "started_at")]
        public long? StartedAt { get; set; }

        /// <summary>
        ///     Paused at (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "paused_at")]
        public long? PausedAt { get; set; }

        /// <summary>
        ///     Completed at (Unix timestamp in seconds).
        /// </summary>
        [DataMember(Name = "completed_at")]
        public long? CompletedAt { get; set; }
    }
}