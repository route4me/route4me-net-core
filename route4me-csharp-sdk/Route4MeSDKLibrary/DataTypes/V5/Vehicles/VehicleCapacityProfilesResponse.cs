using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     The paginated list of vehicle capacity profiles' object.
    /// </summary>
    [DataContract]
    public sealed class VehicleCapacityProfilesResponse
    {
        /// <summary>
        ///     The data object containing a vehicle capacity profile.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public VehicleCapacityProfile[] Data { get; set; }

        /// <summary>
        ///     The links to the vehicle capacity profiles in the retrieved list.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public VehicleCapacityProfileLinks Links { get; set; }

        /// <summary>
        ///     Meta data of the retireved capacity profiles' list
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public VehicleCapacityProfileMetaData Meta { get; set; }

    }

    [DataContract]
    public sealed class VehicleCapacityProfileLinks
    {
        /// <summary>
        ///     URL to the first page.
        /// </summary>
        [DataMember(Name = "first", EmitDefaultValue = false)]
        public string First { get; set; }

        /// <summary>
        ///     URL to the last page.
        /// </summary>
        [DataMember(Name = "last", EmitDefaultValue = false)]
        public string Last { get; set; }

        /// <summary>
        ///     URL to the previous page.
        /// </summary>
        [DataMember(Name = "prev", EmitDefaultValue = false)]
        public string Previous { get; set; }

        /// <summary>
        ///     URL to the next page.
        /// </summary>
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string Next { get; set; }

    }

    [DataContract]
    public sealed class VehicleCapacityProfileMetaData
    {
        /// <summary>
        ///     Current page number
        /// </summary>
        [DataMember(Name = "current_page", EmitDefaultValue = false)]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///     From which vehicle is starting the page.
        /// </summary>
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public int? From { get; set; }

        /// <summary>
        ///     Last page in the vehicles collection.
        /// </summary>
        [DataMember(Name = "last_page", EmitDefaultValue = false)]
        public int? LastPage { get; set; }

        /// <summary>
        ///     Path to the API endpoint.
        /// </summary>
        [DataMember(Name = "path", EmitDefaultValue = false)]
        public string Path { get; set; }

        /// <summary>
        ///     Number of the vehicle capacity profiles per page.
        /// </summary>
        [DataMember(Name = "per_page", EmitDefaultValue = false)]
        public int? PerPage { get; set; }

        /// <summary>
        ///     To which vehicle capacity profile is ending the page.
        /// </summary>
        [DataMember(Name = "to", EmitDefaultValue = false)]
        public int? To { get; set; }

        /// <summary>
        ///     Total number of the vehicle capacity profiles.
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int? Total { get; set; }
    }
}