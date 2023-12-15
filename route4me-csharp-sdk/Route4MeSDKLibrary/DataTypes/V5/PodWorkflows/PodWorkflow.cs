using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.DataTypes.V5.PodWorkflows
{
    /// <summary>
    /// POD Workflow
    /// </summary>
    [DataContract]
    public class PodWorkflow : GenericParameters
    {
        /// <summary>
        /// Workflow GUID
        /// </summary>
        [DataMember(Name = "workflow_guid", EmitDefaultValue = false)]
        public string WorkflowGuid { get; set; }

        /// <summary>
        /// Workflow ID
        /// </summary>
        [DataMember(Name = "workflow_id", EmitDefaultValue = false)]
        public string WorkflowId { get; set; }

        /// <summary>
        /// Root member ID
        /// </summary>
        [DataMember(Name = "root_member_id", EmitDefaultValue = false)]
        public long? RootMemberId { get; set; }

        /// <summary>
        /// Is Enabled
        /// </summary>
        [DataMember(Name = "is_enabled", EmitDefaultValue = false)]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Is Default
        /// </summary>
        [DataMember(Name = "is_default", EmitDefaultValue = false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// Done actions
        /// </summary>
        [DataMember(Name = "done_actions", EmitDefaultValue = false)]
        public JArray DoneActions { get; set; }

        /// <summary>
        /// Failed actions
        /// </summary>
        [DataMember(Name = "failed_actions", EmitDefaultValue = false)]
        public JArray FailedActions { get; set; }
    }
}
