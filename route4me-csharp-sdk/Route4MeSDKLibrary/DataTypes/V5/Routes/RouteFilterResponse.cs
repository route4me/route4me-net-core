using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Routes filter response
    /// </summary>
    [DataContract]
    public class RouteFilterResponse
    {
        /// <summary>
        ///     An array of the routes.
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public RouteFilterResponseData[] Data { get; set; }

        /// <summary>
        ///     Links to the response pages.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public PageLinks Links { get; set; }

        /// <summary>
        ///     Route meta info
        /// </summary>
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public PageMeta Meta { get; set; }

        /// <summary>
        ///     An array of the duplicated route IDs.
        /// </summary>
        [DataMember(Name = "route_ids", EmitDefaultValue = false)]
        public string[] RouteIDs { get; set; }


    }


    public class RouteFilterResponseData
    {

    }
}