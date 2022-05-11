using System.Collections.Generic;
using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal
{
    [DataContract]
    public sealed class OptimizationTimePrediction
    {
        /// <value>Array of the returned optimization problems </value>
        [System.Runtime.Serialization.DataMember(Name = "time-prediction")]
        public List<TimePredictionModel> TimePrediction { get; set; }
    }
}
