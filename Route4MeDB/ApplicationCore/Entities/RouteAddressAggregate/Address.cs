using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.GeocodingAggregate;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.RouteAggregate;

namespace Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate
{
    public class Address : IAggregateRoot
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("route_destination_db_id")]
        public int RouteDestinationDbId { get; set; }

        /// <summary>
        /// Route destination ID
        /// </summary>
        [Column("route_destination_id")]
        public int? RouteDestinationId { get; set; }

        /// <summary>
        /// Route alias
        /// </summary>
        [Column("alias")]
        [MaxLength(250)]
        public string Alias { get; set; }

        /// <summary>
        /// Member ID
        /// </summary>
        // The ID of the member inside the Route4Me system.
        [Column("member_id")]
        public int MemberId { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Column("first_name")]
        [MaxLength(64)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Column("last_name")]
        [MaxLength(64)]
        public string LastName { get; set; }

        /// <summary>
        /// Route destination address
        /// </summary>
        [Column("address")]
        [MaxLength(250)]
        public string AddressString { get; set; }

        /// <summary>
        /// Route address stop type
        /// </summary>
        [Column("address_stop_type")]
        [MaxLength(16)]
        public Enum.AddressStopType AddressStopType { get; set; }

        /// <summary>
        /// Designate this stop as a depot.
        /// A route may have multiple depots/points of origin.
        /// </summary>
        [Column("is_depot")]
        public bool? IsDepot { get; set; }

        /// <summary>
        /// Timeframe violation state
        /// </summary>
        [Column("timeframe_violation_state")]
        public int? TimeframeViolationState { get; set; }

        /// <summary>
        /// Timeframe violation time
        /// </summary>
        [Column("timeframe_violation_time")]
        public int? TimeframeViolationTime { get; set; }

        /// <summary>
        /// Timeframe violation rate
        /// </summary>
        [Column("timeframe_violation_rate")]
        public double? TimeframeViolationRate { get; set; }

        /// <summary>
        /// The latitude of this address
        /// </summary>
        [Range(-90, 90)]
        [Column("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of this address
        /// </summary>
        [Range(-180, 180)]
        [Column("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// The ID of the route being viewed, modified, or erased.
        /// </summary>
        [Column("route_id", TypeName = "varchar(32)")]
        public string RouteId { get; set; }
        
        [Column("route_name")]
        [MaxLength(250)]
        public string RouteName { get; set; }

        /// <summary>
        /// If this route was duplicated from an existing route, this value would have the original route's ID.
        /// </summary>
        [Column("original_route_id")]
        public string OriginalRouteId { get; set; }

        /// <summary>
        /// The ID of the optimization request that was used to initially instantiate this route.
        /// </summary>
        [Column("optimization_problem_id", TypeName = "varchar(32)")]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        /// The destination's sequence number in the route.
        /// </summary>
        [Column("sequence_no")]
        public int? SequenceNo { get; set; }

        /// <summary>
        /// True if the address is geocoded.
        /// </summary>
        [Column("geocoded")]
        public bool? Geocoded { get; set; }

        /// <summary>
        /// The preferred geocoding number.
        /// </summary>
        [Column("preferred_geocoding")]
        public int? PreferredGeocoding { get; set; }

        /// <summary>
        /// True if geocoding failed.
        /// </summary>
        [Column("failed_geocoding")]
        public bool? FailedGeocoding { get; set; }

        /// <summary>
        /// An array containing Geocoding objects.
        /// </summary>
        [Column("geocodings")]
        public Geocoding[] Geocodings { get; set; }

        /// <summary>
        /// When planning a route from the address book or using existing address book IDs, 
        /// pass the address book ID (contact_id) for an address so that Route4Me can run
        /// analytics on the address book addresses that were used to plan routes, and to find previous visits to 
        /// favorite addresses.
        /// </summary>
        [Column("contact_id")]
        public int? ContactId { get; set; }

        /// <summary>
        /// The status flag to mark an address as visited (aka check in).
        /// </summary>
        [Column("is_visited")]
        public bool? IsVisited { get; set; }

        /// <summary>
        /// The status flag to mark an address as departed (aka check out).
        /// </summary>
        [Column("is_departed")]
        public bool? IsDeparted { get; set; }

        /// <summary>
        /// The last known visited timestamp of this address.
        /// </summary>
        [Column("timestamp_last_visited")]
        public uint? TimestampLastVisited { get; set; }

        /// <summary>
        /// The last known departed timestamp of this address.
        /// </summary>
        [Column("timestamp_last_departed")]
        public uint? TimestampLastDeparted { get; set; }

        /// <summary>
        /// Visited address latitude
        /// </summary>
        [Range(-90, 90)]
        [Column("visited_lat")]
        public double? VisitedLatitude { get; set; }

        /// <summary>
        /// Visited address longitude
        /// </summary>
        [Range(-180, 180)]
        [Column("visited_lng")]
        public double? VisitedLongitude { get; set; }

        /// <summary>
        /// Departed address latitude
        /// </summary>
        [Range(-90, 90)]
        [Column("departed_lat")]
        public double? DepartedLatitude { get; set; }

        /// <summary>
        /// Departed address longitude
        /// </summary>
        [Range(-180, 180)]
        [Column("departed_lng")]
        public double? DepartedLongitude { get; set; }

        /// <summary>
        /// The address group
        /// </summary>
        [Column("group")]
        public string Group { get; set; }

        /// <summary>
        /// Pass-through data about this route destination.
        /// The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [Column("customer_po")]
        public string CustomerPo { get; set; }

        /// <summary>
        /// Pass-through data about this route destination.
        /// The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [Column("invoice_no")]
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Pass-through data about this route destination.
        /// The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [Column("reference_no")]
        public string ReferenceNo { get; set; }

        /// <summary>
        /// Pass-through data about this route destination.
        /// The data will be visible on the manifest, website, and mobile apps.
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; }

        /// <summary>
        /// The address order ID
        /// </summary>
        [Column("order_id")]
        public int? OrderId { get; set; }

        /// <summary>
        /// The address cargo weight
        /// </summary>
        [Column("weight")]
        public decimal? Weight { get; set; }

        /// <summary>
        /// The address cost
        /// </summary>
        [Column("cost")]
        public decimal? Cost { get; set; }

        /// <summary>
        /// The address revenue
        /// </summary>
        [Column("revenue")]
        public decimal? Revenue { get; set; }

        /// <summary>
        /// The cubic volume that this destination/order/line-item consumes/contains.
        /// This is how much space it will take up on a vehicle.
        /// </summary>
        [Column("cube")]
        public decimal? Cube { get; set; }

        /// <summary>
        /// The number of pieces/palllets that this destination/order/line-item consumes/contains on a vehicle.
        /// </summary>
        [Column("pieces")]
        public int? Pieces { get; set; }

        /// <summary>
        /// Pass-through data about this route destination.
        /// The data will be visible on the manifest, website, and mobile apps.
        /// Also used to email clients when vehicles are approaching (future capability).
        /// </summary>
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// Pass-through data about this route destination.
        /// The data will be visible on the manifest, website, and mobile apps.
        /// Also used to send SMS messages to clients when vehicles are approaching (future capability).
        /// </summary>
        [Column("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// The number of notes that are already associated with this address on the route.
        /// </summary>
        [Column("destination_note_count")]
        public int? DestinationNoteCount { get; set; }

        /// <summary>
        /// Server-side generated amount of km/miles that it will take to get to the next location on the route.
        /// </summary>
        [Column("drive_time_to_next_destination")]
        public int? DriveTimeToNextDestination { get; set; }

        /// <summary>
        /// Abnormal traffic time to next destination.
        /// </summary>
        [Column("abnormal_traffic_time_to_next_destination")]
        public int? AbnormalTrafficTimeToNextDestination { get; set; }

        /// <summary>
        /// Uncongested time to next destination.
        /// </summary>
        [Column("uncongested_time_to_next_destination")]
        public int? UncongestedTimeToNextDestination { get; set; }

        /// <summary>
        /// Traffic time to next destination.
        /// </summary>
        [Column("traffic_time_to_next_destination")]
        public int? TrafficTimeToNextDestination { get; set; }

        /// <summary>
        /// Server-side generated amount of seconds that it will take to get to the next location.
        /// </summary>
        [Column("distance_to_next_destination")]
        public decimal? DistanceToNextDestination { get; set; }

        /// <summary>
        /// Generated time window start.
        /// </summary>
        [Column("generated_time_window_start")]
        public int? GeneratedTimeWindowStart { get; set; }

        /// <summary>
        /// Estimated time window end based on the optimization engine, after all the sequencing has been completed.
        /// </summary>
        [Column("generated_time_window_end")]
        public int? GeneratedTimeWindowEnd { get; set; }

        /// <summary>
        /// The unique socket channel name which should be used to get real time alerts.
        /// </summary>
        [Column("channel_name")]
        public string ChannelName { get; set; }

        /// <summary>
        /// The address time window start.
        /// </summary>
        [Column("time_window_start")]
        public int? TimeWindowStart { get; set; }

        /// <summary>
        /// The address time window end.
        /// </summary>
        [Column("time_window_end")]
        public int? TimeWindowEnd { get; set; }

        /// <summary>
        /// The address time window start 2.
        /// </summary>
        [Column("time_window_start_2")]
        public int? TimeWindowStart2 { get; set; }

        /// <summary>
        /// The address time window end 2.
        /// </summary>
        [Column("time_window_end_2")]
        public int? TimeWindowEnd2 { get; set; }

        /// <summary>
        /// Geofence detected visited timestamp
        /// </summary>
        [Column("geofence_detected_visited_timestamp")]
        public int? geofence_detected_visited_timestamp { get; set; }

        /// <summary>
        /// Geofence detected departed timestamp
        /// </summary>
        [Column("geofence_detected_departed_timestamp")]
        public int? geofence_detected_departed_timestamp { get; set; }

        /// <summary>
        /// Geofence detected service time
        /// </summary>
        [Column("geofence_detected_service_time")]
        public int? geofence_detected_service_time { get; set; }

        /// <summary>
        /// Geofence detected visited latitude
        /// </summary>
        [Range(-90, 90)]
        [Column("geofence_detected_visited_lat")]
        public double? geofence_detected_visited_lat { get; set; }

        /// <summary>
        /// Geofence detected visited longitude
        /// </summary>
        [Range(-180, 180)]
        [Column("geofence_detected_visited_lng")]
        public double? geofence_detected_visited_lng { get; set; }

        /// <summary>
        /// Geofence detected departed latitude
        /// </summary>
        [Range(-90, 90)]
        [Column("geofence_detected_departed_lat")]
        public double? geofence_detected_departed_lat { get; set; }

        /// <summary>
        /// Geofence detected departed longitude
        /// </summary>
        [Range(-180, 180)]
        [Column("geofence_detected_departed_lng")]
        public double? geofence_detected_departed_lng { get; set; }

        /// <summary>
        /// The expected amount of time that will be spent at this address by the driver/user.
        /// </summary>
        [Column("time")]
        public int? Time { get; set; }

        /// <summary>
        /// The address notes
        /// </summary>
        [Column("notes")]
        public ICollection<AddressNote> Notes { get; set; }

        [Column("path_to_next")]
        public List<PathToNext> PathToNext { get; set; }

        /// <summary>
        /// If present, the priority will sequence addresses in all the optimal routes so that
        /// higher priority addresses are general at the beginning of the route sequence.
        /// 1 is the highest priority, 100000 is the lowest.
        /// </summary>
        [Column("priority")]
        public int? Priority { get; set; }

        /// <summary>
        /// Curbside latitude.
        /// Generate optimal routes and driving directions to this curbside latitude.
        /// </summary>
        [Range(-90, 90)]
        [Column("curbside_lat")]
        public double? CurbsideLatitude { get; set; }

        /// <summary>
        /// Curbside longitude.
        /// Generate optimal routes and driving directions to the curbside longitude.
        /// </summary>
        [Range(-180, 180)]
        [Column("curbside_lng")]
        public double? CurbsideLongitude { get; set; }

        [Column("custom_fields")]
        public string CustomFields { get; set; }

        [NotMapped]
        public Dictionary<string, string> dicCustomFields
        {
            get { return CustomFields == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(CustomFields); }
            set { CustomFields = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The address custom fields in JSON format.
        /// </summary>
        [Column("custom_fields_str_json")]
        public string CustomFieldsStrJson { get; set; }

        /// <summary>
        /// The custom fields configuration.
        /// </summary>
        [Column("custom_fields_config")]
        public string CustomFieldsConfig { get; set; }

        [NotMapped]
        public string[] CustomFieldsConfigArray
        {
            get { return CustomFieldsConfig == null ? null : JsonConvert.DeserializeObject<string[]>(CustomFieldsConfig); }
            set { CustomFieldsConfig = JsonConvert.SerializeObject(value); }
        }

        [Column("manifest")]
        public string Manifest { get; set; }

        [NotMapped]
        public AddressManifest ManifestObj
        {
            get { return Manifest == null ? null : JsonConvert.DeserializeObject<AddressManifest>(Manifest); }
            set { Manifest = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The custom fields configuration in JSON format.
        /// </summary>
        [Column("custom_fields_config_str_json")]
        public string CustomFieldsConfigStrJson { get; set; }

        /// <summary>
        /// System-wide unique code, which permits end-users (recipients) to track the status of their order.
        /// </summary>
        [Column("tracking_number")]
        public string TrackingNumber { get; set; }

        public string OptimizationProblemDbId { get; set; }
        [ForeignKey("OptimizationProblemDbId")]
        public OptimizationProblem optimizationProblem { get; set;}

        public string RouteDbId { get; set; }
        [ForeignKey("RouteDbId")]
        public Route Route { get; set; }
    }
}
