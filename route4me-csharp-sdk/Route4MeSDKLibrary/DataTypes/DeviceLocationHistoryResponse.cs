using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes
{
    [DataContract]
    public sealed class DeviceLocationHistoryResponse
    {
        /// <value>The array of the TrackingHistory objects </value>
        [DataMember(Name = "data")]
        public TrackingHistory[] Data { get; set; }

        [DataMember(Name = "mmd")]
        public DeviceLocationMmd Mmd { get; set; }

    }
}
