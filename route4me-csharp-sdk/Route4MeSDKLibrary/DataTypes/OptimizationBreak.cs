using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Optimization break data structure.
    /// </summary>
    [DataContract]
    public sealed class OptimizationBreak
    {
        /// <summary>
        /// Break mode.
        /// 0 = elapsed time, 1 = service time, 2 = travel time, 3 = location number
        /// </summary>
        [DataMember(Name = "mode", EmitDefaultValue = false)]
        public int? Mode { get; set; }

        /// <summary>
        /// Break mode parameters.
        /// </summary>
        [DataMember(Name = "mode_params", EmitDefaultValue = false)]
        public int[] ModeParams { get; set; }

        /// <summary>
        /// Break repeats number.
        /// </summary>
        [DataMember(Name = "repeats_number", EmitDefaultValue = false)]
        public int? RepeatsNumber { get; set; }

        /// <summary>
        /// Break duration (seconds).
        /// </summary>
        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public int? Duration { get; set; }
    }
}