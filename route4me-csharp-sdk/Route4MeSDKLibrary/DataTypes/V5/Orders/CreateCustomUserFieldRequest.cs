using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Create custom user fields request
    /// </summary>
    [DataContract]
    public class CreateCustomUserFieldRequest : GenericParameters
    {
        /// <summary>
        ///     Custom order field name.
        /// </summary>
        [DataMember(Name = "order_custom_field_name", EmitDefaultValue = false, IsRequired = false)]
        public string OrderCustomFieldName { get; set; }

        /// <summary>
        ///     Custom order field label.
        /// </summary>
        [DataMember(Name = "order_custom_field_label", EmitDefaultValue = false, IsRequired = false)]
        public string OrderCustomFieldLabel { get; set; }

        /// <summary>
        ///     Custom order field type.
        /// </summary>
        [DataMember(Name = "order_custom_field_type", EmitDefaultValue = false, IsRequired = false)]
        public string OrderCustomFieldType { get; set; }

        /// <summary>
        ///     Custom order field type info.
        /// </summary>
        [DataMember(Name = "order_custom_field_type_info", EmitDefaultValue = false, IsRequired = false)]
        public JObject OrderCustomFieldTypeInfo { get; set; }
    }
}