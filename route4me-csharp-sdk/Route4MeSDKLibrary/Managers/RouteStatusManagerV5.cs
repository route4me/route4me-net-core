using System;
using System.Threading.Tasks;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.RouteStatus;

namespace Route4MeSDKLibrary.Managers
{
    public class RouteStatusManagerV5 : Route4MeManagerBase
    {
        public RouteStatusManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Get the status by specifying the path parameter ID.
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse GetRouteStatus(string routeId, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPI<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Get the status by specifying the path parameter ID.
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> GetRouteStatusAsync(string routeId)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPIAsync<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Update the status by specifying the path parameter ID.Route statuses change only forward - planned > started/paused > completed
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse UpdateRouteStatus(string routeId, RouteStatusRequest request, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPI<RouteStatusResponse>(request,
                url,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Update the status by specifying the path parameter ID.Route statuses change only forward - planned > started/paused > completed
        /// </summary>
        /// <param name="routeId">ID of the route</param>
        /// <param name="request">Request body</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> UpdateRouteStatusAsync(string routeId, RouteStatusRequest request)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPIAsync<RouteStatusResponse>(request,
                url,
                HttpMethodType.Post);
        }

        /// <summary>
        /// Get route status history by specifying the path parameter ID.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusHistoryResponse GetRouteStatusHistory(RouteStatusHistoryParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatusHistory.Replace("{routeId}", parameters.RouteId);

            return GetJsonObjectFromAPI<RouteStatusHistoryResponse>(parameters,
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Get route status history by specifying the path parameter ID.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusHistoryResponse, ResultResponse>> GetRouteStatusHistoryAsync(RouteStatusHistoryParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.RouteStatusHistory.Replace("{routeId}", parameters.RouteId);

            return GetJsonObjectFromAPIAsync<RouteStatusHistoryResponse>(parameters,
                url,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Roll back route status by specifying the path parameter ID. Sometimes a status rollback is possible.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public RouteStatusResponse RollbackRouteStatus(string routeId, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.RollbackRouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPI<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);
        }

        /// <summary>
        /// Roll back route status by specifying the path parameter ID. Sometimes a status rollback is possible.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns></returns>
        public Task<Tuple<RouteStatusResponse, ResultResponse>> RollbackRouteStatusAsync(string routeId)
        {
            var url = R4MEInfrastructureSettingsV5.RollbackRouteStatus.Replace("{routeId}", routeId);

            return GetJsonObjectFromAPIAsync<RouteStatusResponse>(new GenericParameters(),
                url,
                HttpMethodType.Get);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Route IDs</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public UpdateRouteStatusAsPlannedResponse UpdateRouteStatusAsPlanned(UpdateRouteStatusAsPlannedParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<UpdateRouteStatusAsPlannedResponse>(parameters,
                R4MEInfrastructureSettingsV5.PlannedRouteStatus,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Route IDs</param>
        /// <returns></returns>
        public Task<Tuple<UpdateRouteStatusAsPlannedResponse, ResultResponse>> UpdateRouteStatusAsPlanned(UpdateRouteStatusAsPlannedParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<UpdateRouteStatusAsPlannedResponse>(parameters,
                R4MEInfrastructureSettingsV5.PlannedRouteStatus,
                HttpMethodType.Post);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns></returns>
        public StatusResponse SetRouteStopStatus(SetRouteStopStatusParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(parameters,
                R4MEInfrastructureSettingsV5.RouteStopStatus,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        /// Store a new Status in the database with planned status.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public Task<Tuple<StatusResponse, ResultResponse>> SetRouteStopStatusAsync(SetRouteStopStatusParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<StatusResponse>(parameters,
                R4MEInfrastructureSettingsV5.RouteStopStatus,
                HttpMethodType.Post);
        }
    }
}