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
        private AddressBookContact()
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
        public int AddressId { get; private set; }

        [Column("address_1")]
        public string Address1 { get; private set; }

        [Column("address_2")]
        public string Address2 { get; private set; }

        [Column("territory_name")]
        public string TerritoryName { get; private set; }

        [Column("address_alias")]
        public string AddressAlias { get; private set; }

        [Column("address_group")]
        public string AddressGroup { get; private set; }

        [Column("first_name")]
        public string FirstName { get; private set; }

        [Column("last_name")]
        public string LastName { get; private set; }

        [Column("local_time_window_start")]
        public int? LocalTimeWindowStart { get; private set; }

        [Column("local_time_window_end")]
        public int? LocalTimeWindowEnd { get; private set; }

        [Column("local_time_window_start_2")]
        public int? LocalTimeWindowStart2 { get; private set; }

        [Column("local_time_window_end_2")]
        public int? LocalTimeWindowEnd2 { get; private set; }

        [Column("address_email")]
        public string AddressEmail { get; private set; }

        [Column("address_phone_number")]
        public string AddressPhoneNumber { get; private set; }

        [Column("cached_lat")]
        public double CachedLat { get; private set; }

        [Column("cached_lng")]
        public double CachedLng { get; private set; }

        [Column("curbside_lat")]
        public double CurbsideLat { get; private set; }

        [Column("curbside_lng")]
        public double CurbsideLng { get; private set; }

        [Column("address_city")]
        public string AddressCity { get; private set; }

        [Column("address_state_id")]
        public string AddressStateId { get; private set; }

        [Column("address_country_id")]
        public string AddressCountryId { get; private set; }

        [Column("address_zip")]
        public string AddressZip { get; private set; }

        [Column("address_custom_data")]
        internal string _AddressCustomData { get; set; }

        [NotMapped]
        public Dictionary<string, string> AddressCustomData
        {
            get { return _AddressCustomData == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(_AddressCustomData); }
            set { _AddressCustomData = JsonConvert.SerializeObject(value); }
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
        public int? ServiceTime { get; private set; }

        [Column("local_timezone_string")]
        public string LocalTimezoneString { get; private set; }

        [Column("color")]
        public string Color { get; private set; }

        [Column("address_icon")]
        public string AddressIcon { get; private set; }

        [Column("address_stop_type")]
        public string AddressStopType { get; private set; }

        [Column("address_cube")]
        public float AddressCube { get; private set; }

        [Column("address_pieces")]
        public int AddressPieces { get; private set; }

        [Column("address_reference_no")]
        public string AddressReferenceNo { get; private set; }

        [Column("address_revenue")]
        public float AddressRevenue { get; private set; }

        [Column("address_weight")]
        public float AddressWeight { get; private set; }

        [Column("address_priority")]
        public int? AddressPriority { get; private set; }

        [Column("address_customer_po")]
        public string AddressCustomerPo { get; private set; }
    }
}

