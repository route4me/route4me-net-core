using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Bounding box
    /// </summary>
    [DataContract]
    public class BoundingBox
    {
        /// <summary>
        ///     Top
        /// </summary>
        [DataMember(Name = "top", EmitDefaultValue = false)]
        public double Top { get; set; }

        /// <summary>
        ///     Left
        /// </summary>
        [DataMember(Name = "left", EmitDefaultValue = false)]
        public double Left { get; set; }

        /// <summary>
        ///     Bottom
        /// </summary>
        [DataMember(Name = "bottom", EmitDefaultValue = false)]
        public double Bottom { get; set; }

        /// <summary>
        ///     Right
        /// </summary>
        [DataMember(Name = "right", EmitDefaultValue = false)]
        public double Right { get; set; }
    }
}