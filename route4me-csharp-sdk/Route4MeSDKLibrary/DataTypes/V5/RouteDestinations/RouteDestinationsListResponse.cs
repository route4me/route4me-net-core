using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Response wrapper for POST /route-destinations/list.
    /// </summary>
    [DataContract]
    public class RouteDestinationsListResponse
    {
        /// <summary>Array of destination list items matching the request filters.</summary>
        [DataMember(Name = "items")]
        public RouteDestinationListResource[] Items { get; set; }
    }

    /// <summary>
    /// Response wrapper for GET /route-destinations/order/{order_uuid}.
    /// </summary>
    [DataContract]
    public class RouteDestinationsByOrderResponse
    {
        /// <summary>Array of route destinations linked to the specified order.</summary>
        [DataMember(Name = "items")]
        public RouteDestinationResource[] Items { get; set; }
    }
}