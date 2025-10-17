using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents a facility type collection resource
    /// </summary>
    [DataContract]
    public class FacilityTypeCollectionResource
    {
        /// <summary>
        ///     An array of the facilities.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public FacilityTypeResource[] Data { get; set; }
    }
}
