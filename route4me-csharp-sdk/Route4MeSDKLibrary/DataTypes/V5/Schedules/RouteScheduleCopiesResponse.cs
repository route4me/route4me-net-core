using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Route schedule copies response
    /// </summary>
    [DataContract]
    public class RouteScheduleCopiesResponse
    {
        /// <summary>
        ///     Parent route ID
        /// </summary>
        [DataMember(Name = "parent_route_id", EmitDefaultValue = false)]
        public string ParentRouteId { get; set; }

        /// <summary>
        ///     Scheduled routes
        /// </summary>
        [DataMember(Name = "scheduled_routes", EmitDefaultValue = false)]
        public ScheduledRoute[] ScheduledRoutes { get; set; }
    }

    /// <summary>
    /// Scheduled route
    /// </summary>
    [DataContract]
    public class ScheduledRoute
    {
        /// <summary>
        ///     Schedule UID
        /// </summary>
        [DataMember(Name = "schedule_uid", EmitDefaultValue = false)]
        public string ScheduleUid { get; set; }

        /// <summary>
        ///     Child route ID
        /// </summary>
        [DataMember(Name = "child_route_id", EmitDefaultValue = false)]
        public string ChildRouteId { get; set; }

        /// <summary>
        ///     Route date
        /// </summary>
        [DataMember(Name = "route_date", EmitDefaultValue = false)]
        public string RouteDate { get; set; }
    }
}