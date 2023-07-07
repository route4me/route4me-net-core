using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     The response for route status history request
    /// </summary>
    [DataContract]
    public class RouteStatusHistoryResponse
    {
        /// <summary>
        ///     Route status data.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteStatusHistoryData[] Data { get; set; }
    }
}