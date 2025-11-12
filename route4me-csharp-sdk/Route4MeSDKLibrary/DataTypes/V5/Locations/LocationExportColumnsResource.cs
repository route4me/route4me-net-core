using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Locations
{
    /// <summary>
    /// Response for location export columns endpoint
    /// </summary>
    [DataContract]
    public class LocationExportColumnsResource
    {
        [DataMember(Name = "field_name")]
        public string FieldName { get; set; }

        [DataMember(Name = "field_title")]
        public string FieldTitle { get; set; }

        [DataMember(Name = "group")]
        public string Group { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        [DataMember(Name = "allowed")]
        public bool? Allowed { get; set; }
    }
}