using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     The order data structure
    /// </summary>
    [DataContract]
    public sealed class Order : GenericParameters
    {
        /// <summary>
        /// Timestamp of an order creation.
        /// </summary>
        [DataMember(Name = "created_timestamp", EmitDefaultValue = false)]
        public long? CreatedTimestamp { get; set; }


        /// <summary>
        /// Timestamp of an order creation.
        /// </summary>
        [DataMember(Name = "updated_timestamp", EmitDefaultValue = false)]
        public long? UpdatedTimestamp { get; set; }


        /// <summary>
        ///     Order ID
        /// </summary>
        [DataMember(Name = "order_id", EmitDefaultValue = false)]
        public long? OrderId { get; set; }

        /// <summary>
        ///     Order status ID. Available values: 
        ///     0: New, 
        ///     1: Inbound Scan, 
        ///     2: Sorted by Territory, 
        ///     3: Loaded, 
        ///     4: Missing, 
        ///     5: Damaged, 
        ///     6: Manually Loaded, 
        ///     7: Routed, 
        ///     8: Unrouted, 
        ///     9: Sorted by Route, 
        ///     10: Route Started, 
        ///     11: Failed, 
        ///     12: Skipped, 
        ///     13: Done, 
        ///     14: Cancelled, 
        ///     15: Scheduled
        /// </summary>
        [DataMember(Name = "order_status_id", EmitDefaultValue = false)]
        public int OrderStatusId { get; set; }

        /// <summary>
        ///     Address 1 field. Required
        /// </summary>
        [DataMember(Name = "address_1")]
        public string Address1 { get; set; }

        /// <summary>
        ///     Address 2 field
        /// </summary>
        [DataMember(Name = "address_2", EmitDefaultValue = false)]
        public string Address2 { get; set; }

        /// <summary>
        ///     The id of the member inside the route4me system
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long MemberId { get; set; }

        /// <summary>
        ///     Geo latitude
        /// </summary>
        [DataMember(Name = "cached_lat", EmitDefaultValue = false)]
        public double CachedLat { get; set; }

        /// <summary>
        ///     Geo longitude
        /// </summary>
        [DataMember(Name = "cached_lng", EmitDefaultValue = false)]
        public double CachedLng { get; set; }

        /// <summary>
        /// Order icon hexadecimal color on a map.
        /// </summary>
        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color { get; set; }

        /// <summary>
        ///     Generate optimal routes and driving directions to this curbside latitude
        /// </summary>
        [DataMember(Name = "curbside_lat", EmitDefaultValue = false)]
        public double? CurbsideLat { get; set; }

        /// <summary>
        ///     Generate optimal routes and driving directions to the curbside langitude
        /// </summary>
        [DataMember(Name = "curbside_lng", EmitDefaultValue = false)]
        public double? CurbsideLng { get; set; }

        /// <summary>
        /// In how many routes is included address.
        /// </summary>
        [DataMember(Name = "in_route_count", EmitDefaultValue = false)]
        public int? InRouteCount { get; set; }

        /// <summary>
        /// When the last time an address was visited.
        /// </summary>
        [DataMember(Name = "last_visited_timestamp", EmitDefaultValue = false)]
        public long? LastVisitedTimestamp { get; set; }

        /// <summary>
        /// When last time an address was included in a route.
        /// </summary>
        [DataMember(Name = "last_routed_timestamp", EmitDefaultValue = false)]
        public long? LastRoutedTimestamp { get; set; }

        /// <summary>
        /// Order inserted date
        /// </summary>
        [DataMember(Name = "day_added_YYMMDD")]
        public string DayAddedYYMMDD { get; set; }

        /// <summary>
        ///     Scheduled day (format: yyyy-MM-dd)
        /// </summary>
        [DataMember(Name = "day_scheduled_for_YYMMDD", EmitDefaultValue = false)]
        public string DayScheduledFor_YYYYMMDD { get; set; }

        /// <summary>
        ///     Address Alias. Required
        /// </summary>
        [DataMember(Name = "address_alias")]
        public string AddressAlias { get; set; }

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
        ///     Service time
        /// </summary>
        [DataMember(Name = "service_time", EmitDefaultValue = false)]
        public long? ServiceTime { get; set; }

        /// <summary>
        ///     Local timezone string
        /// </summary>
        [DataMember(Name = "local_timezone_string", EmitDefaultValue = false)]
        public string LocalTimezoneString { get; set; }

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
        ///     Email
        /// </summary>
        [DataMember(Name = "EXT_FIELD_email", EmitDefaultValue = false)]
        public string ExtFieldEmail { get; set; }

        /// <summary>
        ///     Phone number
        /// </summary>
        [DataMember(Name = "EXT_FIELD_phone", EmitDefaultValue = false)]
        public string ExtFieldPhone { get; set; }

        /// <summary>
        ///     Custom data
        /// </summary>
        [DataMember(Name = "EXT_FIELD_custom_data", EmitDefaultValue = false)]
        public Dictionary<string, string> ExtFieldCustomData { get; set; }

        /// <summary>
        ///     The weight of the order's cargo (2 digits precision).
        /// </summary>
        [DataMember(Name = "EXT_FIELD_weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        /// <summary>
        ///     Order cost (2 digits precision).
        /// </summary>
        [DataMember(Name = "EXT_FIELD_cost", EmitDefaultValue = false)]
        public double? Cost { get; set; }

        /// <summary>
        ///     Order revenue (2 digits precision).
        /// </summary>
        [DataMember(Name = "EXT_FIELD_revenue", EmitDefaultValue = false)]
        public double? Revenue { get; set; }

        /// <summary>
        ///     The cubic volume that this order consumes/contains on a vehicle (2 digits precision).
        /// </summary>
        [DataMember(Name = "EXT_FIELD_cube", EmitDefaultValue = false)]
        public double? Cube { get; set; }

        /// <summary>
        ///     The number of pieces/palllets that this order consumes/contains on a vehicle (2 digits precision).
        /// </summary>
        [DataMember(Name = "EXT_FIELD_pieces", EmitDefaultValue = false)]
        public double? Pieces { get; set; }

        /// <summary>
        ///     The city the address is located in
        /// </summary>
        [DataMember(Name = "address_city", EmitDefaultValue = false)]
        public string AddressCity { get; set; }

        /// <summary>
        ///     The state the address is located in
        /// </summary>
        [DataMember(Name = "address_state_id", EmitDefaultValue = false)]
        public string AddressStateId { get; set; }

        /// <summary>
        ///     The country the address is located in
        /// </summary>
        [DataMember(Name = "address_country_id", EmitDefaultValue = false)]
        public string AddressCountryId { get; set; }

        /// <summary>
        ///     The zip code the address is located in
        /// </summary>
        [DataMember(Name = "address_zip", EmitDefaultValue = false)]
        public string AddressZip { get; set; }

        /// <summary>
        ///     URL to an order icon file
        /// </summary>
        [DataMember(Name = "order_icon", EmitDefaultValue = false)]
        public string OrderIcon { get; set; }

        /// <summary>
        ///     If true, the order is validated.
        /// </summary>
        [DataMember(Name = "is_validated", EmitDefaultValue = false)]
        public bool? IsValidated { get; set; }

        /// <summary>
        ///     If true, the order is pending.
        /// </summary>
        [DataMember(Name = "is_pending", EmitDefaultValue = false)]
        public bool? IsPending { get; set; }

        /// <summary>
        ///     If true, the order is accepted.
        /// </summary>
        [DataMember(Name = "is_accepted", EmitDefaultValue = false)]
        public bool? IsAccepted { get; set; }

        /// <summary>
        ///     If true, the order is started.
        /// </summary>
        [DataMember(Name = "is_started", EmitDefaultValue = false)]
        public bool? IsStarted { get; set; }

        /// <summary>
        ///     If true, the order is completed.
        /// </summary>
        [DataMember(Name = "is_completed", EmitDefaultValue = false)]
        public bool? IsCompleted { get; set; }

        /// <summary>
        ///     Custom user fields.
        /// </summary>
        [DataMember(Name = "custom_user_fields", EmitDefaultValue = false)]
        public OrderCustomField[] CustomUserFields { get; set; }

        /// <summary>
        ///     How many times the order visited.
        /// </summary>
        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int VisitedCount { get; set; }

        /// <summary>
        ///     Route address stop type. For available values see Enums.AddressStopType
        ///     DELIVERY, PICKUP, BREAK, MEETUP, SERVICE, VISIT, DRIVEBY
        /// </summary>
        [DataMember(Name = "address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>
        /// Last status of the order.
        /// </summary>
        [DataMember(Name = "last_status")]
        public string lastStatus { get; set; }

        /// <summary>
        /// An order sorted on date.
        /// </summary>
        [DataMember(Name = "sorted_on_date")]
        public string SortedOnDate { get; set; }

        /// <summary>
        ///     System-wide unique code, which permits end-users (recipients)
        ///     to track the status of their order.
        /// </summary>
        [DataMember(Name = "tracking_number", EmitDefaultValue = false)]
        [ReadOnly(true)]
        public string TrackingNumber { get; set; }

        /// <summary>
        ///     Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        ///     A unique ID of a group the organizations or partners.
        /// </summary>
        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public int? OrganizationId { get; set; }

        /// <summary>
        ///     A day ID, when the order was completed.
        /// </summary>
        [DataMember(Name = "done_day_id", EmitDefaultValue = false)]
        public int? DoneDayId { get; set; }

        /// <summary>
        ///     A day ID, when the order was possessed by an organization 
        ///     (number of the days passed from 1/1/2010).
        /// </summary>
        [DataMember(Name = "possession_day_id", EmitDefaultValue = false)]
        public int? PossessionDayId { get; set; }
    }
}