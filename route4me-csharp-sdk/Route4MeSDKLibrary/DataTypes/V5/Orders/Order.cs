using System.Runtime.Serialization;

using Newtonsoft.Json.Linq;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    ///     The order data structure
    /// </summary>
    [DataContract]
    public class Order : GenericParameters
    {
        /// <summary>
        ///     Order ID
        /// </summary>
        [DataMember(Name = "order_id")]
        public string OrderId { get; set; }

        /// <summary>
        ///     Order UUID
        /// </summary>
        [DataMember(Name = "order_uuid")]
        public string OrderUuid { get; set; }

        /// <summary>
        ///     The id of the member inside the route4me system
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long MemberId { get; set; }

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
        /// Order inserted date
        /// </summary>
        [DataMember(Name = "day_added_YYMMDD", EmitDefaultValue = false)]
        public string DayAddedYYMMDD { get; set; }

        /// <summary>
        ///     Scheduled day (format: yyyy-MM-dd)
        /// </summary>
        [DataMember(Name = "day_scheduled_for_YYMMDD", EmitDefaultValue = false)]
        public string DayScheduledFor_YYYYMMDD { get; set; }

        /// <summary>
        ///     Address Alias. Required
        /// </summary>
        [DataMember(Name = "address_alias", EmitDefaultValue = false)]
        public string AddressAlias { get; set; }

        /// <summary>
        ///     Address 1 field. Required
        /// </summary>
        [DataMember(Name = "address_1", EmitDefaultValue = false)]
        public string Address1 { get; set; }

        /// <summary>
        ///     Address 2 field
        /// </summary>
        [DataMember(Name = "address_2", EmitDefaultValue = false)]
        public string Address2 { get; set; }

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
        ///     Address geo
        /// </summary>
        [DataMember(Name = "address_geo", EmitDefaultValue = false)]
        public GeoPoint AddressGeo { get; set; }

        /// <summary>
        ///     Curbside geo
        /// </summary>
        [DataMember(Name = "curbside_geo", EmitDefaultValue = false)]
        public GeoPoint Curbside { get; set; }

        /// <summary>
        ///     Custom data
        /// </summary>
        [DataMember(Name = "custom_data", EmitDefaultValue = false)]
        public JToken CustomData { get; set; }

        /// <summary>
        ///     First name
        /// </summary>
        [DataMember(Name = "first_name", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        ///     Last name
        /// </summary>
        [DataMember(Name = "last_name", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        ///     Phone number
        /// </summary>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        ///     The weight of the order's cargo (2 digits precision).
        /// </summary>
        [DataMember(Name = "weight", EmitDefaultValue = false)]
        public double? Weight { get; set; }

        /// <summary>
        ///     Order cost (2 digits precision).
        /// </summary>
        [DataMember(Name = "cost", EmitDefaultValue = false)]
        public double? Cost { get; set; }

        /// <summary>
        ///     Order revenue (2 digits precision).
        /// </summary>
        [DataMember(Name = "revenue", EmitDefaultValue = false)]
        public double? Revenue { get; set; }

        /// <summary>
        ///     The cubic volume that this order consumes/contains on a vehicle (2 digits precision).
        /// </summary>
        [DataMember(Name = "cube", EmitDefaultValue = false)]
        public double? Cube { get; set; }

        /// <summary>
        ///     The number of pieces/palllets that this order consumes/contains on a vehicle (2 digits precision).
        /// </summary>
        [DataMember(Name = "pieces", EmitDefaultValue = false)]
        public double? Pieces { get; set; }

        /// <summary>
        ///     How many times the order visited.
        /// </summary>
        [DataMember(Name = "visited_count", EmitDefaultValue = false)]
        public int? VisitedCount { get; set; }

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
        /// Local time window.
        /// </summary>
        [DataMember(Name = "local_time_window", EmitDefaultValue = false)]
        public Range[] LocalTimeWindow { get; set; }

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
        /// Order icon hexadecimal color on a map.
        /// </summary>
        [DataMember(Name = "color", EmitDefaultValue = false)]
        public string Color { get; set; }

        /// <summary>
        ///     URL to an order icon file
        /// </summary>
        [DataMember(Name = "order_icon", EmitDefaultValue = false)]
        public string OrderIcon { get; set; }

        /// <summary>
        ///     If true, the order is validated.
        /// </summary>
        [DataMember(Name = "is_validated", EmitDefaultValue = false)]
        public bool IsValidated { get; set; }

        /// <summary>
        ///     If true, the order is pending.
        /// </summary>
        [DataMember(Name = "is_pending", EmitDefaultValue = false)]
        public bool IsPending { get; set; }

        /// <summary>
        ///     If true, the order is accepted.
        /// </summary>
        [DataMember(Name = "is_accepted", EmitDefaultValue = false)]
        public bool IsAccepted { get; set; }

        /// <summary>
        ///     If true, the order is started.
        /// </summary>
        [DataMember(Name = "is_started", EmitDefaultValue = false)]
        public bool IsStarted { get; set; }

        /// <summary>
        ///     If true, the order is completed.
        /// </summary>
        [DataMember(Name = "is_completed", EmitDefaultValue = false)]
        public bool IsCompleted { get; set; }

        /// <summary>
        ///     System-wide unique code, which permits end-users (recipients)
        ///     to track the status of their order.
        /// </summary>
        [DataMember(Name = "tracking_number", EmitDefaultValue = false)]
        public string TrackingNumber { get; set; }

        /// <summary>
        ///     Route address stop type. For available values see Enums.AddressStopType
        ///     DELIVERY, PICKUP, BREAK, MEETUP, SERVICE, VISIT, DRIVEBY
        /// </summary>
        [DataMember(Name = "address_stop_type", EmitDefaultValue = false)]
        public string AddressStopType { get; set; }

        /// <summary>
        /// Last status of the order.
        /// </summary>
        [DataMember(Name = "last_status", EmitDefaultValue = false)]
        public int LastStatus { get; set; }

        /// <summary>
        /// An order sorted on date.
        /// </summary>
        [DataMember(Name = "sorted_on_date", EmitDefaultValue = false)]
        public string SortedOnDate { get; set; }

        /// <summary>
        /// Custom fields
        /// </summary>
        [DataMember(Name = "custom_fields", EmitDefaultValue = false)]
        public OrderCustomField[] CustomFields { get; set; }

        /// <summary>
        ///     Group.
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public string Group { get; set; }

        /// <summary>
        /// Territory IDs
        /// </summary>
        [DataMember(Name = "territory_ids", EmitDefaultValue = false)]
        public string[] TerritoryIds { get; set; }

        /// <summary>
        /// Aggregation IDs
        /// </summary>
        [DataMember(Name = "aggregation_ids", EmitDefaultValue = false)]
        public string[] AggregationIds { get; set; }

        /// <summary>
        /// Facility IDs (hexadecimal UUID strings)
        /// </summary>
        [DataMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }

        /// <summary>
        /// Organization Api Key
        /// </summary>
        [HttpQueryMember(Name = "organization_api_key", EmitDefaultValue = false)]
        public string OrganizationApiKey { get; set; }
    }
}