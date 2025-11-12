using System.ComponentModel;
using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    /// The route breaks data structure
    /// </summary>
    [DataContract]
    public sealed class RouteBreaks : GenericParameters
    {
        /// <summary>
        ///     An array of the route IDs
        /// </summary>
        [DataMember(Name = "route_id", EmitDefaultValue = false)]
        public string[] RouteId { get; set; }

        /// <summary>
        ///     An array of the RouteBreak type objects.
        /// </summary>
        [DataMember(Name = "breaks", EmitDefaultValue = false)]
        public RouteBreak[] Breaks { get; set; }

        /// <summary>
        ///     If true, the existing breaks are replaced.
        [DataMember(Name = "replace_existing_breaks", EmitDefaultValue = false)]
        [DefaultValue(true)]
        public bool ReplaceExistingBreaks { get; set; }

    }


    /// <summary>
    ///     The route break data structure
    /// </summary>
    [DataContract]
    public sealed class RouteBreak : GenericParameters
    {
        /// <summary>
        ///     Route break type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        ///     Route break duration
        /// </summary>
        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public int? Duration { get; set; }

        /// <summary>
        ///     Route break parameters
        /// </summary>
        [DataMember(Name = "params", EmitDefaultValue = false)]
        public int[] Params { get; set; }

    }
}