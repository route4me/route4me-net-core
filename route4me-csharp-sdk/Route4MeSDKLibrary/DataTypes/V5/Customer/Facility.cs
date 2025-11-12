using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Customers
{
    /// <summary>
    /// Facility
    /// </summary>
    [DataContract]
    public class Facility
    {
        /// <summary>
        /// Facility ID
        /// </summary>
        [DataMember(Name = "facility_id", EmitDefaultValue = false)]
        public string FacilityId { get; set; }

        /// <summary>
        /// Facility name
        /// </summary>
        [DataMember(Name = "facility_name", EmitDefaultValue = false)]
        public string FacilityName { get; set; }
    }
}