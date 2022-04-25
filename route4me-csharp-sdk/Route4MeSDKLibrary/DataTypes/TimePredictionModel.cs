using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    /// <summary>
    /// Time prediction model
    /// </summary>
    [DataContract]
    public sealed class TimePredictionModel
    {
        /// <summary>
        /// An action model (e.g. 'matrix', 'optimization', 'direction')
        /// </summary>
        [DataMember(Name = "model", EmitDefaultValue = false)]
        public string Model { get; set; }

        /// <summary>
        /// Time-consuming prediction for an action model.
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public long Value { get; set; }

        /// <summary>
        /// Time unit (e.g. 'seconds', 'minutes')
        /// </summary>
        [DataMember(Name = "unit", EmitDefaultValue = false)]
        public string Unit { get; set; }
    }
}
