using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    public sealed class VehicleParameters : GenericParameters
    {
        /// <summary>
        ///     If true, returned vehicles array will be paginated
        /// </summary>
        [HttpQueryMemberAttribute(Name = "with_pagination", EmitDefaultValue = false)]
        public bool WithPagination { get; set; }


        /// <summary>
        ///     Current page number in the vehicles collection
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }


        /// <summary>
        ///     Returned vehicles number per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "perPage", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }

        /// <summary>
        ///     An array of the Vehicle IDs.
        /// </summary>
        [DataMember(Name = "ids", EmitDefaultValue = false)]
        public string[] VehicleIDs { get; set; }

        /// <summary>
        ///     Vehicle ID
        /// </summary>
        [DataMember(Name = "vehicle_id", EmitDefaultValue = false)]
        public string VehicleId { get; set; }

        /// <summary>
        ///     Vehicle license plate
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vehicle_license_plate", EmitDefaultValue = false)]
        public string VehicleLicensePlate { get; set; }

        /// <summary>
        ///     Show the vehicles with specified status.
        ///     Avalaible values: 
        /// </summary>
        [HttpQueryMemberAttribute(Name = "show", EmitDefaultValue = false)]
        public string Show { get; set; }

        /// <summary>
        ///     A query text to search for
        /// </summary>
        [HttpQueryMemberAttribute(Name = "search_query", EmitDefaultValue = false)]
        public string SearchQuery { get; set; }

        /// <summary>
        ///     An item of the array vehicle_ids
        /// </summary>
        [HttpQueryMemberAttribute(Name = "vehicle_ids[]", EmitDefaultValue = false)]
        public string VehicleIdItem { get; set; }

        /// <summary>
        ///     A field to order the retrieved list of the vehicles.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "order_by[0][0]", EmitDefaultValue = false)]
        public string FieldToOrderBy { get; set; }

        /// <summary>
        ///     Order direction. Available values: 'asc', 'desc'.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "order_by[0][1]", EmitDefaultValue = false)]
        public string OrderDirection { get; set; }

        /// <summary>
        ///    Start of the date range (e.g. '2022-05-14).
        /// </summary>
        [HttpQueryMemberAttribute(Name = "start", EmitDefaultValue = false)]
        public string Start { get; set; }

        /// <summary>
        ///    End of the date range (e.g. '2022-05-14).
        /// </summary>
        [HttpQueryMemberAttribute(Name = "end", EmitDefaultValue = false)]
        public string End { get; set; }
    }
}