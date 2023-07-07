using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Selected areas as polygon
    /// </summary>
    [DataContract]
    public class SelectedAreasValuePolygon : SelectedAreasValue
    {
        /// <summary>
        ///     Points
        /// </summary>
        [DataMember(Name = "points", EmitDefaultValue = false)]
        public List<List<long>> Points { get; set; }
    }
}