using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Response wrapper for facility types collection
    /// The API returns: { "data": [ {...}, {...} ] }
    /// </summary>
    [DataContract]
    public class FacilityTypeCollectionResource
    {
        /// <summary>
        /// Array of facility types
        /// </summary>
        [DataMember(Name = "data")]
        public FacilityTypeResource[] Data { get; set; }
    }
}