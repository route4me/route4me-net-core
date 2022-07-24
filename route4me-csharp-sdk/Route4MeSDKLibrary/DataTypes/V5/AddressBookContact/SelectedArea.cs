using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Selected area
    /// </summary>
    [DataContract]
    public class SelectedArea
    {
        /// <summary>
        ///     Type (see <seealso cref="SelectedAreasType"/> for available values)
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        ///     Type
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public SelectedAreasValue Value { get; set; }
    }
}