using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    [DataContract]
    public sealed class CustomFieldsResponse
    {
        /// <summary>
        ///     Custom fields
        /// </summary>
        [DataMember(Name = "custom_fields")]
        public CustomField[] CustomFields { get; set; }
    }

    [DataContract]
    public sealed class CustomField
    {
        /// <summary>
        ///     Label
        /// </summary>
        [DataMember(Name = "label")]
        public string Label { get; set; }

        /// <summary>
        ///     Value
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        ///     Kind (static or dynamic)
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        ///     Kind (static or dynamic)
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        ///     Operators exclude
        /// </summary>
        [DataMember(Name = "operators_exclude")]
        public string[] OperatorsExclude { get; set; }
    }
}
