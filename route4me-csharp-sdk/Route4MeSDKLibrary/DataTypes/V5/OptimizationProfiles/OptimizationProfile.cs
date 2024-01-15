using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles
{
    /// <summary>
    /// Optimization profile
    /// </summary>
    [DataContract]
    public class OptimizationProfile
    {
        /// <summary>
        /// Optimization profile ID
        /// </summary>
        [DataMember(Name = "optimization_profile_id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Profile name
        /// </summary>
        [DataMember(Name = "profile_name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Is default
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool IsDefault { get; set; }
    }
}