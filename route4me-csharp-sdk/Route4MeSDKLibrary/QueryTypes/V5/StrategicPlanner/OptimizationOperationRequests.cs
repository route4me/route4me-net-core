using System.Runtime.Serialization;
using Route4MeSDKLibrary.QueryTypes;
using Route4MeSDK.DataTypes.V5.StrategicPlanner;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Request for creating or updating a strategic optimization
    /// </summary>
    [DataContract]
    public sealed class CreateOrUpdateStrategicOptimizationRequest : GenericParameters
    {
        /// <summary>
        /// Strategic optimization ID (for updates)
        /// </summary>
        [DataMember(Name = "strategic_optimization_id", EmitDefaultValue = false)]
        public string StrategicOptimizationId { get; set; }

        /// <summary>
        /// Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public int? RootMemberId { get; set; }

        /// <summary>
        /// Optimization name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Source (e.g., "SFTP", "API", "Upload")
        /// </summary>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Created by member ID
        /// </summary>
        [DataMember(Name = "created_by_member_id", EmitDefaultValue = false)]
        public int? CreatedByMemberId { get; set; }

        /// <summary>
        /// Locations to add/update
        /// </summary>
        [DataMember(Name = "locations", EmitDefaultValue = false)]
        public StrategicOptimizationLocation[] Locations { get; set; }
    }

    /// <summary>
    /// Strategic optimization location data
    /// </summary>
    [DataContract]
    public sealed class StrategicOptimizationLocation
    {
        /// <summary>
        /// Planner location ID
        /// </summary>
        [DataMember(Name = "planner_location_id", EmitDefaultValue = false)]
        public string PlannerLocationId { get; set; }

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
        /// Alias
        /// </summary>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        /// Service time
        /// </summary>
        [DataMember(Name = "time", EmitDefaultValue = false)]
        public int? Time { get; set; }

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
        /// Days
        /// </summary>
        [DataMember(Name = "days", EmitDefaultValue = false)]
        public int[] Days { get; set; }

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
        /// Visits per cycle
        /// </summary>
        [DataMember(Name = "visits_per_cycle", EmitDefaultValue = false)]
        public int? VisitsPerCycle { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public int? Priority { get; set; }

        /// <summary>
        /// Address stop type
        /// </summary>
        [DataMember(Name = "address_stop_type", EmitDefaultValue = false)]
        public string AddressStopType { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public string Group { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }
    }

    /// <summary>
    /// Request for bulk deleting optimizations
    /// </summary>
    [DataContract]
    public sealed class DeleteOptimizationsRequest : ListStrategicOptimizationsRequest
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

    /// <summary>
    /// Request for creating a draft optimization from existing optimization
    /// </summary>
    [DataContract]
    public sealed class CreateDraftOptimizationRequest : ListLocationsRequest
    {
        // Inherits all filters from ListLocationsRequest for selecting which locations to clone
    }
}
