using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Route Schedule
    /// </summary>
    [DataContract]
    public class RouteSchedule : GenericParameters
    {
        /// <summary>
        ///     Member id
        /// </summary>
        [DataMember(Name = "member_id", EmitDefaultValue = false)]
        public long? MemberId { get; set; }

        /// <summary>
        ///     UID
        /// </summary>
        [DataMember(Name = "schedule_uid", EmitDefaultValue = false)]
        public string ScheduleUid { get; set; }

        /// <summary>
        ///     Route id
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Vehicle id
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }
    }
}