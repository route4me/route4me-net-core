using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Selected area as rectangle
    /// </summary>
    [DataContract]
    public class SelectedAreasValueRect : SelectedAreasValue
    {
        /// <summary>
        ///     Top Left
        /// </summary>
        [DataMember(Name = "top_left", EmitDefaultValue = false)]
        public List<long> TopLeft { get; set; }

        /// <summary>
        ///     Bottom Right
        /// </summary>
        [DataMember(Name = "bottom_right", EmitDefaultValue = false)]
        public List<long> BottomRight { get; set; }
    }
}