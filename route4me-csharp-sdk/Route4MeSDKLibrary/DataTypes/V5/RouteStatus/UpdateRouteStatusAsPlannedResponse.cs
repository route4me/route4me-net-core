using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     The response for route status request
    /// </summary>
    [DataContract]
    public class UpdateRouteStatusAsPlannedResponse
    {
        /// <summary>
        ///     Route status data.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteStatusData[] Data { get; set; }
    }
}