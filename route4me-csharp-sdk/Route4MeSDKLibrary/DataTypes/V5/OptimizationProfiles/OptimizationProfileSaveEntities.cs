using Route4MeSDK.QueryTypes;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles
{
    public class OptimizationProfileSaveEntities : GenericParameters
    {
        /// <summary>
        ///     items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public OptimizationProfileSaveEntitiesItem[] Items { get; set; }
    }

    public class OptimizationProfileSaveEntitiesItem : GenericParameters
    {
        /// <summary>
        ///     Guid
        /// </summary>
        [DataMember(Name = "guid", EmitDefaultValue = false)]
        public string Guid { get; set; }

        /// <summary>
        ///     Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///     Parts
        /// </summary>
        [DataMember(Name = "parts", EmitDefaultValue = false)]
        public OptimizationProfileSaveEntitiesItemPart[] Parts { get; set; }
    }

    public class OptimizationProfileSaveEntitiesItemPart : GenericParameters
    {
        /// <summary>
        ///     Guid
        /// </summary>
        [DataMember(Name = "guid", EmitDefaultValue = false)]
        public string Guid { get; set; }

        /// <summary>
        ///     Data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public JObject Data { get; set; }

        /// <summary>
        ///     Config
        /// </summary>
        [DataMember(Name = "config", EmitDefaultValue = false)]
        public JObject Config { get; set; }
    }
}
