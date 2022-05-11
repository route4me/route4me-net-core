using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from a destination removing process from an optimization
    /// </summary>
    [DataContract]
    internal sealed class RemoveDestinationFromOptimizationResponse
    {
        /// <value>True if a destination was successuly removed from an optimization</value>
        [System.Runtime.Serialization.DataMember(Name = "deleted")]
        public bool Deleted { get; set; }

        /// <value>ID of a removed destination</value>
        [System.Runtime.Serialization.DataMember(Name = "route_destination_id")]
        public long RouteDestinationId { get; set; }
    }
}
