using System;
using System.Linq;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

using Route4MeSDKLibrary.DataTypes.V5.PodWorkflows;
using Route4MeSDKLibrary.Managers;
using Route4MeSDKLibrary.QueryTypes.V5.PodWorkflows;

namespace Route4MeSdkV5UnitTest.V5.PodWorkflows
{
    [TestFixture]
    public class PodWorkflowsTests
    {
        private static readonly string CApiKey = ApiKeys.ActualApiKey;

        [Test]
        public void GetPodWorkflowsTest()
        {
            var route4Me = new PodWorkflowManagerV5(CApiKey);

            var parameters = new PodWorkflowListParameters
            {
                PerPage = 10
            };

            var workflows = route4Me.GetPodWorkflows(parameters, out var err);
            
            Assert.IsNotNull(workflows, V5TestHelper.GetAllErrorMessagesFormatted(err));

            Assert.That(workflows.GetType(), Is.EqualTo(typeof(PodWorkflowsResponse)));
        }

        [Test]
        public void GetPodWorkflowTest()
        {
            var route4Me = new PodWorkflowManagerV5(CApiKey);

            var parameters = new PodWorkflowListParameters
            {
                PerPage = 10
            };

            var workflows = route4Me.GetPodWorkflows(parameters, out var err);
            Assert.IsNotNull(workflows?.Data, V5TestHelper.GetAllErrorMessagesFormatted(err));
            var workflow = workflows.Data.FirstOrDefault();

            if (workflow != null)
            {
                var loadedWorkflow = route4Me.GetPodWorkflow(workflow.WorkflowGuid, out _);

                Assert.That(loadedWorkflow.Data, Is.Not.Null);
                Assert.That(loadedWorkflow.Data.WorkflowId, Is.EqualTo(workflow.WorkflowId));
            }
        }

        [Test]
        public void CreatePodWorkflowTest()
        {
            var route4Me = new PodWorkflowManagerV5(CApiKey);

            var workflow = new PodWorkflow();
            workflow.WorkflowId = $"test_id_{Guid.NewGuid()}";
            workflow.IsDefault = true;
            workflow.Title = "Title";
            workflow.DoneActions = JArray.Parse("[\r\n{\r\n\"title\": \"Signee Name\",\r\n\"type\": \"signeeName\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n}\r\n]");
            workflow.FailedActions = JArray.Parse("[\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Reason\",\r\n\"type\": \"questionnaire\",\r\n\"required\": false,\r\n\"options\": {\r\n\"question_content\": \"123\",\r\n\"input_type\": \"multi-choice\",\r\n\"answers\": [\r\n{\r\n\"value\": \"123\"\r\n},\r\n{\r\n\"value\": \"456\"\r\n}\r\n]\r\n}\r\n}\r\n]");
            workflow.IsDefault = false;

            var createdWorkflow = route4Me.CreatePodWorkflow(workflow, out var err);
            
            Assert.IsNotNull(createdWorkflow?.Data, V5TestHelper.GetAllErrorMessagesFormatted(err));

            var loadedWorkflow = route4Me.GetPodWorkflow(createdWorkflow.Data.WorkflowGuid, out _);

            Assert.That(loadedWorkflow.Data.WorkflowId, Is.EqualTo(workflow.WorkflowId));
            Assert.That(loadedWorkflow.Data.WorkflowGuid, Is.Not.Empty);

            route4Me.DeletePodWorkflow(loadedWorkflow.Data.WorkflowGuid, out _);
        }

        [Test]
        public void UpdatePodWorkflowTest()
        {
            var route4Me = new PodWorkflowManagerV5(CApiKey);

            var workflow = new PodWorkflow();
            workflow.WorkflowId = $"test_id_{Guid.NewGuid()}";
            workflow.IsDefault = true;
            workflow.Title = "Title";
            workflow.DoneActions = JArray.Parse("[\r\n{\r\n\"title\": \"Signee Name\",\r\n\"type\": \"signeeName\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n}\r\n]");
            workflow.FailedActions = JArray.Parse("[\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Reason\",\r\n\"type\": \"questionnaire\",\r\n\"required\": false,\r\n\"options\": {\r\n\"question_content\": \"123\",\r\n\"input_type\": \"multi-choice\",\r\n\"answers\": [\r\n{\r\n\"value\": \"123\"\r\n},\r\n{\r\n\"value\": \"456\"\r\n}\r\n]\r\n}\r\n}\r\n]");
            workflow.IsDefault = false;

            var createdWorkflow = route4Me.CreatePodWorkflow(workflow, out var err);
            Assert.IsNotNull(createdWorkflow?.Data, V5TestHelper.GetAllErrorMessagesFormatted(err));

            createdWorkflow.Data.Title = $"test_id_{Guid.NewGuid()}";
            var updatedWorkflow = route4Me.UpdatePodWorkflow(createdWorkflow.Data.WorkflowGuid, createdWorkflow.Data, out _);

            var loadedWorkflow = route4Me.GetPodWorkflow(updatedWorkflow.Data.WorkflowGuid, out _);

            Assert.That(loadedWorkflow.Data.Title, Is.EqualTo(createdWorkflow.Data.Title));

            route4Me.DeletePodWorkflow(loadedWorkflow.Data.WorkflowGuid, out _);
        }

        [Test]
        public void DeletePodWorkflowTest()
        {
            var route4Me = new PodWorkflowManagerV5(CApiKey);

            var workflow = new PodWorkflow();
            workflow.WorkflowId = $"test_id_{Guid.NewGuid()}";
            workflow.IsDefault = true;
            workflow.Title = "Title";
            workflow.DoneActions = JArray.Parse("[\r\n{\r\n\"title\": \"Signee Name\",\r\n\"type\": \"signeeName\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n}\r\n]");
            workflow.FailedActions = JArray.Parse("[\r\n{\r\n\"title\": \"Take a picture\",\r\n\"type\": \"photo\",\r\n\"required\": true\r\n},\r\n{\r\n\"title\": \"Reason\",\r\n\"type\": \"questionnaire\",\r\n\"required\": false,\r\n\"options\": {\r\n\"question_content\": \"123\",\r\n\"input_type\": \"multi-choice\",\r\n\"answers\": [\r\n{\r\n\"value\": \"123\"\r\n},\r\n{\r\n\"value\": \"456\"\r\n}\r\n]\r\n}\r\n}\r\n]");
            workflow.IsDefault = false;

            var createdWorkflow = route4Me.CreatePodWorkflow(workflow, out var err);

            Assert.IsNotNull(createdWorkflow?.Data, V5TestHelper.GetAllErrorMessagesFormatted(err));
            route4Me.DeletePodWorkflow(createdWorkflow.Data.WorkflowGuid, out _);

            var loadedWorkflow = route4Me.GetPodWorkflow(createdWorkflow.Data.WorkflowGuid, out _);

            Assert.That(loadedWorkflow, Is.Null);
        }
    }
}