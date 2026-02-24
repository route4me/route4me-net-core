using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.RouteDestinations
{
    /// <summary>
    /// Route destination item returned within a list response
    /// (POST /route-destinations/list).
    /// Inherits all fields from <see cref="AbstractRouteDestinationResource"/>.
    /// </summary>
    [DataContract]
    public class RouteDestinationListResource : AbstractRouteDestinationResource
    {
    }
}