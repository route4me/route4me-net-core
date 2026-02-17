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
    }
}