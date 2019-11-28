using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static Route4MeDB.ApplicationCore.Enum;
using Newtonsoft.Json;

namespace Route4MeDB.ApplicationCore.Entities.RouteAggregate
{
    [OwnedAttribute]
    public class RouteParameters
    {
        public RouteParameters() {  }

        /// <summary>
        /// Let the R4M API know if this SDK request is coming 
        /// from a file upload within your environment (for analytics).
        /// </summary>
        [Column("is_upload")]
        public bool? IsUpload { get; set; }

        /// <summary>
        /// The tour type of this route. rt is short for round trip, 
        /// the optimization engine changes its behavior for round trip routes.
        /// </summary>
        [Column("rt")]
        public bool? RT { get; set; }

        /// <summary>
        /// By disabling optimization, the route optimization engine 
        /// will not resequence the stops in your
        /// </summary>
        [Column("disable_optimization")]
        public bool? DisableOptimization { get; set; }

        /// <summary>
        /// The name of this route. this route name will be accessible in the search API, 
        /// and also will be displayed on the mobile device of a user.
        /// </summary>
        [Column("route_name")]
        public string RouteName { get; set; }

        /// <summary>
        /// The route start date in UTC, unix timestamp seconds.
        /// Used to show users when the route will begin, also used for reporting and analytics.
        /// </summary>
        [Column("route_date")]
        public long? RouteDate { get; set; }

        /// <summary>
        /// Offset in seconds relative to the route start date (i.e. 9AM would be 60 * 60 * 9)
        /// </summary>
        [Column("route_time")]
        public int? RouteTime { get; set; }

        /// <summary>
        /// Specify if the route can be viewed by unauthenticated users.
        /// </summary>
        [Obsolete("Always false")]
        [Column("shared_publicly")]
        public string SharedPublicly { get; set; }


        /// <summary>Gets or sets the optimize parameter.
        /// <para>Availabale values:</para>
        /// <value>Distance</value>, 
        /// <value>Time</value>, 
        /// <value>timeWithTraffic</value>
        /// </summary>
        [Column("optimize")]
        public string Optimize { get; set; }

        /// <summary>
        /// When the tour type is not round trip (rt = false), 
        /// enable lock last so that the final destination is fixed.
        /// <remarks>
        /// <para>
        /// Example: driver leaves a depot, but must always arrive at home 
        /// (or a specific gas station) at the end of the route.
        /// </para>
        /// </remarks>
        /// </summary>
        [Column("lock_last")]
        public bool? LockLast { get; set; }

        /// <summary>
        /// Vehicle capacity.
        /// <para>How much cargo can the vehicle carry (units, e.g. cubic meters)</para>
        /// </summary>
        [Column("vehicle_capacity")]
        public int? VehicleCapacity { get; set; }

        /// <summary>
        /// Maximum distance for a single vehicle in the route (always in miles)
        /// </summary>
        [Column("vehicle_max_distance_mi")]
        public int? VehicleMaxDistanceMI { get; set; }

        /// <summary>
        /// Maximum allowed revenue from a subtour
        /// </summary>
        [Column("subtour_max_revenue")]
        public int? SubtourMaxRevenue { get; set; }

        /// <summary>
        /// Maximum cargo volume a vehicle can cary
        /// </summary>
        [Column("vehicle_max_cargo_volume")]
        public double? VehicleMaxCargoVolume { get; set; }

        /// <summary>
        /// Maximum cargo weight a vehicle can cary
        /// </summary>
        [Column("vehicle_max_cargo_weight")]
        public double? VehicleMaxCargoWeight { get; set; }

        /// <summary>
        /// The distance measurement unit for the route.
        /// </summary>
        /// <remarks>km or mi, the route4me api will convert all measurements into these units</remarks>
        [Column("distance_unit")]
        public string DistanceUnit { get; set; }

        /// <summary>
        /// The mode of travel that the directions should be optimized for.
        /// <para>Available values:
        /// <value>Driving</value>, 
        /// <value>Walking</value>, 
        /// <value>Trucking</value>, 
        /// <value>Cycling</value>, 
        /// <value>Transit</value>.
        /// </para>
        /// </summary>
        [Column("travel_mode")]
        public string TravelMode { get; set; }

        /// <summary>
        /// Options which let the user choose which road obstacles to avoid. 
        /// This has no impact on route sequencing.
        /// <para>Available values:
        /// <value>Highways</value>, 
        /// <value>Tolls</value>, 
        /// <value>minimizeHighways</value>, 
        /// <value>minimizeTolls</value>, 
        /// <value>""</value>.
        /// </para>
        /// </summary>
        [Column("avoid")]
        public string Avoid { get; set; }

        /// <summary>
        /// An array of the Avoidance zones IDs
        /// </summary>
        [Column("avoidance_zones")]
        public string AvoidanceZones { get; set; }

