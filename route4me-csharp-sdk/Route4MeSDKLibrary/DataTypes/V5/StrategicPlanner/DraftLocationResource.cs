using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Strategic planner draft location resource
    /// </summary>
    [DataContract]
    public sealed class DraftLocationResource
    {
        /// <summary>
        /// Location ID (hex32 format)
        /// </summary>
        [DataMember(Name = "location_id", EmitDefaultValue = false)]
        public string LocationId { get; set; }

        /// <summary>
        /// Strategic optimization ID
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Contact ID
        /// </summary>
        [DataMember(Name = "contact_id", EmitDefaultValue = false)]
        public int? ContactId { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Alias/name for the location
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        /// Service time
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public string Time { get; set; }

        /// <summary>
        /// Days when location can be visited (e.g., ["mon", "tue", "wed"])
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public string[] Days { get; set; }

        /// <summary>
        /// Starting cycle
        /// </summary>
        [DataMember(Name = "starting_cycle", EmitDefaultValue = false)]
        public int? StartingCycle { get; set; }

        /// <summary>
        /// Days between each cycle
        /// </summary>
        [DataMember(Name = "days_between_cycle", EmitDefaultValue = false)]
        public int? DaysBetweenCycle { get; set; }

        /// <summary>
        /// Range of days for scheduling
        /// </summary>
        [DataMember(Name = "days_range", EmitDefaultValue = false)]
        public int? DaysRange { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        /// <summary>
        /// Weight unit
        /// </summary>
        [DataMember(Name = "weight_unit", EmitDefaultValue = false)]
        public string WeightUnit { get; set; }

        /// <summary>
        /// Cost
        /// </summary>
        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public double? Cost { get; set; }

        /// <summary>
        /// Cost unit
        /// </summary>
        [DataMember(Name = "cost_unit", EmitDefaultValue = false)]
        public string CostUnit { get; set; }

        /// <summary>
        /// Revenue
        /// </summary>
        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public double? Revenue { get; set; }

        /// <summary>
        /// Revenue unit
        /// </summary>
        [DataMember(Name = "revenue_unit", EmitDefaultValue = false)]
        public string RevenueUnit { get; set; }

        /// <summary>
        /// Cube
        /// </summary>
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public double? Cube { get; set; }

        /// <summary>
        /// Cube unit
        /// </summary>
        [DataMember(Name = "cube_unit", EmitDefaultValue = false)]
        public string CubeUnit { get; set; }

        /// <summary>
        /// Pieces
        /// </summary>
        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public int? Pieces { get; set; }

        /// <summary>
        /// Time window start (seconds since midnight)
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        /// Time window end (seconds since midnight)
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        /// Second time window start (seconds since midnight)
        /// </summary>
        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public int? TimeWindowStart2 { get; set; }

        /// <summary>
        /// Second time window end (seconds since midnight)
        /// </summary>
        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public int? TimeWindowEnd2 { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }

        /// <summary>
        /// Is depot flag
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool? IsDepot { get; set; }

        /// <summary>
        /// Is routed flag
        /// </summary>
        [DataMember(Name = "is_routed", EmitDefaultValue = false)]
        public bool? IsRouted { get; set; }

        /// <summary>
        /// Required skills for the location
        /// </summary>
        [DataMember(Name = "required_skills", EmitDefaultValue = false)]
        public string[] RequiredSkills { get; set; }

        /// <summary>
        /// Custom fields
        /// </summary>
        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public object[] CustomFields { get; set; }
    }
}
