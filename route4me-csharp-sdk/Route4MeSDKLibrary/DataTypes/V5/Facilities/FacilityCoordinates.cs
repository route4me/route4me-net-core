using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Facilities
{
    /// <summary>
    /// Represents facility coordinates
    /// </summary>
    [DataContract]
    public class FacilityCoordinates
    {
        /// <summary>
        /// Gets or sets the facility cooordinate latitude
        /// </summary>
        [DataMember(Name = "lat", EmitDefaultValue = false)]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the facility cooordinate longitude
        /// </summary>
        [DataMember(Name = "lng", EmitDefaultValue = false)]
        public double Longitude { get; set; }
    }
}
