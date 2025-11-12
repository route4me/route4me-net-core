namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Parameters for getting notes by destination
    /// </summary>
    public class DestinationNotesParameters : GenericParameters
    {
        /// <summary>
        ///     Route destination ID (integer or 32-char hex string)
        /// </summary>
        [HttpQueryMember(Name = "route_destination_id", EmitDefaultValue = false)]
        public string RouteDestinationId { get; set; }

        /// <summary>
        ///     Page number
        /// </summary>
        [HttpQueryMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>
        ///     Items per page
        /// </summary>
        [HttpQueryMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }
    }
}