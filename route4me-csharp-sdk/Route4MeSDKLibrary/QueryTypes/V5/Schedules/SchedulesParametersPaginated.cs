using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Schedules
{
    /// <summary>
    /// Parameters for the schedules paginated request
    /// </summary>
    public class SchedulesParametersPaginated : GenericParameters
    {
        /// <summary>
        /// Page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "page", EmitDefaultValue = false)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Items per page
        /// </summary>
        [HttpQueryMemberAttribute(Name = "per_page", EmitDefaultValue = false)]
        public int PerPage { get; set; } = 15;

        /// <summary>
        /// Enable the paginated output.
        /// </summary>
        [HttpQueryMemberAttribute(Name = "with_pagination", EmitDefaultValue = false)]
        public bool WithPagination { get; set; }
    }
}