using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles
{
    public class OptimizationProfileDeleteEntitiesRequest : GenericParameters
    {
        /// <summary>
        ///     Items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public OptimizationProfileDeleteEntitiesRequestItem[] Items { get; set; }
    }

    public class OptimizationProfileDeleteEntitiesRequestItem : GenericParameters
    {
        /// <summary>
        ///     Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
    }
}