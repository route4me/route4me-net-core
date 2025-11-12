using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    [DataContract]
    public class RouteScheduleResponse
    {
        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteScheduleResponseData[] Data { get; set; }
    }

    [DataContract]
    public class RouteScheduleResponseData
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
        ///     Schedule UID
        /// </summary>
        [DataMember(Name = "schedule_uid", EmitDefaultValue = false)]
        public string ScheduleUid { get; set; }

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
        ///     Schedule
        /// </summary>
        [DataMember(Name = "schedule", EmitDefaultValue = false)]
        public Schedule Schedule { get; set; }
    }
}