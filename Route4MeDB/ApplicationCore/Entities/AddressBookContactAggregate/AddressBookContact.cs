using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class AddressBookContact : BaseEntity, IAggregateRoot
    {
        public AddressBookContact()
        {
            // required by EF
        }

        public AddressBookContact(int addressId, string address1)
        {
            Guard.Against.NullOrEmpty(address1, nameof(address1));
            Guard.Against.Null(addressId, nameof(addressId));

            AddressId = addressId;
            Address1 = address1;
        }

        public AddressBookContact(AddressBookContact addressBookContact)
        {

        }

        [Column("created_timestamp")]
        public int CreatedTimestamp { get; set; }

        [Column("address_id")]
        public int AddressId { get; set; }

        [Column("address_1")]
        public string Address1 { get; set; }

        [Column("address_2")]
        public string Address2 { get; set; }

        [Column("territory_name")]
        public string TerritoryName { get; set; }

        [Column("address_alias")]
        public string AddressAlias { get; set; }

        [Column("address_group")]
        public string AddressGroup { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("local_time_window_start")]
        public int? LocalTimeWindowStart { get; set; }

        [Column("local_time_window_end")]
        public int? LocalTimeWindowEnd { get; set; }

        [Column("local_time_window_start_2")]
        public int? LocalTimeWindowStart2 { get; set; }

        [Column("local_time_window_end_2")]
        public int? LocalTimeWindowEnd2 { get; set; }

        [Column("address_email")]
        public string AddressEmail { get; set; }

        [Column("address_phone_number")]
        public string AddressPhoneNumber { get; set; }

        [Column("cached_lat")]
        public double CachedLat { get; set; }

        [Column("cached_lng")]
        public double CachedLng { get; set; }

        [Column("curbside_lat")]
        public double CurbsideLat { get; set; }

        [Column("curbside_lng")]
        public double CurbsideLng { get; set; }

        [Column("address_city")]
        public string AddressCity { get; set; }

        [Column("address_state_id")]
        public string AddressStateId { get; set; }

        [Column("address_country_id")]
        public string AddressCountryId { get; set; }

        [Column("address_zip")]
        public string AddressZip { get; set; }

        [Column("address_custom_data")]
        public string AddressCustomData { get; set; }

        [NotMapped]
        public Dictionary<string, string> AddressCustomDatas
        {
            get { return AddressCustomData == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(AddressCustomData); }
            set { AddressCustomData = JsonConvert.SerializeObject(value); }
        }

        [Column("schedule")]
        internal string _Schedules { get; set; }

        [NotMapped]
        public IList<Schedule> Schedules
        {
            get { return _Schedules == null ? null : JsonConvert.DeserializeObject<IList<Schedule>>(_Schedules); }
            set { _Schedules = JsonConvert.SerializeObject(value); }
        }

        [Column("schedule_blacklist")]
        internal string _ScheduleBlackList { get; set; }

        [NotMapped]
        public string[] ScheduleBlackList
        {    
            get { return _ScheduleBlackList == null ? null : JsonConvert.DeserializeObject<string[]>(_ScheduleBlackList); }
            set { _ScheduleBlackList = JsonConvert.SerializeObject(value); }
        }

        [Column("service_time")]
        public int? ServiceTime { get; set; }

        [Column("local_timezone_string")]
        public string LocalTimezoneString { get; set; }

        [Column("color")]
        public string Color { get; set; }

        [Column("address_icon")]
        public string AddressIcon { get; set; }

        [Column("address_stop_type")]
        public string AddressStopType { get; set; }

        [Column("address_cube")]
        public float AddressCube { get; set; }

        [Column("address_pieces")]
        public int AddressPieces { get; set; }

        [Column("address_reference_no")]
        public string AddressReferenceNo { get; set; }

        [Column("address_revenue")]
        public float AddressRevenue { get; set; }

        [Column("address_weight")]
        public float AddressWeight { get; set; }

        [Column("address_priority")]
        public int? AddressPriority { get; set; }

        [Column("address_customer_po")]
        public string AddressCustomerPo { get; set; }
    }
}

