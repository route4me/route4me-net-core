using System;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Route4MeDB.ApplicationCore.Entities.GeocodingAggregate;
using Route4MeDB.ApplicationCore.Entities.RouteAddressAggregate;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    public class Route : BaseEntity, IAggregateRoot
    {
        [Key, Column("route_db_id", TypeName = "varchar(36)")]
        public string RouteDbId { get; set; }

        /// <summary>
        /// The route ID
        /// </summary>
        [Column("route_id", TypeName = "varchar(32)")]
        public string RouteId { get; set; }

        [Column("optimization_problem_id")]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        /// The member ID
        /// </summary>
        [Column("member_id")]
        public int? MemberId { get; set; }

        [Column("created_timestamp")]
        public Int64? CreatedTimestamp { get; set; }

        [Column("parameters")]
        public string Parameters { get; set; }

        [NotMapped]
        public RouteParameters ParametersObj
        {
            get { return Parameters == null ? null : JsonConvert.DeserializeObject<RouteParameters>(Parameters); }
            set { Parameters = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The member's email
        /// </summary>
        [Column("member_email")]
        public string MemberEmail { get; set; }

        /// <summary>
        /// The member's first name
        /// </summary>
        [Column("member_first_name")]
        public string MemberFirstName { get; set; }

        /// <summary>
        /// The member's last name
        /// </summary>
        [Column("member_last_name")]
        public string MemberLastName { get; set; }

        /// <summary>
        /// URL to a member picture
        /// </summary>
        [Column("member_picture")]
        public string MemberPicture { get; set; }

        /// <summary>
        /// Member tracking subheadline
        /// </summary>
        [Column("member_tracking_subheadline")]
        public string MemberTrackingSubheadline { get; set; }

        /// <summary>
        /// Route rating done by a user.
        /// <para>Available values: <value>0, 1, 2, 3, 4, 5</value></para>
        /// <remarks><para>A null value means no rating was given. 
        /// Users can rate routes so that future optimizations take these ratings into account.</para></remarks>
        /// </summary>
        [Column("user_route_rating")]
        public int? UserRouteRating { get; set; }

        /// <summary>
        /// If true, the order is approved for execution
        /// </summary>
        [Column("approved_for_execution")]
        public bool ApprovedForExecution { get; set; }

        /// <summary>
        /// If true, route is unrouted.
        /// </summary>
        [Column("is_unrouted")]
        public bool? IsUnrouted { get; set; }

        /// <summary>
        /// Counter of the approved revisions
        /// </summary>
        [Column("approved_revisions_counter")]
        public int? ApprovedRevisionsCounter { get; set; }

        /// <summary>
        /// Vehicle alias
        /// </summary>
        [Column("vehicle_alias")]
        public string VehicleAlias { get; set; }

        /// <summary>
        /// Driver alias
        /// </summary>
        [Column("driver_alias")]
        public string DriverAlias { get; set; }

        /// <summary>
        /// Cost of the route
        /// </summary>
        [Column("route_cost")]
        public double? RouteCost { get; set; }

        /// <summary>
        /// Route weight
        /// </summary>
        [Column("route_weight")]
        public double? RouteWeight { get; set; }

        /// <summary>
        /// Route total cubic volume of the carried cargo (vehicle capacity)
        /// </summary>
        [Column("route_cube")]
        public double? RouteCube { get; set; }

        /// <summary>
        /// The total number of route pieces (vehicle pieces).
        /// </summary>
        [Column("route_pieces")]
        public int? RoutePieces { get; set; }

        /// <summary>
        /// Total route revenue
        /// </summary>
        [Column("route_revenue")]
        public double? RouteRevenue { get; set; }

        /// <summary>
        /// Net revenue per distance unit
        /// </summary>
        [Column("net_revenue_per_distance_unit")]
        public double? NetRevenuePerDistanceUnit { get; set; }

        [Column("udu_distance_unit", TypeName = "varchar(8)")]
        public string UduDistanceUnit { get; set; }

        [Column("udu_actual_travel_distance")]
        public double? UduActualTravelDistance { get; set; }

        [Column("total_wait_time")]
        public long? TotalWaitTime { get; set; }

        [Column("udu_trip_distance")]
        public double? UduTripDistance { get; set; }

        /// <summary>
        /// Miles per gallon
        /// </summary>
        [Column("mpg")]
        public double? Mpg { get; set; }

        /// <summary>
        /// Total route's trip distance
        /// </summary>
        [Column("trip_distance")]
        public double? TripDistance { get; set; }

        /// <summary>
        /// Gas price
        /// </summary>
        [Column("gas_price")]
        public double? GasPrice { get; set; }

        /// <summary>
        /// Total route duration (seconds)
        /// </summary>
        [Column("route_duration_sec")]
        public Int64? RouteDurationSec { get; set; }

        /// <summary>
        /// Planned total route duration.
        /// <remarks><para>
        /// The duration between the latest window end and the earliest window start.
        /// </para></remarks>
        /// </summary>
        [Column("planned_total_route_duration")]
        public Int64? PlannedTotalRouteDuration { get; set; }

        /// <summary>
        /// Actual travel distance.
        /// </summary>
        [Column("actual_travel_distance")]
        public double? ActualTravelDistance { get; set; }

        /// <summary>
        /// Actual travel time.
        /// </summary>
        [Column("actual_travel_time")]
        public Int64? ActualTravelTime { get; set; }

        /// <summary>
        /// Actual footsteps.
        /// </summary>
        [Column("actual_footsteps")]
        public int? ActualFootSteps { get; set; }

        /// <summary>
        /// Working time
        /// </summary>
        [Column("working_time")]
        public Int64? WorkingTime { get; set; }

        /// <summary>
        /// Driving time
        /// </summary>
        [Column("driving_time")]
        public Int64? DrivingTime { get; set; }

        /// <summary>
        /// Idling time
        /// </summary>
        [Column("idling_time")]
        public Int64? IdlingTime { get; set; }

        /// <summary>
        /// Paying miles
        /// </summary>
        [Column("paying_miles")]
        public double? PayingMiles { get; set; }

        /// <summary>
        /// Channel name.
        /// </summary>
        [Column("channel_name")]
        public string ChannelName { get; set; }

        /// <summary>
        /// Geofence polygon type.
        /// </summary>
        [Column("geofence_polygon_type")]
        public Enum.TerritoryType GeofencePolygonType { get; set; }

        /// <summary>
        /// Geofence polygon size.
        /// </summary>
        [Column("geofence_polygon_size")]
        public int? GeofencePolygonSize { get; set; }

        /// <summary>
        /// Destination count.
        /// </summary>
        [Column("destination_count")]
        public int? DestinationCount { get; set; }

        /// <summary>
        /// Notes count in the route.
        /// </summary>
        [Column("notes_count")]
        public int? NotesCount { get; set; }

        /// <summary>
        /// Vehicle.
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; }

        [NotMapped]
        public RouteVehicle VehicleObj
        {
            get { return Vehicle == null ? null : JsonConvert.DeserializeObject<RouteVehicle>(Vehicle); }
            set { Vehicle = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Member config key-value pairs.
        /// </summary>
        [Column("member_config_storage")]
        public string MemberConfigStorage { get; set; }

        [NotMapped]
        public Dictionary<string, string> MemberConfigStorageDic
        {
            get { return MemberConfigStorage == null ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(MemberConfigStorage); }
            set { MemberConfigStorage = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Route notes
        /// </summary>
        [Column("notes")]
        public ICollection<AddressNote> Notes { get; set; }

        [Column("directions")]
        public List<Direction> Directions { get; set; }

        [Column("links")]
        public string Links { get; set; }

        [NotMapped]
        public Links LinksObj
        {
            get { return Links == null ? null : JsonConvert.DeserializeObject<Links>(Links); }
            set { Links = JsonConvert.SerializeObject(value); }
        }

        [Column("addresses")]
        public List<Address> Addresses { get; set; }

        [Column("tracking_history")]
        public List<TrackingHistory> TrackingHistories { get; set; }

        [Column("path")]
        public List<Path> Pathes { get; set; }

        /// <summary>
        /// Original route.
        /// </summary>
        [Column("original_route")]
        public string OriginalRoute { get; set; }

        [NotMapped]
        public Route OriginalRouteObj
        {
            get { return OriginalRoute == null ? null : JsonConvert.DeserializeObject<Route>(OriginalRoute); }
            set { OriginalRoute = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// Bundled items
        /// </summary>
        [Column("bundle_items")]
        public string BundleItems { get; set; }

        [NotMapped]
        public BundledItemResponse[] BundleItemsArray
        {
            get { return BundleItems == null ? null : JsonConvert.DeserializeObject<BundledItemResponse[]>(BundleItems); }
            set { BundleItems = JsonConvert.SerializeObject(value); }
        }

        public string OptimizationProblemDbId { get; set; }
        [ForeignKey("OptimizationProblemDbId")]
        public OptimizationProblem OptimizationProblem { get; set; }
    }
}
