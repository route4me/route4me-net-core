using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response object from a route destination moving process.
    /// </summary>
    [DataContract]
    internal sealed class MoveDestinationToRouteResponse
    {
        /// <value>If true the destination was removed successfully</value>
        [System.Runtime.Serialization.DataMember(Name = "success")]
        public bool Success { get; set; }

        /// <value>The error string</value>
        [System.Runtime.Serialization.DataMember(Name = "error")]
        public string Error { get; set; }
    }
}
