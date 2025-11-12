using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.QueryTypes.V5.Schedules
{
    /// <summary>
    /// Parameters for route schedule preview
    /// </summary>
    public class RouteSchedulePreviewParameters : GenericParameters
    {
        /// <summary>
        /// Start
        /// <remarks>Example : 2020-09-20</remarks>
        /// </summary>
        [HttpQueryMember(Name = "start", EmitDefaultValue = false)]
        public string Start { get; set; }

        /// <summary>
        /// End
        /// <remarks>Example : 2020-09-20</remarks>
        /// </summary>
        [HttpQueryMember(Name = "end", EmitDefaultValue = false)]
        public string End { get; set; }
    }
}