using System.Runtime.Serialization;

namespace Route4MeSDK.DataTypes.V5
{
    /// <summary>
    ///     Archive orders response
    /// </summary>
    [DataContract]
    public class ArchiveOrdersResponse
    {
        /// <summary>
        ///     Next page cursor
        /// </summary>
        [DataMember(Name = "next_page_cursor", EmitDefaultValue = false)]
        public string NextPageCursor { get; set; }

        /// <summary>
        ///     The list of archived orders
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        public ArchivedOrder[] Items { get; set; }
    }
}