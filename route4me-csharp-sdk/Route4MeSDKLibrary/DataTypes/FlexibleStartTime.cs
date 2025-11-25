using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    ///     Flexible start time configuration for route optimization.
    ///     Allows the optimization engine to determine the optimal departure time
    ///     within the specified time window.
    /// </summary>
    [DataContract]
    public class FlexibleStartTime
    {
        /// <summary>
        ///     The earliest allowed start time in seconds from midnight.
        ///     <remarks>
        ///         <para>Example: 28800 = 8:00 AM (8 * 60 * 60)</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "earliest_start", EmitDefaultValue = false)]
        public int? EarliestStart { get; set; }

        /// <summary>
        ///     The latest allowed start time in seconds from midnight.
        ///     <remarks>
        ///         <para>Example: 36000 = 10:00 AM (10 * 60 * 60)</para>
        ///     </remarks>
        /// </summary>
        [DataMember(Name = "latest_start", EmitDefaultValue = false)]
        public int? LatestStart { get; set; }
    }
}
