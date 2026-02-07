using System.Runtime.Serialization;
using Route4MeSDK.DataTypes.V5.StrategicPlanner;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for listing strategic optimization locations
    /// </summary>
    [DataContract]
    public sealed class ListLocationsRequest : BaseListRequest
    {
        /// <summary>
        /// Filters for locations
        /// </summary>
        [DataMember(Name = "filters", EmitDefaultValue = false)]
        public LocationFilters Filters { get; set; }

        /// <summary>
        /// Fields to return in the response
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }
    }

    /// <summary>
    /// Filters for strategic optimization locations
    /// </summary>
    [DataContract]
    public sealed class LocationFilters
    {
        /// <summary>
        /// Search query
        /// </summary>
        [DataMember(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        /// Strategic optimization ID
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Is depot filter
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool? IsDepot { get; set; }

        /// <summary>
        /// Contact IDs
        /// </summary>
        [DataMember(Name = "contact_id", EmitDefaultValue = false)]
        public int[] ContactId { get; set; }

        /// <summary>
        /// Days filter (exact values: 0-6 for Sunday-Saturday)
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public int[] Days { get; set; }

        /// <summary>
        /// Starting cycle range [min, max]
        /// </summary>
        [DataMember(Name = "starting_cycle", EmitDefaultValue = false)]
        public int[] StartingCycle { get; set; }

        /// <summary>
        /// Days between cycle range [min, max]
        /// </summary>
        [DataMember(Name = "days_between_cycle", EmitDefaultValue = false)]
        public int[] DaysBetweenCycle { get; set; }

        /// <summary>
        /// Days range filter [min, max]
        /// </summary>
        [DataMember(Name = "days_range", EmitDefaultValue = false)]
        public int[] DaysRange { get; set; }

        /// <summary>
        /// Time window range [min, max]
        /// </summary>
        [DataMember(Name = "time_window", EmitDefaultValue = false)]
        public int[] TimeWindow { get; set; }

        /// <summary>
        /// Visits per cycle range [min, max]
        /// </summary>
        [DataMember(Name = "visits_per_cycle", EmitDefaultValue = false)]
        public int[] VisitsPerCycle { get; set; }

        /// <summary>
        /// Bounding box for geographical filtering
        /// </summary>
        [DataMember(Name = "bounding_box", EmitDefaultValue = false)]
        public BoundingBox BoundingBox { get; set; }
    }
}
