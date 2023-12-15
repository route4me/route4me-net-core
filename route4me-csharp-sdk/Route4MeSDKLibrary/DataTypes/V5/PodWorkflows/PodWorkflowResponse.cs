using System.Runtime.Serialization;

namespace Route4MeSDKLibrary.DataTypes.V5.PodWorkflows
{
    /// <summary>
    /// POD Workflow Response
    /// </summary>
    [DataContract]
    public class PodWorkflowResponse
    {
        /// <summary>
        /// POD Workflow
        /// </summary>
        [DataMember(Name = "data", EmitDefaultValue = false)]
        public PodWorkflow Data { get; set; }
    }
}