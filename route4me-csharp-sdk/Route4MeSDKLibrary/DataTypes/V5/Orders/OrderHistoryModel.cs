using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    ///     The order history model
    /// </summary>
    [DataContract]
    public class OrderHistoryModel
    {
        /// <summary>
        ///     Order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public long OrderId { get; set; }

        /// <summary>
        ///     The id of the member inside the route4me system
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     Address 1
        /// </summary>
        [DataMember(Name = "address_1", EmitDefaultValue = false)]
        public string Address1 { get; set; }

        /// <summary>
        ///     Address 2
        /// </summary>
        [DataMember(Name = "address_2", EmitDefaultValue = false)]
        public string Address2 { get; set; }

        /// <summary>
        ///     Address alias
        /// </summary>
        [DataMember(Name = "address_alias", EmitDefaultValue = false)]
        public string AddressAlias { get; set; }

        /// <summary>
        ///     City address
        /// </summary>
        [DataMember(Name = "address_city", EmitDefaultValue = false)]
        public string AddressCity { get; set; }

        /// <summary>
        ///     Address state id
        /// </summary>
        [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
        public string AddressStateId { get; set; }

        /// <summary>
        ///     Address ZIP code
        /// </summary>
        [DataMember(Name = "address_zip", EmitDefaultValue = false)]
        public string AddressZip { get; set; }

        /// <summary>
        ///     Address country id
        /// </summary>
        [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
        public string AddressCountryId { get; set; }

        /// <summary>
        ///     Geo latitude. Required
        /// </summary>
        [DataMember(Name = "cached_lat")]
        public double CachedLat { get; set; }

        /// <summary>
        ///     Geo longitude. Required
        /// </summary>
        [DataMember(Name = "cached_lng")]
        public double CachedLng { get; set; }

        /// <summary>
        ///     Generate optimal routes and driving directions to this curbside latitude
        /// </summary>
        [DataMember(Name = "curbside_lat", EmitDefaultValue = false)]
        public double? CurbsideLat { get; set; }

        /// <summary>
        ///     Generate optimal routes and driving directions to the curbside longitude
        /// </summary>
        [DataMember(Name = "curbside_lng", EmitDefaultValue = false)]
        public double? CurbsideLng { get; set; }

        /// <summary>
        ///     City ID
        /// </summary>
        [DataMember(Name = "city_id", EmitDefaultValue = false)]
        public long? CityId { get; set; }

        /// <summary>
        ///     Timestamp added
        /// </summary>
        [DataMember(Name = "timestamp_added", EmitDefaultValue = false)]
        public string TimestampAdded { get; set; }

        /// <summary>
        ///     Date added
        /// </summary>
        [DataMember(Name = "date_added", EmitDefaultValue = false)]
        public string DateAdded { get; set; }

        /// <summary>
        ///     Time added
        /// </summary>
        [DataMember(Name = "time_added", EmitDefaultValue = false)]
        public string TimeAdded { get; set; }

        /// <summary>
        ///     Day is added
        /// </summary>
        [DataMember(Name = "day_id_added", EmitDefaultValue = false)]
        public string DayIsAdded { get; set; }

        /// <summary>
        ///     Day is scheduled for
        /// </summary>
        [DataMember(Name = "day_id_scheduled_for", EmitDefaultValue = false)]
        public string DayIsScheduledFor { get; set; }

        /// <summary>
        ///     Order status ID
        /// </summary>
        [DataMember(Name = "order_status_id", EmitDefaultValue = false)]
        public int OrderStatusId { get; set; }

        /// <summary>
        ///     Is pending
        /// </summary>
        [DataMember(Name = "is_pending", EmitDefaultValue = false)]
        public bool IsPending { get; set; }

        /// <summary>
        ///     Is accepted
        /// </summary>
        [DataMember(Name = "is_accepted", EmitDefaultValue = false)]
        public bool IsAccepted { get; set; }

        /// <summary>
        ///     Is started
        /// </summary>
        [DataMember(Name = "is_started", EmitDefaultValue = false)]
        public bool IsStarted { get; set; }

        /// <summary>
        ///     Is completed
        /// </summary>
        [DataMember(Name = "is_completed", EmitDefaultValue = false)]
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     Is deleted
        /// </summary>
        [DataMember(Name = "is_deleted", EmitDefaultValue = false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        ///     Is validated
        /// </summary>
        [DataMember(Name = "is_validated", EmitDefaultValue = false)]
        public bool IsValidated { get; set; }

        /// <summary>
        ///    Departed timestamp
        /// </summary>
        [DataMember(Name = "departed_timestamp", EmitDefaultValue = false)]
        public long? DepartedTimestamp { get; set; }

        /// <summary>
        ///     Departed address latitude
        /// </summary>
        [DataMember(Name = "departed_lat")]
        public double? DepartedLatitude { get; set; }

        /// <summary>
        ///     Departed address longitude
        /// </summary>
        [DataMember(Name = "departed_lng")]
        public double? DepartedLongitude { get; set; }

        /// <summary>
        ///     Local time window start
        /// </summary>
        [DataMember(Name = "time_window_start", EmitDefaultValue = false)]
        public long? TimeWindowStart { get; set; }

        /// <summary>
        ///     Local time window end
        /// </summary>
        [DataMember(Name = "time_window_end", EmitDefaultValue = false)]
        public long? TimeWindowEnd { get; set; }

        /// <summary>
        ///     Second Local time window start
        /// </summary>
        [DataMember(Name = "time_window_start_2", EmitDefaultValue = false)]
        public long? TimeWindowStart2 { get; set; }

        /// <summary>
        ///     Second local time window end
        /// </summary>
        [DataMember(Name = "time_window_end_2", EmitDefaultValue = false)]
        public long? TimeWindowEnd2 { get; set; }

        /// <summary>
        ///     Local time window start
        /// </summary>
        [DataMember(Name = "local_time_window_start", EmitDefaultValue = false)]
        public long? LocalTimeWindowStart { get; set; }

        /// <summary>
        ///     Local time window end
        /// </summary>
        [DataMember(Name = "local_time_window_end", EmitDefaultValue = false)]
        public long? LocalTimeWindowEnd { get; set; }

        /// <summary>
        ///     Second Local time window start
        /// </summary>
        [DataMember(Name = "local_time_window_start_2", EmitDefaultValue = false)]
        public long? LocalTimeWindowStart2 { get; set; }

        /// <summary>
        ///     Second local time window end
        /// </summary>
        [DataMember(Name = "local_time_window_end_2", EmitDefaultValue = false)]
        public long? LocalTimeWindowEnd2 { get; set; }

        /// <summary>
        ///     Local timezone string
        /// </summary>
        [DataMember(Name = "local_timezone_string", EmitDefaultValue = false)]
        public string LocalTimezoneString { get; set; }

        /// <summary>
        ///     Service time
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public long? ServiceTime { get; set; }

        /// <summary>
        ///     Color on the map.
        /// </summary>
        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color { get; set; }

        /// <summary>
        ///     Order icon
        /// </summary>
        [DataMember(Name = "order_icon", EmitDefaultValue = false)]
        public string OrderIcon { get; set; }

        /// <summary>
        ///     Customer PO
        /// </summary>
        [DataMember(Name = "EXT_FIELD_customer_po", EmitDefaultValue = false)]
        public double? ExtFieldCustomerPo { get; set; }

        /// <summary>
        ///     Invoice NO
        /// </summary>
        [DataMember(Name = "EXT_FIELD_invoice_no", EmitDefaultValue = false)]
        public double? ExtFieldInvoiceNo { get; set; }

        /// <summary>
        ///     Reference NO
        /// </summary>
        [DataMember(Name = "EXT_FIELD_reference_no", EmitDefaultValue = false)]
        public double? ExtFieldReferenceNo { get; set; }

        /// <summary>
        ///     Order NO
        /// </summary>
        [DataMember(Name = "EXT_FIELD_order_no", EmitDefaultValue = false)]
        public double? ExtFieldOrderNo { get; set; }

        /// <summary>
        ///     Weight
        /// </summary>
        [DataMember(Name = "EXT_FIELD_weight", EmitDefaultValue = false)]
        public double? ExtFieldWeight { get; set; }

        /// <summary>
        ///     Cost
        /// </summary>
        [DataMember(Name = "EXT_FIELD_cost", EmitDefaultValue = false)]
        public double? ExtFieldCost { get; set; }

        /// <summary>
        ///     Revenue
        /// </summary>
        [DataMember(Name = "EXT_FIELD_revenue", EmitDefaultValue = false)]
        public double? ExtFieldRevenue { get; set; }

        /// <summary>
        ///     Cube
        /// </summary>
        [DataMember(Name = "EXT_FIELD_cube", EmitDefaultValue = false)]
        public double? ExtFieldCube { get; set; }

        /// <summary>
        ///     Pieces
        /// </summary>
        [DataMember(Name = "EXT_FIELD_pieces", EmitDefaultValue = false)]
        public double? ExtFieldPieces { get; set; }

        /// <summary>
        ///     Time
        /// </summary>
        [DataMember(Name = "EXT_FIELD_time", EmitDefaultValue = false)]
        public double? ExtFieldTime { get; set; }

        /// <summary>
        ///     First name
        /// </summary>
        [DataMember(Name = "EXT_FIELD_first_name", EmitDefaultValue = false)]
        public string ExtFieldFirstName { get; set; }

        /// <summary>
        ///     Last name
        /// </summary>
        [DataMember(Name = "EXT_FIELD_last_name", EmitDefaultValue = false)]
        public string ExtFieldLastName { get; set; }

        /// <summary>
        ///     E-mail
        /// </summary>
        [DataMember(Name = "EXT_FIELD_email", EmitDefaultValue = false)]
        public string ExtFieldEmail { get; set; }

        /// <summary>
        ///     Phone
        /// </summary>
        [DataMember(Name = "EXT_FIELD_phone", EmitDefaultValue = false)]
        public string ExtFieldPhone { get; set; }

        /// <summary>
        ///     Custom data
        /// </summary>
        [DataMember(Name = "EXT_FIELD_custom_data", EmitDefaultValue = false)]
        public string ExtFieldCustomData { get; set; }

        /// <summary>
        ///     How many times the order visited.
        /// </summary>
        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int VisitedCount { get; set; }

        /// <summary>
        ///     Number of the routes containing the contact.
        /// </summary>
        [DataMember(Name = "in_route_count", EmitDefaultValue = false)]
        public int? InRouteCount { get; set; }

        /// <summary>
        ///     Last visited timestamp.
        /// </summary>
        [DataMember(Name = "last_visited_timestamp", EmitDefaultValue = false)]
        public long? LastVisitedTimestamp { get; set; }

        /// <summary>
        ///     Last routed timestamp.
        /// </summary>
        [DataMember(Name = "last_routed_timestamp", EmitDefaultValue = false)]
        public long? LastRoutedTimestamp { get; set; }

        /// <summary>
        ///     Timestamp of update
        /// </summary>
        [DataMember(Name = "updated_timestamp", EmitDefaultValue = false)]
        public long? UpdatedTimestamp { get; set; }

        /// <summary>
        ///     System-wide unique code, which permits end-users (recipients)
        ///     to track the status of their order.
        /// </summary>
        [DataMember(Name = "tracking_number", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string TrackingNumber { get; set; }

        /// <summary>
        ///     Route member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        ///     Organization ID
        /// </summary>
        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public long? OrganizationId { get; set; }

        /// <summary>
        ///     Route address stop type. For available values see Enums.AddressStopType
        /// </summary>
        [DataMember(Name = "address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>
        ///     Last status.
        /// </summary>
        [DataMember(Name = "last_status")]
        public string LastStatus { get; set; }
    }
}