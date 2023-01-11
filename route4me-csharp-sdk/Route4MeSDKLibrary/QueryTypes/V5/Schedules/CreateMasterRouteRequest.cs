using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Schedules
{
    /// <summary>
    /// Request body to create master route
    /// </summary>
    public class CreateMasterRouteRequest : GenericParameters
    {
        /// <summary>
        ///     Route ID
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Route name
        /// </summary>
        [DataMember(Name = "route_name", EmitDefaultValue = false)]
        public string RouteName { get; set; }

        /// <summary>
        ///     Member id
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     Vehicle id
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Schedule name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        ///     Schedule black list
        /// </summary>
        [DataMember(Name = "schedule_blacklist", EmitDefaultValue = false)]
        public string[] ScheduleBlacklist { get; set; }

        /// <summary>
        ///     Advanced interval
        /// </summary>
        [DataMember(Name = "advance_interval", EmitDefaultValue = false)]
        public int? AdvancedInterval { get; set; }

        /// <summary>
        ///     Advanced schedule interval days
        /// </summary>
        [DataMember(Name = "advance_schedule_interval_days", EmitDefaultValue = false)]
        public int? AdvancedScheduleIntervalDays { get; set; }

        /// <summary>
        ///     Timezone
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        ///     Schedule
        /// </summary>
        [DataMember(Name = "schedule", EmitDefaultValue = false)]
        public JToken ScheduleData { get; set; }
    }
}
