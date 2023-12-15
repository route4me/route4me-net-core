using Route4MeSDK.DataTypes.V5;
using Route4MeSDK;
using System.Threading.Tasks;
using System;
using Route4MeSDKLibrary.QueryTypes.V5.PodWorkflows;
using Route4MeSDKLibrary.DataTypes.V5.PodWorkflows;
using Route4MeSDK.QueryTypes;

namespace Route4MeSDKLibrary.Managers
{
    public class PodWorkflowManagerV5 : Route4MeManagerBase
    {
        public PodWorkflowManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Get POD workflows
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>List of POD workflows</returns>
        public PodWorkflowsResponse GetPodWorkflows(PodWorkflowListParameters parameters, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<PodWorkflowsResponse>(parameters,
                R4MEInfrastructureSettingsV5.Workflows,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get POD workflows
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>List of POD workflows</returns>
        public async Task<Tuple<PodWorkflowsResponse, ResultResponse>> GetPodWorkflowsAsync(PodWorkflowListParameters parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<PodWorkflowsResponse>(parameters,
                R4MEInfrastructureSettingsV5.Workflows,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<PodWorkflowsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get POD workflow
        /// </summary>
        /// <param name="uuid">UUID of the PodWorkflow to retrieve</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>POD workflow</returns>
        public PodWorkflowResponse GetPodWorkflow(string uuid, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Workflows;
            var response = GetJsonObjectFromAPI<PodWorkflowResponse>(new GenericParameters(),
                url + $"/{uuid}",
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get POD workflows
        /// </summary>
        /// <param name="uuid">UUID of the PodWorkflow to retrieve</param>
        /// <returns>List of POD workflow</returns>
        public async Task<Tuple<PodWorkflowResponse, ResultResponse>> GetPodWorkflowAsync(string uuid)
        {
            var url = R4MEInfrastructureSettingsV5.Workflows;
            var result = await GetJsonObjectFromAPIAsync<PodWorkflowResponse>(new GenericParameters(),
                url + $"/{uuid}",
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<PodWorkflowResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Create POD workflow
        /// </summary>
        /// <param name="workflow">POD Workflow</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Created POD workflow</returns>
        public PodWorkflowResponse CreatePodWorkflow(PodWorkflow workflow, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<PodWorkflowResponse>(workflow,
                R4MEInfrastructureSettingsV5.Workflows, HttpMethodType.Post, null, false, true, out resultResponse, null, true);

            return response;
        }

        /// <summary>
        /// Create POD workflow
        /// </summary>
        /// <param name="workflow">POD Workflow</param>
        /// <returns>Created POD workflow</returns>
        public async Task<Tuple<PodWorkflowResponse, ResultResponse>> CreatePodWorkflowAsync(PodWorkflow workflow)
        {
            var result = await GetJsonObjectFromAPIAsync<PodWorkflowResponse>(workflow,
                R4MEInfrastructureSettingsV5.Workflows,
                HttpMethodType.Post, null, true, false, null, true).ConfigureAwait(false);

            return new Tuple<PodWorkflowResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Update POD workflow
        /// </summary>
        /// <param name="uuid">UUID of the PodWorkflow to update</param>
        /// <param name="workflow">POD Workflow</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Created POD workflow</returns>
        public PodWorkflowResponse UpdatePodWorkflow(string uuid, PodWorkflow workflow, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Workflows;
            var response = GetJsonObjectFromAPI<PodWorkflowResponse>(workflow,
                url + $"/{uuid}",
                HttpMethodType.Put, null, false, true, out resultResponse, null, true);

            return response;
        }

        /// <summary>
        /// Update POD workflow
        /// </summary>
        /// <param name="uuid">UUID of the PodWorkflow to update</param>
        /// <param name="workflow">POD Workflow</param>
        /// <returns>Created POD workflow</returns>
        public async Task<Tuple<PodWorkflowResponse, ResultResponse>> UpdatePodWorkflowAsync(string uuid, PodWorkflow workflow)
        {
            var url = R4MEInfrastructureSettingsV5.Workflows;
            var result = await GetJsonObjectFromAPIAsync<PodWorkflowResponse>(workflow,
                url + $"/{uuid}",
                HttpMethodType.Put,
                null, true, false, null, true).ConfigureAwait(false);

            return new Tuple<PodWorkflowResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Delete POD workflow
        /// </summary>
        /// <param name="uuid">UUID of the PodWorkflow to delete</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Status</returns>
        public StatusResponse DeletePodWorkflow(string uuid, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Workflows;
            var response = GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                url + $"/{uuid}",
                HttpMethodType.Delete, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Delete POD workflow
        /// </summary>
        /// <param name="uuid">UUID of the PodWorkflow to delete</param>
        /// <returns>Status</returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> DeletePodWorkflowAsync(string uuid)
        {
            var url = R4MEInfrastructureSettingsV5.Workflows;
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                url + $"/{uuid}",
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

    }
}
