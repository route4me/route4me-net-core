using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Facility GPS coordinates
    /// </summary>
    [DataContract]
    public class FacilityCoordinates
    {
        /// <summary>
        /// Latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double? Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double? Lng { get; set; }
    }
}

