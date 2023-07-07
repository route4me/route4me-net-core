using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Schedules
{
    /// <summary>
    /// Parameters for the schedules paginated request
    /// </summary>
    public class DeleteScheduleParameters : GenericParameters
    {
        /// <summary>
        /// Schedule UID.
        /// </summary>
        [DataMember(Name = "schedule_uid", EmitDefaultValue = false)]
        public string ScheduleUid { get; set; }

        /// <summary>
        /// Delete the Schedule that matches the specified Schedule ID. If the deleted Schedule has one or multiple associated Routes, these Routes are also deleted.
        /// </summary>
        [HttpQueryMember(Name = "with-routes ", EmitDefaultValue = false)]
        public bool WithRoutes { get; set; }
    }
}