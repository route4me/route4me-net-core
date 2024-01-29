using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Search parameter request body
    /// </summary>
    [DataContract]
    public class SearchParamRequestBody
    {
        /// <summary>
        ///     Query
        /// </summary>
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        ///     Matches
        /// </summary>
        [DataMember(Name = "matches", EmitDefaultValue = false)]
        public SearchParamRequestBodyMatchTerms Matches { get; set; }

        /// <summary>
        ///     Terms
        /// </summary>
        [DataMember(Name = "terms", EmitDefaultValue = false)]
        public SearchParamRequestBodyMatchTerms Terms { get; set; }
    }
}