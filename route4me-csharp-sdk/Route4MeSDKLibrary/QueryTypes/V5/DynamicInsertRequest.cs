using System.ComponentModel;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// The data structure fo dynamic insert request.
    /// </summary>
    [DataContract]
    public sealed class DynamicInsertRequest : GenericParameters
    {
        /// <summary>
        ///     Dynamic insert mode. <see cref="DynamicInsertMode"/>
        /// </summary>
        [DataMember(Name = "insert_mode", EmitDefaultValue = false)]
        public string InsertMode { get; set; }

        /// <summary>
        ///     Scheduled date (e.g. '2022-06-09')
        /// </summary>
        [DataMember(Name = "scheduled_for", EmitDefaultValue = false)]
        public string ScheduledFor { get; set; }

        /// <summary>
        ///     Latitude of a location
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Latitude { get; set; }

        /// <summary>
        ///     Latitude of a Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Longitude { get; set; }

        /// <summary>
        ///     Limitation of the lookup results.
        /// </summary>
        [DataMember(Name = "lookup_results_limit", EmitDefaultValue = false)]
        [DefaultValue(3)]
        public int? LookupResultsLimit { get; set; }

        /// <summary>
        ///     Dynamic insert mode. <see cref="DynamicInsertRecomendBy"/>
        /// </summary>
        [DataMember(Name = "recommend_by", EmitDefaultValue = false)]
        public string RecommendBy { get; set; }

        /// <summary>
        ///     Maximum increase percentage allowed.
        /// </summary>
        [DataMember(Name = "max_increase_percent_allowed", EmitDefaultValue = false)]
        [DefaultValue(500)]
        public int MaxIncreasePercentAllowed { get; set; }

        /// <summary>
        ///     Route Ids
        ///     Required if scheduled_for is null <see cref="ScheduledFor"/>
        /// </summary>
        [DataMember(Name = "route_ids", EmitDefaultValue = false)]
        public string[] RouteIds { get; set; }

        /// <summary>
        ///     Start of the new destination's time window, seconds from midnight.
        ///     When no time window is supplied, the time-window check covers only the stops already on the route.
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        ///     End of the new destination's time window, seconds from midnight.
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        ///     Start of the new destination's second time window, seconds from midnight.
        /// </summary>
        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public int? TimeWindowStart2 { get; set; }

        /// <summary>
        ///     End of the new destination's second time window, seconds from midnight.
        /// </summary>
        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public int? TimeWindowEnd2 { get; set; }

        /// <summary>
        ///     Only routes attributed to at least one of these facilities are considered.
        ///     The special value "empty" matches routes with no facility assigned.
        /// </summary>
        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }

        /// <summary>
        ///     Route filters, using the same vocabulary as the routes list.
        ///     Do not set route ids, facility ids, schedule date or the master-route flag here:
        ///     the API refuses them. Use <see cref="RouteIds"/>, <see cref="FacilityIds"/> and <see cref="ScheduledFor"/> instead.
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public RouteFilterParametersFilters Filters { get; set; }

        /// <summary>
        ///     Free-text search over the candidate routes.
        /// </summary>
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }
    }
}