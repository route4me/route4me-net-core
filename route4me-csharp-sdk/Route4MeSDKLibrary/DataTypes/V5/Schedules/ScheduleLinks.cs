using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Schedules
{
    /// <summary>
    /// Schedule links
    /// </summary>
    [DataContract]
    public class ScheduleLinks
    {
        /// <summary>
        ///     First
        /// </summary>
        [DataMember(Name = "first", EmitDefaultValue = false)]
        public string First { get; set; }

        /// <summary>
        ///     Last
        /// </summary>
        [DataMember(Name = "last", EmitDefaultValue = false)]
        public string Last { get; set; }

        /// <summary>
        ///     Prev
        /// </summary>
        [DataMember(Name = "prev", EmitDefaultValue = false)]
        public string Prev { get; set; }

        /// <summary>
        ///     Next
        /// </summary>
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string Next { get; set; }
    }
}