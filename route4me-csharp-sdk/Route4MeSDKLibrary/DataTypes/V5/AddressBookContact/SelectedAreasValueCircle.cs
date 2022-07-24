using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    ///  Selected area as circle
    /// </summary>
    [DataContract]
    public class SelectedAreasValueCircle : SelectedAreasValue
    {
        /// <summary>
        ///     Center
        /// </summary>
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public GeoPoint Center { get; set; }

        /// <summary>
        ///     Distance
        /// </summary>
        [DataMember(Name = "distance", EmitDefaultValue = false)]
        public double Distance { get; set; }
    }
}