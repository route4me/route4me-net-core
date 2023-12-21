using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response returned by the remove optimization command
    /// </summary>
    [DataContract]
    internal sealed class RemoveOptimizationResponse
    {
        /// <value>True if an optimization was removed successfuly </value>
        [DataMember(Name = "status")]
        public bool Status { get; set; }

        /// <value>The number of the removed optimizations </value>
        [DataMember(Name = "removed")]
        public int? Removed { get; set; }
    }
}
