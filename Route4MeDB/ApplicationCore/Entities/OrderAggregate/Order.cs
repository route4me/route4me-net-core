using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public Order()
        {
            // required by EF
        }

        public Order(Order orderParameters)
        {

        }

        public Order(string address1, double cachedLat, double cachedLng, string addressAlias = null, int? orderId = null)
        {
            Guard.Against.NullOrEmpty(address1, nameof(address1));
            Guard.Against.Null(cachedLat, nameof(cachedLat));
            Guard.Against.Null(cachedLng, nameof(cachedLng));

            Address1 = address1;
            CachedLat = cachedLat;
            CachedLng = cachedLng;

            if (orderId != null) OrderId = Convert.ToInt32(orderId);
            if (addressAlias != null) AddressAlias = addressAlias;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("order_db_id")]
        public int OrderDbId { get; set; }

        /// <summary>
        /// Order ID
        /// </summary>
        [Column("order_id")]
        public int? OrderId { get; set; }

        /// <summary>
        /// Address 1 field. Required
        /// </summary>
        [Column("address_1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Address 2 field
        /// </summary>
        [Column("address_2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Geo latitude. Required
        /// </summary>
        [Range(-90, 90)]
        [Column("cached_lat")]
        public double CachedLat { get; set; }

        /// <summary>
        /// Geo longitude. Required
        /// </summary>
        [Range(-180, 180)]
        [Column("cached_lng")]
        public double CachedLng { get; set; }

        /// <summary>
        /// Generate optimal routes and driving directions to this curbside latitude
        /// </summary>
        [Range(-90, 90)]
        [Column("curbside_lat")]
        public double CurbsideLat { get; set; }

        /// <summary>
        /// Generate optimal routes and driving directions to the curbside langitude
        /// </summary>
        [Range(-180, 180)]
        [Column("curbside_lng")]
        public double CurbsideLng { get; set; }

        /// <summary>
        /// Scheduled day
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
        [RegularExpression(@"2(0|1)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])")]
        [Column("day_scheduled_for_YYMMDD")]
        public string DayScheduledForYyMmDd { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy-MM-dd}")]
        [RegularExpression(@"2(0|1)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])")]
        [Column("day_added_YYMMDD")]
        public string DayAddedYyMmDd { get; set; }

        /// <summary>
        /// Address Alias. Required
        /// </summary>
        [Column("address_alias")]
        public string AddressAlias { get; set; }

        /// <summary>
        /// Local time window start
        /// </summary>
        [Column("local_time_window_start")]
        public int? LocalTimeWindowStart { get; set; }

        /// <summary>
        /// Local time window end
        /// </summary>
        [Column("local_time_window_end")]
        public long? LocalTimeWindowEnd { get; set; }

        /// <summary>
        /// Second Local time window start
        /// </summary>
        [Column("local_time_window_start_2")]
        public long? LocalTimeWindowStart2 { get; set; }

        /// <summary>
        /// Second local time window end
        /// </summary>
        [Column("local_time_window_end_2")]
        public long? LocalTimeWindowEnd2 { get; set; }

        /// <summary>
        /// Service time
        /// </summary>
        [Column("service_time")]
        public long? ServiceTime { get; set; }

        /// <summary>
        /// Address City
        /// </summary>
        [Column("address_city")]
        public string AddressCity { get; set; }

        /// <summary>
        /// Address state ID
        /// </summary>
        [Column("address_state_id")]
        public string AddressStateId { get; set; }

        /// <summary>
        /// Address country ID
        /// </summary>
        [Column("address_country_id")]
        public string AddressCountryId { get; set; }

        /// <summary>
        /// Address ZIP code
        /// </summary>
        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$")]
        [Column("address_zip")]
        public string AddressZip { get; set; }

        // <summary>
        /// Order status ID
        /// </summary>
        [Column("order_status_id")]
        public int? OrderStatusId { get; set; }

        /// <summary>
        /// The id of the member inside the route4me system
        /// </summary>
        [Column("member_id")]
        public int? MemberId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Column("EXT_FIELD_first_name")]
        public string EXT_FIELD_first_name { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Column("EXT_FIELD_last_name")]
        public string EXT_FIELD_last_name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Column("EXT_FIELD_email")]
        public string EXT_FIELD_email { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        [Column("EXT_FIELD_phone")]
        public string EXT_FIELD_phone { get; set; }

        /// <summary>
        /// Custom data
        /// </summary>
        [Column("EXT_FIELD_custom_data")]
        public string ExtFieldCustomData { get; set; }

        [NotMapped]
        public Dictionary<string, string> EXT_FIELD_custom_datas
        {
            get { return ExtFieldCustomData == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(ExtFieldCustomData); }
            set { ExtFieldCustomData = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Local timezone string
        /// </summary>
        [Column("local_timezone_string")]
        public string LocalTimezoneString { get; set; }

        /// <summary>
        /// Order icon
        /// </summary>
        [Column("order_icon")]
        public string OrderIcon { get; set; }

        /// <summary>
        /// Custom user fields.
        /// </summary>
        [Column("custom_user_fields")]
        public string CustomUserFields { get; set; }

        [NotMapped]
        public OrderCustomField[] CustomFieldsObj
        {
            get { return CustomUserFields == null ? null : JsonConvert.DeserializeObject<OrderCustomField[]>(CustomUserFields); }
            set { CustomUserFields = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// How many times the order visited.
        /// </summary>
        [Column("visited_count")]
        public int VisitedCount { get; set; }
    }
}
