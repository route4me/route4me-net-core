namespace Route4MeSDK.QueryTypes.V5.Notes
{
    /// <summary>
    ///     Parameters for getting notes by route
    /// </summary>
    public class RouteNotesParameters : GenericParameters
    {
        /// <summary>
        ///     Route ID (32-char hex string)
        /// </summary>
        [HttpQueryMember(Name = "route_id", EmitDefaultValue = false)]
        public string RouteId { get; set; }

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
