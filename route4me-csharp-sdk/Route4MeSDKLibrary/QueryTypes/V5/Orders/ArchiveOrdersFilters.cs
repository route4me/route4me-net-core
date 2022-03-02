using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Filters for Archive Order request
    /// </summary>
    [DataContract]
    public class ArchiveOrdersFilters : GenericParameters
    {
        /// <summary>
        ///     Days an orders were inserted.
        /// </summary>
        [DataMember(Name = "day_added", EmitDefaultValue = false)]
        public string[] DayAdded { get; set; }
    }
}