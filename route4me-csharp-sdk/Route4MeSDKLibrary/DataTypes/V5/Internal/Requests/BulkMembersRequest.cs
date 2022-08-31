using System.ComponentModel;
using System.Runtime.Serialization;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

namespace Route4MeSDKLibrary.DataTypes.V5.Internal.Requests
{
    /// <summary>
    ///     The request class to bulk create the team members.
    /// </summary>
    [DataContract]
    internal sealed class BulkMembersRequest : GenericParameters
    {
        /// <summary>
        /// Array of the team member requests
        /// </summary>
        [DataMember(Name = "users")]
        public TeamRequest[] Users { get; set; }

        /// <summary>
        /// Conflict resolving rule (see <seealso cref="Conflicts"/>)
        /// </summary>
        [HttpQueryMember(Name = "conflicts", EmitDefaultValue = false)]
        public string Conflicts { get; set; }
    }

    public enum Conflicts
    {
        [Description("fail")]  Fail,
        [Description("overwrite")]  Overwrite,
        [Description("skip")]  Skip
    }
}
