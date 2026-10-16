using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Dynamic insert matched route.
    /// </summary>
    [DataContract]
    public class DynamicInsertMatchedRoute
    {
        /// <summary>
        ///     Route name
        /// </summary>
        [DataMember(Name = "route_name")]
        public string RouteName { get; set; }

        /// <summary>
        ///     Route ID
        /// </summary>
        [DataMember(Name = "route_id")]
        public string RouteId { get; set; }

        /// <summary>
        ///     Recommended insertion stop number.
        /// </summary>
        [DataMember(Name = "recommended_insertion_stop_number", EmitDefaultValue = false)]
        public int? RecommendedInsertionStopNumber { get; set; }

        /// <summary>
        ///     Old distance.
        /// </summary>
        [DataMember(Name = "old_distance", EmitDefaultValue = false)]
        public double? OldDistance { get; set; }

        /// <summary>
        ///     New distance.
        /// </summary>
        [DataMember(Name = "new_distance", EmitDefaultValue = false)]
        public double? NewDistance { get; set; }

        /// <summary>
        ///     Old time.
        /// </summary>
        [DataMember(Name = "old_time", EmitDefaultValue = false)]
        public double? OldTime { get; set; }

        /// <summary>
        ///     New time.
        /// </summary>
        [DataMember(Name = "new_time", EmitDefaultValue = false)]
        public double? NewTime { get; set; }

        // <summary>
        ///     Percentage distance increase.
        /// </summary>
        [DataMember(Name = "percentage_distance_increase", EmitDefaultValue = false)]
        public double? PercentageDistanceIncrease { get; set; }

        // <summary>
        ///     Percentage time increase.
        /// </summary>
        [DataMember(Name = "percentage_time_increase", EmitDefaultValue = false)]
        public double? PercentageTimeIncrease { get; set; }

        /// <summary>
        ///     Time violation.
        /// </summary>
        [DataMember(Name = "time_violation", EmitDefaultValue = false)]
        public double? TimeViolation { get; set; }

        /// <summary>
        ///     Time-window violations introduced by inserting the new destination into this route.
        ///     Not the same as <see cref="TimeViolation"/>, which is the route max duration overflow.
        /// </summary>
        [DataMember(Name = "time_window_violations", EmitDefaultValue = false)]
        public DynamicInsertTimeWindowViolations TimeWindowViolations { get; set; }
    }

    /// <summary>
    ///     Time-window violations introduced by a dynamic insert.
    /// </summary>
    [DataContract]
    public class DynamicInsertTimeWindowViolations
    {
        /// <summary>
        ///     Number of stops that are served on time without the insertion and late with it.
        /// </summary>
        [DataMember(Name = "count", EmitDefaultValue = false)]
        public int? Count { get; set; }

        /// <summary>
        ///     Size of the worst violation in seconds.
        /// </summary>
        [DataMember(Name = "max_late_seconds", EmitDefaultValue = false)]
        public int? MaxLateSeconds { get; set; }

        /// <summary>
        ///     The affected stops, ordered by late seconds descending.
        /// </summary>
        [DataMember(Name = "destinations", EmitDefaultValue = false)]
        public DynamicInsertTimeWindowViolation[] Destinations { get; set; }
    }

    /// <summary>
    ///     A stop that is served on time without the insertion and late with it.
    /// </summary>
    [DataContract]
    public class DynamicInsertTimeWindowViolation
    {
        /// <summary>
        ///     Identifier of the affected stop. Null for the destination being inserted.
        /// </summary>
        [DataMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public long? RouteDestinationId { get; set; }

        /// <summary>
        ///     Position of the stop after the insertion.
        /// </summary>
        [DataMember(Name = "new_sequence_no", EmitDefaultValue = false)]
        public int? NewSequenceNo { get; set; }

        /// <summary>
        ///     Start of the window the stop is served against, seconds from midnight.
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        ///     End of the window the stop is served against, seconds from midnight.
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        ///     Size of the violation: arrival + service time - time window end.
        /// </summary>
        [DataMember(Name = "late_seconds", EmitDefaultValue = false)]
        public int? LateSeconds { get; set; }
    }
}