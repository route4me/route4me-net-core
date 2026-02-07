using System.Runtime.Serialization;

using Route4MeSDKLibrary.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.StrategicPlanner
{
    /// <summary>
    /// Base class for bulk operation requests
    /// </summary>
    [DataContract]
    public class BulkOperationRequest : GenericParameters
    {
        /// <summary>
        /// Array of IDs to operate on (hex32 format)
        /// </summary>
        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public string[] Ids { get; set; }

        /// <summary>
        /// If true, operates on all items except the specified IDs
        /// </summary>
        [DataMember(Name = "is_exclusion_mode", EmitDefaultValue = false)]
        public bool? IsExclusionMode { get; set; }

        /// <summary>
        /// Timezone
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }
    }
}
