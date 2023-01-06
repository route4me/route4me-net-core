using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Schedule list response
    /// </summary>
    [DataContract]
    public class ScheduleList
    {
        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public Schedule[] Data { get; set; }
    }
}