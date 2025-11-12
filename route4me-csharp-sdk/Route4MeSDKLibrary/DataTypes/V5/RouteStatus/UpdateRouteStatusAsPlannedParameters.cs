using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     Parameters for update route status as planned
    /// </summary>
    [DataContract]
    public class UpdateRouteStatusAsPlannedParameters : GenericParameters
    {
        /// <summary>
        /// Route IDs
        /// </summary>
        [DataMember(Name = "route_ids", EmitDefaultValue = false)]
        public string[] RouteIds { get; set; }
    }
}