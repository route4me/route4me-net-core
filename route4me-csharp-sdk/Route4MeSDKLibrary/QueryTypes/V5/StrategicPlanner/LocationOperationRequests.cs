using System.Runtime.Serialization;

using Route4MeSDKLibrary.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for updating a location
    /// </summary>
    [DataContract]
    public sealed class LocationUpdateRequest : GenericParameters
    {
        /// <summary>
        /// Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        /// <summary>
        /// Alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        /// Days when location can be visited
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public string[] Days { get; set; }

        /// <summary>
        /// Starting cycle
        /// </summary>
        [DataMember(Name = "starting_cycle", EmitDefaultValue = false)]
        public int? StartingCycle { get; set; }

        /// <summary>
        /// Days between cycle
        /// </summary>
        [DataMember(Name = "days_between_cycle", EmitDefaultValue = false)]
        public int? DaysBetweenCycle { get; set; }

        /// <summary>
        /// Days range
        /// </summary>
        [DataMember(Name = "days_range", EmitDefaultValue = false)]
        public int? DaysRange { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        /// <summary>
        /// Cost
        /// </summary>
        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public double? Cost { get; set; }

        /// <summary>
        /// Revenue
        /// </summary>
        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public double? Revenue { get; set; }

        /// <summary>
        /// Cube
        /// </summary>
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public double? Cube { get; set; }

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
        /// Second time window start
        /// </summary>
        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public int? TimeWindowStart2 { get; set; }

        /// <summary>
        /// Second time window end
        /// </summary>
        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public int? TimeWindowEnd2 { get; set; }

        /// <summary>
        /// Is depot flag
        /// </summary>
        [DataMember(Name = "is_depot", EmitDefaultValue = false)]
        public bool? IsDepot { get; set; }

        /// <summary>
        /// Custom fields
        /// </summary>
        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public string[] CustomFields { get; set; }

        /// <summary>
        /// Required skills
        /// </summary>
        [DataMember(Name = "required_skills", EmitDefaultValue = false)]
        public object[] RequiredSkills { get; set; }
    }

    /// <summary>
    /// Request for bulk updating locations
    /// </summary>
    [DataContract]
    public sealed class BulkUpdateLocationsRequest : ListLocationsRequest
    {
        /// <summary>
        /// Data to update
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public LocationBulkUpdateData Data { get; set; }

        /// <summary>
        /// IDs to update
        /// </summary>
        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public string[] Ids { get; set; }

        /// <summary>
        /// Is exclusion mode
        /// </summary>
        [DataMember(Name = "is_exclusion_mode", EmitDefaultValue = false)]
        public bool? IsExclusionMode { get; set; }
    }

    /// <summary>
    /// Location bulk update data
    /// </summary>
    [DataContract]
    public sealed class LocationBulkUpdateData
    {
        /// <summary>
        /// Days to update
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public string[] Days { get; set; }

        /// <summary>
        /// Starting cycle
        /// </summary>
        [DataMember(Name = "starting_cycle", EmitDefaultValue = false)]
        public int? StartingCycle { get; set; }

        /// <summary>
        /// Days between cycle
        /// </summary>
        [DataMember(Name = "days_between_cycle", EmitDefaultValue = false)]
        public int? DaysBetweenCycle { get; set; }

        /// <summary>
        /// Days range
        /// </summary>
        [DataMember(Name = "days_range", EmitDefaultValue = false)]
        public int? DaysRange { get; set; }

        /// <summary>
        /// Time window start
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        /// Time window end
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        /// Second time window start
        /// </summary>
        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public int? TimeWindowStart2 { get; set; }

        /// <summary>
        /// Second time window end
        /// </summary>
        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public int? TimeWindowEnd2 { get; set; }
    }

    /// <summary>
    /// Request for bulk deleting locations
    /// </summary>
    [DataContract]
    public sealed class BulkDeleteLocationsRequest : ListLocationsRequest
    {
        /// <summary>
        /// IDs to delete
        /// </summary>
        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public string[] Ids { get; set; }

        /// <summary>
        /// Is exclusion mode
        /// </summary>
        [DataMember(Name = "is_exclusion_mode", EmitDefaultValue = false)]
        public bool? IsExclusionMode { get; set; }
    }
}
