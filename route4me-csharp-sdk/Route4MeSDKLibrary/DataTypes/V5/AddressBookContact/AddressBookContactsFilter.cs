using System.Runtime.Serialization;

using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.AddressBookContact
{
    /// <summary>
    /// Filter for address book contracts request
    /// </summary>
    [DataContract]
    public class AddressBookContactsFilter : GenericParameters
    {
        /// <summary>
        ///     Query string.
        /// </summary>
        [DataMember(Name = "query", EmitDefaultValue = false)]
        public string Query { get; set; }

        /// <summary>
        ///     Selected Areas
        /// </summary>
        [DataMember(Name = "selected_areas", EmitDefaultValue = false)]
        public SelectedArea[] SelectedAreas { get; set; }

        /// <summary>
        ///     Bounding box
        /// </summary>
        [DataMember(Name = "bounding_box", EmitDefaultValue = false)]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        ///     Center
        /// </summary>
        [DataMember(Name = "center", EmitDefaultValue = false)]
        public GeoPoint Center { get; set; }

        /// <summary>
        ///     Distance from the area center.
        /// </summary>
        [DataMember(Name = "distance", EmitDefaultValue = false)]
        public double Distance { get; set; }

        /// <summary>
        ///     Display option of the contacts. (see <seealso cref="DisplayValues"/> for available values)
        /// </summary>
        [DataMember(Name = "display", EmitDefaultValue = false)]
        public string Display { get; set; }

        /// <summary>
        ///     A member the contact assigned to.
        /// </summary>
        [DataMember(Name = "assigned_member_id", EmitDefaultValue = false)]
        public long? AssignedMemberId { get; set; }

        /// <summary>
        ///     If true, the contact assigned to a member.
        /// </summary>
        [DataMember(Name = "is_assigned", EmitDefaultValue = false)]
        public bool IsAssigned { get; set; }
    }
}