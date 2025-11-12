using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal
{
    /// <summary>
    ///     The response returned by the get optimizations command
    /// </summary>
    [DataContract]
    internal sealed class DataObjectOptimizations
    {
        /// <value>Array of the returned optimization problems </value>
        [System.Runtime.Serialization.DataMember(Name = "optimizations")]
        public DataObject[] Optimizations { get; set; }

        /// <value>The number of the returned optimization problems </value>
        [System.Runtime.Serialization.DataMember(Name = "totalRecords")]
        public int TotalRecords { get; set; }
    }
}