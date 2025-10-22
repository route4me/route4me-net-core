using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.OptimizationProfiles
{
    /// <summary>
    /// Response wrapper for optimization profile API calls
    /// </summary>
    [DataContract]
    public class OptimizationProfileResponse
    {
        /// <summary>
        /// The optimization profile data
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public OptimizationProfile Data { get; set; }
    }
}

