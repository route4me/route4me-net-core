using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Links of the paged response.
    /// </summary>
    [DataContract]
    public sealed class PageLinks
    {
        /// <summary>
        ///     URL to the first page
        /// </summary>
        [DataMember(Name = "first", EmitDefaultValue = false)]
        public string First { get; set; }

        ///     URL to the last page
        /// </summary>
        [DataMember(Name = "last", EmitDefaultValue = false)]
        public string Last { get; set; }

        ///     URL to the previous page
        /// </summary>
        [DataMember(Name = "prev", EmitDefaultValue = false)]
        public string Prev { get; set; }

        ///     URL to the next page
        /// </summary>
        [DataMember(Name = "next", EmitDefaultValue = false)]
        public string Next { get; set; }
    }
}