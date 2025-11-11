using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5
{
    /// <summary>
    ///     Query parameters for retrieving team members/users.
    ///     Used with the GET /api/v5.0/team/users endpoint.
    /// </summary>
    [DataContract]
    public sealed class TeamUsersQueryParameters : GenericParameters
    {
        /// <summary>
        ///     Search in the Members by the corresponding query phrase.
        /// </summary>
        [HttpQueryMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        ///     Drivers only filter.
        /// </summary>
        [HttpQueryMember(Name = "drivers_only", EmitDefaultValue = false)]
        public bool? DriversOnly { get; set; }

        /// <summary>
        ///     Optimization problem id.
        /// </summary>
        [HttpQueryMember(Name = "optimization_problem_id", EmitDefaultValue = false)]
        public string OptimizationProblemId { get; set; }

        /// <summary>
        ///     Array of field and direction to order by.
        ///     Example: [["field_name","asc"]]
        /// </summary>
        [HttpQueryMember(Name = "order_by", EmitDefaultValue = false)]
        public string[][] OrderBy { get; set; }

        /// <summary>
        ///     Filter by facility IDs (hexadecimal UUID strings).
        /// </summary>
        [HttpQueryMember(Name = "facility_ids", EmitDefaultValue = false)]
        public string[] FacilityIds { get; set; }
    }
}