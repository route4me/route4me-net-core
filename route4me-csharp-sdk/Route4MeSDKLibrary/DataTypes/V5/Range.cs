using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5
{
    /// <summary>
    /// Range
    /// </summary>
    [DataContract]
    public class Range
    {
        /// <summary>
        ///     Start
        /// </summary>
        [DataMember(Name = "start", EmitDefaultValue = false)]
        public int Start { get; set; }

        /// <summary>
        ///     End
        /// </summary>
        [DataMember(Name = "end", EmitDefaultValue = false)]
        public int End { get; set; }
    }
}