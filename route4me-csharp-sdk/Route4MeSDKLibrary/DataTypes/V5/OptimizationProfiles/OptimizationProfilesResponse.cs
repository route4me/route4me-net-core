using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles
{
    /// <summary>
    /// Optimization profiles response
    /// </summary>
    [DataContract]
    public class OptimizationProfilesResponse
    {
        /// <summary>
        /// Optimization profiles
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public OptimizationProfile[] Items { get; set; }
    }
}
