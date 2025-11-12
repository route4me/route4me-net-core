using System;

using Newtonsoft.Json.Linq;

using Route4MeSDKLibrary.DataTypes.V5.PodWorkflows;
using Route4MeSDKLibrary.Managers;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void CreateReadUpdateDeleteForPodWorkflow()
        {
            var route4Me = new PodWorkflowManagerV5(ActualApiKey);

            // Create
            var workflow = new PodWorkflow();
            workflow.WorkflowId = $"test_id_{Guid.NewGuid()}";
            workflow.IsDefault = false;
            workflow.Title = "Title";
            workflow.DoneActions = JArray.Parse("[\r\n{\r\n\"title\": \"Signee Name\",\r\n\"type\": \"signeeName\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n}\r\n]");
            workflow.FailedActions = JArray.Parse("[\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Reason\",\r\n\"type\": \"questionnaire\",\r\n\"required\": false,\r\n\"options\": {\r\n\"question_content\": \"123\",\r\n\"input_type\": \"multi-choice\",\r\n\"answers\": [\r\n{\r\n\"value\": \"123\"\r\n},\r\n{\r\n\"value\": \"456\"\r\n}\r\n]\r\n}\r\n}\r\n]");

            var createdWorkflow = route4Me.CreatePodWorkflow(workflow, out var err);

            // Read
            var loadedWorkflow = route4Me.GetPodWorkflow(createdWorkflow.Data.WorkflowGuid, out _);

            // Update
            loadedWorkflow.Data.Title = $"test_id_{Guid.NewGuid()}";
            var updatedWorkflow = route4Me.UpdatePodWorkflow(loadedWorkflow.Data.WorkflowGuid, loadedWorkflow.Data, out _);

            // Delete
            route4Me.DeletePodWorkflow(updatedWorkflow.Data.WorkflowGuid, out _);
        }
    }
}