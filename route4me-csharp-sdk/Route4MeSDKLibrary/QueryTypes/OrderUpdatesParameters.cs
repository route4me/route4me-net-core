using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes
{
    /// <summary>
    /// Parameters to get orders updates
    /// </summary>
    public class OrderUpdatesParameters : GenericParameters
    {
        /// <summary>
        ///     Last known timestamp, unix seconds
        /// </summary>
        [HttpQueryMember(Name = "last_known_ts", EmitDefaultValue = false)]
        public long LastKnownTs { get; set; }
    }
}