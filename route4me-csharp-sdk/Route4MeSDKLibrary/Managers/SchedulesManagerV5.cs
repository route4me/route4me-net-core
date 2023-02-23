using System;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary.DataTypes.V5.Schedules;
using Route4MeSDKLibrary.QueryTypes.V5.Schedules;

namespace Route4MeSDKLibrary.Managers
{
    public class SchedulesManagerV5 : Route4MeManagerBase
    {
        public SchedulesManagerV5(string apiKey) : base(apiKey)
        {
        }

        /// <summary>
        /// Get the paginated list of Schedules.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Paginated list of Schedules</returns>
        public ScheduleListWithPagination GetScheduleListWithPagination(SchedulesParametersPaginated parameters, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<ScheduleListWithPagination>(parameters,
                R4MEInfrastructureSettingsV5.SchedulesPagination,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get the paginated list of Schedules.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Paginated list of Schedules</returns>
        public async Task<Tuple<ScheduleListWithPagination, ResultResponse>> GetScheduleListWithPaginationAsync(SchedulesParametersPaginated parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<ScheduleListWithPagination>(parameters,
                R4MEInfrastructureSettingsV5.SchedulesPagination,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ScheduleListWithPagination, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get the list of Schedules.
        /// </summary>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>The list of schedules</returns>
        public ScheduleList GetSchedulesList(out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<ScheduleList>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.Schedules,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get the list of Schedules.
        /// </summary>
        /// <returns>The list of schedules</returns>
        public async Task<Tuple<ScheduleList, ResultResponse>> GetSchedulesListAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<ScheduleList>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.Schedules,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Create a new Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="schedule">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Created schedule</returns>
        public ScheduleList CreateSchedule(Schedule schedule, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<ScheduleList>(schedule,
                R4MEInfrastructureSettingsV5.Schedules,
                HttpMethodType.Post, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Create a new Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="schedule">Body payload</param>
        /// <returns>Created schedule</returns>
        public async Task<Tuple<ScheduleList, ResultResponse>> CreateScheduleAsync(Schedule schedule)
        {
            var result = await GetJsonObjectFromAPIAsync<ScheduleList>(schedule,
                R4MEInfrastructureSettingsV5.Schedules,
                HttpMethodType.Post,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<ScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get the schedule
        /// </summary>
        /// <param name="scheduleUid">Schedule UID</param>
        /// <param name="resultResponse"></param>
        /// <returns></returns>
        public ScheduleList GetSchedule(string scheduleUid, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Schedules;
            var response = GetJsonObjectFromAPI<ScheduleList>(new GenericParameters(),
                url + $"/{scheduleUid}",
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get the schedule
        /// </summary>
        /// <param name="scheduleUid">Schedule UID</param>
        /// <returns></returns>
        public async Task<Tuple<ScheduleList, ResultResponse>> GetScheduleAsync(string scheduleUid)
        {
            var url = R4MEInfrastructureSettingsV5.Schedules;

            var result = await GetJsonObjectFromAPIAsync<ScheduleList>(new GenericParameters(),
                url + $"/{scheduleUid}",
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Update the existing Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="scheduleUid">Schedule UID</param>
        /// <param name="schedule">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Updated schedule</returns>
        public ScheduleList UpdateSchedule(string scheduleUid, Schedule schedule, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Schedules + $"/{scheduleUid}";
            schedule.ScheduleUid = null;
            var response = GetJsonObjectFromAPI<ScheduleList>(schedule,
                url,
                HttpMethodType.Put, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Update the existing Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="scheduleUid">Schedule UID</param>
        /// <param name="schedule">Body payload</param>
        /// <returns>Updated schedule</returns>
        public async Task<Tuple<ScheduleList, ResultResponse>> UpdateScheduleAsync(string scheduleUid, Schedule schedule)
        {
            var url = R4MEInfrastructureSettingsV5.Schedules + $"/{scheduleUid}";

            var result = await GetJsonObjectFromAPIAsync<ScheduleList>(schedule,
                url,
                HttpMethodType.Put,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<ScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Delete the specified Schedule with the option to delete the associated route.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Deleted query</returns>
        public ScheduleList DeleteSchedule(DeleteScheduleParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Schedules;
            var response = GetJsonObjectFromAPI<ScheduleList>(parameters,
                url + $"/{parameters.ScheduleUid}",
                HttpMethodType.Delete, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Delete the specified Schedule with the option to delete the associated route.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Deleted query</returns>
        public async Task<Tuple<ScheduleList, ResultResponse>> DeleteScheduleAsync(DeleteScheduleParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.Schedules;

            var result = await GetJsonObjectFromAPIAsync<ScheduleList>(parameters,
                url + $"/{parameters.ScheduleUid}",
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<ScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get the paginated list of Route Schedules.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>List of Route Schedules</returns>
        public RouteScheduleListWithPagination GetRouteScheduleListWithPagination(SchedulesParametersPaginated parameters, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleListWithPagination>(parameters,
                R4MEInfrastructureSettingsV5.RouteSchedulesPagination,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get the paginated list of Route Schedules.
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>List of Route Schedules</returns>
        public async Task<Tuple<RouteScheduleListWithPagination, ResultResponse>> GetRouteScheduleListWithPaginationAsync(SchedulesParametersPaginated parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleListWithPagination>(parameters,
                R4MEInfrastructureSettingsV5.RouteSchedulesPagination,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<RouteScheduleListWithPagination, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get the list of Route Schedules.
        /// </summary>
        /// <returns>List of Route Schedules</returns>
        public RouteScheduleList GetRouteSchedulesList(out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleList>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules,
                HttpMethodType.Get, false, true,
                out resultResponse);

            return response;
        }

        /// <summary>
        /// Get the list of Route Schedules.
        /// </summary>
        /// <returns>List of Route Schedules</returns>
        public async Task<Tuple<RouteScheduleList, ResultResponse>> GetRouteSchedulesListAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleList>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<RouteScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Create a new Route Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="schedule">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Created route schedule</returns>
        public RouteScheduleList CreateRouteSchedule(RouteSchedule schedule, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleList>(schedule,
                R4MEInfrastructureSettingsV5.RouteSchedules,
                HttpMethodType.Post, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Create a new Route Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="schedule">Body payload</param>
        /// <returns>Created route schedule</returns>
        public async Task<Tuple<RouteScheduleList, ResultResponse>> CreateRouteScheduleAsync(RouteSchedule schedule)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleList>(schedule,
                R4MEInfrastructureSettingsV5.RouteSchedules,
                HttpMethodType.Post,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<RouteScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Create a new Master Route by sending the corresponding body payload.
        /// </summary>
        /// <param name="request">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Status response</returns>
        public CreateMasterRouteResponse CreateMasterRoute(CreateMasterRouteRequest request, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<CreateMasterRouteResponse>(request,
                R4MEInfrastructureSettingsV5.MasterRoutes,
                HttpMethodType.Post, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Create a new Master Route by sending the corresponding body payload.
        /// </summary>
        /// <param name="request">Body payload</param>
        /// <returns>Status response</returns>
        public async Task<Tuple<CreateMasterRouteResponse, ResultResponse>> CreateMasterRouteAsync(CreateMasterRouteRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CreateMasterRouteResponse>(request,
                R4MEInfrastructureSettingsV5.MasterRoutes,
                HttpMethodType.Post,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<CreateMasterRouteResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get the Route Schedule
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>The Route Schedule</returns>
        public RouteScheduleResponse GetRouteSchedule(string routeId, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}",
                HttpMethodType.Get, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Get the Route Schedule
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns>The Route Schedule</returns>
        public async Task<Tuple<RouteScheduleResponse, ResultResponse>> GetRouteScheduleAsync(string routeId)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}",
                HttpMethodType.Get,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<RouteScheduleResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Update the existing Route Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="routeSchedule">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Updated route schedule</returns>
        public RouteScheduleList UpdateRouteSchedule(string routeId, RouteSchedule routeSchedule, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleList>(routeSchedule,
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}",
                HttpMethodType.Put, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Update the existing Route Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="routeSchedule">Body payload</param>
        /// <returns>Updated route schedule</returns>
        public async Task<Tuple<RouteScheduleList, ResultResponse>> UpdateRouteScheduleAsync(string routeId, RouteSchedule routeSchedule)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleList>(routeSchedule,
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}",
                HttpMethodType.Put,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<RouteScheduleList, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Delete the route schedules.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Status response</returns>
        public StatusResponse DeleteRouteSchedule(string routeId, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}",
                HttpMethodType.Delete, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Delete the route schedules.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns>Status response</returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> DeleteRouteScheduleAsync(string routeId)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}",
                HttpMethodType.Delete,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Delete the specified route schedule.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Status response</returns>
        public StatusResponse DeleteRouteScheduleItems(string routeId, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}" + "/items",
                HttpMethodType.Delete, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Delete the specified route schedule.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns>Status response</returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> DeleteRouteScheduleItemsAsync(string routeId)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}" + "/items",
                HttpMethodType.Delete,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get the Route Schedule preview
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="parameters">Query parameters</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Route Schedule preview</returns>
        public RouteSchedulePreview GetRouteSchedulePreview(string routeId, RouteSchedulePreviewParameters parameters, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteSchedulePreview>(parameters,
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}" + "/preview",
                HttpMethodType.Get, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Get the Route Schedule preview
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="parameters">Query parameters</param>
        /// <returns>Route Schedule preview</returns>
        public async Task<Tuple<RouteSchedulePreview, ResultResponse>> GetRouteSchedulePreviewAsync(string routeId, RouteSchedulePreviewParameters parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteSchedulePreview>(parameters,
                R4MEInfrastructureSettingsV5.RouteSchedules + $"/{routeId}" + "/preview",
                HttpMethodType.Get,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<RouteSchedulePreview, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Replace the existing Route Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="request">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Replaced route schedule</returns>
        public RouteScheduleResponse ReplaceRouteSchedule(string routeId, ReplaceRouteScheduleBodyRequest request, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleResponse>(request,
                R4MEInfrastructureSettingsV5.ReplaceRouteSchedules + $"/{routeId}",
                HttpMethodType.Put, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Replace the existing Route Schedule by sending the corresponding body payload.
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="request">Body payload</param>
        /// <returns>Replaced route schedule</returns>
        public async Task<Tuple<RouteScheduleResponse, ResultResponse>> ReplaceRouteScheduleAsync(string routeId, ReplaceRouteScheduleBodyRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleResponse>(request,
                R4MEInfrastructureSettingsV5.ReplaceRouteSchedules + $"/{routeId}",
                HttpMethodType.Put,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<RouteScheduleResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Check if the Scheduled Route was copied
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Status response</returns>
        /// /// <remarks>TODO: 404 error at the moment </remarks>
        public StatusResponse IsRouteScheduleCopied(string routeId, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedulesIsCopy + $"/{routeId}",
                HttpMethodType.Get, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Check if the Scheduled Route was copied
        /// </summary>
        /// <param name="routeId">Route ID</param>
        /// <returns>Status response</returns>
        /// <remarks>TODO: 404 error at the moment </remarks>
        public async Task<Tuple<StatusResponse, ResultResponse>> IsRouteScheduleCopiedAsync(string routeId)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedulesIsCopy + $"/{routeId}",
                HttpMethodType.Get,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        /// Get all routes copied from the specified Scheduled Route by sending the corresponding body payload.
        /// </summary>
        /// <param name="request">Body payload</param>
        /// <param name="resultResponse">Failed response</param>
        /// <returns>Response</returns>
        /// <remarks>TODO: response structure is different. API is not workable at the moment </remarks>
        public RouteScheduleCopiesResponse GetRouteScheduleCopies(GetRouteScheduleCopiesRequest request, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<RouteScheduleCopiesResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedulesCopies,
                HttpMethodType.Get, null, false, true,
                out resultResponse, serializeBodyWithNewtonJson: true);

            return response;
        }

        /// <summary>
        /// Get all routes copied from the specified Scheduled Route by sending the corresponding body payload.
        /// </summary>
        /// <param name="request">Body payload</param>
        /// <returns>Response</returns>
        /// <remarks>TODO: response structure is different. API is not workable at the moment </remarks>
        public async Task<Tuple<RouteScheduleCopiesResponse, ResultResponse>> GetRouteScheduleCopiesAsync(GetRouteScheduleCopiesRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<RouteScheduleCopiesResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.RouteSchedulesCopies,
                HttpMethodType.Get,
                null,
                true,
                false, serializeBodyWithNewtonJson: true).ConfigureAwait(false);

            return new Tuple<RouteScheduleCopiesResponse, ResultResponse>(result.Item1, result.Item2);
        }
    }
}
