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

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("address_1")]
        public string Address1 { get; set; }

        [Column("address_2")]
        public string Address2 { get; set; }

        [Column("cached_lat")]
        public double CachedLat { get; set; }

        [Column("cached_lng")]
        public double CachedLng { get; set; }

        [Column("curbside_lat")]
        public double CurbsideLat { get; set; }

        [Column("curbside_lng")]
        public double CurbsideLng { get; set; }

        [Column("day_scheduled_for_YYMMDD")]
        public string DayScheduledForYyMmDd { get; set; }

        [Column("day_added_YYMMDD")]
        public string DayAddedYyMmDd { get; set; }

        [Column("address_alias")]
        public string AddressAlias { get; set; }

        [Column("local_time_window_start")]
        public int? LocalTimeWindowStart { get; set; }

        [Column("local_time_window_end")]
        public int? LocalTimeWindowEnd { get; set; }

        [Column("local_time_window_start_2")]
        public int? LocalTimeWindowStart2 { get; set; }

        [Column("local_time_window_end_2")]
        public int? LocalTimeWindowEnd2 { get; set; }

        [Column("service_time")]
        public int? ServiceTime { get; set; }

        [Column("address_city")]
        public string AddressCity { get; set; }

        [Column("address_state_id")]
        public string AddressStateId { get; set; }

        [Column("address_country_id")]
        public string AddressCountryId { get; set; }

        [Column("address_zip")]
        public string AddressZip { get; set; }

        [Column("order_status_id")]
        public int? OrderStatusId { get; set; }

        [Column("member_id")]
        public int? MemberId { get; set; }

        [Column("EXT_FIELD_first_name")]
        public string EXT_FIELD_first_name { get; set; }

        [Column("EXT_FIELD_last_name")]
        public string EXT_FIELD_last_name { get; set; }

        [Column("EXT_FIELD_email")]
        public string EXT_FIELD_email { get; set; }

        [Column("EXT_FIELD_phone")]
        public string EXT_FIELD_phone { get; set; }

        [Column("EXT_FIELD_custom_data")]
        public string ExtFieldCustomData { get; set; }

        [NotMapped]
        public Dictionary<string, string> EXT_FIELD_custom_datas
        {
            get { return ExtFieldCustomData == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(ExtFieldCustomData); }
            set { ExtFieldCustomData = JsonConvert.SerializeObject(value); }
        }

        [Column("local_timezone_string")]
        public string LocalTimezoneString { get; set; }

        [Column("order_icon")]
        public string OrderIcon { get; set; }
    }
}
