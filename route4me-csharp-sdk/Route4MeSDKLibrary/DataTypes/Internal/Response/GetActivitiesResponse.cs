using System.Runtime.Serialization;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response from the activities getting process.
    /// </summary>
    [DataContract]
    internal sealed class GetActivitiesResponse
    {
        /// <value>An array of the Activity type objects</value>
        [DataMember(Name = "results")]
        public Activity[] Results { get; set; }

        /// <value>The number of the Activity type objects/value>
        [DataMember(Name = "total")]
        public uint Total { get; set; }
    }
}
