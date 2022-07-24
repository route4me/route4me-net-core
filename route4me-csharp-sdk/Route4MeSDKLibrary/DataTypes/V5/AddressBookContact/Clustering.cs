using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    [DataContract]
    public class Clustering
    {
        /// <summary>
        ///     Clustering precision. minimum: 1, maximum: 12, default: 5.
        /// </summary>
        [DataMember(Name = "precision", EmitDefaultValue = false)]
        public int Precision { get; set; }
    }
}