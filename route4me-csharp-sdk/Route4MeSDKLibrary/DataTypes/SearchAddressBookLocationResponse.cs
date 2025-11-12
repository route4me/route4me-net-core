using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes
{
    /// <summary>
    ///     The response from the address book locations searching process.
    /// </summary>
    [DataContract]
    public sealed class SearchAddressBookLocationResponse
    {
        /// <value>The list of the selected fields values</value>
        [DataMember(Name = "results")]
        public List<object[]> Results { get; set; }

        /// <value>Number of the returned address book contacts</value>
        [DataMember(Name = "total")]
        public uint Total { get; set; }

        /// <value>Array of the selected fields</value>
        [DataMember(Name = "fields")]
        public string[] Fields { get; set; }
    }
}