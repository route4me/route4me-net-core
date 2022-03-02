using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    ///     The order diff
    /// </summary>
    [DataContract]
    public class OrderDiff
    {
        /// <summary>
        ///     Field
        /// </summary>
        [DataMember(Name = "field", EmitDefaultValue = false)]
        public string Field { get; set; }

        /// <summary>
        ///     Old value
        /// </summary>
        [DataMember(Name = "old_value", EmitDefaultValue = false)]
        public string OldValue { get; set; }

        /// <summary>
        ///     New value
        /// </summary>
        [DataMember(Name = "new_value", EmitDefaultValue = false)]
        public string NewValue { get; set; }
    }
}