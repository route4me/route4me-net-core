using System.Runtime.Serialization;

namespace Route4MeSDK.QueryTypes.V5
{
    [DataContract]
    public sealed class GetVehicleParameters : GenericParameters
    {
        /// <summary>
        ///     Current page number in the vehicles collection
        /// </summary>
        [HttpQueryMember(Name = "page", EmitDefaultValue = false)]
        public uint? Page { get; set; }


        /// <summary>
        ///     Returned vehicles number per page
        /// </summary>
        [HttpQueryMember(Name = "perPage", EmitDefaultValue = false)]
        public uint? PerPage { get; set; }
    }
}