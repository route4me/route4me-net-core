using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Scheduler configuration parameters for strategic optimizations
    /// </summary>
    [DataContract]
    public sealed class SchedulerConfiguration
    {
        /// <summary>
        /// Number of scheduling cycles
        /// </summary>
        [DataMember(Name = "cycles", EmitDefaultValue = false)]
        public int? Cycles { get; set; }

        /// <summary>
        /// Days to exclude from scheduling (e.g., "sun", "mon", "tue", etc.)
        /// </summary>
        [DataMember(Name = "blackout_days", EmitDefaultValue = false)]
        public string[] BlackoutDays { get; set; }

        /// <summary>
        /// Specific dates to exclude (format: MM-DD, e.g., "12-25")
        /// </summary>
        [DataMember(Name = "blackout_dates", EmitDefaultValue = false)]
        public string[] BlackoutDates { get; set; }

        /// <summary>
        /// Length of each cycle in days
        /// </summary>
        [DataMember(Name = "cycle_length", EmitDefaultValue = false)]
        public int? CycleLength { get; set; }

        /// <summary>
        /// Start date for scheduling (YYYY-MM-DD)
        /// </summary>
        [DataMember(Name = "start_date", EmitDefaultValue = false)]
        public string StartDate { get; set; }
    }
}
