using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Schedules
{
    /// <summary>
    /// Body payload to request route schedule copies
    /// </summary>
    public class GetRouteScheduleCopiesRequest : GenericParameters
    {
        /// <summary>
        ///     Route id
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        ///     Schedule ID
        /// </summary>
        [DataMember(Name = "schedule_id", EmitDefaultValue = false)]
        public string ScheduleId { get; set; }

        /// <summary>
        ///     Route date
        /// </summary>
        [DataMember(Name = "route_date", EmitDefaultValue = false)]
        public string RouteDate { get; set; }
    }
}
