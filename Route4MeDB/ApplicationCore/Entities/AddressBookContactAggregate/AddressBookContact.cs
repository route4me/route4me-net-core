using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate
{
    public class AddressBookContact : BaseEntity, IAggregateRoot, ICloneable
    {
        public AddressBookContact()
        {
            // required by EF
        }

        /// <summary>
        /// AddressBookContact class constructor.
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="cachedLat"></param>
        /// <param name="cachedLng"></param>
        /// <param name="addressAlias"></param>
        /// <param name="addressId"></param>
        public AddressBookContact(string address1, double cachedLat, double cachedLng, string addressAlias = null, int? addressId =null )
        {
            Guard.Against.NullOrEmpty(address1, nameof(address1));
            Guard.Against.Null(cachedLat, nameof(cachedLat));
            Guard.Against.Null(cachedLng, nameof(cachedLng));

            Address1 = address1;
            CachedLat = cachedLat;
            CachedLng = cachedLng;

            if (addressId != null) AddressId = (int)addressId;
            if (addressAlias != null) AddressAlias = addressAlias;
        }

        /// <summary>
        /// AddressBookContact class constructor.
        /// <remarks>Creates new object with the selected fields of an existing contact</remarks>
        /// </summary>
        /// <param name="addressBookContact">Existing address book contact</param>
        /// <param name="fields">List of the fields to be used for creating new contact</param>
        public AddressBookContact(AddressBookContact addressBookContact, List<string> fields)
        {
            addressBookContact.GetType().GetProperties().ToList()
                .ForEach(x => {
                    if (fields.Contains(x.Name)) x.SetValue(this, x.GetValue(addressBookContact));
                });
        }

        [Column("created_timestamp")]
        public int CreatedTimestamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("address_id")]
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

        [Range(-90, 90)]
        [Column("cached_lat")]
        public double CachedLat { get; set; }

        [Range(-180, 180)]
        [Column("cached_lng")]
        public double CachedLng { get; set; }

        [Range(-90, 90)]
        [Column("curbside_lat")]
        public double CurbsideLat { get; set; }

        [Range(-180, 180)]
        [Column("curbside_lng")]
        public double CurbsideLng { get; set; }

        [Column("address_city")]
        public string AddressCity { get; set; }

        [Column("address_state_id")]
        public string AddressStateId { get; set; }

        [Column("address_country_id")]
        public string AddressCountryId { get; set; }

        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$")]
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

        public object Clone()
        {
            AddressBookContact clonedContact = new AddressBookContact();
            this.GetType().GetProperties().ToList()
                .ForEach(x => {
                    if (x.Name!="AddressId") x.SetValue(clonedContact, x.GetValue(this));
                });

            return clonedContact;
        }
    }
}

