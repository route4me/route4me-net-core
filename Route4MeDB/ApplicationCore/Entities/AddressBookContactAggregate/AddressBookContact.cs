using Route4MeDB.ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

            if (addressId != null) AddressId = Convert.ToInt32(addressId);
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

        /// <summary>
        /// Time when the contact was created.
        /// </summary>
        [Column("created_timestamp")]
        public long CreatedTimestamp { get; set; }

        [Key, Column("address_db_id")]
        public int AddressDbId { get; set; }

        /// <summary>
        /// Unique ID of the contact.
        /// </summary>
        [Column("address_id")]
        public int? AddressId { get; set; }

        /// <summary>
        /// The geographic address of the contact.
        /// </summary>
        [Column("address_1")]
        public string Address1 { get; set; }

        /// <summary>
        /// Second geographic address of the contact.
        /// </summary>
        [Column("address_2")]
        public string Address2 { get; set; }

        /// <summary>
        /// Unique ID of the member.
        /// </summary>
        [Column("member_id")]
        public int? MemberId { get; set; }

        /// <summary>
        /// A territory shape name the contact belongs.
        /// </summary>
        [Column("territory_name")]
        public string TerritoryName { get; set; }

        /// <summary>
        /// The contact's alias.
        /// </summary>
        [Column("address_alias")]
        public string AddressAlias { get; set; }

        /// <summary>
        /// A group the contact belongs.
        /// </summary>
        [Column("address_group")]
        public string AddressGroup { get; set; }

        /// <summary>
        /// The first name of the contact person.
        /// </summary>
        [Column("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the contact person.
        /// </summary>
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Start of the contact's local time window.
        /// </summary>
        [Column("local_time_window_start")]
        public long? LocalTimeWindowStart { get; set; }

        /// <summary>
        /// End of the contact's local time window.
        /// </summary>
        [Column("local_time_window_end")]
        public long? LocalTimeWindowEnd { get; set; }

        /// <summary>
        /// Start of the contact's second local time window.
        /// </summary>
        [Column("local_time_window_start_2")]
        public long? LocalTimeWindowStart2 { get; set; }

        /// <summary>
        /// End of the contact's second local time window.
        /// </summary>
        [Column("local_time_window_end_2")]
        public long? LocalTimeWindowEnd2 { get; set; }

        /// <summary>
        /// The contact's email.
        /// </summary>
        [Column("address_email")]
        public string AddressEmail { get; set; }

        /// <summary>
        /// The contact's phone number.
        /// </summary>
        [Column("address_phone_number")]
        public string AddressPhoneNumber { get; set; }

        /// <summary>
        /// A latitude of the contact's cached position.
        /// </summary>
        [Range(-90, 90)]
        [Column("cached_lat")]
        public double CachedLat { get; set; }

        /// <summary>
        /// A longitude of the contact's cached position.
        /// </summary>
        [Range(-180, 180)]
        [Column("cached_lng")]
        public double CachedLng { get; set; }

        /// <summary>
        /// A latitude of the contact's curbside.
        /// </summary>
        [Range(-90, 90)]
        [Column("curbside_lat")]
        public double CurbsideLat { get; set; }

        /// <summary>
        /// A longitude of the contact's curbside.
        /// </summary>
        [Range(-180, 180)]
        [Column("curbside_lng")]
        public double CurbsideLng { get; set; }

        /// <summary>
        /// A city the contact belongs.
        /// </summary>
        [Column("address_city")]
        public string AddressCity { get; set; }

        /// <summary>
        /// The ID of the state the contact belongs.
        /// </summary>
        [Column("address_state_id")]
        public string AddressStateId { get; set; }

        /// <summary>
        /// The ID of the country the contact belongs.
        /// </summary>
        [Column("address_country_id")]
        public string AddressCountryId { get; set; }

        /// <summary>
        /// The contact's ZIP code.
        /// </summary>
        [RegularExpression("^[0-9]{5}(?:-[0-9]{4})?$")]
        [Column("address_zip")]
        public string AddressZip { get; set; }

        /// <summary>
        /// An array of the contact's custom field-value pairs.
        /// </summary>
        [Column("address_custom_data")]
        public string AddressCustomData { get; set; }
        
        /// <summary>
        /// Property for get/set custom data as Dictionary<string, string> object
        /// Gets/sets string data from/to AddressCustomData fild.
        /// This property will be not mapped as the table field.
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> AddressCustomDataDic
        {
            get { return AddressCustomData == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(AddressCustomData, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }); }
            set { AddressCustomData = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// An array of the contact's schedules.
        /// </summary>
        [Column("schedule")]
        public string Schedules { get; set; }

        [NotMapped]
        public Schedule[] SchedulesArray
        {
            get { return Schedules == null ? null : JsonConvert.DeserializeObject<Schedule[]>(Schedules); }
            set { Schedules = JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }); }
        }

        /// <summary>
        /// The list of dates that should be omitted from the schedules.
        /// </summary>
        [Column("schedule_blacklist")]
        public string ScheduleBlackList { get; set; }

        [NotMapped]
        public string[] ScheduleBlackListArray
        {
            get { return ScheduleBlackList == null ? null : JsonConvert.DeserializeObject<string[]>(ScheduleBlackList, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }); }
            set { ScheduleBlackList = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Number of the routes containing the contact.
        /// </summary>
        [Column("in_route_count")]
        public int? InRouteCount { get; set; }

        /// <summary>
        /// Number of the visits to the contact.
        /// </summary>
        [Column("visited_count")]
        public int? VisitedCount { get; set; }

        /// <summary>
        /// When the contact was last visited.
        /// </summary>
        [Column("last_visited_timestamp")]
        public long? LastVisitedTimestamp { get; set; }

        /// <summary>
        /// When the contact was last routed.
        /// </summary>
        [Column("last_routed_timestamp")]
        public long? LastRoutedTimestamp { get; set; }

        /// <summary>
        /// The service time at the contact's address.
        /// </summary>
        [Column("service_time")]
        public long? ServiceTime { get; set; }

        /// <summary>
        /// The contact's local timezone.
        /// </summary>
        [Column("local_timezone_string")]
        public string LocalTimezoneString { get; set; }

        /// <summary>
        /// The contact's color on the map.
        /// </summary>
        [Column("color")]
        public string Color { get; set; }

        /// <summary>
        /// The contact's icon on the map.
        /// </summary>
        [Column("address_icon")]
        public string AddressIcon { get; set; }

        /// <summary>
        /// The contact's stop type.
        /// </summary>
        [Column("address_stop_type")]
        public string AddressStopType { get; set; }

        /// <summary>
        /// The cubic volume of the contact's cargo.
        /// </summary>
        [Column("address_cube")]
        public double? AddressCube { get; set; }

        /// <summary>
        /// The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        /// </summary>
        [Column("address_pieces")]
        public int? AddressPieces { get; set; }

        /// <summary>
        /// The reference number of the address.
        /// </summary>
        [Column("address_reference_no")]
        public string AddressReferenceNo { get; set; }

        /// <summary>
        /// The revenue from the contact.
        /// </summary>
        [Column("address_revenue")]
        public double? AddressRevenue { get; set; }

        /// <summary>
        /// The weight of the contact's cargo.
        /// </summary>
        [Column("address_weight")]
        public double? AddressWeight { get; set; }

        /// <summary>
        /// If present, the priority will sequence addresses in all the optimal routes so that
        /// higher priority addresses are general at the beginning of the route sequence.
        /// 1 is the highest priority, 100000 is the lowest.
        /// </summary>
        [Column("address_priority")]
        public int? AddressPriority { get; set; }

        /// <summary>
        /// The customer purchase order of the contact.
        /// </summary>
        [Column("address_customer_po")]
        public string AddressCustomerPo { get; set; }

        /// <summary>
        /// If true, a location assigned to a route.
        /// </summary>
        [Column("is_assigned")]
        public bool? IsAssigned { get; set; }

        public object Clone()
        {
            AddressBookContact clonedContact = new AddressBookContact();
            this.GetType().GetProperties().ToList()
                .ForEach(x => {
                    if (x.Name!="AddressDbId") x.SetValue(clonedContact, x.GetValue(this));
                });

            return clonedContact;
        }
    }
}

