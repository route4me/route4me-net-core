using System.Runtime.Serialization;

using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response from the addresses uploading process to temporary storage.
    /// </summary>
    [DataContract]
    public sealed class UploadAddressesToTemporaryStorageResponse : GenericParameters
    {
        /// <value>The optimization problem ID</value>
        [System.Runtime.Serialization.DataMember(Name = "optimization_problem_id", IsRequired = false)]
        public string OptimizationProblemId { get; set; }

        /// <value>The temporary addresses storage ID</value>
        [System.Runtime.Serialization.DataMember(Name = "temporary_addresses_storage_id", IsRequired = false)]
        public string TemporaryAddressesStorageId { get; set; }

        /// <value>Number of the uploaded addresses</value>
        [System.Runtime.Serialization.DataMember(Name = "address_count", IsRequired = false)]
        public uint AddressCount { get; set; }

        /// <value>Status of the process: true, false</value>
        [System.Runtime.Serialization.DataMember(Name = "status", IsRequired = false)]
        public bool Status { get; set; }
    }
}