using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Full route destination resource returned by GET /route-destinations/{id}.
    /// Extends <see cref="AbstractRouteDestinationResource"/> with the customer identifier.
    /// </summary>
    [DataContract]
    public class RouteDestinationResource : AbstractRouteDestinationResource
    {
        /// <summary>
        /// Customer identifier (32-character hex UUID) associated with this stop.
        /// </summary>
        [DataMember(Name = "customer_id")]
        public string CustomerId { get; set; }
    }
}
