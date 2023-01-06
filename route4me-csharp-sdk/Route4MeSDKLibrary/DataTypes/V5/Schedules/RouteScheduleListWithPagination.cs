using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Route Schedule list pagination response
    /// </summary>
    [DataContract]
    public class RouteScheduleListWithPagination : RouteScheduleList
    {
        /// <summary>
        ///     Links
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public ScheduleLinks Links { get; set; }

        /// <summary>
        ///     Meta
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public ScheduleMeta Meta { get; set; }
    }
}