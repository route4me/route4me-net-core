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
    }
}