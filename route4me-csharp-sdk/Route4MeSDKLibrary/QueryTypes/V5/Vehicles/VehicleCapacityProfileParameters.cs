namespace Route4MeSDK.QueryTypes.V5
{
    /// <summary>
    ///     Request parameters for the vehicle capacity profiles.
    /// </summary>
    public sealed class VehicleCapacityProfileParameters : GenericParameters
    {
        /// <summary>
        ///     If true, returned pages are merged.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "merge_page", EmitDefaultValue = false)]
        public bool MergePage { get; set; }

        /// <summary>
        ///     Current page number in the vehicle capacity profiles collection
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }

        /// <summary>
        ///     Returned vehicles capacity profiles number per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "perPage", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

        /// <summary>
        ///     Vehicle capacity profile ID
        /// </summary>
        [HttpQueryMemberAttribute(Name = "id", EmitDefaultValue = false)]
        public long? VehicleCapacityProfileId { get; set; }
    }
}