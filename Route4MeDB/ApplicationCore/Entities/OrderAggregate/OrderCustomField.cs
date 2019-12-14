using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Route4MeDB.ApplicationCore.Entities.OrderAggregate
{
    public class OrderCustomField : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("order_custom_field_db_id")]
        public int OrderCustomFieldDbId { get; set; }

        /// <summary>
        /// Custom order field ID.
        /// </summary>
        [Column("order_custom_field_id")]
        public int OrderCustomFieldId { get; set; }

        /// <summary>
        /// Custom order field name.
        /// </summary>
        [Column("order_custom_field_name")]
        public string OrderCustomFieldName { get; set; }

        /// <summary>
        /// Custom order field label.
        /// </summary>
        [Column("order_custom_field_label")]
        public string OrderCustomFieldLabel { get; set; }

        /// <summary>
        /// Custom order field type.
        /// </summary>
        [Column("order_custom_field_type")]
        public string OrderCustomFieldType { get; set; }

        /// <summary>
        /// Custom order field value.
        /// </summary>
        [Column("order_custom_field_value")]
        public string OrderCustomFieldValue { get; set; }

        /// <summary>
        /// Account owner member ID.
        /// </summary>
        [Column("root_owner_member_id")]
        public int RootOwnerMemberId { get; set; }

        /// <summary>
        /// Information about an order's custom field.
        /// You can specify the propertiesof the different types in this property,
        /// but the property "short_label" is reserved - it specifies custom field column header 
        /// in the orders table in the page: https://route4me.com/orders
        /// </summary>
        [Column("order_custom_field_type_info")]
        public string OrderCustomFieldTypeInfo { get; set; }

        [NotMapped]
        public Dictionary<string, object> dicOrderCustomFieldTypeInfo
        {
            get { return OrderCustomFieldTypeInfo == null ? null : JsonConvert.DeserializeObject<Dictionary<string, object>>(OrderCustomFieldTypeInfo); }
            set { OrderCustomFieldTypeInfo = JsonConvert.SerializeObject(value); }
        }
    }
}
