using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Destination lookup information returned by GET /route-destinations/sorting?label_code={code}.
    /// Provides route and stop context for a destination identified by its label code.
    /// </summary>
    [DataContract]
    public class SortingFilteringResource
    {
        /// <summary>Integer ID of the route destination.</summary>
        [DataMember(Name = "destination_id")]
        public int? DestinationId { get; set; }

        /// <summary>UUID of the route destination (32-character hex string).</summary>
        [DataMember(Name = "destination_uuid")]
        public string DestinationUuid { get; set; }

        /// <summary>Display name of the route this destination belongs to.</summary>
        [DataMember(Name = "route_name")]
        public string RouteName { get; set; }

        /// <summary>32-character hex ID of the parent route.</summary>
        [DataMember(Name = "route_id")]
        public string RouteId { get; set; }

        /// <summary>Unix timestamp of the scheduled date for this destination.</summary>
        [DataMember(Name = "scheduled_for")]
        public int? ScheduledFor { get; set; }

        /// <summary>Street address of the destination.</summary>
        [DataMember(Name = "destination_address")]
        public string DestinationAddress { get; set; }

        /// <summary>Zero-based stop sequence number within the route.</summary>
        [DataMember(Name = "sequence_no")]
        public int? SequenceNo { get; set; }

        /// <summary>Full name of the assigned driver. Null if unassigned.</summary>
        [DataMember(Name = "assigned_user_driver")]
        public string AssignedUserDriver { get; set; }

        /// <summary>Member (driver) ID. Null if unassigned.</summary>
        [DataMember(Name = "member_id")]
        public int? MemberId { get; set; }

        /// <summary>Alias of the assigned vehicle. Null if unassigned.</summary>
        [DataMember(Name = "assigned_vehicle")]
        public string AssignedVehicle { get; set; }

        /// <summary>32-character hex ID of the assigned vehicle. Null if unassigned.</summary>
        [DataMember(Name = "vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>Tracking number associated with this destination.</summary>
        [DataMember(Name = "tracking_number")]
        public string TrackingNumber { get; set; }

        /// <summary>Label code used to look up this destination (e.g., "label_5").</summary>
        [DataMember(Name = "label_code")]
        public string LabelCode { get; set; }

        /// <summary>Encoded polyline path to this destination. Null if not available.</summary>
        [DataMember(Name = "path")]
        public string Path { get; set; }
    }
}