        [NotMapped]
        public string[] AvoidanceZonesArray
        {
            get { return AvoidanceZones == null ? null : JsonConvert.DeserializeObject<string[]>(AvoidanceZones); }
            set { AvoidanceZones = JsonConvert.SerializeObject(value); }
        }

        /// <summary>
        /// The vehicle ID
        /// </summary>
        [Column("vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>
        /// The vehicle ID, to be assigned to the route.
        /// </summary>
        [Obsolete("All new routes should be assigned to a member_id")]
        [Column("driver_id")]
        public string DriverId { get; set; }

        /// <summary>
        /// The latitude of the device making this sdk request
        /// </summary>
        [Column("dev_lat")]
        public double? DevLatitude { get; set; }

        /// <summary>
        /// The longitude of the device making this sdk request
        /// </summary>
        [Column("dev_lng")]
        public double? DevLongitude { get; set; }

        /// <summary>
        /// <note type="note"><br />When using a multiple driver algorithm, this is the maximum permissible duration of a generated route.
        /// <para>The optimization system will automatically create more routes when the route_max_duration is exceeded for a route.</para>
        /// <para>However it will create an 'unrouted' list of addresses if the maximum number of drivers is exceeded</para>
        /// </note>
        /// </summary>
        /// <value>The maximum duration of the route.</value>
        [Column("route_max_duration")]
        public int? RouteMaxDuration { get; set; }

        /// <summary>The email address to notify upon completion of an optimization request</summary>
        /// <value>The route email.</value>
        [Column("route_email")]
        public string RouteEmail { get; set; }

        /// <summary>Type of route being created: ENUM(api,null)</summary>
        /// <value>The type of the route.</value>
        [Column("route_type")]
        public string RouteType { get; set; }

        [Obsolete("All routes are stored by default at this time")]
        [Column("store_route")]
        public bool? StoreRoute { get; set; }

        /// <summary>
        /// Metric system. Available values:
        /// <para><value>1 = ROUTE4ME_METRIC_EUCLIDEAN</value> (use euclidean distance when computing point to point distance)</para>
        /// <para><value>2 = ROUTE4ME_METRIC_MANHATTAN</value> (use manhattan distance (taxicab geometry) when computing point to point distance)</para>
        /// <para><value>3 = ROUTE4ME_METRIC_GEODESIC</value> (use geodesic distance when computing point to point distance)</para>
        /// <para><value>4 = ROUTE4ME_METRIC_MATRIX (default)</value> (use road network driving distance when computing point to point distance)</para>
        /// <para><value>5 = ROUTE4ME_METRIC_EXACT_2D</value> (use exact rectilinear distance)</para>
        /// </summary>
        [Column("metric")]
        public Metric Metric { get; set; }

        //the type of algorithm to use when optimizing the route

        /// <summary>
        /// The algorithm type to use when optimizing the route. See <see cref="DataTypes.AlgorithmType"/>
        /// </summary>
        [Column("algorithm_type")]
        public AlgorithmType AlgorithmType { get; set; }

        /// <summary>
        /// The route owner's member ID.
        /// <remarks>
        /// <para>In order for users in your organization to have routes assigned to them, 
        /// you must provide their member ID within the Route4Me system.</para>
        /// <para>A list of member IDs can be retrieved with view_users API method.</para>
        /// </remarks>
        /// </summary>
        [Column("member_id")]
        public int? MemberId { get; set; }

        /// <summary>
        /// Specify the ip address of the remote user making this optimization request.
        /// </summary>
        [Column("ip")]
        public int? Ip { get; set; }

        /// <summary>
        /// The method to use when compute the distance between the points in a route.
        /// <para>Available values:</para>
        /// <para><value>1 = DEFAULT</value> (R4M PROPRIETARY ROUTING)</para>
        /// <para><value>2 = DEPRECRATED</value></para>
        /// <para><value>3 = R4M TRAFFIC ENGINE</value></para>
        /// <para><value>4 = DEPRECATED</value></para>
        /// <para><value>5 = DEPRECATED</value></para>
        /// <para><value>6 = TRUCKING</value></para>
        /// </summary>
        [Column("dm")]
        public int? DM { get; set; }

        /// <summary>
        /// Directions method.
        /// <para>Available values:</para>
        /// <para><value>1 = DEFAULT</value> (R4M PROPRIETARY INTERNAL NAVIGATION SYSTEM)</para>
        /// <para><value>2 = DEPRECATED</value></para>
        /// <para><value>3 = TRUCKING</value></para>
        /// <para><value>4 = DEPRECATED</value></para>
        /// </summary>
        [Column("dirm")]
        public int? Dirm { get; set; }

        /// <summary>
        /// Legacy feature which permits a user to request an example number of optimized routes.
        /// </summary>
        [Column("parts")]
        public int? Parts { get; set; }

        /// <summary>
        /// Minimum number of optimized routes.
        /// </summary>
        [Column("parts_min")]
        public int? PartsMin { get; set; }

        /// <summary>
        /// 32 Character MD5 String ID of the device that was used to plan this route.
        /// </summary>
        [Obsolete("Always null")]
        [Column("device_id")]
        public string DeviceID { get; set; }

        /// <summary>
        /// The type of device making this request.
        /// <para>Available values:</para>
        /// <value>web</value>, 
        /// <value>iphone</value>, 
        /// <value>ipad</value>, 
        /// <value>android_phone</value>, 
        /// <value>android_tablet</value>
        /// </summary>
        [Column("device_type")]
        public Enum.DeviceType DeviceType { get; set; }

        /// <summary>
        /// If true, the vehicle has a trailer.
        /// <remarks>
        /// <para>For routes that have trucking directions enabled, directions generated
        /// will ensure compliance so that road directions generated do not take the vehicle
        /// where trailers are prohibited.</para>
        /// </remarks>
        /// </summary>
        [Column("has_trailer")]
        public bool? HasTrailer { get; set; }

        /// <summary>
        /// If true, the vehicle will first drive then wait between stops.
        /// </summary>
        [Column("first_drive_then_wait_between_stops")]
        public bool? FirstDriveThenWaitBetweenStops { get; set; }

        /// <summary>
        /// The vehicle's trailer weight
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance so that road directions generated do not take the vehicle 
        /// on roads where the weight of the vehicle in tons exceeds this value.
        /// </para></remarks>
        /// </summary>
        [Column("trailer_weight_t")]
        public double? TrailerWeightT { get; set; }

        /// <summary>
        /// If travel_mode is Trucking, specifies the truck weight.
        /// </summary>
        [Column("limited_weight_t")]
        public double? LimitedWeightT { get; set; }

        /// <summary>
        /// The vehicle's weight per axle (tons)
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated
        /// will ensure compliance so that road directions generated do not take the vehicle
        /// where the weight per axle in tons exceeds this value.
        /// </para></remarks>
        /// </summary>
        [Column("weight_per_axle_t")]
        public double? WeightPerAxleT { get; set; }

        /// <summary>
        /// The truck height.
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance of this maximum height of truck when generating 
        /// road network driving directions.
        /// </para></remarks>
        /// </summary>
        [Column("truck_height")]
        public double? TruckHeightMeters { get; set; }

        /// <summary>
        /// The truck width.
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance of this width of the truck when generating road network 
        /// driving directions.
        /// </para></remarks>
        /// </summary>
        [Column("truck_width")]
        public double? TruckWidthMeters { get; set; }

        /// <summary>
        /// The truck length.
        /// <remarks><para>
        /// For routes that have trucking directions enabled, directions generated 
        /// will ensure compliance of this length of the truck when generating 
        /// road network driving directions.
        /// </para></remarks>
        /// </summary>
        [Column("truck_length")]
        public double? TruckLengthMeters { get; set; }

        /// <summary>
        /// The minimum number of stops permitted per created subroute.
        /// </summary>
        [Column("min_tour_size")]
        public int? MinTourSize { get; set; }

        /// <summary>
        /// The maximum number of stops permitted per created subroute.
        /// </summary>
        [Column("max_tour_size")]
        public int? MaxTourSize { get; set; }

        /// <summary>
        /// The optimization quality.
        /// <para>Available values:</para>
        /// <para><value>1</value> - Generate Optimized Routes As Quickly as Possible;</para>
        /// <para><value>2</value> - Generate Routes That Look Better On A Map;</para>
        /// <para><value>3</value> - Generate The Shortest And Quickest Possible Routes.</para>
        /// </summary>
        [Column("optimization_quality")]
        public int? OptimizationQuality { get; set; }

        /// <summary>
        /// If equal to 1, uturn is allowed for the vehicle.
        /// </summary>
        [Column("uturn")]
        public int? Uturn { get; set; }

        /// <summary>
        /// If equal to 1, leftturn is allowed for the vehicle.
        /// </summary>
        [Column("leftturn")]
        public int? LeftTurn { get; set; }

        /// <summary>
        /// If equal to 1, rightturn is allowed for the vehicle.
        /// </summary>
        [Column("rightturn")]
        public int? RightTurn { get; set; }

        /// <summary>
        /// If the service time is specified, all the route addresses wil have same service time. 
        /// See <see cref="OverrideAddresses"/>
        /// </summary>
        [Column("override_addresses")]
        public string overrideAddresses { get; set; }

        [NotMapped]
        public OverrideAddresses overrideAddressesObject
        {
            get { return overrideAddresses == null ? null : JsonConvert.DeserializeObject<OverrideAddresses>(overrideAddresses); }
            set { overrideAddresses = JsonConvert.SerializeObject(value); }
        }
    }
}
