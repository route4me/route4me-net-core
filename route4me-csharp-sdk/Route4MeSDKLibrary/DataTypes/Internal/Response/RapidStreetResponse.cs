using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.Internal.Response
{
    /// <summary>
    ///     The response for the rapid street data request
    /// </summary>
    [DataContract]
    internal sealed class RapidStreetResponse
    {
        /// <value>The zip code</value>
        [DataMember(Name = "zipcode")]
        public string Zipcode { get; set; }

        /// <value>The street name</value>
        [DataMember(Name = "street_name")]
        public string StreetName { get; set; }
    }
}