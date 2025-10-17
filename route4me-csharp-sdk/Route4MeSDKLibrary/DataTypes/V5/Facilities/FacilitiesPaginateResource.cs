using Route4MeSDK.DataTypes.V5;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents a paginated facility collection resource
    /// </summary>
    [DataContract]
    public class FacilitiesPaginateResource
    {
        /// <summary>
        /// Gets or sets facility collection
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public FacilityResource[] Data { get; set; }

        /// <summary>
        /// Gets or sets page links
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public PageLinks Links { get; set; }

        /// <summary>
        /// Gets or sets meta info
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public PageMeta Meta { get; set; }

        /// <summary>
        /// Gets or sets total facilities
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int Total { get; set; }
    }
}
