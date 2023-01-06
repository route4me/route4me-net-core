using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Route Schedule list response
    /// </summary>
    [DataContract]
    public class RouteScheduleList
    {
        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteSchedule[] Data { get; set; }
    }
}