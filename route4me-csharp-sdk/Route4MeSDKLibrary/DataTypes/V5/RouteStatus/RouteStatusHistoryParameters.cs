using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteStatus
{
    /// <summary>
    ///     Parameters for Route Status History API
    /// </summary>
    [DataContract]
    public class RouteStatusHistoryParameters : GenericParameters
    {
        /// <summary>
        /// Route ID
        /// </summary>
        [HttpQueryMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

        /// <summary>
        /// Order by (asc, desc)
        /// </summary>
        [HttpQueryMember(Name = "order_by ", EmitDefaultValue = false)]
        public string OrderBy { get; set; }

        /// <summary>
        /// Start
        /// </summary>
        [HttpQueryMember(Name = "start ", EmitDefaultValue = false)]
        public long Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        [HttpQueryMember(Name = "end ", EmitDefaultValue = false)]
        public long End { get; set; }
    }
}