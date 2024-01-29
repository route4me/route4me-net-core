using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Route4MeSDK.DataTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.Orders
{
    /// <summary>
    /// Order custom field
    /// </summary>
    [DataContract]
    public class OrderCustomField
    {
        /// <summary>
        ///     Custom order field ID.
        /// </summary>
        [DataMember(Name = "order_custom_field_uuid", EmitDefaultValue = false)]
        public string OrderCustomFieldUuid { get; set; }

        /// <summary>
        ///     Custom order field name.
        /// </summary>
        [DataMember(Name = "order_custom_field_name", EmitDefaultValue = false, IsRequired = false)]
        public string OrderCustomFieldName { get; set; }

        /// <summary>
        ///     Custom order field label.
        /// </summary>
        [DataMember(Name = "order_custom_field_label", EmitDefaultValue = false)]
        public string OrderCustomFieldLabel { get; set; }

        /// <summary>
        ///     Custom order field type.
        /// </summary>
        [DataMember(Name = "order_custom_field_type", EmitDefaultValue = false)]
        public string OrderCustomFieldType { get; set; }

        /// <summary>
        ///     Custom order field value.
        /// </summary>
        [DataMember(Name = "order_custom_field_value", EmitDefaultValue = false)]
        public string OrderCustomFieldValue { get; set; }

        /// <summary>
        ///     Information about an order's custom field.     
        /// </summary>
        [DataMember(Name = "order_custom_field_type_info", EmitDefaultValue = false)]
        public JObject OrderCustomFieldTypeInfo { get; set; }
    }
}