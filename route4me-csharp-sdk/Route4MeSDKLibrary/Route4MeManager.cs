using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using fastJSON;
using Route4MeSDK.DataTypes;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDKLibrary;
using Route4MeSDKLibrary.DataTypes;
using Route4MeSDKLibrary.DataTypes.Internal;
using Route4MeSDKLibrary.DataTypes.Internal.QueryTypes;
using Route4MeSDKLibrary.DataTypes.Internal.Requests;
using Route4MeSDKLibrary.DataTypes.Internal.Response;
using Route4MeSDKLibrary.QueryTypes;
using Address = Route4MeSDK.DataTypes.Address;
using AddressBookContact = Route4MeSDK.DataTypes.AddressBookContact;
using AddressBookContactsResponse = Route4MeSDK.DataTypes.AddressBookContactsResponse;
using DataObject = Route4MeSDK.DataTypes.DataObject;
using DataObjectRoute = Route4MeSDK.DataTypes.DataObjectRoute;
using RouteParameters = Route4MeSDK.DataTypes.RouteParameters;
using StatusResponse = Route4MeSDK.DataTypes.StatusResponse;

namespace Route4MeSDK
{
    /// <summary>
    ///     This class encapsulates the Route4Me REST API
    ///     1. Create an instance of Route4MeManager with the api_key
    ///     1. Shortcut methods: Use shortcuts methods (for example Route4MeManager.GetOptimization()) to access the most
    ///     popular functionality.
    ///     See examples Route4MeExamples.GetOptimization(), Route4MeExamples.SingleDriverRoundTrip()
    ///     2. Generic methods: Use generic methods (for example Route4MeManager.GetJsonObjectFromAPI() or
    ///     Route4MeManager.GetStringResponseFromAPI())
    ///     to access any availaible functionality.
    ///     See examples Route4MeExamples.GenericExample(), Route4MeExamples.SingleDriverRoundTripGeneric()
    /// </summary>
    public sealed class Route4MeManager
    {
        #region Fields

        private readonly string _mApiKey;

        #endregion

        #region Methods

        #region Constructors

        public Route4MeManager(string apiKey)
        {
            _mApiKey = apiKey;
        }

        #endregion

        #region Route4Me Shortcut Methods

        #region Optimizations

        /// <summary>
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <param name="errorString">Returned error string in case of an optimization processs failing</param>
        /// <returns>Generated optimization problem object</returns>
        public DataObject RunOptimization(OptimizationParameters optimizationParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                false,
                true,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Generates optimized routes
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters and the addresses.
        /// </param>
        /// <returns>Generated optimization problem object</returns>
        public Task<Tuple<DataObject, string>> RunOptimizationAsync(OptimizationParameters optimizationParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post, null, false, true);
        }

        /// <summary>
        ///     Generates optimized routes by order territories.
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters, addresses and order territories.
        /// </param>
        /// <param name="errorString">Returned error string in case of an optimization processs failing</param>
        /// <returns>An array of the optimization problem and smart optimization problem objects</returns>
        public DataObject[] RunOptimizationByOrderTerritories(OptimizationParameters optimizationParameters,
            out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObject[]>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post,
                false,
                true,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Generates optimized routes by order territories.
        /// </summary>
        /// <param name="optimizationParameters">
        ///     The input parameters for the routes optimization, which encapsulates:
        ///     the route parameters, addresses and order territories.
        /// </param>
        /// <returns>An array of the optimization problem and smart optimization problem objects</returns>
        public Task<Tuple<DataObject[], string>> RunOptimizationByOrderTerritoriesAsync(OptimizationParameters optimizationParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObject[]>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Post, null, false, true);
        }

        /// <summary>
        ///     Returns an optimization problem by the parameter OptimizationProblemID
        /// </summary>
        /// <param name="optimizationParameters">The optimization parameters bject containing the parameter OptimizationProblemID</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Optimization problem object</returns>
        public DataObject GetOptimization(OptimizationParameters optimizationParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Get, null, false, true,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Returns an optimization problem by the parameter OptimizationProblemID
        /// </summary>
        /// <param name="optimizationParameters">The optimization parameters bject containing the parameter OptimizationProblemID</param>
        /// <returns>Optimization problem object</returns>
        public Task<Tuple<DataObject, string>> GetOptimizationAsync(OptimizationParameters optimizationParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Get, null, false, true);
        }

        /// <summary>
        ///     For getting optimization problems limited by the parameters: offset, limit.
        /// </summary>
        /// <param name="queryParameters">
        ///     The array of the query parameters containing the parameters:
        ///     <para><c>offset</c>: Search starting position</para>
        ///     <para><c>limit</c>: The number of records to return.</para>
        /// </param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Array of the optimization problems</returns>
        public DataObject[] GetOptimizations(OptimizationParameters queryParameters, out string errorString)
        {
            var dataObjectOptimizations = GetJsonObjectFromAPI<DataObjectOptimizations>(queryParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Get, null, false, true,
                out errorString);

            return dataObjectOptimizations?.Optimizations;
        }

        /// <summary>
        ///     For getting optimization problems limited by the parameters: offset, limit.
        /// </summary>
        /// <param name="queryParameters">
        ///     The array of the query parameters containing the parameters:
        ///     <para><c>offset</c>: Search starting position</para>
        ///     <para><c>limit</c>: The number of records to return.</para>
        /// </param>
        /// <returns>Array of the optimization problems</returns>
        public async Task<Tuple<DataObject[], string>> GetOptimizationsAsync(OptimizationParameters queryParameters)
        {
            var dataObjectOptimizations = await GetJsonObjectFromAPIAsync<DataObjectOptimizations>(queryParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Get, null, false, true).ConfigureAwait(false);

            return new Tuple<DataObject[], string>(dataObjectOptimizations.Item1?.Optimizations,
                dataObjectOptimizations.Item2);
        }

        /// <summary>
        /// Time prediction by action type ('matrix', 'optimization', 'direction')
        /// </summary>
        /// <param name="queryParameters">Optimization parameters</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Optimization time-consuming prediction</returns>
        public OptimizationTimePrediction GetOptimizationPrediction(OptimizationParameters queryParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<OptimizationTimePrediction>(queryParameters,
                R4MEInfrastructureSettings.TimePrediction,
                HttpMethodType.Post,
                false,
                false,
                out errorString);

            return result;
        }

        /// <summary>
        /// Time prediction by action type ('matrix', 'optimization', 'direction')
        /// </summary>
        /// <param name="queryParameters">Optimization parameters</param>
        /// <returns>Optimization time-consuming prediction</returns>
        public Task<Tuple<OptimizationTimePrediction, string>> GetOptimizationPredictionAsync(OptimizationParameters queryParameters)
        {
            return GetJsonObjectFromAPIAsync<OptimizationTimePrediction>(queryParameters,
                R4MEInfrastructureSettings.TimePrediction,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Updates an existing optimization problem
        /// </summary>
        /// <param name="optimizationParameters">Parameters for updating an optimization</param>
        /// <param name="errorString">Error string</param>
        /// <returns>Updated optimization</returns>
        public DataObject UpdateOptimization(OptimizationParameters optimizationParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Put,
                false,
                true,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Updates an existing optimization problem
        /// </summary>
        /// <param name="optimizationParameters">Parameters for updating an optimization</param>
        /// <returns>Updated optimization</returns>
        public Task<Tuple<DataObject, string>> UpdateOptimizationAsync(OptimizationParameters optimizationParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObject>(optimizationParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Put, null, false, true);
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem ID </param>
        /// <param name="errorString"> Returned error string in case of the processs failing </param>
        /// <returns> Result status true/false </returns>
        public bool RemoveOptimization(string[] optimizationProblemIDs, out string errorString)
        {
            var remParameters = new RemoveOptimizationRequest
            {
                Redirect = 0,
                OptimizationProblemIds = optimizationProblemIDs
            };

            var response = GetJsonObjectFromAPI<RemoveOptimizationResponse>(remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete,
                out errorString);
            if (response != null)
            {
                if (response.Status && response.Removed > 0) return true;
                return false;
            }

            if (errorString == "")
                errorString = "Error removing optimization";
            return false;
        }

        /// <summary>
        ///     Remove an existing optimization belonging to an user.
        /// </summary>
        /// <param name="optimizationProblemIDs"> Optimization Problem ID </param>
        /// <returns> Result status true/false </returns>
        public async Task<Tuple<bool, string>> RemoveOptimizationAsync(string[] optimizationProblemIDs)
        {
            var remParameters = new RemoveOptimizationRequest
            {
                Redirect = 0,
                OptimizationProblemIds = optimizationProblemIDs
            };

            var response = await GetJsonObjectFromAPIAsync<RemoveOptimizationResponse>(remParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Delete).ConfigureAwait(false);
            if (response.Item1 != null)
            {
                if (response.Item1.Status && response.Item1.Removed > 0) return new Tuple<bool, string>(true, response.Item2);
                return new Tuple<bool, string>(false, response.Item2);
            }

            if (response.Item2 == "")
                response = new Tuple<RemoveOptimizationResponse, string>(response.Item1, "Error removing optimization");
            return new Tuple<bool, string>(false, response.Item2);
        }

        /// <summary>
        ///     Removes a destination from an optimization
        /// </summary>
        /// <param name="optimizationId">optimization problem ID</param>
        /// <param name="destinationId">ID of a destination to be removed from an optimziation</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if  destination as removed from an optimization, otherwise - false</returns>
        public bool RemoveDestinationFromOptimization(string optimizationId, int destinationId, out string errorString)
        {
            var genericParameters = new GenericParameters();
            genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationId);
            genericParameters.ParametersCollection.Add("route_destination_id", destinationId.ToString());

            var response = GetJsonObjectFromAPI<RemoveDestinationFromOptimizationResponse>(genericParameters,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Delete,
                out errorString);

            return response != null && response.Deleted;
        }

        /// <summary>
        ///     Removes a destination from an optimization
        /// </summary>
        /// <param name="optimizationId">optimization problem ID</param>
        /// <param name="destinationId">ID of a destination to be removed from an optimziation</param>
        /// <returns>True if  destination as removed from an optimization, otherwise - false</returns>
        public async Task<Tuple<bool, string>> RemoveDestinationFromOptimizationAsync(string optimizationId, int destinationId)
        {
            var genericParameters = new GenericParameters();
            genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationId);
            genericParameters.ParametersCollection.Add("route_destination_id", destinationId.ToString());

            var response = await GetJsonObjectFromAPIAsync<RemoveDestinationFromOptimizationResponse>(genericParameters,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Delete).ConfigureAwait(false);

            return response.Item1 != null && response.Item1.Deleted ? new Tuple<bool, string>(true, response.Item2) : new Tuple<bool, string>(false, response.Item2);
        }

        #endregion

        #region Hybrid Optimization

        /// <summary>
        ///     Returns a hybrid optimization for the specified date
        /// </summary>
        /// <param name="hybridOptimizationParameters">The hybrid optimization parameters containing schedule date and timezone</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A hYbrid optimization object</returns>
        public DataObject GetHybridOptimization(HybridOptimizationParameters hybridOptimizationParameters,
            out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObject>(hybridOptimizationParameters,
                R4MEInfrastructureSettings.HybridOptimization,
                HttpMethodType.Get, null, false, true,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Returns a hybrid optimization for the specified date
        /// </summary>
        /// <param name="hybridOptimizationParameters">The hybrid optimization parameters containing schedule date and timezone</param>
        /// <returns>A hYbrid optimization object</returns>
        public Task<Tuple<DataObject, string>> GetHybridOptimizationAsync(HybridOptimizationParameters hybridOptimizationParameters)
        {
            return GetJsonObjectFromAPIAsync<DataObject>(hybridOptimizationParameters,
                R4MEInfrastructureSettings.HybridOptimization,
                HttpMethodType.Get, null, false, true);
        }

        /// <summary>
        ///     Adds the depots to a hybrid optimization
        /// </summary>
        /// <param name="hybridDepotParameters">
        ///     The hybrid depot parameters containing parameters:
        ///     optimization_problem_id, delete_old_depots, new_depots
        /// </param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if the depots were added to an optimization successfuly, otherwise - false</returns>
        public bool AddDepotsToHybridOptimization(HybridDepotParameters hybridDepotParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<StatusResponse>(hybridDepotParameters,
                R4MEInfrastructureSettings.HybridDepots,
                HttpMethodType.Post,
                out errorString);

            return result != null
                ? result.GetType() == typeof(StatusResponse)
                    ? result.Status
                    : false
                : false;
        }

        /// <summary>
        ///     Adds the depots to a hybrid optimization
        /// </summary>
        /// <param name="hybridDepotParameters">
        ///     The hybrid depot parameters containing parameters:
        ///     optimization_problem_id, delete_old_depots, new_depots
        /// </param>
        /// <returns>True if the depots were added to an optimization successfuly, otherwise - false</returns>
        public async Task<Tuple<bool, string>> AddDepotsToHybridOptimizationAsync(HybridDepotParameters hybridDepotParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(hybridDepotParameters,
                R4MEInfrastructureSettings.HybridDepots,
                HttpMethodType.Post).ConfigureAwait(false);

            return result.Item1 != null
                ? result.Item1.GetType() == typeof(StatusResponse)
                    ? new Tuple<bool, string>(result.Item1.Status, result.Item2)
                    : new Tuple<bool, string>(false, result.Item2)
                : new Tuple<bool, string>(false, result.Item2);
        }

        #endregion

        #region Routes

        /// <summary>
        ///     Returns a route by route ID
        /// </summary>
        /// <param name="routeParameters">The route parameters containg a route ID</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A route object</returns>
        public DataObjectRoute GetRoute(RouteParametersQuery routeParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Get,
                null, false, true,
                out errorString);

            #region Shift the route date and route time to make them as shown in the web app.

            if (result != null && result.GetType() == typeof(DataObjectRoute) && routeParameters.ShiftByTimeZone)
                result = ShiftRouteDateTimeByTz(result);

            #endregion

            return result;
        }

        /// <summary>
        ///     Returns a route by route ID
        /// </summary>
        /// <param name="routeParameters">The route parameters containg a route ID</param>
        /// <returns>A route object</returns>
        public async Task<Tuple<DataObjectRoute, string>> GetRouteAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Get, null, false, true).ConfigureAwait(false);

            #region Shift the route date and route time to make them as shown in the web app.

            if (result.Item1 != null && result.Item1.GetType() == typeof(DataObjectRoute) && routeParameters.ShiftByTimeZone)
                result = new Tuple<DataObjectRoute, string>(ShiftRouteDateTimeByTz(result.Item1), result.Item2);

            #endregion

            return result;
        }

        /// <summary>
        ///     Shift the route date and route time to make them as shown in the web app.
        /// </summary>
        /// <param name="route">Input route object</param>
        /// <returns>Modified route object</returns>
        public DataObjectRoute ShiftRouteDateTimeByTz(DataObjectRoute route)
        {
            var tz = R4MeUtils.GetLocalTimeZone();
            var totalTime = (long) (route.Parameters.RouteDate + route.Parameters.RouteTime);

            totalTime += tz;

            route.Parameters.RouteDate = totalTime / 86400 * 86400;
            route.Parameters.RouteTime = (int) (totalTime - route.Parameters.RouteDate);

            return route;
        }

        /// <summary>
        ///     Returns array of the routes limited by the parameters: offset and limit.
        /// </summary>
        /// <param name="routeParameters">The route parameters containing the parameters: offset, limit</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>An array of the routes</returns>
        public DataObjectRoute[] GetRoutes(RouteParametersQuery routeParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Get,
                null, false, true,
                out errorString);

            if (result != null && result.GetType() == typeof(DataObjectRoute[]) && routeParameters.ShiftByTimeZone)
            {
                var lsRoutes = new List<DataObjectRoute>();

                foreach (var route in result)
                {
                    lsRoutes.Add(ShiftRouteDateTimeByTz(route));
                }
                result = lsRoutes.ToArray();
            }

            return result;
        }

        /// <summary>
        ///     Returns array of the routes limited by the parameters: offset and limit.
        /// </summary>
        /// <param name="routeParameters">The route parameters containing the parameters: offset, limit</param>
        /// <returns>An array of the routes</returns>
        public async Task<Tuple<DataObjectRoute[], string>> GetRoutesAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute[]>(routeParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Get, null, false, true).ConfigureAwait(false);

            if (result.Item1 != null && result.Item1.GetType() == typeof(DataObjectRoute[]) && routeParameters.ShiftByTimeZone)
            {
                var lsRoutes = new List<DataObjectRoute>();

                foreach (var route in result.Item1)
                {
                    lsRoutes.Add(ShiftRouteDateTimeByTz(route));
                }

                result = new Tuple<DataObjectRoute[], string>(lsRoutes.ToArray(), result.Item2);
            }

            return result;
        }

        /// <summary>
        ///     Returns a route ID from the first route of an optimization
        /// </summary>
        /// <param name="optimizationProblemId">Optimization problem ID</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A route ID</returns>
        public string GetRouteId(string optimizationProblemId, out string errorString)
        {
            var genericParameters = new GenericParameters();
            genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationProblemId);
            genericParameters.ParametersCollection.Add("wait_for_final_state", "1");

            var response = GetJsonObjectFromAPI<DataObject>(genericParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Get, null, false, true,
                out errorString);

            return response != null && response.Routes != null && response.Routes.Length > 0
                ? response.Routes[0].RouteId
                : null;
        }

        /// <summary>
        ///     Returns a route ID from the first route of an optimization
        /// </summary>
        /// <param name="optimizationProblemId">Optimization problem ID</param>
        /// <returns>A route ID</returns>
        public async Task<Tuple<string, string>> GetRouteIdAsync(string optimizationProblemId)
        {
            var genericParameters = new GenericParameters();
            genericParameters.ParametersCollection.Add("optimization_problem_id", optimizationProblemId);
            genericParameters.ParametersCollection.Add("wait_for_final_state", "1");

            var response = await GetJsonObjectFromAPIAsync<DataObject>(genericParameters,
                R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Get, null, false, true).ConfigureAwait(false);

            return new Tuple<string, string>(response.Item1?.Routes?.FirstOrDefault()?.RouteId, response.Item2);
        }

        /// <summary>
        ///     Updates route by the rotue parameters
        /// </summary>
        /// <param name="routeParameters">The route parameters</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A route</returns>
        public DataObjectRoute UpdateRoute(RouteParametersQuery routeParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put,
                null, false, true,
                out errorString);

            if (result != null && result.GetType() == typeof(DataObjectRoute) && routeParameters.ShiftByTimeZone)
                result = ShiftRouteDateTimeByTz(result);

            return result;
        }

        /// <summary>
        ///     Updates route by the rotue parameters
        /// </summary>
        /// <param name="routeParameters">The route parameters</param>
        /// <returns>A route</returns>
        public async Task<Tuple<DataObjectRoute, string>> UpdateRouteAsync(RouteParametersQuery routeParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute>(routeParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, null, false, true).ConfigureAwait(false);

            if (result.Item1 != null && result.Item1.GetType() == typeof(DataObjectRoute) && routeParameters.ShiftByTimeZone)
                result = new Tuple<DataObjectRoute, string>(ShiftRouteDateTimeByTz(result.Item1), result.Item2);

            return result;
        }

        private DataObjectRoute RemoveDuplicatedAddressesFromRoute(DataObjectRoute route, bool ShiftByTimeZone = false)
        {
            var lsAddress = new List<Address>();

            foreach (var addr1 in route.Addresses)
                if (!lsAddress.Contains(addr1) &&
                    lsAddress
                        .Where(x => x.RouteDestinationId == addr1.RouteDestinationId)
                        .FirstOrDefault() == null)
                    lsAddress.Add(addr1);

            route.Addresses = lsAddress.ToArray();

            if (route.GetType() == typeof(DataObjectRoute) && ShiftByTimeZone)
                route = ShiftRouteDateTimeByTz(route);

            return route;
        }

        /// <summary>
        ///     Update route by changed DataObjectRoute object directly.
        /// </summary>
        /// <param name="route">A route of the DataObjectRoute type as input parameters.</param>
        /// <param name="initialRoute">An initial route before update.</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Updated route</returns>
        public DataObjectRoute UpdateRoute(DataObjectRoute route, DataObjectRoute initialRoute, out string errorString)
        {
            errorString = "";

            if (initialRoute == null)
            {
                errorString = "An initial route should be specified";
                return null;
            }

            route = RemoveDuplicatedAddressesFromRoute(route);
            initialRoute = RemoveDuplicatedAddressesFromRoute(initialRoute);

            #region // ApprovedForExecution

            string approvedForExecution;

            if (initialRoute.ApprovedForExecution != route.ApprovedForExecution)
            {
                approvedForExecution = string.Concat("{\"approved_for_execution\": ",
                    route.ApprovedForExecution ? "true" : "false", "}");

                var genParams = new RouteParametersQuery
                {
                    RouteId = initialRoute.RouteId
                };

                var content = new StringContent(approvedForExecution, Encoding.UTF8, "application/json");

                initialRoute = GetJsonObjectFromAPI<DataObjectRoute>
                (genParams, R4MEInfrastructureSettings.RouteHost,
                    HttpMethodType.Put, content, false, true, out errorString);

                if (initialRoute == null) return null;
            }

            #endregion

            #region // Resequence if sequence was changed

            var resequenceJson = "";

            foreach (var addr1 in initialRoute.Addresses)
            {
                var addr = route.Addresses.Where(x => x.RouteDestinationId == addr1.RouteDestinationId)
                    .FirstOrDefault();

                if (addr != null && (addr.SequenceNo != addr1.SequenceNo || addr.IsDepot != addr1.IsDepot))
                {
                    resequenceJson += "{\"route_destination_id\":" + addr1.RouteDestinationId;

                    if (addr.SequenceNo != addr1.SequenceNo)
                        resequenceJson += "," + "\"sequence_no\":" + addr.SequenceNo;
                    else if (addr.IsDepot != addr1.IsDepot)
                        resequenceJson += "," + "\"is_depot\":" + addr.IsDepot.ToString().ToLower();

                    resequenceJson += "},";
                }
            }

            if (resequenceJson.Length > 10)
            {
                resequenceJson = resequenceJson.TrimEnd(',');
                resequenceJson = "{\"addresses\": [" + resequenceJson + "]}";

                var genParams = new RouteParametersQuery
                {
                    RouteId = initialRoute.RouteId
                };

                var content = new StringContent(resequenceJson, Encoding.UTF8, "application/json");

                initialRoute = GetJsonObjectFromAPI<DataObjectRoute>
                (genParams, R4MEInfrastructureSettings.RouteHost,
                    HttpMethodType.Put, content, false, true, out errorString);

                if (initialRoute == null) return null;
            }

            #endregion

            #region // Update Route Parameters

            if (route.Parameters != null)
            {
                var updatableRouteParametersProperties = R4MeUtils
                    .GetPropertiesWithDifferentValues(route.Parameters, initialRoute.Parameters, out errorString);

                if (updatableRouteParametersProperties != null && updatableRouteParametersProperties.Count > 0)
                {
                    var dynamicRouteProperties = new Route4MeDynamicClass();

                    dynamicRouteProperties.CopyPropertiesFromClass(route.Parameters, updatableRouteParametersProperties,
                        out var _);

                    var routeParamsJsonString =
                        R4MeUtils.SerializeObjectToJson(dynamicRouteProperties.DynamicProperties, true);

                    routeParamsJsonString = string.Concat("{\"parameters\":", routeParamsJsonString, "}");

                    var genParams = new RouteParametersQuery
                    {
                        RouteId = initialRoute.RouteId
                    };

                    var content = new StringContent(routeParamsJsonString, Encoding.UTF8, "application/json");

                    initialRoute = GetJsonObjectFromAPI<DataObjectRoute>
                    (genParams, R4MEInfrastructureSettings.RouteHost,
                        HttpMethodType.Put, content, false, true, out errorString);
                }
            }

            #endregion

            if (initialRoute == null) return null;

            #region // Update Route Addresses

            if (route.Addresses != null && route.Addresses.Length > 0)
                foreach (var address in route.Addresses)
                {
                    var initialAddress = initialRoute.Addresses
                        .Where(x => x.RouteDestinationId == address.RouteDestinationId)
                        .FirstOrDefault();

                    if (initialAddress == null)
                        initialAddress = initialRoute.Addresses
                            .Where(x => x.AddressString == address.AddressString)
                            .FirstOrDefault();

                    if (initialAddress == null) continue;

                    var updatableAddressProperties = R4MeUtils
                        .GetPropertiesWithDifferentValues(address, initialAddress, out errorString);

                    if (updatableAddressProperties.Contains("IsDepot")) updatableAddressProperties.Remove("IsDepot");
                    if (updatableAddressProperties.Contains("SequenceNo"))
                        updatableAddressProperties.Remove("SequenceNo");
                    if (updatableAddressProperties.Contains("OptimizationProblemId")
                        && updatableAddressProperties.Count == 1)
                        updatableAddressProperties.Remove("OptimizationProblemId");

                    if (updatableAddressProperties != null && updatableAddressProperties.Count > 0)
                    {
                        var dynamicAddressProperties = new Route4MeDynamicClass();

                        dynamicAddressProperties.CopyPropertiesFromClass(address, updatableAddressProperties,
                            out _);

                        var addressParamsJsonString =
                            R4MeUtils.SerializeObjectToJson(dynamicAddressProperties.DynamicProperties, true);

                        var genParams = new RouteParametersQuery
                        {
                            RouteId = initialRoute.RouteId,
                            RouteDestinationId = address.RouteDestinationId
                        };

                        var content = new StringContent(addressParamsJsonString, Encoding.UTF8, "application/json");

                        var updatedAddress = GetJsonObjectFromAPI<Address>
                        (genParams, R4MEInfrastructureSettings.GetAddress,
                            HttpMethodType.Put, content, out errorString);

                        updatedAddress.IsDepot = initialAddress.IsDepot;
                        updatedAddress.SequenceNo = initialAddress.SequenceNo;

                        if ((address?.Notes?.Length ?? -1) > 0)
                        {
                            var addressNotes = new List<AddressNote>();

                            foreach (var note1 in address.Notes)
                                if ((note1?.NoteId ?? null) == -1)
                                {
                                    var noteParameters = new NoteParameters
                                    {
                                        RouteId = initialRoute.RouteId,
                                        AddressId = updatedAddress.RouteDestinationId != null
                                            ? (int) updatedAddress.RouteDestinationId
                                            : note1.RouteDestinationId,
                                        Latitude = note1.Latitude,
                                        Longitude = note1.Longitude
                                    };

                                    if (note1.ActivityType != null) noteParameters.ActivityType = note1.ActivityType;
                                    if (note1.DeviceType != null) noteParameters.DeviceType = note1.DeviceType;

                                    var noteContent = note1.Contents != null ? note1.Contents : null;

                                    if (noteContent != null) noteParameters.StrNoteContents = noteContent;

                                    Dictionary<string, string> customNotes = null;

                                    if ((note1?.CustomTypes?.Length ?? -1) > 0)
                                    {
                                        customNotes = new Dictionary<string, string>();

                                        foreach (var customNote in note1.CustomTypes)
                                            customNotes.Add("custom_note_type[" + customNote.NoteCustomTypeID + "]",
                                                customNote.NoteCustomValue);
                                    }

                                    if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

                                    if (note1.UploadUrl != null) noteParameters.StrFileName = note1.UploadUrl;

                                    var addedNote = AddAddressNote(noteParameters, out var errorString3);
                                    if (errorString3 != "") errorString += "\nNote Adding Error: " + errorString3;

                                    if (addedNote != null) addressNotes.Add(addedNote);
                                }
                                else
                                {
                                    addressNotes.Add(note1);
                                }

                            address.Notes = addressNotes.ToArray();
                            updatedAddress.Notes = addressNotes.ToArray();
                        }

                        if (updatedAddress != null && updatedAddress.GetType() == typeof(Address))
                        {
                            var addressIndex = Array.IndexOf(initialRoute.Addresses, initialAddress);
                            if (addressIndex > -1) initialRoute.Addresses[addressIndex] = updatedAddress;
                        }
                    }
                }

            #endregion

            return initialRoute;
        }

        /// <summary>
        ///     Update route by changed DataObjectRoute object directly.
        /// </summary>
        /// <param name="route">A route of the DataObjectRoute type as input parameters.</param>
        /// <param name="initialRoute">An initial route before update.</param>
        /// <returns>Updated route</returns>
        public async Task<Tuple<DataObjectRoute, string>> UpdateRouteAsync(DataObjectRoute route, DataObjectRoute initialRoute)
        {
            string errorString = null;
            if (initialRoute == null)
            {
                return new Tuple<DataObjectRoute, string>(null, "An initial route should be specified");
            }

            route = RemoveDuplicatedAddressesFromRoute(route);
            initialRoute = RemoveDuplicatedAddressesFromRoute(initialRoute);

            #region // ApprovedForExecution

            string approvedForExecution;

            if (initialRoute.ApprovedForExecution != route.ApprovedForExecution)
            {
                approvedForExecution = string.Concat("{\"approved_for_execution\": ",
                    route.ApprovedForExecution ? "true" : "false", "}");

                var genParams = new RouteParametersQuery
                {
                    RouteId = initialRoute.RouteId
                };

                var content = new StringContent(approvedForExecution, Encoding.UTF8, "application/json");

                var res = await GetJsonObjectFromAPIAsync<DataObjectRoute>
                (genParams, R4MEInfrastructureSettings.RouteHost,
                    HttpMethodType.Put, content, false, true).ConfigureAwait(false);
                initialRoute = res.Item1;

                if (initialRoute == null) return res;
            }

            #endregion

            #region // Resequence if sequence was changed

            var resequenceJson = "";

            foreach (var addr1 in initialRoute.Addresses)
            {
                var addr = route.Addresses.Where(x => x.RouteDestinationId == addr1.RouteDestinationId)
                    .FirstOrDefault();

                if (addr != null && (addr.SequenceNo != addr1.SequenceNo || addr.IsDepot != addr1.IsDepot))
                {
                    resequenceJson += "{\"route_destination_id\":" + addr1.RouteDestinationId;

                    if (addr.SequenceNo != addr1.SequenceNo)
                        resequenceJson += "," + "\"sequence_no\":" + addr.SequenceNo;
                    else if (addr.IsDepot != addr1.IsDepot)
                        resequenceJson += "," + "\"is_depot\":" + addr.IsDepot.ToString().ToLower();

                    resequenceJson += "},";
                }
            }

            if (resequenceJson.Length > 10)
            {
                resequenceJson = resequenceJson.TrimEnd(',');
                resequenceJson = "{\"addresses\": [" + resequenceJson + "]}";

                var genParams = new RouteParametersQuery
                {
                    RouteId = initialRoute.RouteId
                };

                var content = new StringContent(resequenceJson, Encoding.UTF8, "application/json");

                var res = await GetJsonObjectFromAPIAsync<DataObjectRoute>
                (genParams, R4MEInfrastructureSettings.RouteHost,
                    HttpMethodType.Put, content, false, true).ConfigureAwait(false);
                initialRoute = res.Item1;

                if (initialRoute == null) return res;
            }

            #endregion

            #region // Update Route Parameters

            if (route.Parameters != null)
            {
                var updatableRouteParametersProperties = R4MeUtils
                    .GetPropertiesWithDifferentValues(route.Parameters, initialRoute.Parameters, out errorString);

                if (updatableRouteParametersProperties != null && updatableRouteParametersProperties.Count > 0)
                {
                    var dynamicRouteProperties = new Route4MeDynamicClass();

                    dynamicRouteProperties.CopyPropertiesFromClass(route.Parameters, updatableRouteParametersProperties,
                        out var _);

                    var routeParamsJsonString =
                        R4MeUtils.SerializeObjectToJson(dynamicRouteProperties.DynamicProperties, true);

                    routeParamsJsonString = string.Concat("{\"parameters\":", routeParamsJsonString, "}");

                    var genParams = new RouteParametersQuery
                    {
                        RouteId = initialRoute.RouteId
                    };

                    var content = new StringContent(routeParamsJsonString, Encoding.UTF8, "application/json");

                    var res = await GetJsonObjectFromAPIAsync<DataObjectRoute>
                    (genParams, R4MEInfrastructureSettings.RouteHost,
                        HttpMethodType.Put, content, false, true).ConfigureAwait(false);

                    initialRoute = res.Item1;
                }
            }

            #endregion

            if (initialRoute == null) return null;

            #region // Update Route Addresses

            if (route.Addresses != null && route.Addresses.Length > 0)
                foreach (var address in route.Addresses)
                {
                    var initialAddress = initialRoute.Addresses
                        .Where(x => x.RouteDestinationId == address.RouteDestinationId)
                        .FirstOrDefault();

                    if (initialAddress == null)
                        initialAddress = initialRoute.Addresses
                            .Where(x => x.AddressString == address.AddressString)
                            .FirstOrDefault();

                    if (initialAddress == null) continue;

                    var updatableAddressProperties = R4MeUtils
                        .GetPropertiesWithDifferentValues(address, initialAddress, out errorString);

                    if (updatableAddressProperties.Contains("IsDepot")) updatableAddressProperties.Remove("IsDepot");
                    if (updatableAddressProperties.Contains("SequenceNo"))
                        updatableAddressProperties.Remove("SequenceNo");
                    if (updatableAddressProperties.Contains("OptimizationProblemId")
                        && updatableAddressProperties.Count == 1)
                        updatableAddressProperties.Remove("OptimizationProblemId");

                    if (updatableAddressProperties != null && updatableAddressProperties.Count > 0)
                    {
                        var dynamicAddressProperties = new Route4MeDynamicClass();

                        dynamicAddressProperties.CopyPropertiesFromClass(address, updatableAddressProperties,
                            out _);

                        var addressParamsJsonString =
                            R4MeUtils.SerializeObjectToJson(dynamicAddressProperties.DynamicProperties, true);

                        var genParams = new RouteParametersQuery
                        {
                            RouteId = initialRoute.RouteId,
                            RouteDestinationId = address.RouteDestinationId
                        };

                        var content = new StringContent(addressParamsJsonString, Encoding.UTF8, "application/json");

                        var res = await GetJsonObjectFromAPIAsync<Address>
                        (genParams, R4MEInfrastructureSettings.GetAddress,
                            HttpMethodType.Put, content, false, false).ConfigureAwait(false);
                        var updatedAddress = res.Item1;
                        errorString = res.Item2;

                        updatedAddress.IsDepot = initialAddress.IsDepot;
                        updatedAddress.SequenceNo = initialAddress.SequenceNo;

                        if ((address?.Notes?.Length ?? -1) > 0)
                        {
                            var addressNotes = new List<AddressNote>();

                            foreach (var note1 in address.Notes)
                                if ((note1?.NoteId ?? null) == -1)
                                {
                                    var noteParameters = new NoteParameters
                                    {
                                        RouteId = initialRoute.RouteId,
                                        AddressId = updatedAddress.RouteDestinationId != null
                                            ? (int)updatedAddress.RouteDestinationId
                                            : note1.RouteDestinationId,
                                        Latitude = note1.Latitude,
                                        Longitude = note1.Longitude
                                    };

                                    if (note1.ActivityType != null) noteParameters.ActivityType = note1.ActivityType;
                                    if (note1.DeviceType != null) noteParameters.DeviceType = note1.DeviceType;

                                    var noteContent = note1.Contents != null ? note1.Contents : null;

                                    if (noteContent != null) noteParameters.StrNoteContents = noteContent;

                                    Dictionary<string, string> customNotes = null;

                                    if ((note1?.CustomTypes?.Length ?? -1) > 0)
                                    {
                                        customNotes = new Dictionary<string, string>();

                                        foreach (var customNote in note1.CustomTypes)
                                            customNotes.Add("custom_note_type[" + customNote.NoteCustomTypeID + "]",
                                                customNote.NoteCustomValue);
                                    }

                                    if (customNotes != null) noteParameters.CustomNoteTypes = customNotes;

                                    if (note1.UploadUrl != null) noteParameters.StrFileName = note1.UploadUrl;

                                    var addedNote = AddAddressNote(noteParameters, out var errorString3);
                                    if (errorString3 != "") errorString += "\nNote Adding Error: " + errorString3;

                                    if (addedNote != null) addressNotes.Add(addedNote);
                                }
                                else
                                {
                                    addressNotes.Add(note1);
                                }

                            address.Notes = addressNotes.ToArray();
                            updatedAddress.Notes = addressNotes.ToArray();
                        }

                        if (updatedAddress != null && updatedAddress.GetType() == typeof(Address))
                        {
                            var addressIndex = Array.IndexOf(initialRoute.Addresses, initialAddress);
                            if (addressIndex > -1) initialRoute.Addresses[addressIndex] = updatedAddress;
                        }
                    }
                }

            #endregion

            return new Tuple<DataObjectRoute, string>(initialRoute, errorString);
        }

        /// <summary>
        ///     Duplicates a route
        /// </summary>
        /// <param name="queryParameters">The query parameters containing a route ID to be duplicated</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>DuplicateRouteResponse type object</returns>
        public DuplicateRouteResponse DuplicateRoute(RouteParametersQuery queryParameters, out string errorString)
        {
            //queryParameters.ParametersCollection["to"] = "none";
            var response = GetJsonObjectFromAPI<DuplicateRouteResponse>(queryParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Post,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Duplicates a route
        /// </summary>
        /// <param name="queryParameters">The query parameters containing a route ID to be duplicated</param>
        /// <returns>DuplicateRouteResponse type object</returns>
        public Task<Tuple<DuplicateRouteResponse, string>> DuplicateRouteAsync(RouteParametersQuery queryParameters)
        {
            return GetJsonObjectFromAPIAsync<DuplicateRouteResponse>(queryParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Removes the routes from a user's account
        /// </summary>
        /// <param name="routeIds">The array of the route IDs to be removed</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Array of the removed routes IDs</returns>
        public string[] DeleteRoutes(string[] routeIds, out string errorString)
        {
            var strRouteIds = "";

            foreach (var routeId in routeIds)
            {
                if (strRouteIds.Length > 0) strRouteIds += ",";
                strRouteIds += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", strRouteIds);

            var response = GetJsonObjectFromAPI<DeleteRouteResponse>(genericParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Delete,
                out errorString);

            return response?.RouteIds;
        }

        /// <summary>
        ///     Removes the routes from a user's account
        /// </summary>
        /// <param name="routeIds">The array of the route IDs to be removed</param>
        /// <returns>Array of the removed routes IDs</returns>
        public async Task<Tuple<string[], string>> DeleteRoutesAsync(string[] routeIds)
        {
            var strRouteIds = "";

            foreach (var routeId in routeIds)
            {
                if (strRouteIds.Length > 0) strRouteIds += ",";
                strRouteIds += routeId;
            }

            var genericParameters = new GenericParameters();

            genericParameters.ParametersCollection.Add("route_id", strRouteIds);

            var response = await GetJsonObjectFromAPIAsync<DeleteRouteResponse>(genericParameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<string[], string>(response.Item1?.RouteIds, response.Item2);
        }

        /// <summary>
        ///     Merges the routes
        /// </summary>
        /// <param name="mergeRoutesParameters">
        ///     The parameters containing:
        ///     <para>RouteIds: IDs of the routes to be merged</para>
        ///     <para>DepotAddress: a depot address of the merged route</para>
        ///     <para>RemoveOrigin: if true, the origin routes will be removed</para>
        ///     <para>DepotLat: the depot's latitude</para>
        ///     <para>DepotLng: the depot's longitude</para>
        /// </param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if the routes were merged successfuly</returns>
        public bool MergeRoutes(MergeRoutesQuery mergeRoutesParameters, out string errorString)
        {
            var roParames = new GenericParameters();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("route_ids", mergeRoutesParameters.RouteIds),
                new KeyValuePair<string, string>("depot_address", mergeRoutesParameters.DepotAddress),
                new KeyValuePair<string, string>("remove_origin", mergeRoutesParameters.RemoveOrigin.ToString()),
                new KeyValuePair<string, string>("depot_lat", mergeRoutesParameters.DepotLat.ToString()),
                new KeyValuePair<string, string>("depot_lng", mergeRoutesParameters.DepotLng.ToString())
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = GetJsonObjectFromAPI<StatusResponse>
                (roParames, R4MEInfrastructureSettings.MergeRoutes,
                    HttpMethodType.Post, httpContent, out errorString);

                return response != null && response.Status;
            }
        }

        /// <summary>
        ///     Merges the routes
        /// </summary>
        /// <param name="mergeRoutesParameters">
        ///     The parameters containing:
        ///     <para>RouteIds: IDs of the routes to be merged</para>
        ///     <para>DepotAddress: a depot address of the merged route</para>
        ///     <para>RemoveOrigin: if true, the origin routes will be removed</para>
        ///     <para>DepotLat: the depot's latitude</para>
        ///     <para>DepotLng: the depot's longitude</para>
        /// </param>
        /// <returns>True if the routes were merged successfuly</returns>
        public async Task<Tuple<bool, string>> MergeRoutesAsync(MergeRoutesQuery mergeRoutesParameters)
        {
            var roParames = new GenericParameters();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("route_ids", mergeRoutesParameters.RouteIds),
                new KeyValuePair<string, string>("depot_address", mergeRoutesParameters.DepotAddress),
                new KeyValuePair<string, string>("remove_origin", mergeRoutesParameters.RemoveOrigin.ToString()),
                new KeyValuePair<string, string>("depot_lat", mergeRoutesParameters.DepotLat.ToString()),
                new KeyValuePair<string, string>("depot_lng", mergeRoutesParameters.DepotLng.ToString())
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = await GetJsonObjectFromAPIAsync<StatusResponse>
                (roParames, R4MEInfrastructureSettings.MergeRoutes,
                    HttpMethodType.Post, httpContent, false, false).ConfigureAwait(false);

                return  new Tuple<bool, string>(response.Item1 != null && response.Item1.Status, response.Item2);
            }
        }

        /// <summary>
        ///     Resequences/roptimizes a route. TO DO: this endpoint seems to be deprecated and should be disabled
        /// </summary>
        /// <param name="roParames">The parameters for reoptimizng</param>
        /// <param name="errorString">Error string</param>
        /// <returns>True, if a route reoptimized/resequences successfully</returns>
        [Obsolete("The method is obsolete, use the method ReoptimizeRoute instead.")]
        public bool ResequenceReoptimizeRoute(Dictionary<string, string> roParames, out string errorString)
        {
            var request = new RouteParametersQuery
            {
                RouteId = roParames["route_id"],
                DisableOptimization = roParames["disable_optimization"] == "1" ? true : false,
                Optimize = roParames["optimize"]
            };

            var response = GetJsonObjectFromAPI<StatusResponse>(
                request,
                R4MEInfrastructureSettings.RouteReoptimize,
                HttpMethodType.Get,
                out errorString);

            return response != null && response.Status ? true : false;
        }

        /// <summary>
        ///     Reoptimze a route
        /// </summary>
        /// <param name="queryParams">
        ///     Route query parameters containing parameters:
        ///     <para>ReOptimize = true, enables reoptimization of a route</para>
        ///     <para>Remaining=0 - disables resequencing of the remaining stops. </para>
        ///     <para>Remaining=1 - enables resequencing of the remaining stops. </para>
        /// </param>
        /// <param name="errorString">Error string</param>
        /// <returns>Reoptimized route</returns>
        public DataObjectRoute ReoptimizeRoute(RouteParametersQuery queryParams, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute>(queryParams,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put,
                null, false, true,
                out errorString);

            if (result != null && result.GetType() == typeof(DataObjectRoute) && queryParams.ShiftByTimeZone)
                result = ShiftRouteDateTimeByTz(result);

            return result;
        }

        /// <summary>
        ///     Reoptimze a route
        /// </summary>
        /// <param name="queryParams">
        ///     Route query parameters containing parameters:
        ///     <para>ReOptimize = true, enables reoptimization of a route</para>
        ///     <para>Remaining=0 - disables resequencing of the remaining stops. </para>
        ///     <para>Remaining=1 - enables resequencing of the remaining stops. </para>
        /// </param>
        /// <returns>Reoptimized route</returns>
        public async Task<Tuple<DataObjectRoute, string>> ReoptimizeRouteAsync(RouteParametersQuery queryParams)
        {
            var result = await GetJsonObjectFromAPIAsync<DataObjectRoute>(queryParams,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, null, false, true).ConfigureAwait(false);

            if (result.Item1 != null && result.Item1.GetType() == typeof(DataObjectRoute) && queryParams.ShiftByTimeZone)
                result = new Tuple<DataObjectRoute, string>(ShiftRouteDateTimeByTz(result.Item1), result.Item2);

            return result;
        }

        /// <summary>
        ///     Re-sequences manually a route
        /// </summary>
        /// <param name="rParams">The parameters object RouteParametersQuery containing the parameter RouteId </param>
        /// <param name="addresses">The route addresses</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>A re-sequenced route</returns>
        public DataObjectRoute ManuallyResequenceRoute(RouteParametersQuery rParams, Address[] addresses,
            out string errorString)
        {
            var request = new ManuallyResequenceRouteRequest
            {
                RouteId = rParams.RouteId
            };

            var lsAddresses = new List<AddressInfo>();

            var iMaxSequenceNumber = 0;

            foreach (var address in addresses)
            {
                var aInfo = new AddressInfo
                {
                    DestinationId = address.RouteDestinationId ?? -1,
                    SequenceNo = address.SequenceNo ?? iMaxSequenceNumber
                };

                lsAddresses.Add(aInfo);

                iMaxSequenceNumber++;
            }

            request.Addresses = lsAddresses.ToArray();

            var route1 = GetJsonObjectFromAPI<DataObjectRoute>(request,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put,
                null, false, true,
                out errorString);


            if (route1 != null && route1.GetType() == typeof(DataObjectRoute) && rParams.ShiftByTimeZone)
                route1 = ShiftRouteDateTimeByTz(route1);

            return route1;
        }

        /// <summary>
        ///     Re-sequences manually a route
        /// </summary>
        /// <param name="rParams">The parameters object RouteParametersQuery containing the parameter RouteId </param>
        /// <param name="addresses">The route addresses</param>
        /// <returns>A re-sequenced route</returns>
        public async Task<Tuple<DataObjectRoute, string>> ManuallyResequenceRouteAsync(RouteParametersQuery rParams, Address[] addresses)
        {
            var request = new ManuallyResequenceRouteRequest
            {
                RouteId = rParams.RouteId
            };

            var lsAddresses = new List<AddressInfo>();

            var iMaxSequenceNumber = 0;

            foreach (var address in addresses)
            {
                var aInfo = new AddressInfo
                {
                    DestinationId = address.RouteDestinationId ?? -1,
                    SequenceNo = address.SequenceNo ?? iMaxSequenceNumber
                };

                lsAddresses.Add(aInfo);

                iMaxSequenceNumber++;
            }

            request.Addresses = lsAddresses.ToArray();

            var route1 = await GetJsonObjectFromAPIAsync<DataObjectRoute>(request,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, null, false, true).ConfigureAwait(false);


            if (route1.Item1 != null && route1.Item1.GetType() == typeof(DataObjectRoute) && rParams.ShiftByTimeZone)
                route1 = new Tuple<DataObjectRoute, string>(ShiftRouteDateTimeByTz(route1.Item1), route1.Item2);

            return route1;
        }

        /// <summary>
        ///     Shares a route by an email
        /// </summary>
        /// <param name="roParames">
        ///     The RouteParametersQuery parameters object contains parameters:
        ///     <para>RouteId: a route ID to be shared</para>
        ///     <para>ResponseFormat: the response format</para>
        /// </param>
        /// <param name="email">Recipient email</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>True if a route was shared</returns>
        public bool RouteSharing(RouteParametersQuery roParames, string email, out string errorString)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("recipient_email", email)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = GetJsonObjectFromAPI<StatusResponse>(
                    roParames,
                    R4MEInfrastructureSettings.RouteSharing,
                    HttpMethodType.Post,
                    httpContent,
                    out errorString);

                return response != null && response.Status;
            }
        }

        /// <summary>
        ///     Shares a route by an email
        /// </summary>
        /// <param name="roParames">
        ///     The RouteParametersQuery parameters object contains parameters:
        ///     <para>RouteId: a route ID to be shared</para>
        ///     <para>ResponseFormat: the response format</para>
        /// </param>
        /// <param name="email">Recipient email</param>
        /// <returns>True if a route was shared</returns>
        public async Task<Tuple<bool, string>> RouteSharingAsync(RouteParametersQuery roParames, string email)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("recipient_email", email)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = await GetJsonObjectFromAPIAsync<StatusResponse>(
                    roParames,
                    R4MEInfrastructureSettings.RouteSharing,
                    HttpMethodType.Post,
                    httpContent,
                    false,
                    false).ConfigureAwait(false);

                return new Tuple<bool, string>(response.Item1 != null && response.Item1.Status, response.Item2);
            }
        }

        /// <summary>
        ///     Updates a route's custom data
        /// </summary>
        /// <param name="routeParameters">
        ///     The RouteParametersQuery object contains parameters:
        ///     <para>RouteId: a route ID to be updated</para>
        ///     <para>RouteDestinationId: the ID of the route destination to be updated</para>
        /// </param>
        /// <param name="customData">The keyvalue pairs of the type Dictionary<string, string></param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>Updated route destination</returns>
        public Address UpdateRouteCustomData(RouteParametersQuery routeParameters,
            Dictionary<string, string> customData, out string errorString)
        {
            var request = new UpdateRouteCustomDataRequest
            {
                RouteId = routeParameters.RouteId,
                RouteDestinationId = routeParameters.RouteDestinationId,
                CustomFields = customData
            };

            return GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updates a route's custom data
        /// </summary>
        /// <param name="routeParameters">
        ///     The RouteParametersQuery object contains parameters:
        ///     <para>RouteId: a route ID to be updated</para>
        ///     <para>RouteDestinationId: the ID of the route destination to be updated</para>
        /// </param>
        /// <param name="customData">The keyvalue pairs of the type Dictionary<string, string></param>
        /// <returns>Updated route destination</returns>
        public Task<Tuple<Address, string>> UpdateRouteCustomDataAsync(RouteParametersQuery routeParameters,
            Dictionary<string, string> customData)
        {
            var request = new UpdateRouteCustomDataRequest
            {
                RouteId = routeParameters.RouteId,
                RouteDestinationId = routeParameters.RouteDestinationId,
                CustomFields = customData
            };

            return GetJsonObjectFromAPIAsync<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put);
        }

        /// <summary>
        ///     Updated a route destination
        /// </summary>
        /// <param name="addressParameters">Contains an address object</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>The updated address</returns>
        public Address UpdateRouteDestination(Address addressParameters, out string errorString)
        {
            var request = new UpdateRouteDestinationRequest
            {
                RouteId = addressParameters.RouteId,
                RouteDestinationId = addressParameters.RouteDestinationId
            };

            foreach (var propInfo in typeof(Address).GetProperties())
                propInfo.SetValue(request, propInfo.GetValue(addressParameters));

            return GetJsonObjectFromAPI<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updated a route destination
        /// </summary>
        /// <param name="addressParameters">Contains an address object</param>
        /// <returns>The updated address</returns>
        public Task<Tuple<Address, string>> UpdateRouteDestinationAsync(Address addressParameters)
        {
            var request = new UpdateRouteDestinationRequest
            {
                RouteId = addressParameters.RouteId,
                RouteDestinationId = addressParameters.RouteDestinationId
            };

            foreach (var propInfo in typeof(Address).GetProperties())
                propInfo.SetValue(request, propInfo.GetValue(addressParameters));

            return GetJsonObjectFromAPIAsync<Address>(request, R4MEInfrastructureSettings.GetAddress, HttpMethodType.Put);
        }

        /// <summary>
        ///     Updates an optimization destination
        /// </summary>
        /// <param name="addressParameters">Contains an address object</param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>The updated address</returns>
        public Address UpdateOptimizationDestination(Address addressParameters, out string errorString)
        {
            var request = new UpdateRouteDestinationRequest
            {
                OptimizationProblemId = addressParameters.OptimizationProblemId
            };

            foreach (var propInfo in typeof(Address).GetProperties())
                propInfo.SetValue(request, propInfo.GetValue(addressParameters));

            var dataObject = GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Put, null, false, true, out errorString);

            return dataObject?.Addresses?.Where(x => x.RouteDestinationId == addressParameters.RouteDestinationId)
                .FirstOrDefault();
        }

        /// <summary>
        ///     Updates an optimization destination
        /// </summary>
        /// <param name="addressParameters">Contains an address object</param>
        /// <returns>The updated address</returns>
        public async Task<Tuple<Address, string>> UpdateOptimizationDestinationAsync(Address addressParameters)
        {
            var request = new UpdateRouteDestinationRequest
            {
                OptimizationProblemId = addressParameters.OptimizationProblemId
            };

            foreach (var propInfo in typeof(Address).GetProperties())
                propInfo.SetValue(request, propInfo.GetValue(addressParameters));

            var dataObject = await GetJsonObjectFromAPIAsync<DataObject>(request, R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Put, null, false, true).ConfigureAwait(false);

            return new Tuple<Address, string>(dataObject.Item1?.Addresses?.Where(x => x.RouteDestinationId == addressParameters.RouteDestinationId)
                .FirstOrDefault(), dataObject.Item2);
        }

        /// <summary>
        ///     Get schedule calendar from the user's account.
        /// </summary>
        /// <param name="scheduleCalendarParams">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>Schedule calendar of the member</returns>
        public ScheduleCalendarResponse GetScheduleCalendar(ScheduleCalendarQuery scheduleCalendarParams,
            out string errorString)
        {
            var response = GetJsonObjectFromAPI<ScheduleCalendarResponse>(scheduleCalendarParams,
                R4MEInfrastructureSettings.ScheduleCalendar,
                HttpMethodType.Get,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Get schedule calendar from the user's account.
        /// </summary>
        /// <param name="scheduleCalendarParams">Query parameters</param>
        /// <returns>Schedule calendar of the member</returns>
        public Task<Tuple<ScheduleCalendarResponse, string>> GetScheduleCalendarAsync(ScheduleCalendarQuery scheduleCalendarParams)
        {
            return GetJsonObjectFromAPIAsync<ScheduleCalendarResponse>(scheduleCalendarParams,
                R4MEInfrastructureSettings.ScheduleCalendar,
                HttpMethodType.Get);
        }

        #endregion

        #region Tracking

        /// <summary>
        ///     Returns a last location of the device on the route
        /// </summary>
        /// <param name="parameters">
        ///     Contains the parameters:
        ///     <para>route_id: the route ID</para>
        ///     <para>device_tracking_history: If 1 device tracking history will be returned</para>
        /// </param>
        /// <param name="errorString">Returned error string in case of the processs failing</param>
        /// <returns>An optimization with the tracking data</returns>
        public DataObjectRoute GetLastLocation(RouteParametersQuery parameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<DataObjectRoute>(parameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Get,
                false,
                true,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Returns a last location of the device on the route
        /// </summary>
        /// <param name="parameters">
        ///     Contains the parameters:
        ///     <para>route_id: the route ID</para>
        ///     <para>device_tracking_history: If 1 device tracking history will be returned</para>
        /// </param>
        /// <returns>An optimization with the tracking data</returns>
        public Task<Tuple<DataObjectRoute, string>> GetLastLocationAsync(RouteParametersQuery parameters)
        {
            return GetJsonObjectFromAPIAsync<DataObjectRoute>(parameters,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Get,
                null,
                false,
                true);
        }

        /// <summary>
        ///     Returns device location history from the specified date range.
        /// </summary>
        /// <param name="gpsParameters">Query parameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>
        ///     If response contains not null data, returns object of the type GetDeviceLocationHistoryResponse.
        ///     If query was without error, but nothing was found, returns null.
        ///     If query failed, return error string.
        /// </returns>
        public DeviceLocationHistoryResponse GetDeviceLocationHistory(GPSParameters gpsParameters,
            out string errorString)
        {
            var result = GetJsonObjectFromAPI<DeviceLocationHistoryResponse>(gpsParameters,
                R4MEInfrastructureSettings.DeviceLocation,
                HttpMethodType.Get,
                false,
                false,
                out errorString);

            return result == null && errorString != ""
                ? null
                : result?.Data.Length == 0
                    ? null
                    : result;
        }

        /// <summary>
        ///     Returns device location history from the specified date range.
        /// </summary>
        /// <param name="gpsParameters">Query parameters</param>
        /// <returns>
        ///     If response contains not null data, returns object of the type GetDeviceLocationHistoryResponse.
        ///     If query was without error, but nothing was found, returns null.
        ///     If query failed, return error string.
        /// </returns>
        public async Task<Tuple<DeviceLocationHistoryResponse, string>> GetDeviceLocationHistoryAsync(GPSParameters gpsParameters)
        {
            var result = await GetJsonObjectFromAPIAsync<DeviceLocationHistoryResponse>(gpsParameters,
                R4MEInfrastructureSettings.DeviceLocation,
                HttpMethodType.Get,
                null,
                false,
                false).ConfigureAwait(false);

            return result.Item1 == null && result.Item2 != ""
                ? null
                : result.Item1?.Data.Length == 0
                    ? null
                    : result;
        }

        /// <summary>
        ///     Sets GPS info in the device
        /// </summary>
        /// <param name="gpsParameters">The parameters of the type GPSParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>
        ///     The response containing parameters:
        ///     <para>status: if true GPD info was set successfuly on a device</para>
        ///     <para>tx_id: tracking info ID</para>
        /// </returns>
        public SetGpsResponse SetGPS(GPSParameters gpsParameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<SetGpsResponse>(gpsParameters,
                R4MEInfrastructureSettings.SetGpsHost,
                HttpMethodType.Get,
                false,
                false,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Sets GPS info in the device
        /// </summary>
        /// <param name="gpsParameters">The parameters of the type GPSParameters</param>
        /// <returns>
        ///     The response containing parameters:
        ///     <para>status: if true GPD info was set successfuly on a device</para>
        ///     <para>tx_id: tracking info ID</para>
        /// </returns>
        public Task<Tuple<SetGpsResponse, string>> SetGPSAsync(GPSParameters gpsParameters)
        {
            return GetJsonObjectFromAPIAsync<SetGpsResponse>(gpsParameters,
                R4MEInfrastructureSettings.SetGpsHost,
                HttpMethodType.Get,
                null,
                false,
                false);
        }

        /// <summary>
        ///     Searchs for an asset
        /// </summary>
        /// <param name="tracking">The tracking code</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The object of the type FindAssetResponse</returns>
        public FindAssetResponse FindAsset(string tracking, out string errorString)
        {
            var request = new FindAssetRequest {Tracking = tracking};

            return GetJsonObjectFromAPI<FindAssetResponse>(request, R4MEInfrastructureSettings.AssetTracking,
                HttpMethodType.Get, false, false, out errorString);
        }

        /// <summary>
        ///     Searchs for an asset
        /// </summary>
        /// <param name="tracking">The tracking code</param>
        /// <returns>The object of the type FindAssetResponse</returns>
        public Task<Tuple<FindAssetResponse, string>> FindAssetAsync(string tracking)
        {
            var request = new FindAssetRequest { Tracking = tracking };

            return GetJsonObjectFromAPIAsync<FindAssetResponse>(request, R4MEInfrastructureSettings.AssetTracking,
                HttpMethodType.Get, null, false, false);
        }

        /// <summary>
        ///     Get user locations
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An array of the user locations</returns>
        public UserLocation[] GetUserLocations(GenericParameters parameters, out string errorString)
        {
            var userLocations = GetJsonObjectFromAPI<UserLocation[]>(parameters,
                R4MEInfrastructureSettings.UserLocation,
                HttpMethodType.Get,
                false, false, out errorString);

            return userLocations;
        }

        /// <summary>
        ///     Get user locations
        /// </summary>
        /// <param name="parameters">Query parameters</param>
        /// <returns>An array of the user locations</returns>
        public Task<Tuple<UserLocation[], string>> GetUserLocationsAsync(GenericParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<UserLocation[]>(parameters,
                R4MEInfrastructureSettings.UserLocation,
                HttpMethodType.Get, null, false, false);
        }

        #endregion

        #region Users

        /// <summary>
        ///     Returns the object containing array of the user objects (deprecated)
        /// </summary>
        /// <param name="parameters">Empty GenericParameters object</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The object of the type GetUsersResponse</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.GetTeamMembers instead.")]
        public GetUsersResponse GetUsers(GenericParameters parameters, out string errorString)
        {
            var result = GetJsonObjectFromAPI<GetUsersResponse>(parameters,
                R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Get,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Returns the object containing array of the user objects (deprecated)
        /// </summary>
        /// <param name="parameters">Empty GenericParameters object</param>
        /// <returns>The object of the type GetUsersResponse</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.GetTeamMembersAsync instead.")]
        public Task<Tuple<GetUsersResponse, string>> GetUsersAsync(GenericParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<GetUsersResponse>(parameters,
                R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Creates new sub-user(member) in the user's account (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponseV4</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.CreateTeamMember instead.")]
        public MemberResponseV4 CreateUser(MemberParametersV4 memParams, out string errorString)
        {
            return GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Post, out errorString);
        }

        /// <summary>
        ///     Creates new sub-user(member) in the user's account (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4</param>
        /// <returns>An object of the type MemberResponseV4</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.CreateTeamMemberAsync instead.")]
        public Task<Tuple<MemberResponseV4, string>> CreateUserAsync(MemberParametersV4 memParams)
        {
            return GetJsonObjectFromAPIAsync<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Removes a sub-user(member) from the user's account (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4 containg the parameter member_id</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>True if a member was successfuly removed from the user's account</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.RemoveTeamMember instead.")]
        public bool UserDelete(MemberParametersV4 memParams, out string errorString)
        {
            var response = GetJsonObjectFromAPI<StatusResponse>(
                memParams,
                R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Delete,
                out errorString);

            return response != null && response.Status;
        }

        /// <summary>
        ///     Removes a sub-user(member) from the user's account (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4 containg the parameter member_id</param>
        /// <returns>True if a member was successfuly removed from the user's account</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.RemoveTeamMemberAsync instead.")]
        public async Task<Tuple<bool, string>> UserDeleteAsync(MemberParametersV4 memParams)
        {
            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(
                memParams,
                R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(response.Item1 != null && response.Item1.Status, response.Item2);
        }

        /// <summary>
        ///     Return a user by the parameter member_id (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4 containg the parameter member_id</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponseV4</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.GetTeamMemberById instead.")]
        public MemberResponseV4 GetUserById(MemberParametersV4 memParams, out string errorString)
        {
            return GetJsonObjectFromAPI<MemberResponseV4>(
                memParams,
                R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Return a user by the parameter member_id (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4 containg the parameter member_id</param>
        /// <returns>An object of the type MemberResponseV4</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.GetTeamMemberByIdAsync instead.")]
        public Task<Tuple<MemberResponseV4, string>> GetUserByIdAsync(MemberParametersV4 memParams)
        {
            return GetJsonObjectFromAPIAsync<MemberResponseV4>(
                memParams,
                R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Get,
                false);
        }

        /// <summary>
        ///     Updates a user (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponseV4</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.UpdateTeamMember instead.")]
        public MemberResponseV4 UserUpdate(MemberParametersV4 memParams, out string errorString)
        {
            return GetJsonObjectFromAPI<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost,
                HttpMethodType.Put, out errorString);
        }

        /// <summary>
        ///     Updates a user (deprecated)
        /// </summary>
        /// <param name="memParams">An object of the type MemberParametersV4</param>
        /// <returns>An object of the type MemberResponseV4</returns>
        [Obsolete("The method is obsolete, use the method TeamManagementManagerV5.UpdateTeamMemberAsync instead.")]
        public Task<Tuple<MemberResponseV4, string>> UserUpdateAsync(MemberParametersV4 memParams)
        {
            return GetJsonObjectFromAPIAsync<MemberResponseV4>(memParams, R4MEInfrastructureSettings.GetUsersHost, HttpMethodType.Put);
        }

        /// <summary>
        ///     Authenticates a user in the Route4Me system
        /// </summary>
        /// <param name="memParams">
        ///     An object of the type MemberParameters containing the parameters:
        ///     <para>StrEmail: user email</para>
        ///     <para>StrPassword: user password</para>
        ///     <para>Format: response format</para>
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponse</returns>
        public MemberResponse UserAuthentication(MemberParameters memParams, out string errorString)
        {
            var roParams = new MemberParameters();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("strEmail", memParams.StrEmail),
                new KeyValuePair<string, string>("strPassword", memParams.StrPassword),
                new KeyValuePair<string, string>("format", memParams.Format)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                return GetJsonObjectFromAPI<MemberResponse>(
                    roParams,
                    R4MEInfrastructureSettings.UserAuthentication,
                    HttpMethodType.Post,
                    httpContent,
                    out errorString);
            }
        }

        /// <summary>
        ///     Authenticates a user in the Route4Me system
        /// </summary>
        /// <param name="memParams">
        ///     An object of the type MemberParameters containing the parameters:
        ///     <para>StrEmail: user email</para>
        ///     <para>StrPassword: user password</para>
        ///     <para>Format: response format</para>
        /// </param>
        /// <returns>An object of the type MemberResponse</returns>
        public async Task<Tuple<MemberResponse, string>> UserAuthenticationAsync(MemberParameters memParams)
        {
            var roParams = new MemberParameters();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("strEmail", memParams.StrEmail),
                new KeyValuePair<string, string>("strPassword", memParams.StrPassword),
                new KeyValuePair<string, string>("format", memParams.Format)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                return await GetJsonObjectFromAPIAsync<MemberResponse>(
                    roParams,
                    R4MEInfrastructureSettings.UserAuthentication,
                    HttpMethodType.Post,
                    httpContent,
                    false,
                    false).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Registrates a user in the Route4Me system
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponse</returns>
        public MemberResponse UserRegistration(MemberParameters memParams, out string errorString)
        {
            var roParams = new MemberParameters
            {
                Plan = memParams.Plan,
                MemberType = memParams.MemberType
            };

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("strIndustry", memParams.StrIndustry),
                new KeyValuePair<string, string>("strFirstName", memParams.StrFirstName),
                new KeyValuePair<string, string>("strLastName", memParams.StrLastName),
                new KeyValuePair<string, string>("strEmail", memParams.StrEmail),
                new KeyValuePair<string, string>("format", memParams.Format),
                new KeyValuePair<string, string>("chkTerms", memParams.ChkTerms == 1 ? "1" : "0"),
                new KeyValuePair<string, string>("device_type", memParams.DeviceType),
                new KeyValuePair<string, string>("strPassword_1", memParams.StrPassword_1),
                new KeyValuePair<string, string>("strPassword_2", memParams.StrPassword_2)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                return GetJsonObjectFromAPI<MemberResponse>(roParams, R4MEInfrastructureSettings.UserRegistration,
                    HttpMethodType.Post, httpContent, out errorString);
            }
        }

        /// <summary>
        ///     Registrates a user in the Route4Me system
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters</param>
        /// <returns>An object of the type MemberResponse</returns>
        public async Task<Tuple<MemberResponse, string>> UserRegistrationAsync(MemberParameters memParams)
        {
            var roParams = new MemberParameters
            {
                Plan = memParams.Plan,
                MemberType = memParams.MemberType
            };

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("strIndustry", memParams.StrIndustry),
                new KeyValuePair<string, string>("strFirstName", memParams.StrFirstName),
                new KeyValuePair<string, string>("strLastName", memParams.StrLastName),
                new KeyValuePair<string, string>("strEmail", memParams.StrEmail),
                new KeyValuePair<string, string>("format", memParams.Format),
                new KeyValuePair<string, string>("chkTerms", memParams.ChkTerms == 1 ? "1" : "0"),
                new KeyValuePair<string, string>("device_type", memParams.DeviceType),
                new KeyValuePair<string, string>("strPassword_1", memParams.StrPassword_1),
                new KeyValuePair<string, string>("strPassword_2", memParams.StrPassword_2)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                return await GetJsonObjectFromAPIAsync<MemberResponse>(roParams, R4MEInfrastructureSettings.UserRegistration,
                    HttpMethodType.Post, httpContent, false, false).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Validates user session
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberResponse</returns>
        public MemberResponse ValidateSession(MemberParameters memParams, out string errorString)
        {
            var request = new ValidateSessionRequest
            {
                SessionGuid = memParams.SessionGuid,
                MemberId = memParams.MemberId,
                Format = memParams.Format
            };

            return GetJsonObjectFromAPI<MemberResponse>(request, R4MEInfrastructureSettings.ValidateSession,
                HttpMethodType.Get, false, false, out errorString);
        }

        /// <summary>
        ///     Validates user session
        /// </summary>
        /// <param name="memParams">An object of the type MemberParameters</param>
        /// <returns>An object of the type MemberResponse</returns>
        public Task<Tuple<MemberResponse, string>> ValidateSessionAsync(MemberParameters memParams)
        {
            var request = new ValidateSessionRequest
            {
                SessionGuid = memParams.SessionGuid,
                MemberId = memParams.MemberId,
                Format = memParams.Format
            };

            return GetJsonObjectFromAPIAsync<MemberResponse>(request, R4MEInfrastructureSettings.ValidateSession,
                HttpMethodType.Get, null, false, false);
        }

        /// <summary>
        ///     Creates new user's configuration
        /// </summary>
        /// <param name="confParams">
        ///     An object of the type MemberConfigurationParameters containing the parameters:
        ///     <para>config_key: configuration key</para>
        ///     <para>config_value: configuration value</para>
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public MemberConfigurationResponse CreateNewConfigurationKey(MemberConfigurationParameters confParams,
            out string errorString)
        {
            //confParams.PrepareForSerialization();
            return GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Post, out errorString);
        }

        /// <summary>
        ///     Creates new user's configuration
        /// </summary>
        /// <param name="confParams">
        ///     An object of the type MemberConfigurationParameters containing the parameters:
        ///     <para>config_key: configuration key</para>
        ///     <para>config_value: configuration value</para>
        /// </param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public Task<Tuple<MemberConfigurationResponse, string>> CreateNewConfigurationKeyAsync(MemberConfigurationParameters confParams)
        {
            //confParams.PrepareForSerialization();
            return GetJsonObjectFromAPIAsync<MemberConfigurationResponse>(confParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Post);
        }

        public MemberConfigurationResponse CreateNewConfigurationKey(MemberConfigurationParameters[] confParams,
            out string errorString)
        {
            var genParams = new GenericParameters();

            using (var httpContent = new StringContent(JSON.ToJSON(confParams), Encoding.UTF8, "application/json"))
            {
                var response = GetJsonObjectFromAPI<MemberConfigurationResponse>
                (genParams, R4MEInfrastructureSettings.UserConfiguration,
                    HttpMethodType.Post, httpContent, out errorString);

                return response;
            }
        }

        public async Task<Tuple<MemberConfigurationResponse, string>> CreateNewConfigurationKeyAsync(MemberConfigurationParameters[] confParams)
        {
            var genParams = new GenericParameters();

            using (var httpContent = new StringContent(JSON.ToJSON(confParams), Encoding.UTF8, "application/json"))
            {
                var response = await GetJsonObjectFromAPIAsync<MemberConfigurationResponse>
                (genParams, R4MEInfrastructureSettings.UserConfiguration,
                    HttpMethodType.Post, httpContent, false, false).ConfigureAwait(false);

                return response;
            }
        }

        /// <summary>
        ///     Removes a user's configuration key
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters containing the parameter config_key</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public MemberConfigurationResponse RemoveConfigurationKey(MemberConfigurationParameters confParams,
            out string errorString)
        {
            return GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Delete, out errorString);
        }

        /// <summary>
        ///     Removes a user's configuration key
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters containing the parameter config_key</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public Task<Tuple<MemberConfigurationResponse, string>> RemoveConfigurationKeyAsync(MemberConfigurationParameters confParams)
        {
            return GetJsonObjectFromAPIAsync<MemberConfigurationResponse>(confParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Delete);
        }

        /// <summary>
        ///     Returns configuration data from a user's account.
        /// </summary>
        /// <param name="confParams">
        ///     An object of the type MemberConfigurationParameters (empty or containing the parameter
        ///     config_key)
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public MemberConfigurationDataResponse GetConfigurationData(MemberConfigurationParameters confParams,
            out string errorString)
        {
            var mParams = new GetConfigurationDataRequest();

            if (confParams != null) mParams.ConfigKey = confParams.ConfigKey;

            return GetJsonObjectFromAPI<MemberConfigurationDataResponse>(mParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Get, out errorString);
        }

        /// <summary>
        ///     Returns configuration data from a user's account.
        /// </summary>
        /// <param name="confParams">
        ///     An object of the type MemberConfigurationParameters (empty or containing the parameter
        ///     config_key)
        /// </param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public Task<Tuple<MemberConfigurationDataResponse, string>> GetConfigurationDataAsync(MemberConfigurationParameters confParams)
        {
            var mParams = new GetConfigurationDataRequest();

            if (confParams != null) mParams.ConfigKey = confParams.ConfigKey;

            return GetJsonObjectFromAPIAsync<MemberConfigurationDataResponse>(mParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Get);
        }

        /// <summary>
        ///     Updates a configuration key.
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public MemberConfigurationResponse UpdateConfigurationKey(MemberConfigurationParameters confParams,
            out string errorString)
        {
            return GetJsonObjectFromAPI<MemberConfigurationResponse>(confParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Put, out errorString);
        }

        /// <summary>
        ///     Updates a configuration key.
        /// </summary>
        /// <param name="confParams">An object of the type MemberConfigurationParameters</param>
        /// <returns>An object of the type MemberConfigurationResponse</returns>
        public Task<Tuple<MemberConfigurationResponse, string>> UpdateConfigurationKeyAsync(MemberConfigurationParameters confParams)
        {
            return GetJsonObjectFromAPIAsync<MemberConfigurationResponse>(confParams,
                R4MEInfrastructureSettings.UserConfiguration, HttpMethodType.Put);
        }

        public MemberCapabilities GetMemberCapabilities(string apiKey, out string errorString)
        {
            var parameters = new GenericParameters();

            parameters.ParametersCollection.Add("ApiKey", apiKey);

            var result = GetJsonObjectFromAPI<MemberCapabilities>(parameters,
                R4MEInfrastructureSettings.MemberCapabilities,
                HttpMethodType.Get,
                out errorString);

            return result;
        }

        public Task<Tuple<MemberCapabilities, string>> GetMemberCapabilitiesAsync(string apiKey)
        {
            var parameters = new GenericParameters();

            parameters.ParametersCollection.Add("ApiKey", apiKey);

            return GetJsonObjectFromAPIAsync<MemberCapabilities>(parameters,
                R4MEInfrastructureSettings.MemberCapabilities,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Check if the member with the actualApiKey has commercial member capability.
        /// </summary>
        /// <param name="actualApiKey">Actual API key</param>
        /// <param name="demoApiKey">Demo API key</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>True, if the member has commercial capability</returns>
        public bool MemberHasCommercialCapability(string actualApiKey, string demoApiKey, out string errorString)
        {
            try
            {
                var memberCapabilities = GetMemberCapabilities(actualApiKey, out errorString);

                if (actualApiKey == demoApiKey || memberCapabilities == null) return false;

                var commercialSubscription = memberCapabilities
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(x => x.Name == "Commercial");

                if (commercialSubscription == null) return false;

                if (commercialSubscription.GetValue(memberCapabilities).GetType() != typeof(bool)) return false;

                var isCommercial = (bool) commercialSubscription.GetValue(memberCapabilities);

                if (!isCommercial) return false;

                return true;
            }
            catch (Exception ex)
            {
                errorString = ex.Message;
                return false;
            }
        }

        /// <summary>
        ///     Check if the member with the actualApiKey has commercial member capability.
        /// </summary>
        /// <param name="actualApiKey">Actual API key</param>
        /// <param name="demoApiKey">Demo API key</param>
        /// <returns>True, if the member has commercial capability</returns>
        public async Task<Tuple<bool, string>> MemberHasCommercialCapabilityAsync(string actualApiKey, string demoApiKey)
        {
            try
            {
                var memberCapabilities = await GetMemberCapabilitiesAsync(actualApiKey).ConfigureAwait(false);

                if (actualApiKey == demoApiKey || memberCapabilities.Item1 == null) return new Tuple<bool, string>(false, memberCapabilities.Item2);

                var commercialSubscription = memberCapabilities
                    .GetType()
                    .GetProperties()
                    .FirstOrDefault(x => x.Name == "Commercial");

                if (commercialSubscription == null) return new Tuple<bool, string>(false, memberCapabilities.Item2);

                if (commercialSubscription.GetValue(memberCapabilities).GetType() != typeof(bool)) return new Tuple<bool, string>(false, memberCapabilities.Item2);

                var isCommercial = (bool)commercialSubscription.GetValue(memberCapabilities);

                if (!isCommercial) return new Tuple<bool, string>(false, memberCapabilities.Item2);

                return new Tuple<bool, string>(true, memberCapabilities.Item2);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        #endregion

        #region Address Notes

        /// <summary>
        ///     Returns an array of the address notes
        /// </summary>
        /// <param name="noteParameters">
        ///     An object of the type NoteParameters containing the parameters:
        ///     <para>RouteId: a route ID</para>
        ///     <para>AddressId: a route destination ID</para>
        /// </param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the AddressNote type objects </returns>
        public AddressNote[] GetAddressNotes(NoteParameters noteParameters, out string errorString)
        {
            var addressParameters = new AddressParameters
            {
                RouteId = noteParameters.RouteId,
                RouteDestinationId = noteParameters.AddressId,
                Notes = true
            };

            var address = GetAddress(addressParameters, out errorString);

            return address?.Notes;
        }

        /// <summary>
        ///     Returns an array of the address notes
        /// </summary>
        /// <param name="noteParameters">
        ///     An object of the type NoteParameters containing the parameters:
        ///     <para>RouteId: a route ID</para>
        ///     <para>AddressId: a route destination ID</para>
        /// </param>
        /// <returns>An array of the AddressNote type objects </returns>
        public async Task<Tuple<AddressNote[], string>> GetAddressNotesAsync(NoteParameters noteParameters)
        {
            var addressParameters = new AddressParameters
            {
                RouteId = noteParameters.RouteId,
                RouteDestinationId = noteParameters.AddressId,
                Notes = true
            };

            var address = await GetAddressAsync(addressParameters).ConfigureAwait(false);

            return new Tuple<AddressNote[], string>(address.Item1?.Notes, address.Item2);
        }

        /// <summary>
        ///     Adds a file as an address note to the route destination.
        /// </summary>
        /// <param name="noteParameters">The NoteParameters type object</param>
        /// <param name="noteContents">The note content text</param>
        /// <param name="attachmentFilePath">An attached file path</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An AddressNote type object</returns>
        public AddressNote AddAddressNote(NoteParameters noteParameters, string noteContents, string attachmentFilePath,
            out string errorString)
        {
            var strUpdateType = "unclassified";

            if (!string.IsNullOrEmpty(noteParameters.ActivityType))
                strUpdateType = noteParameters.ActivityType;

            HttpContent httpContent;
            FileStream attachmentFileStream = null;
            StreamContent attachmentStreamContent = null;

            if (attachmentFilePath != null)
            {
                attachmentFileStream = File.OpenRead(attachmentFilePath);
                attachmentStreamContent = new StreamContent(attachmentFileStream);

                httpContent = new MultipartFormDataContent
                {
                    {attachmentStreamContent, "strFilename", Path.GetFileName(attachmentFilePath)},
                    {new StringContent(strUpdateType), "strUpdateType"},
                    {new StringContent(noteContents), "strNoteContents"}
                };
            }
            else
            {
                var keyValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("strUpdateType", strUpdateType),
                    new KeyValuePair<string, string>("strNoteContents", noteContents)
                };

                httpContent = new FormUrlEncodedContent(keyValues);
            }

            var response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
                R4MEInfrastructureSettings.AddRouteNotesHost,
                HttpMethodType.Post,
                httpContent,
                out errorString);

            if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
            if (attachmentFileStream != null) attachmentFileStream.Dispose();

            if (response != null && response.Note == null && response.Status == false)
                errorString = "Note not added";

            return response?.Note;
        }

        /// <summary>
        ///     Adds a file as an address note to the route destination.
        /// </summary>
        /// <param name="noteParameters">The NoteParameters type object</param>
        /// <param name="noteContents">The note content text</param>
        /// <param name="attachmentFilePath">An attached file path</param>
        /// <returns>An AddressNote type object</returns>
        public async Task<Tuple<AddressNote, string>> AddAddressNoteAsync(NoteParameters noteParameters, string noteContents, string attachmentFilePath)
        {
            var strUpdateType = "unclassified";

            if (!string.IsNullOrEmpty(noteParameters.ActivityType))
                strUpdateType = noteParameters.ActivityType;

            HttpContent httpContent;
            FileStream attachmentFileStream = null;
            StreamContent attachmentStreamContent = null;

            if (attachmentFilePath != null)
            {
                attachmentFileStream = File.OpenRead(attachmentFilePath);
                attachmentStreamContent = new StreamContent(attachmentFileStream);

                httpContent = new MultipartFormDataContent
                {
                    {attachmentStreamContent, "strFilename", Path.GetFileName(attachmentFilePath)},
                    {new StringContent(strUpdateType), "strUpdateType"},
                    {new StringContent(noteContents), "strNoteContents"}
                };
            }
            else
            {
                var keyValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("strUpdateType", strUpdateType),
                    new KeyValuePair<string, string>("strNoteContents", noteContents)
                };

                httpContent = new FormUrlEncodedContent(keyValues);
            }

            var response = await GetJsonObjectFromAPIAsync<AddAddressNoteResponse>(noteParameters,
                R4MEInfrastructureSettings.AddRouteNotesHost,
                HttpMethodType.Post,
                httpContent,
                false,
                false).ConfigureAwait(false);

            if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
            if (attachmentFileStream != null) attachmentFileStream.Dispose();

            if (response.Item1 != null && response.Item1.Note == null && response.Item1.Status == false)
                response = new Tuple<AddAddressNoteResponse, string>(response.Item1, "Note not added");

            return new Tuple<AddressNote, string>(response.Item1?.Note, response.Item2);
        }

        /// <summary>
        ///     The method offers ability to send a complex note at once,
        ///     with text content, uploading file, custom notes.
        /// </summary>
        /// <param name="noteParameters">
        ///     The note parameters of the type NoteParameters
        ///     Note: contains form data elemets too
        /// </param>
        /// <param name="errorString">Error string</param>
        /// <returns>Created address note</returns>
        public AddressNote AddAddressNote(NoteParameters noteParameters, out string errorString)
        {
            //HttpContent httpContent = null;
            FileStream attachmentFileStream = null;
            StreamContent attachmentStreamContent = null;

            var multipartFormDataContent = new MultipartFormDataContent();

            if (noteParameters.StrFileName != null)
            {
                attachmentFileStream = File.OpenRead(noteParameters.StrFileName);
                attachmentStreamContent = new StreamContent(attachmentFileStream);
                multipartFormDataContent.Add(attachmentStreamContent, "strFilename",
                    Path.GetFileName(noteParameters.StrFileName));
            }

            multipartFormDataContent.Add(new StringContent(noteParameters.ActivityType), "strUpdateType");
            multipartFormDataContent.Add(new StringContent(noteParameters.StrNoteContents), "strNoteContents");

            if (noteParameters.CustomNoteTypes != null && noteParameters.CustomNoteTypes.Count > 0)
                foreach (var customNote in noteParameters.CustomNoteTypes)
                    multipartFormDataContent.Add(new StringContent(customNote.Value), customNote.Key);

            HttpContent httpContent = multipartFormDataContent;

            var response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
                R4MEInfrastructureSettings.AddRouteNotesHost,
                HttpMethodType.Post,
                httpContent,
                out errorString);


            if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
            if (attachmentFileStream != null) attachmentFileStream.Dispose();

            if (response != null && response.Note == null && response.Status == false)
                errorString = "Note not added";

            return response?.Note;
        }

        /// <summary>
        ///     The method offers ability to send a complex note at once,
        ///     with text content, uploading file, custom notes.
        /// </summary>
        /// <param name="noteParameters">
        ///     The note parameters of the type NoteParameters
        ///     Note: contains form data elemets too
        /// </param>
        /// <returns>Created address note</returns>
        public async Task<Tuple<AddressNote, string>> AddAddressNoteAsync(NoteParameters noteParameters)
        {
            //HttpContent httpContent = null;
            FileStream attachmentFileStream = null;
            StreamContent attachmentStreamContent = null;

            var multipartFormDataContent = new MultipartFormDataContent();

            if (noteParameters.StrFileName != null)
            {
                attachmentFileStream = File.OpenRead(noteParameters.StrFileName);
                attachmentStreamContent = new StreamContent(attachmentFileStream);
                multipartFormDataContent.Add(attachmentStreamContent, "strFilename",
                    Path.GetFileName(noteParameters.StrFileName));
            }

            multipartFormDataContent.Add(new StringContent(noteParameters.ActivityType), "strUpdateType");
            multipartFormDataContent.Add(new StringContent(noteParameters.StrNoteContents), "strNoteContents");

            if (noteParameters.CustomNoteTypes != null && noteParameters.CustomNoteTypes.Count > 0)
                foreach (var customNote in noteParameters.CustomNoteTypes)
                    multipartFormDataContent.Add(new StringContent(customNote.Value), customNote.Key);

            HttpContent httpContent = multipartFormDataContent;

            var response = await GetJsonObjectFromAPIAsync<AddAddressNoteResponse>(noteParameters,
                R4MEInfrastructureSettings.AddRouteNotesHost,
                HttpMethodType.Post,
                httpContent,
                false,
                false).ConfigureAwait(false);


            if (attachmentStreamContent != null) attachmentStreamContent.Dispose();
            if (attachmentFileStream != null) attachmentFileStream.Dispose();

            if (response.Item1 != null && response.Item1.Note == null && response.Item1.Status == false)
                response = new Tuple<AddAddressNoteResponse, string>(response.Item1, "Note not added");

            return new Tuple<AddressNote, string>(response.Item1?.Note, response.Item2);
        }

        /// <summary>
        ///     Adds an address note to the route destination.
        /// </summary>
        /// <param name="noteParameters">The NoteParameters type object</param>
        /// <param name="noteContents">The note content text</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An AddressNote type object</returns>
        public AddressNote AddAddressNote(NoteParameters noteParameters, string noteContents, out string errorString)
        {
            return AddAddressNote(noteParameters, noteContents, null, out errorString);
        }

        /// <summary>
        ///     Adds an address note to the route destination.
        /// </summary>
        /// <param name="noteParameters">The NoteParameters type object</param>
        /// <param name="noteContents">The note content text</param>
        /// <returns>An AddressNote type object</returns>
        public Task<Tuple<AddressNote, string>> AddAddressNoteAsync(NoteParameters noteParameters, string noteContents)
        {
            return AddAddressNoteAsync(noteParameters, noteContents, null);
        }

        /// <summary>
        ///     Adds custom note type to a route destination.
        /// </summary>
        /// <param name="customType">A custom note type</param>
        /// <param name="values">Array of the string type notes</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>If succefful, returns non-negative affected number, otherwise: -1</returns>
        public object AddCustomNoteType(string customType, string[] values, out string errorString)
        {
            var request = new AddCustomNoteTypeRequest
            {
                Type = customType,
                Values = values
            };

            var response = GetJsonObjectFromAPI<AddCustomNoteTypeResponse>(request,
                R4MEInfrastructureSettings.CustomNoteType,
                HttpMethodType.Post,
                out errorString);

            return response != null ? response.Result == "OK" ? response.Affected : -1 : (object) errorString;
        }

        /// <summary>
        ///     Adds custom note type to a route destination.
        /// </summary>
        /// <param name="customType">A custom note type</param>
        /// <param name="values">Array of the string type notes</param>
        /// <returns>If succefful, returns non-negative affected number, otherwise: -1</returns>
        public async Task<Tuple<object, string>> AddCustomNoteTypeAsync(string customType, string[] values)
        {
            var request = new AddCustomNoteTypeRequest
            {
                Type = customType,
                Values = values
            };

            var response = await GetJsonObjectFromAPIAsync<AddCustomNoteTypeResponse>(request,
                R4MEInfrastructureSettings.CustomNoteType,
                HttpMethodType.Post).ConfigureAwait(false);

            return new Tuple<object, string>(response.Item1 != null ? response.Item1.Result == "OK" ? response.Item1.Affected : -1 : (object)response.Item2, response.Item2);
        }

        /// <summary>
        ///     Removes a custom note type from a user's account.
        /// </summary>
        /// <param name="customNoteId">The custom note type ID</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>if succefful, returns non-negative affected number, otherwise: -1</returns>
        public object RemoveCustomNoteType(long customNoteId, out string errorString)
        {
            var request = new RemoveCustomNoteTypeRequest {Id = customNoteId};

            var response = GetJsonObjectFromAPI<AddCustomNoteTypeResponse>(request,
                R4MEInfrastructureSettings.CustomNoteType,
                HttpMethodType.Delete,
                out errorString);

            return response != null ? response.Result == "OK" ? response.Affected : -1 : (object) errorString;
        }

        /// <summary>
        ///     Removes a custom note type from a user's account.
        /// </summary>
        /// <param name="customNoteId">The custom note type ID</param>
        /// <returns>if succefful, returns non-negative affected number, otherwise: -1</returns>
        public async Task<Tuple<object, string>> RemoveCustomNoteTypeAsync(int customNoteId)
        {
            var request = new RemoveCustomNoteTypeRequest { Id = customNoteId };

            var response = await GetJsonObjectFromAPIAsync<AddCustomNoteTypeResponse>(request,
                R4MEInfrastructureSettings.CustomNoteType,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<object, string>(response.Item1 != null ? response.Item1.Result == "OK" ? response.Item1.Affected : -1 : (object)response.Item2, response.Item2);
        }

        /// <summary>
        ///     Returns an array of the custom note types
        /// </summary>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the custom note types</returns>
        public object GetAllCustomNoteTypes(out string errorString)
        {
            var request = new GenericParameters();

            var response = GetJsonObjectFromAPI<CustomNoteType[]>(request,
                R4MEInfrastructureSettings.CustomNoteType,
                HttpMethodType.Get,
                out errorString);
            return response ?? (object) errorString;
        }

        /// <summary>
        ///     Returns an array of the custom note types
        /// </summary>
        /// <returns>An array of the custom note types</returns>
        public async Task<Tuple<object, string>> GetAllCustomNoteTypesAsync()
        {
            var request = new GenericParameters();

            var response = await GetJsonObjectFromAPIAsync<CustomNoteType[]>(request,
                R4MEInfrastructureSettings.CustomNoteType,
                HttpMethodType.Get).ConfigureAwait(false);
            return  new Tuple<object, string>(response.Item1 ?? (object)response.Item2, response.Item2);
        }

        /// <summary>
        ///     Adds a custom note to aroute
        /// </summary>
        /// <param name="noteParameters">A NoteParameters type object</param>
        /// <param name="customNotes">The Dictionary<string, string> type object</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The AddAddressNoteResponse type object</returns>
        public object AddCustomNoteToRoute(NoteParameters noteParameters, Dictionary<string, string> customNotes,
            out string errorString)
        {
            var keyValues = new List<KeyValuePair<string, string>>();

            customNotes.ForEach(kv1 => { keyValues.Add(new KeyValuePair<string, string>(kv1.Key, kv1.Value)); });

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = GetJsonObjectFromAPI<AddAddressNoteResponse>(noteParameters,
                    R4MEInfrastructureSettings.AddRouteNotesHost,
                    HttpMethodType.Post,
                    httpContent,
                    out errorString);

                return response == null
                    ? errorString
                    : response.GetType() != typeof(AddAddressNoteResponse)
                        ? "Can not add custom note to the route"
                        : response.Status
                            ? (object) response.Note
                            : "Can not add custom note to the route";
            }
        }

        /// <summary>
        ///     Adds a custom note to aroute
        /// </summary>
        /// <param name="noteParameters">A NoteParameters type object</param>
        /// <param name="customNotes">The Dictionary<string, string> type object</param>
        /// <returns>The AddAddressNoteResponse type object</returns>
        public async Task<Tuple<object, string>> AddCustomNoteToRouteAsync(NoteParameters noteParameters, Dictionary<string, string> customNotes)
        {
            var keyValues = new List<KeyValuePair<string, string>>();

            customNotes.ForEach(kv1 => { keyValues.Add(new KeyValuePair<string, string>(kv1.Key, kv1.Value)); });

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = await GetJsonObjectFromAPIAsync<AddAddressNoteResponse>(noteParameters,
                    R4MEInfrastructureSettings.AddRouteNotesHost,
                    HttpMethodType.Post,
                    httpContent,
                    false,
                    false).ConfigureAwait(false);

                return new Tuple<object, string>(response.Item1 == null
                    ? response.Item2
                    : response.GetType() != typeof(AddAddressNoteResponse)
                        ? "Can not add custom note to the route"
                        : response.Item1.Status
                            ? (object)response.Item1.Note
                            : "Can not add custom note to the route", response.Item2);
            }
        }

        #endregion

        #region Activities

        /// <summary>
        ///     Returns the activity feed
        /// </summary>
        /// <param name="activityParameters"> Input parameters </param>
        /// <param name="errorString">Error message text</param>
        /// <returns> List of the Activity type objects </returns>
        public Activity[] GetActivityFeed(ActivityParameters activityParameters, out string errorString)
        {
            var response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
                R4MEInfrastructureSettings.ActivityFeedHost,
                HttpMethodType.Get,
                out errorString);

            return response?.Results;
        }

        /// <summary>
        ///     Returns the activity feed
        /// </summary>
        /// <param name="activityParameters"> Input parameters </param>
        /// <returns> List of the Activity type objects </returns>
        public async Task<Tuple<Activity[], string>> GetActivityFeedAsync(ActivityParameters activityParameters)
        {
            var response = await GetJsonObjectFromAPIAsync<GetActivitiesResponse>(activityParameters,
                R4MEInfrastructureSettings.ActivityFeedHost,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<Activity[], string>(response.Item1?.Results, response.Item2);
        }

        /// <summary>
        ///     Get all the activities limited by query parameters.
        /// </summary>
        /// <param name="activityParameters">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An array of the activities.</returns>
        public Activity[] GetActivities(ActivityParameters activityParameters, out string errorString)
        {
            var response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
                R4MEInfrastructureSettings.GetActivitiesHost,
                HttpMethodType.Get,
                out errorString);

            return response?.Results;
        }

        /// <summary>
        ///     Get all the activities limited by query parameters.
        /// </summary>
        /// <param name="activityParameters">Query parameters</param>
        /// <returns>An array of the activities.</returns>
        public async Task<Tuple<Activity[], string>> GetActivitiesAsync(ActivityParameters activityParameters)
        {
            var response = await GetJsonObjectFromAPIAsync<GetActivitiesResponse>(activityParameters,
                R4MEInfrastructureSettings.GetActivitiesHost,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<Activity[], string>(response.Item1?.Results, response.Item2);
        }

        /// <summary>
        ///     Creates a user's activity by sending a custom message to the activity stream.
        /// </summary>
        /// <param name="activity"> The Activity type object to add </param>
        /// <param name="errorString"> Error message text </param>
        /// <returns> True if a custom message logged successfuly </returns>
        public bool LogCustomActivity(Activity activity, out string errorString)
        {
            activity.PrepareForSerialization();

            var response = GetJsonObjectFromAPI<StatusResponse>(activity,
                R4MEInfrastructureSettings.ActivityFeedHost,
                HttpMethodType.Post,
                out errorString);

            return response != null && response.Status;
        }

        /// <summary>
        ///     Creates a user's activity by sending a custom message to the activity stream.
        /// </summary>
        /// <param name="activity"> The Activity type object to add </param>
        /// <returns> True if a custom message logged successfuly </returns>
        public async Task<Tuple<bool, string>> LogCustomActivityAsync(Activity activity)
        {
            activity.PrepareForSerialization();

            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(activity,
                R4MEInfrastructureSettings.ActivityFeedHost,
                HttpMethodType.Post).ConfigureAwait(false);

            return new Tuple<bool, string>(response.Item1 != null && response.Item1.Status, response.Item2);
        }

        /// <summary>
        ///     Returns the array of the Activity type objects as a response.
        /// </summary>
        /// <param name="activityParameters">The ActivityParameters type objects as an input parameters</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>An array of the Activity type objects</returns>
        public Activity[] GetAnalytics(ActivityParameters activityParameters, out string errorString)
        {
            var response = GetJsonObjectFromAPI<GetActivitiesResponse>(activityParameters,
                R4MEInfrastructureSettings.ActivityFeedHost,
                HttpMethodType.Get,
                out errorString);

            return response?.Results;
        }

        /// <summary>
        ///     Returns the array of the Activity type objects as a response.
        /// </summary>
        /// <param name="activityParameters">The ActivityParameters type objects as an input parameters</param>
        /// <returns>An array of the Activity type objects</returns>
        public async Task<Tuple<Activity[], string>> GetAnalyticsAsync(ActivityParameters activityParameters)
        {
            var response = await GetJsonObjectFromAPIAsync<GetActivitiesResponse>(activityParameters,
                R4MEInfrastructureSettings.ActivityFeedHost,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<Activity[], string>(response.Item1?.Results, response.Item2);
        }

        #endregion

        #region Destinations

        /// <summary>
        ///     Returns an Address type object as the response
        /// </summary>
        /// <param name="addressParameters">An AddressParameters type object containing the route ID as the input parameter.</param>
        /// <param name="errorString">Error message text</param>
        /// <returns>The Address type object</returns>
        public Address GetAddress(AddressParameters addressParameters, out string errorString)
        {
            return GetJsonObjectFromAPI<Address>(addressParameters,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Returns an Address type object as the response
        /// </summary>
        /// <param name="addressParameters">An AddressParameters type object containing the route ID as the input parameter.</param>
        /// <returns>The Address type object</returns>
        public Task<Tuple<Address, string>> GetAddressAsync(AddressParameters addressParameters)
        {
            return GetJsonObjectFromAPIAsync<Address>(addressParameters,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Adds address(es) into a route.
        /// </summary>
        /// <param name="routeId"> The route ID </param>
        /// <param name="addresses"> Valid array of the Address type objects. </param>
        /// <param name="optimalPosition"> If true, an address will be inserted at optimal position of a route </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> An array of the IDs of added addresses </returns>
        public int[] AddRouteDestinations(string routeId, Address[] addresses, bool optimalPosition,
            out string errorString)
        {
            var request = new AddRouteDestinationRequest
            {
                RouteId = routeId,
                Addresses = addresses,
                OptimalPosition = optimalPosition
            };

            var response = GetJsonObjectFromAPI<DataObject>(request,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, null, false, true,
                out errorString);

            var arrDestinationIds = new List<int>();

            if (response != null && response.Addresses != null)
            {
                addresses.ForEach(addressNew =>
                {
                    response.Addresses.Where(addressResp =>
                            (string.IsNullOrEmpty(addressNew.AddressString) || addressNew.AddressString.Equals(addressResp.AddressString, StringComparison.InvariantCultureIgnoreCase)) &&
                            Math.Abs(addressResp.Latitude - addressNew.Latitude) < 0.0001 &&
                            Math.Abs(addressResp.Longitude - addressNew.Longitude) < 0.0001 &&
                            addressResp.RouteDestinationId != null)
                        .ForEach(addrResp => { arrDestinationIds.Add((int)addrResp.RouteDestinationId); });
                });
            }

            return arrDestinationIds.ToArray();
        }

        /// <summary>
        ///     Adds address(es) into a route.
        /// </summary>
        /// <param name="routeId"> The route ID </param>
        /// <param name="addresses"> Valid array of the Address type objects. </param>
        /// <param name="optimalPosition"> If true, an address will be inserted at optimal position of a route </param>
        /// <returns> An array of the IDs of added addresses </returns>
        public async Task<Tuple<int[], string>> AddRouteDestinationsAsync(string routeId, Address[] addresses, bool optimalPosition)
        {
            var request = new AddRouteDestinationRequest
            {
                RouteId = routeId,
                Addresses = addresses,
                OptimalPosition = optimalPosition
            };

            var response = await GetJsonObjectFromAPIAsync<DataObject>(request,
                R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, null, false, true).ConfigureAwait(false);

            var arrDestinationIds = new List<int>();

            if (response.Item1 != null && response.Item1.Addresses != null)
            {
                addresses.ForEach(addressNew =>
                {
                    response.Item1.Addresses.Where(addressResp =>
                            (string.IsNullOrEmpty(addressNew.AddressString) || addressNew.AddressString.Equals(addressResp.AddressString, StringComparison.InvariantCultureIgnoreCase)) &&
                            Math.Abs(addressResp.Latitude - addressNew.Latitude) < 0.0001 &&
                            Math.Abs(addressResp.Longitude - addressNew.Longitude) < 0.0001 &&
                            addressResp.RouteDestinationId != null)
                        .ForEach(addrResp => { arrDestinationIds.Add((int)addrResp.RouteDestinationId); });
                });
            }

            return new Tuple<int[], string>(arrDestinationIds.ToArray(), response.Item2);
        }

        /// <summary>
        ///     Adds the address(es) into a route.
        /// </summary>
        /// <param name="routeId"> The route ID </param>
        /// <param name="addresses"> Valid array of the Address type objects. </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>An array of the IDs of added addresses </returns>
        public int[] AddRouteDestinations(string routeId, Address[] addresses, out string errorString)
        {
            return AddRouteDestinations(routeId, addresses, true, out errorString);
        }

        /// <summary>
        ///     Adds the address(es) into a route.
        /// </summary>
        /// <param name="routeId"> The route ID </param>
        /// <param name="addresses"> Valid array of the Address type objects. </param>
        /// <returns>An array of the IDs of added addresses </returns>
        public Task<Tuple<int[], string>> AddRouteDestinationsAsync(string routeId, Address[] addresses)
        {
            return AddRouteDestinationsAsync(routeId, addresses, true);
        }

        /// <summary>
        ///     Add specified destinations to an optimization
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An array of the added address IDs</returns>
        public long?[] AddOptimizationDestinations(string optimizationId, Address[] addresses, out string errorString)
        {
            var request = new AddRouteDestinationRequest
            {
                OptimizationProblemId = optimizationId,
                Addresses = addresses
            };

            var addressesList = addresses.Select(x => x.AddressString).ToList();

            var dataObject = GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Put, null, false, true, out errorString);

            return dataObject?.Addresses?.Where(x => addressesList.Contains(x.AddressString))
                .Select(y => y.RouteDestinationId).ToArray();
        }

        /// <summary>
        ///     Add specified destinations to an optimization
        /// </summary>
        /// <param name="optimizationId">Optimization ID</param>
        /// <param name="addresses">An array of the addresses</param>
        /// <returns>An array of the added address IDs</returns>
        public async Task<Tuple<long?[], string>> AddOptimizationDestinationsAsync(string optimizationId, Address[] addresses)
        {
            var request = new AddRouteDestinationRequest
            {
                OptimizationProblemId = optimizationId,
                Addresses = addresses
            };

            var addressesList = addresses.Select(x => x.AddressString).ToList();

            var dataObject = await GetJsonObjectFromAPIAsync<DataObject>(request, R4MEInfrastructureSettings.ApiHost,
                HttpMethodType.Put, null, false, true).ConfigureAwait(false);

            return new Tuple<long?[], string>(dataObject.Item1?.Addresses?.Where(x => addressesList.Contains(x.AddressString))
                .Select(y => y.RouteDestinationId).ToArray(), dataObject.Item2);
        }

        /// <summary>
        ///     Removes a route dstination from a route
        /// </summary>
        /// <param name="routeId">The route ID</param>
        /// <param name="destinationId">The route destination ID</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>True if a destination removing finished successfully</returns>
        public bool RemoveRouteDestination(string routeId, int destinationId, out string errorString)
        {
            var request = new RemoveRouteDestinationRequest
            {
                RouteId = routeId,
                RouteDestinationId = destinationId
            };

            var response = GetJsonObjectFromAPI<RemoveRouteDestinationResponse>(request,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Delete,
                out errorString);

            return response != null && response.Deleted;
        }

        /// <summary>
        ///     Removes a route dstination from a route
        /// </summary>
        /// <param name="routeId">The route ID</param>
        /// <param name="destinationId">The route destination ID</param>
        /// <returns>True if a destination removing finished successfully</returns>
        public async Task<Tuple<bool, string>> RemoveRouteDestinationAsync(string routeId, int destinationId)
        {
            var request = new RemoveRouteDestinationRequest
            {
                RouteId = routeId,
                RouteDestinationId = destinationId
            };

            var response = await GetJsonObjectFromAPIAsync<RemoveRouteDestinationResponse>(request,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(response.Item1 != null && response.Item1.Deleted, response.Item2);
        }

        /// <summary>
        ///     Moves a route destination to other route.
        /// </summary>
        /// <param name="toRouteId">The destination route id</param>
        /// <param name="routeDestinationId">The route destiantion ID to be moved</param>
        /// <param name="afterDestinationId">The route destination ID after which will be inserted the moved destination </param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Ture if a destination was moved uccessfully</returns>
        public bool MoveDestinationToRoute(string toRouteId, long routeDestinationId, int afterDestinationId,
            out string errorString)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("to_route_id", toRouteId),
                new KeyValuePair<string, string>("route_destination_id", Convert.ToString(routeDestinationId)),
                new KeyValuePair<string, string>("after_destination_id", Convert.ToString(afterDestinationId))
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = GetJsonObjectFromAPI<MoveDestinationToRouteResponse>(new GenericParameters(),
                    R4MEInfrastructureSettings.MoveRouteDestination,
                    HttpMethodType.Post,
                    httpContent,
                    out errorString);

                if (response.Error != null) errorString = response.Error;

                return response.Success;
            }
        }

        /// <summary>
        ///     Moves a route destination to other route.
        /// </summary>
        /// <param name="toRouteId">The destination route id</param>
        /// <param name="routeDestinationId">The route destiantion ID to be moved</param>
        /// <param name="afterDestinationId">The route destination ID after which will be inserted the moved destination </param>
        /// <returns>Ture if a destination was moved uccessfully</returns>
        public async Task<Tuple<bool, string>> MoveDestinationToRouteAsync(string toRouteId, long routeDestinationId, int afterDestinationId)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("to_route_id", toRouteId),
                new KeyValuePair<string, string>("route_destination_id", Convert.ToString(routeDestinationId)),
                new KeyValuePair<string, string>("after_destination_id", Convert.ToString(afterDestinationId))
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = await GetJsonObjectFromAPIAsync<MoveDestinationToRouteResponse>(new GenericParameters(),
                    R4MEInfrastructureSettings.MoveRouteDestination,
                    HttpMethodType.Post,
                    httpContent,
                    false,
                    false).ConfigureAwait(false);

                if (response.Item1.Error != null) response = new Tuple<MoveDestinationToRouteResponse, string>(response.Item1, response.Item1.Error);

                return new Tuple<bool, string>(response.Item1.Success, response.Item2);
            }
        }

        /// <summary>
        ///     Marks an address as visited
        /// </summary>
        /// <param name="aParams">
        ///     An AddressParameters type object containing the route ID and route_destination_id as the input
        ///     parameters
        /// </param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Number of the marked addresses</returns>
        public int MarkAddressVisited(AddressParameters aParams, out string errorString)
        {
            var response = GetJsonObjectFromAPI<string>(aParams, R4MEInfrastructureSettings.MarkAddressVisited,
                HttpMethodType.Get, out errorString);

            return int.TryParse(response, out _) ? Convert.ToInt32(response) : 0;
        }

        /// <summary>
        ///     Marks an address as visited
        /// </summary>
        /// <param name="aParams">
        ///     An AddressParameters type object containing the route ID and route_destination_id as the input
        ///     parameters
        /// </param>
        /// <returns>Number of the marked addresses</returns>
        public async Task<Tuple<int, string>> MarkAddressVisitedAsync(AddressParameters aParams)
        {
            var response = await GetJsonObjectFromAPIAsync<string>(aParams, R4MEInfrastructureSettings.MarkAddressVisited,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<int, string>(int.TryParse(response.Item1, out _) ? Convert.ToInt32(response.Item1) : 0, response.Item2);
        }

        /// <summary>
        ///     Marks an address as departed.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Number of the marked addresses</returns>
        public int MarkAddressDeparted(AddressParameters aParams, out string errorString)
        {
            var response = GetJsonObjectFromAPI<MarkAddressDepartedResponse>(
                aParams,
                R4MEInfrastructureSettings.MarkAddressDeparted,
                HttpMethodType.Get, out errorString);

            return response != null ? response.Status ? 1 : 0 : 0;
        }

        /// <summary>
        ///     Marks an address as departed.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <returns>Number of the marked addresses</returns>
        public async Task<Tuple<int, string>> MarkAddressDepartedAsync(AddressParameters aParams)
        {
            var response = await GetJsonObjectFromAPIAsync<MarkAddressDepartedResponse>(
                aParams,
                R4MEInfrastructureSettings.MarkAddressDeparted,
                HttpMethodType.Get).ConfigureAwait(false);

            return new Tuple<int, string>(response.Item1 != null ? response.Item1.Status ? 1 : 0 : 0, response.Item2);
        }

        /// <summary>
        ///     Marks an address as marked as visited.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An Address type object</returns>
        public Address MarkAddressAsMarkedAsVisited(AddressParameters aParams, out string errorString)
        {
            return GetJsonObjectFromAPI<Address>(
                aParams,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Marks an address as marked as visited.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <returns>An Address type object</returns>
        public Task<Tuple<Address, string>> MarkAddressAsMarkedAsVisitedAsync(AddressParameters aParams)
        {
            return GetJsonObjectFromAPIAsync<Address>(
                aParams,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Put);
        }

        /// <summary>
        ///     Marks an address as marked as departed.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An Address type object</returns>
        public Address MarkAddressAsMarkedAsDeparted(AddressParameters aParams, out string errorString)
        {
            return GetJsonObjectFromAPI<Address>(
                aParams,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Marks an address as marked as departed.
        /// </summary>
        /// <param name="aParams">An AddressParameters type object as the input parameters</param>
        /// <returns>An Address type object</returns>
        public Task<Tuple<Address, string>> MarkAddressAsMarkedAsDepartedAsync(AddressParameters aParams)
        {
            return GetJsonObjectFromAPIAsync<Address>(
                aParams,
                R4MEInfrastructureSettings.GetAddress,
                HttpMethodType.Put);
        }

        #endregion

        #region Address Book

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParameters">
        ///     >An AddressParameters type object as the input parameters containg the parameters:
        ///     Offset, Limit
        /// </param>
        /// <param name="total">out: Number of the returned contacts</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The array of the address book contacts</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.GetAddressBookContacts instead.")]
        public AddressBookContact[] GetAddressBookContacts(AddressBookParameters addressBookParameters, out uint total,
            out string errorString)
        {
            var response = GetJsonObjectFromAPI<GetAddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Get,
                out errorString);

            total = response?.Total ?? 0;

            return response?.Results;
        }

        /// <summary>
        ///     Returns address book contacts
        /// </summary>
        /// <param name="addressBookParameters">
        ///     >An AddressParameters type object as the input parameters containg the parameters:
        ///     Offset, Limit
        /// </param>
        /// <returns>The array of the address book contacts</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.GetAddressBookContactsAsync instead.")]
        public async Task<Tuple<AddressBookContact[], uint, string>> GetAddressBookContactsAsync(AddressBookParameters addressBookParameters)
        {
            var response = await GetJsonObjectFromAPIAsync<GetAddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Get).ConfigureAwait(false);

            var total = response.Item1?.Total ?? 0;

            return new Tuple<AddressBookContact[], uint, string>(response.Item1?.Results, total, response.Item2);
        }

        /// <summary>
        ///     Returns an address book contact
        /// </summary>
        /// <param name="addressBookParameters">
        ///     An AddressParameters type object as the input parameter
        ///     containing the parameter AddressId (comma-delimited list of the address IDs)
        /// </param>
        /// <param name="total">out: Number of the returned contacts</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The array of the address book contacts</returns>
        public AddressBookContact[] GetAddressBookLocation(AddressBookParameters addressBookParameters, out uint total,
            out string errorString)
        {
            if (addressBookParameters.AddressId != null && !addressBookParameters.AddressId.Contains(","))
                addressBookParameters.AddressId += "," + addressBookParameters.AddressId;

            var response = GetJsonObjectFromAPI<GetAddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Get,
                false,
                false,
                out errorString);

            total = response?.Total ?? 0;

            return response?.Results;
        }

        /// <summary>
        ///     Returns an address book contact
        /// </summary>
        /// <param name="addressBookParameters">
        ///     An AddressParameters type object as the input parameter
        ///     containing the parameter AddressId (comma-delimited list of the address IDs)
        /// </param>
        /// <returns>The array of the address book contacts</returns>
        public async Task<Tuple<AddressBookContact[], uint, string>> GetAddressBookLocationAsync(AddressBookParameters addressBookParameters)
        {
            if (addressBookParameters.AddressId != null && !addressBookParameters.AddressId.Contains(","))
                addressBookParameters.AddressId += "," + addressBookParameters.AddressId;

            var response = await GetJsonObjectFromAPIAsync<GetAddressBookContactsResponse>(addressBookParameters,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Get,
                null,
                false,
                true).ConfigureAwait(false);

            var total = response.Item1?.Total ?? 0;

            return new Tuple<AddressBookContact[], uint, string>(response.Item1?.Results, total, response.Item2);
        }

        /// <summary>
        ///     Searches for the address book locations
        /// </summary>
        /// <param name="addressBookParameters">An AddressParameters type object as the input parameter</param>
        /// <param name="contactsFromObjects">out: Contacts</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>List of the selected fields values</returns>
        public SearchAddressBookLocationResponse SearchAddressBookLocation(AddressBookParameters addressBookParameters,
            out List<AddressBookContact> contactsFromObjects, out string errorString)
        {
            if (addressBookParameters.Fields == null)
            {
                errorString = "Fields property should be specified.";
                contactsFromObjects = null;
                return null;
            }

            var request = new SearchAddressBookLocationRequest();

            contactsFromObjects = new List<AddressBookContact>();

            if (addressBookParameters.AddressId != null) request.AddressId = addressBookParameters.AddressId;
            if (addressBookParameters.Query != null) request.Query = addressBookParameters.Query;
            request.Fields = addressBookParameters.Fields;
            if (addressBookParameters.Offset != null)
                request.Offset = addressBookParameters.Offset >= 0 ? (int) addressBookParameters.Offset : 0;
            if (addressBookParameters.Limit != null)
                request.Limit = addressBookParameters.Limit >= 0 ? (int) addressBookParameters.Limit : 0;

            var response = GetJsonObjectFromAPI<SearchAddressBookLocationResponse>(request,
                R4MEInfrastructureSettings.AddressBook, HttpMethodType.Get, false, true, out var errorString0);

            if (response != null && response.Total > 0)
            {
                var orderedPropertyNames =
                    R4MeUtils.OrderPropertiesByPosition<AddressBookContact>(response.Fields.ToList(), out errorString);

                foreach (var contactObjects in response.Results)
                {
                    var contactFromObject = new AddressBookContact();
                    foreach (var propertyName in orderedPropertyNames)
                    {
                        var value = contactObjects[orderedPropertyNames.IndexOf(propertyName)];

                        var valueType = value != null ? value.GetType().Name : "";

                        var propInfo = typeof(AddressBookContact).GetProperty(propertyName);
                        //Console.WriteLine(valueType);

                        switch (propertyName)
                        {
                            case "address_custom_data":
                                //var customData = R4MeUtils.ToDictionary<string>(value);
                                var customData =
                                    R4MeUtils.ToObject<Dictionary<string, string>>(value, out var errorString1);
                                if (errorString1 == "")
                                    propInfo.SetValue(contactFromObject, customData);
                                else
                                    propInfo.SetValue(contactFromObject,
                                        new Dictionary<string, string> {{"<WRONG DATA>", "<WRONG DATA>"}});
                                break;
                            case "schedule":
                                var schedules = R4MeUtils.ToObject<Schedule[]>(value, out var errorString2);
                                if (errorString2 == "")
                                    propInfo.SetValue(contactFromObject, schedules);
                                else
                                    propInfo.SetValue(contactFromObject, null);
                                break;
                            case "schedule_blacklist":
                                var scheduleBlackList = R4MeUtils.ToObject<string[]>(value, out var errorString3);
                                if (errorString3 == "")
                                    propInfo.SetValue(contactFromObject, scheduleBlackList);
                                else
                                    propInfo.SetValue(contactFromObject, new[] {"<WRONG DATA>"});
                                break;
                            default:
                                var convertedValue = valueType != ""
                                    ? R4MeUtils.ConvertObjectToPropertyType(value, propInfo)
                                    : value;
                                propInfo.SetValue(contactFromObject, convertedValue);
                                break;
                        }
                    }

                    contactsFromObjects.Add(contactFromObject);
                }
            }
            else
            {
                errorString = errorString0;
            }

            return response;
        }

        /// <summary>
        ///     Searches for the address book locations
        /// </summary>
        /// <param name="addressBookParameters">An AddressParameters type object as the input parameter</param>
        /// <returns>List of the selected fields values</returns>
        public async Task<Tuple<SearchAddressBookLocationResponse, List<AddressBookContact>, string>> SearchAddressBookLocationAsync(AddressBookParameters addressBookParameters)
        {
            if (addressBookParameters.Fields == null)
            {
                return new Tuple<SearchAddressBookLocationResponse, List<AddressBookContact>, string>(null, null, "Fields property should be specified.");
            }

            var request = new SearchAddressBookLocationRequest();

            var contactsFromObjects = new List<AddressBookContact>();

            if (addressBookParameters.AddressId != null) request.AddressId = addressBookParameters.AddressId;
            if (addressBookParameters.Query != null) request.Query = addressBookParameters.Query;
            request.Fields = addressBookParameters.Fields;
            if (addressBookParameters.Offset != null)
                request.Offset = addressBookParameters.Offset >= 0 ? (int)addressBookParameters.Offset : 0;
            if (addressBookParameters.Limit != null)
                request.Limit = addressBookParameters.Limit >= 0 ? (int)addressBookParameters.Limit : 0;

            var response = await GetJsonObjectFromAPIAsync<SearchAddressBookLocationResponse>(request,
                R4MEInfrastructureSettings.AddressBook, HttpMethodType.Get, null, false, true).ConfigureAwait(false);

            string errorString;
            if (response.Item1 != null && response.Item1.Total > 0)
            {
                var orderedPropertyNames =
                    R4MeUtils.OrderPropertiesByPosition<AddressBookContact>(response.Item1.Fields.ToList(), out errorString);

                foreach (var contactObjects in response.Item1.Results)
                {
                    var contactFromObject = new AddressBookContact();
                    foreach (var propertyName in orderedPropertyNames)
                    {
                        var value = contactObjects[orderedPropertyNames.IndexOf(propertyName)];

                        var valueType = value != null ? value.GetType().Name : "";

                        var propInfo = typeof(AddressBookContact).GetProperty(propertyName);
                        //Console.WriteLine(valueType);

                        switch (propertyName)
                        {
                            case "address_custom_data":
                                //var customData = R4MeUtils.ToDictionary<string>(value);
                                var customData =
                                    R4MeUtils.ToObject<Dictionary<string, string>>(value, out var errorString1);
                                if (errorString1 == "")
                                    propInfo.SetValue(contactFromObject, customData);
                                else
                                    propInfo.SetValue(contactFromObject,
                                        new Dictionary<string, string> { { "<WRONG DATA>", "<WRONG DATA>" } });
                                break;
                            case "schedule":
                                var schedules = R4MeUtils.ToObject<Schedule[]>(value, out var errorString2);
                                if (errorString2 == "")
                                    propInfo.SetValue(contactFromObject, schedules);
                                else
                                    propInfo.SetValue(contactFromObject, null);
                                break;
                            case "schedule_blacklist":
                                var scheduleBlackList = R4MeUtils.ToObject<string[]>(value, out var errorString3);
                                if (errorString3 == "")
                                    propInfo.SetValue(contactFromObject, scheduleBlackList);
                                else
                                    propInfo.SetValue(contactFromObject, new[] { "<WRONG DATA>" });
                                break;
                            default:
                                var convertedValue = valueType != ""
                                    ? R4MeUtils.ConvertObjectToPropertyType(value, propInfo)
                                    : value;
                                propInfo.SetValue(contactFromObject, convertedValue);
                                break;
                        }
                    }

                    contactsFromObjects.Add(contactFromObject);
                }
            }
            else
            {
                errorString = response.Item2;
            }

            return new Tuple<SearchAddressBookLocationResponse, List<AddressBookContact>, string>(response.Item1, contactsFromObjects, errorString);
        }

        /// <summary>
        ///     Adds an address book contact to a user's account.
        /// </summary>
        /// <param name="contact">The AddressBookContact type object as input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The AddressBookContact type object</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.AddAddressBookContact instead.")]
        public AddressBookContact AddAddressBookContact(AddressBookContact contact, out string errorString)
        {
            contact.PrepareForSerialization();

            return GetJsonObjectFromAPI<AddressBookContact>(contact,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Post,
                false,
                true,
                out errorString);
        }

        /// <summary>
        ///     Adds an address book contact to a user's account.
        /// </summary>
        /// <param name="contact">The AddressBookContact type object as input parameters</param>
        /// <returns>The AddressBookContact type object</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.AddAddressBookContactAsync instead.")]
        public Task<Tuple<AddressBookContact, string>> AddAddressBookContactAsync(AddressBookContact contact)
        {
            contact.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<AddressBookContact>(contact,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Post,
                null,
                false,
                true);
        }

        /// <summary>
        ///     Updates an address book contact.
        /// </summary>
        /// <param name="contact">The AddressBookContact type object as input parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The AddressBookContact type object</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.UpdateAddressBookContact instead.")]
        public AddressBookContact UpdateAddressBookContact(AddressBookContact contact, out string errorString)
        {
            contact.PrepareForSerialization();
            return GetJsonObjectFromAPI<AddressBookContact>(contact,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updates an address book contact.
        /// </summary>
        /// <param name="contact">The AddressBookContact type object as input parameters</param>
        /// <returns>The AddressBookContact type object</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.UpdateAddressBookContactAsync instead.")]
        public Task<Tuple<AddressBookContact, string>> UpdateAddressBookContactAsync(AddressBookContact contact)
        {
            contact.PrepareForSerialization();
            return GetJsonObjectFromAPIAsync<AddressBookContact>(contact,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Put);
        }

        /// <summary>
        ///     Updates an address book contact.
        ///     Used in case fo sending specified, limited number of the Contact parameters.
        /// </summary>
        /// <param name="contact">Address Book Contact</param>
        /// <param name="updatableProperties">
        ///     List of the properties which should be updated -
        ///     despite are they null or not
        /// </param>
        /// <param name="errorString">Error strings</param>
        /// <returns>Address book contact</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.UpdateAddressBookContact instead.")]
        public AddressBookContact UpdateAddressBookContact(AddressBookContact contact, List<string> updatableProperties,
            out string errorString)
        {
            var myDynamicClass = new Route4MeDynamicClass();
            myDynamicClass.CopyPropertiesFromClass(contact, updatableProperties, out _);

            var jsonString = JSON.ToJSON(myDynamicClass.DynamicProperties);

            var genParams = new GenericParameters();

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = GetJsonObjectFromAPI<AddressBookContact>
            (genParams, R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Put, content, false, true, out errorString);

            return response;
        }

        /// <summary>
        ///     Updates an address book contact.
        ///     Used in case fo sending specified, limited number of the Contact parameters.
        /// </summary>
        /// <param name="contact">Address Book Contact</param>
        /// <param name="updatableProperties">
        ///     List of the properties which should be updated -
        ///     despite are they null or not
        /// </param>
        /// <returns>Address book contact</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.UpdateAddressBookContactAsync instead.")]
        public Task<Tuple<AddressBookContact, string>> UpdateAddressBookContactAsync(AddressBookContact contact, List<string> updatableProperties)
        {
            var myDynamicClass = new Route4MeDynamicClass();
            myDynamicClass.CopyPropertiesFromClass(contact, updatableProperties, out _);

            var jsonString = JSON.ToJSON(myDynamicClass.DynamicProperties);

            var genParams = new GenericParameters();

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return GetJsonObjectFromAPIAsync<AddressBookContact>
            (genParams, R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Put, content, false, true);
        }

        /// <summary>
        ///     Updates a contact by comparing initial and modified contact objects and
        ///     by updating only modified proeprties of a contact.
        /// </summary>
        /// <param name="contact">A address book contact object as input (modified or created virtual contact)</param>
        /// <param name="initialContact">An initial address book contact</param>
        /// <param name="errorString">Error string</param>
        /// <returns>Updated address book contact</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.UpdateAddressBookContact instead.")]
        public AddressBookContact UpdateAddressBookContact(AddressBookContact contact,
            AddressBookContact initialContact, out string errorString)
        {
            errorString = "";

            if (initialContact == null || initialContact == contact)
            {
                errorString = "The initial and modified contacts should not be null";
                return null;
            }

            var updatableContactProperties = R4MeUtils
                .GetPropertiesWithDifferentValues(contact, initialContact, out errorString);

            updatableContactProperties.Add("AddressId");

            if (updatableContactProperties.Count > 0)
            {
                var dynamicContactProperties = new Route4MeDynamicClass();

                dynamicContactProperties.CopyPropertiesFromClass(contact, updatableContactProperties,
                    out _);

                var contactParamsJsonString =
                    R4MeUtils.SerializeObjectToJson(dynamicContactProperties.DynamicProperties, true);

                var genParams = new GenericParameters();

                var content = new StringContent(contactParamsJsonString, Encoding.UTF8, "application/json");

                var response = GetJsonObjectFromAPI<AddressBookContact>
                (genParams, R4MEInfrastructureSettings.AddressBook,
                    HttpMethodType.Put, content, false, true, out errorString);

                return response;
            }

            return null;
        }

        /// <summary>
        ///     Updates a contact by comparing initial and modified contact objects and
        ///     by updating only modified proeprties of a contact.
        /// </summary>
        /// <param name="contact">A address book contact object as input (modified or created virtual contact)</param>
        /// <param name="initialContact">An initial address book contact</param>
        /// <returns>Updated address book contact</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.UpdateAddressBookContactAsync instead.")]
        public Task<Tuple<AddressBookContact, string>> UpdateAddressBookContactAsync(AddressBookContact contact, AddressBookContact initialContact)
        {
            string errorString;

            if (initialContact == null || initialContact == contact)
            {
                errorString = "The initial and modified contacts should not be null";
                return Task.FromResult(new Tuple<AddressBookContact, string>(null, errorString));
            }

            var updatableContactProperties = R4MeUtils
                .GetPropertiesWithDifferentValues(contact, initialContact, out errorString);

            updatableContactProperties.Add("AddressId");

            if (updatableContactProperties.Count > 0)
            {
                var dynamicContactProperties = new Route4MeDynamicClass();

                dynamicContactProperties.CopyPropertiesFromClass(contact, updatableContactProperties,
                    out _);

                var contactParamsJsonString =
                    R4MeUtils.SerializeObjectToJson(dynamicContactProperties.DynamicProperties, true);

                var genParams = new GenericParameters();

                var content = new StringContent(contactParamsJsonString, Encoding.UTF8, "application/json");

                return GetJsonObjectFromAPIAsync<AddressBookContact>
                (genParams, R4MEInfrastructureSettings.AddressBook,
                    HttpMethodType.Put, content, false, true);
            }

            return Task.FromResult(new Tuple<AddressBookContact, string>(null, errorString));
        }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="addressIds">The array of the address IDs</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>If true the contacts were removed successfully</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.RemoveAddressBookContacts instead.")]
        public bool RemoveAddressBookContacts(string[] addressIds, out string errorString)
        {
            var request = new RemoveAddressBookContactsRequest
            {
                AddressIds = addressIds
            };

            var response = GetJsonObjectFromAPI<StatusResponse>(request,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Delete,
                out errorString);

            return response != null && response.Status;
        }

        /// <summary>
        ///     Remove the address book contacts.
        /// </summary>
        /// <param name="addressIds">The array of the address IDs</param>
        /// <returns>If true the contacts were removed successfully</returns>
        [Obsolete("The method is obsolete, use the method AddressBookContactsManagerV5.RemoveAddressBookContactsAsync instead.")]
        public async Task<Tuple<bool, string>> RemoveAddressBookContactsAsync(string[] addressIds)
        {
            var request = new RemoveAddressBookContactsRequest
            {
                AddressIds = addressIds
            };

            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(request,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(response.Item1 != null && response.Item1.Status, response.Item2);
        }

        #endregion

        #region Address Book Group

        /// <summary>
        ///     Get the address book groups
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An array of the address book contacts</returns>
        public AddressBookGroup[] GetAddressBookGroups(AddressBookGroupParameters addressBookGroupParameters,
            out string errorString)
        {
            var response = GetJsonObjectFromAPI<AddressBookGroup[]>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Get,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Get the address book groups
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <returns>An array of the address book contacts</returns>
        public Task<Tuple<AddressBookGroup[], string>> GetAddressBookGroupsAsync(AddressBookGroupParameters addressBookGroupParameters)
        {
            return GetJsonObjectFromAPIAsync<AddressBookGroup[]>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get a specified address book group
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An address book group</returns>
        public AddressBookGroup GetAddressBookGroup(AddressBookGroupParameters addressBookGroupParameters,
            out string errorString)
        {
            addressBookGroupParameters.PrepareForSerialization();
            var response = GetJsonObjectFromAPI<AddressBookGroup>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Get,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Get a specified address book group
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <returns>An address book group</returns>
        public Task<Tuple<AddressBookGroup, string>> GetAddressBookGroupAsync(AddressBookGroupParameters addressBookGroupParameters)
        {
            addressBookGroupParameters.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<AddressBookGroup>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get address book contacts by specified group
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An address book group</returns>
        public AddressBookSearchResponse GetAddressBookContactsByGroup(
            AddressBookGroupParameters addressBookGroupParameters, out string errorString)
        {
            addressBookGroupParameters.PrepareForSerialization();
            var response = GetJsonObjectFromAPI<AddressBookSearchResponse>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBookGroupSearch,
                HttpMethodType.Post,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Get address book contacts by specified group
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <returns>An address book group</returns>
        public Task<Tuple<AddressBookSearchResponse, string>> GetAddressBookContactsByGroupAsync(
            AddressBookGroupParameters addressBookGroupParameters)
        {
            addressBookGroupParameters.PrepareForSerialization();
            return GetJsonObjectFromAPIAsync<AddressBookSearchResponse>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBookGroupSearch,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Get response containing contact IDs by sending custom field and its value array.
        /// </summary>
        /// <param name="customField">Custom field name</param>
        /// <param name="customFieldValues">An array of the custom field values</param>
        /// <param name="errorString">Error string</param>
        /// <returns>Response containing an array of the Contact IDs</returns>
        public long[] GetAddressBookContactsByCustomField(string customField, string[] customFieldValues,
            out string errorString)
        {
            var addressBookGroupRules = new List<AddressBookGroupRule>();

            if ((customField?.Length ?? 0) < 1 || (customFieldValues?.Length ?? 0) < 1)
            {
                errorString = "Empty custom field value(s)";
                return null;
            }

            foreach (var customFieldValue in customFieldValues)
            {
                var addressBookGroupRule = new AddressBookGroupRule
                {
                    ID = "custom_data." + customField,
                    Field = "custom_data." + customField,
                    Operator = "contains",
                    Value = customFieldValue
                };

                addressBookGroupRules.Add(addressBookGroupRule);
            }

            var addressBookGroupFilter = new AddressBookGroupFilter
            {
                Condition = "OR",
                Rules = addressBookGroupRules.ToArray()
            };

            var addressBookGroupParameters = new AddressBookGroup
            {
                GroupName = "Custom Fied Contains",
                GroupColor = "92e1c0",
                Filter = addressBookGroupFilter
            };

            var addressBookGroup = AddAddressBookGroup(addressBookGroupParameters,
                out errorString);

            if (addressBookGroup == null || addressBookGroup.GetType() != typeof(AddressBookGroup)) return null;

            var addressBookGroupParams = new AddressBookGroupParameters
            {
                GroupId = addressBookGroup.GroupId,
                Fields = new[] {"address_id"}
            };

            var response = GetAddressBookContactsByGroup(
                addressBookGroupParams,
                out errorString);

            if ((response?.Results?.Length ?? 0) < 1) return null;

            var contactIDs = new List<long>();

            foreach (object[] oContId in response.Results)
                if (long.TryParse(oContId[0].ToString(), out var __))
                    contactIDs.Add(Convert.ToInt64(oContId[0]));

            var removeGroupParams = new AddressBookGroupParameters {GroupId = addressBookGroup.GroupId};
            RemoveAddressBookGroup(removeGroupParams, out errorString);

            return contactIDs.Count > 0 ? contactIDs.ToArray() : null;
        }

        /// <summary>
        ///     Get response containing contact IDs by sending custom field and its value array.
        /// </summary>
        /// <param name="customField">Custom field name</param>
        /// <param name="customFieldValues">An array of the custom field values</param>
        /// <returns>Response containing an array of the Contact IDs</returns>
        public async Task<Tuple<long[], string>> GetAddressBookContactsByCustomFieldAsync(string customField, string[] customFieldValues)
        {
            var addressBookGroupRules = new List<AddressBookGroupRule>();

            if ((customField?.Length ?? 0) < 1 || (customFieldValues?.Length ?? 0) < 1)
            {
                return new Tuple<long[], string>(null, "Empty custom field value(s)");
            }

            foreach (var customFieldValue in customFieldValues)
            {
                var addressBookGroupRule = new AddressBookGroupRule
                {
                    ID = "custom_data." + customField,
                    Field = "custom_data." + customField,
                    Operator = "contains",
                    Value = customFieldValue
                };

                addressBookGroupRules.Add(addressBookGroupRule);
            }

            var addressBookGroupFilter = new AddressBookGroupFilter
            {
                Condition = "OR",
                Rules = addressBookGroupRules.ToArray()
            };

            var addressBookGroupParameters = new AddressBookGroup
            {
                GroupName = "Custom Fied Contains",
                GroupColor = "92e1c0",
                Filter = addressBookGroupFilter
            };

            var addressBookGroup = await AddAddressBookGroupAsync(addressBookGroupParameters).ConfigureAwait(false);

            if (addressBookGroup.Item1 == null || addressBookGroup.Item1.GetType() != typeof(AddressBookGroup)) return new Tuple<long[], string>(null, addressBookGroup.Item2);

            var addressBookGroupParams = new AddressBookGroupParameters
            {
                GroupId = addressBookGroup.Item1.GroupId,
                Fields = new[] { "address_id" }
            };

            var response = await GetAddressBookContactsByGroupAsync(
                addressBookGroupParams).ConfigureAwait(false);

            if ((response.Item1?.Results?.Length ?? 0) < 1) return new Tuple<long[], string>(null, response.Item2);

            var contactIDs = new List<long>();

            foreach (object[] oContId in response.Item1.Results)
                if (long.TryParse(oContId[0].ToString(), out var __))
                    contactIDs.Add(Convert.ToInt64(oContId[0]));

            var removeGroupParams = new AddressBookGroupParameters { GroupId = addressBookGroup.Item1.GroupId };
            var res = await RemoveAddressBookGroupAsync(removeGroupParams).ConfigureAwait(false);

            return new Tuple<long[], string>(contactIDs.Count > 0 ? contactIDs.ToArray() : null, res.Item2);
        }

        /// <summary>
        ///     Search the address book groups by specified filter.
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public AddressBookContactsResponse SearchAddressBookContactsByFilter(
            AddressBookGroupParameters addressBookGroupParameters, out string errorString)
        {
            addressBookGroupParameters.PrepareForSerialization();
            var response = GetJsonObjectFromAPI<AddressBookContactsResponse>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Post,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Search the address book groups by specified filter.
        /// </summary>
        /// <param name="addressBookGroupParameters">Query parameters</param>
        /// <returns>An AddressBookContactsResponse type object</returns>
        public Task<Tuple<AddressBookContactsResponse, string>> SearchAddressBookContactsByFilterAsync(AddressBookGroupParameters addressBookGroupParameters)
        {
            addressBookGroupParameters.PrepareForSerialization();
            return GetJsonObjectFromAPIAsync<AddressBookContactsResponse>(addressBookGroupParameters,
                R4MEInfrastructureSettings.AddressBook,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Create an address book group
        /// </summary>
        /// <param name="group">An address book group</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An address book group</returns>
        public AddressBookGroup AddAddressBookGroup(AddressBookGroup group, out string errorString)
        {
            group.PrepareForSerialization();
            var result = GetJsonObjectFromAPI<AddressBookGroup>(group,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Post,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Create an address book group
        /// </summary>
        /// <param name="group">An address book group</param>
        /// <returns>An address book group</returns>
        public Task<Tuple<AddressBookGroup, string>> AddAddressBookGroupAsync(AddressBookGroup group)
        {
            group.PrepareForSerialization();
            return GetJsonObjectFromAPIAsync<AddressBookGroup>(group,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Update an address book group
        /// </summary>
        /// <param name="group">An address book group</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An address book group</returns>
        public AddressBookGroup UpdateAddressBookGroup(AddressBookGroup group, out string errorString)
        {
            group.PrepareForSerialization();

            var result = GetJsonObjectFromAPI<AddressBookGroup>(
                group,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Put,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Update an address book group
        /// </summary>
        /// <param name="group">An address book group</param>
        /// <returns>An address book group</returns>
        public Task<Tuple<AddressBookGroup, string>> UpdateAddressBookGroupAsync(AddressBookGroup group)
        {
            group.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<AddressBookGroup>(
                group,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Put);
        }

        /// <summary>
        ///     Remove an address book group
        /// </summary>
        /// <param name="groupID">A group ID</param>
        /// <param name="errorString">Error string</param>
        /// <returns>Group creating status</returns>
        public StatusResponse RemoveAddressBookGroup(AddressBookGroupParameters groupID, out string errorString)
        {
            groupID.PrepareForSerialization();
            var result = GetJsonObjectFromAPI<StatusResponse>(groupID,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Delete,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Remove an address book group
        /// </summary>
        /// <param name="groupID">A group ID</param>
        /// <returns>Group creating status</returns>
        public Task<Tuple<StatusResponse, string>> RemoveAddressBookGroupAsync(AddressBookGroupParameters groupID)
        {
            groupID.PrepareForSerialization();
            return GetJsonObjectFromAPIAsync<StatusResponse>(groupID,
                R4MEInfrastructureSettings.AddressBookGroup,
                HttpMethodType.Delete);
        }

        #endregion

        #region Avoidance Zones

        /// <summary>
        ///     Create avoidance zone
        /// </summary>
        /// <param name="avoidanceZoneParameters"> Parameters for request </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Avoidance zone Object </returns>
        public AvoidanceZone AddAvoidanceZone(AvoidanceZoneParameters avoidanceZoneParameters, out string errorString)
        {
            var avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneParameters,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Post,
                out errorString);
            return avoidanceZone;
        }

        /// <summary>
        ///     Create avoidance zone
        /// </summary>
        /// <param name="avoidanceZoneParameters"> Parameters for request </param>
        /// <returns> Avoidance zone Object </returns>
        public Task<Tuple<AvoidanceZone, string>> AddAvoidanceZoneAsync(AvoidanceZoneParameters avoidanceZoneParameters)
        {
            return GetJsonObjectFromAPIAsync<AvoidanceZone>(avoidanceZoneParameters,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Get avoidance zones
        /// </summary>
        /// <param name="avoidanceZoneQuery"> Parameters for request </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Avoidance zone Object list </returns>
        public AvoidanceZone[] GetAvoidanceZones(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
        {
            var avoidanceZones = GetJsonObjectFromAPI<AvoidanceZone[]>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Get,
                out errorString);
            return avoidanceZones;
        }

        /// <summary>
        ///     Get avoidance zones
        /// </summary>
        /// <param name="avoidanceZoneQuery"> Parameters for request </param>
        /// <returns> Avoidance zone Object list </returns>
        public Task<Tuple<AvoidanceZone[], string>> GetAvoidanceZonesAsync(AvoidanceZoneQuery avoidanceZoneQuery)
        {
            return GetJsonObjectFromAPIAsync<AvoidanceZone[]>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get avoidance zone by parameters (territory id, device id)
        /// </summary>
        /// <param name="avoidanceZoneQuery"> Parameters for request </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Avoidance zone Object </returns>
        public AvoidanceZone GetAvoidanceZone(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
        {
            var avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Get,
                out errorString);
            return avoidanceZone;
        }

        /// <summary>
        ///     Get avoidance zone by parameters (territory id, device id)
        /// </summary>
        /// <param name="avoidanceZoneQuery"> Parameters for request </param>
        /// <returns> Avoidance zone Object </returns>
        public Task<Tuple<AvoidanceZone, string>> GetAvoidanceZoneAsync(AvoidanceZoneQuery avoidanceZoneQuery)
        {
            return GetJsonObjectFromAPIAsync<AvoidanceZone>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Update avoidance zone (by territory id, device id)
        /// </summary>
        /// <param name="avoidanceZoneParameters"> Parameters for request </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Avoidance zone Object </returns>
        public AvoidanceZone UpdateAvoidanceZone(AvoidanceZoneParameters avoidanceZoneParameters,
            out string errorString)
        {
            var avoidanceZone = GetJsonObjectFromAPI<AvoidanceZone>(avoidanceZoneParameters,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Put,
                out errorString);
            return avoidanceZone;
        }

        /// <summary>
        ///     Update avoidance zone (by territory id, device id)
        /// </summary>
        /// <param name="avoidanceZoneParameters"> Parameters for request </param>
        /// <returns> Avoidance zone Object </returns>
        public Task<Tuple<AvoidanceZone, string>> UpdateAvoidanceZoneAsync(AvoidanceZoneParameters avoidanceZoneParameters)
        {
            return GetJsonObjectFromAPIAsync<AvoidanceZone>(avoidanceZoneParameters,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Put);
        }

        /// <summary>
        ///     Delete avoidance zone (by territory id, device id)
        /// </summary>
        /// <param name="avoidanceZoneQuery"> Parameters for request </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Result status true/false </returns>
        public bool DeleteAvoidanceZone(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
        {
            var result = GetJsonObjectFromAPI<StatusResponse>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Delete,
                out errorString);

            return result.Status;
        }

        /// <summary>
        ///     Delete avoidance zone (by territory id, device id)
        /// </summary>
        /// <param name="avoidanceZoneQuery"> Parameters for request </param>
        /// <returns> Result status true/false </returns>
        public async Task<Tuple<bool, string>> DeleteAvoidanceZone(AvoidanceZoneQuery avoidanceZoneQuery)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Avoidance,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(result.Item1.Status, result.Item2);
        }

        #endregion

        #region Orders

        /// <summary>
        ///     Gets the Orders
        /// </summary>
        /// <param name="ordersQuery"> The query parameters for the orders request process </param>
        /// <param name="total"> out: Total number of the orders </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> List of the Order type objects </returns>
        public Order[] GetOrders(OrderParameters ordersQuery, out uint total, out string errorString)
        {
            var response = GetJsonObjectFromAPI<GetOrdersResponse>(ordersQuery,
                R4MEInfrastructureSettings.Order,
                HttpMethodType.Get,
                out errorString);

            total = response?.Total ?? 0;

            return response?.Results;
        }

        /// <summary>
        ///     Gets the Orders
        /// </summary>
        /// <param name="ordersQuery"> The query parameters for the orders request process </param>
        /// <returns> List of the Order type objects </returns>
        public async Task<Tuple<Order[], uint, string>> GetOrdersAsync(OrderParameters ordersQuery)
        {
            var response = await GetJsonObjectFromAPIAsync<GetOrdersResponse>(ordersQuery,
                R4MEInfrastructureSettings.Order,
                HttpMethodType.Get).ConfigureAwait(false);

            var total = response.Item1?.Total ?? 0;

            return new Tuple<Order[], uint, string>(response.Item1?.Results, total, response.Item2);
        }

        /// <summary>
        ///     Gets an array of the Order type objects by list of the order IDs.
        /// </summary>
        /// <param name="orderQuery">
        ///     The OrderParameters type object as the input parameters containing coma-delimited list of the
        ///     order IDs.
        /// </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>List of the Order type objects</returns>
        public Order GetOrderByID(OrderParameters orderQuery, out string errorString)
        {
            //string[] ids = orderQuery.order_id.Split(',');
            //if (ids.Length == 1) orderQuery.order_id = orderQuery.order_id + "," + orderQuery.order_id;

            var response = GetJsonObjectFromAPI<Order>(orderQuery,
                R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);

            return response;
        }

        /// <summary>
        ///     Gets an array of the Order type objects by list of the order IDs.
        /// </summary>
        /// <param name="orderQuery">
        ///     The OrderParameters type object as the input parameters containing coma-delimited list of the
        ///     order IDs.
        /// </param>
        /// <returns>List of the Order type objects</returns>
        public Task<Tuple<Order, string>> GetOrderByIDAsync(OrderParameters orderQuery)
        {
            //string[] ids = orderQuery.order_id.Split(',');
            //if (ids.Length == 1) orderQuery.order_id = orderQuery.order_id + "," + orderQuery.order_id;

            return GetJsonObjectFromAPIAsync<Order>(orderQuery,
                R4MEInfrastructureSettings.Order, HttpMethodType.Get);
        }

        /// <summary>
        ///     Searches for the orders.
        /// </summary>
        /// <param name="orderQuery">The OrderParameters type object as the query parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>List of the Order type objects</returns>
        public object SearchOrders(OrderParameters orderQuery, out string errorString)
        {
            var showFields = (orderQuery?.Fields?.Length ?? 0) >= 1;

            if (showFields)
                return GetJsonObjectFromAPI<SearchOrdersResponse>(orderQuery,
                    R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);
            return GetJsonObjectFromAPI<GetOrdersResponse>(orderQuery,
                R4MEInfrastructureSettings.Order, HttpMethodType.Get, out errorString);
        }

        /// <summary>
        ///     Searches for the orders.
        /// </summary>
        /// <param name="orderQuery">The OrderParameters type object as the query parameters</param>
        /// <returns>List of the Order type objects</returns>
        public async Task<Tuple<object, string>> SearchOrdersAsync(OrderParameters orderQuery)
        {
            var showFields = (orderQuery?.Fields?.Length ?? 0) >= 1;

            if (showFields)
            {
                var result1 = await GetJsonObjectFromAPIAsync<SearchOrdersResponse>(orderQuery,
                    R4MEInfrastructureSettings.Order, HttpMethodType.Get).ConfigureAwait(false);
                return new Tuple<object, string>(result1.Item1, result1.Item2);
            }
            var result2 = await GetJsonObjectFromAPIAsync<GetOrdersResponse>(orderQuery,
                R4MEInfrastructureSettings.Order, HttpMethodType.Get).ConfigureAwait(false);
            return new Tuple<object, string>(result2.Item1, result2.Item2);
        }

        /// <summary>
        ///     Filter for the orders filtering
        /// </summary>
        /// <param name="orderFilter">The OrderFilterParameters object as a HTTP request payload</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Array of the Order type objects</returns>
        public Order[] FilterOrders(OrderFilterParameters orderFilter, out string errorString)
        {
            var response = GetJsonObjectFromAPI<GetOrdersResponse>(orderFilter,
                R4MEInfrastructureSettings.Order, HttpMethodType.Post, out errorString);

            return response?.Results;
        }

        /// <summary>
        ///     Filter for the orders filtering
        /// </summary>
        /// <param name="orderFilter">The OrderFilterParameters object as a HTTP request payload</param>
        /// <returns>Array of the Order type objects</returns>
        public async Task<Tuple<Order[], string>> FilterOrdersAsync(OrderFilterParameters orderFilter)
        {
            var response = await GetJsonObjectFromAPIAsync<GetOrdersResponse>(orderFilter,
                R4MEInfrastructureSettings.Order, HttpMethodType.Post).ConfigureAwait(false);

            return new Tuple<Order[], string>(response.Item1?.Results, response.Item2);
        }

        /// <summary>
        ///     Creates an order
        /// </summary>
        /// <param name="order"> The Order type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Order object </returns>
        public Order AddOrder(Order order, out string errorString)
        {
            order.PrepareForSerialization();

            return GetJsonObjectFromAPI<Order>(order, R4MEInfrastructureSettings.Order,
                HttpMethodType.Post,
                out errorString);
        }

        /// <summary>
        ///     Creates an order
        /// </summary>
        /// <param name="order"> The Order type object as the request payload </param>
        /// <returns> Order object </returns>
        public Task<Tuple<Order, string>> AddOrderAsync(Order order)
        {
            order.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<Order>(order, R4MEInfrastructureSettings.Order,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Updates an order
        /// </summary>
        /// <param name="order"> The Order type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> An Order type object </returns>
        public Order UpdateOrder(Order order, out string errorString)
        {
            order.PrepareForSerialization();

            return GetJsonObjectFromAPI<Order>(order, R4MEInfrastructureSettings.Order,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updates an order
        /// </summary>
        /// <param name="order"> The Order type object as the request payload </param>
        /// <returns> An Order type object </returns>
        public Task<Tuple<Order, string>> UpdateOrderAsync(Order order)
        {
            order.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<Order>(order, R4MEInfrastructureSettings.Order, HttpMethodType.Put);
        }

        /// <summary>
        ///     Removes the orders
        /// </summary>
        /// <param name="orderIds"> The array of the order IDs </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Result status: true/false </returns>
        public bool RemoveOrders(string[] orderIds, out string errorString)
        {
            var request = new RemoveOrdersRequest
            {
                OrderIds = orderIds
            };

            var response = GetJsonObjectFromAPI<StatusResponse>(request,
                R4MEInfrastructureSettings.Order,
                HttpMethodType.Delete,
                out errorString);

            return response != null && response.Status;
        }

        /// <summary>
        ///     Removes the orders
        /// </summary>
        /// <param name="orderIds"> The array of the order IDs </param>
        /// <returns> Result status: true/false </returns>
        public async Task<Tuple<bool, string>> RemoveOrdersAsync(string[] orderIds)
        {
            var request = new RemoveOrdersRequest
            {
                OrderIds = orderIds
            };

            var response = await GetJsonObjectFromAPIAsync<StatusResponse>(request,
                R4MEInfrastructureSettings.Order,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(response.Item1 != null && response.Item1.Status, response.Item2);
        }

        /// <summary>
        ///     Adds the orders to a route.
        /// </summary>
        /// <param name="rQueryParams">The RouteParametersQuery type objects as the query parameters</param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="rParams">The route parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The RouteResponse type object</returns>
        public RouteResponse AddOrdersToRoute(RouteParametersQuery rQueryParams, Address[] addresses,
            RouteParameters rParams, out string errorString)
        {
            var request = new AddOrdersToRouteRequest
            {
                RouteId = rQueryParams.RouteId,
                Redirect = rQueryParams.Redirect == true ? 1 : 0,
                Addresses = addresses,
                Parameters = rParams
            };

            return GetJsonObjectFromAPI<RouteResponse>(request, R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, false, true, out errorString);
        }

        /// <summary>
        ///     Adds the orders to a route.
        /// </summary>
        /// <param name="rQueryParams">The RouteParametersQuery type objects as the query parameters</param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The RouteResponse type object</returns>
        public RouteResponse AddOrdersToRoute(RouteParametersQuery rQueryParams, Address[] addresses,
            out string errorString)
        {
            var request = new AddOrdersToRouteRequest
            {
                RouteId = rQueryParams.RouteId,
                Addresses = addresses
            };

            return GetJsonObjectFromAPI<RouteResponse>(request, R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, false, true, out errorString);
        }

        /// <summary>
        ///     Adds the orders to a route.
        /// </summary>
        /// <param name="rQueryParams">The RouteParametersQuery type objects as the query parameters</param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="rParams">The route parameters</param>
        /// <returns>The RouteResponse type object</returns>
        public Task<Tuple<RouteResponse, string>> AddOrdersToRouteAsync(RouteParametersQuery rQueryParams, Address[] addresses,
            RouteParameters rParams)
        {
            var request = new AddOrdersToRouteRequest
            {
                RouteId = rQueryParams.RouteId,
                Redirect = rQueryParams.Redirect == true ? 1 : 0,
                Addresses = addresses,
                Parameters = rParams
            };

            return GetJsonObjectFromAPIAsync<RouteResponse>(request, R4MEInfrastructureSettings.RouteHost,
                HttpMethodType.Put, null, false, true);
        }

        /// <summary>
        ///     Adds the orders to an optimization.
        /// </summary>
        /// <param name="rQueryParams"> The RouteParametersQuery type objects as the query parameters </param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="rParams">The route parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An optimization problem object</returns>
        public DataObject AddOrdersToOptimization(OptimizationParameters rQueryParams, Address[] addresses,
            RouteParameters rParams, out string errorString)
        {
            var request = new AddOrdersToOptimizationRequest
            {
                OptimizationProblemId = rQueryParams.OptimizationProblemID,
                Redirect = rQueryParams.Redirect == true ? 1 : 0,
                Addresses = addresses,
                Parameters = rParams
            };

            return GetJsonObjectFromAPI<DataObject>(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put,
                false, true, out errorString);
        }

        /// <summary>
        ///     Adds the orders to an optimization.
        /// </summary>
        /// <param name="rQueryParams"> The RouteParametersQuery type objects as the query parameters </param>
        /// <param name="addresses">An array of the addresses</param>
        /// <param name="rParams">The route parameters</param>
        /// <returns>An optimization problem object</returns>
        public Task<Tuple<DataObject, string>> AddOrdersToOptimizationAsync(OptimizationParameters rQueryParams, Address[] addresses, RouteParameters rParams)
        {
            var request = new AddOrdersToOptimizationRequest
            {
                OptimizationProblemId = rQueryParams.OptimizationProblemID,
                Redirect = rQueryParams.Redirect == true ? 1 : 0,
                Addresses = addresses,
                Parameters = rParams
            };

            return GetJsonObjectFromAPIAsync<DataObject>(request, R4MEInfrastructureSettings.ApiHost, HttpMethodType.Put, null, false, true);
        }

        /// <summary>
        ///     Transfer an order to another account
        /// </summary>
        /// <param name="transferredOrder"> An order to transfer </param>
        /// <param name="anotherPrimeryApiKey">A primery API key of a destination account</param>
        /// <returns>Transferred order</returns>
        public Order TransferOrderToOtherPrimaryAccount(Order transferredOrder, string anotherPrimeryApiKey, out string errorString)
        {
            var urlParams = new GenericParameters();
            urlParams.ParametersCollection.Add("api_key", anotherPrimeryApiKey);

            var updatableOrderProperties = new List<string>()
            {
                "Address1", "RootMemberId", "OrderId"
            };

            var dynamicOrderProperties = new Route4MeDynamicClass();

            dynamicOrderProperties.CopyPropertiesFromClass(transferredOrder, updatableOrderProperties, out _);

            string bodyJson = R4MeUtils.SerializeObjectToJson(dynamicOrderProperties.DynamicProperties, true);

            HttpContent content = new StringContent(bodyJson);
            content.Headers.Add("x-api-key", _mApiKey);

            return GetJsonObjectFromAPI<Order>(urlParams, R4MEInfrastructureSettings.Order,
                HttpMethodType.Put,
                content,
                false,
                true,
                out errorString);
        }

        /// <summary>
        ///     Transfer an order to another account
        /// </summary>
        /// <param name="transferredOrder">An order to transfer</param>
        /// <param name="anotherPrimeryApiKey">A primery API key of a destination account</param>
        /// <returns>Transferred order or failure response</returns>
        public Task<Tuple<Order, string>> TransferOrderToOtherPrimaryAccountAsync(Order transferredOrder, string anotherPrimeryApiKey)
        {
            var urlParams = new GenericParameters();
            urlParams.ParametersCollection.Add("api_key", anotherPrimeryApiKey);

            var updatableOrderProperties = new List<string>()
            {
                "Address1", "RootMemberId", "OrderId"
            };

            var dynamicOrderProperties = new Route4MeDynamicClass();

            dynamicOrderProperties.CopyPropertiesFromClass(transferredOrder, updatableOrderProperties, out _);

            string bodyJson = R4MeUtils.SerializeObjectToJson(dynamicOrderProperties.DynamicProperties, true);

            HttpContent content = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            content.Headers.Add("x-api-key", _mApiKey);

            //var content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

            return GetJsonObjectFromAPIAsync<Order>(urlParams, R4MEInfrastructureSettings.Order,
                HttpMethodType.Put,
                content,
                false,
                false);
        }

        /// <summary>
        ///     Gets the Orders Updates
        /// </summary>
        /// <returns> List of the Orders updates </returns>
        public OrdersUpdatesResponse GetOrdersUpdates(OrderUpdatesParameters parameters, out string errorString)
        {
            var response = GetJsonObjectFromAPI<OrdersUpdatesResponse>(parameters,
                R4MEInfrastructureSettings.GetOrdersUpdate,
                HttpMethodType.Get,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Gets the Orders Updates
        /// </summary>
        /// <returns> List of the Orders updates </returns>
        public Task<Tuple<OrdersUpdatesResponse, string>> GetOrdersUpdatesAsync(OrderUpdatesParameters parameters)
        {
            var response = GetJsonObjectFromAPIAsync<OrdersUpdatesResponse>(parameters,
                R4MEInfrastructureSettings.GetOrdersUpdate,
                HttpMethodType.Get);

            return response;
        }


        #endregion

        #region Order Custom User Field

        /// <summary>
        ///     Get the order's custom user fields.
        /// </summary>
        /// <param name="errorString">Error string</param>
        /// <returns>An array of the order's custom user fieds</returns>
        public OrderCustomField[] GetOrderCustomUserFields(out string errorString)
        {
            var genParams = new GenericParameters();

            var response = GetJsonObjectFromAPI<OrderCustomField[]>(genParams,
                R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Get,
                out errorString);

            return response;
        }

        /// <summary>
        ///     Get the order's custom user fields.
        /// </summary>
        /// <returns>An array of the order's custom user fieds</returns>
        public Task<Tuple<OrderCustomField[], string>> GetOrderCustomUserFieldsAsync()
        {
            var genParams = new GenericParameters();

            return GetJsonObjectFromAPIAsync<OrderCustomField[]>(genParams,
                R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Create an order's custom user field.
        /// </summary>
        /// <param name="orderCustomUserField">An order's custom user field</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An OrderCustomFieldCreateResponse type object</returns>
        public OrderCustomFieldCreateResponse CreateOrderCustomUserField(
            OrderCustomFieldParameters orderCustomUserField, out string errorString)
        {
            return GetJsonObjectFromAPI<OrderCustomFieldCreateResponse>
            (orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Post, false, false, out errorString);
        }

        /// <summary>
        ///     Create an order's custom user field.
        /// </summary>
        /// <param name="orderCustomUserField">An order's custom user field</param>
        /// <returns>An OrderCustomFieldCreateResponse type object</returns>
        public Task<Tuple<OrderCustomFieldCreateResponse, string>> CreateOrderCustomUserFieldAsync(OrderCustomFieldParameters orderCustomUserField)
        {
            return GetJsonObjectFromAPIAsync<OrderCustomFieldCreateResponse>
            (orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Post, null, false, false);
        }

        /// <summary>
        ///     Remove an order's custom user field.
        /// </summary>
        /// <param name="orderCustomUserField">An order's custom user field.</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An OrderCustomFieldCreateResponse type object</returns>
        public OrderCustomFieldCreateResponse RemoveOrderCustomUserField(
            OrderCustomFieldParameters orderCustomUserField, out string errorString)
        {
            return GetJsonObjectFromAPI<OrderCustomFieldCreateResponse>
            (orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Delete, false, false, out errorString);
        }

        /// <summary>
        ///     Remove an order's custom user field.
        /// </summary>
        /// <param name="orderCustomUserField">An order's custom user field.</param>
        /// <returns>An OrderCustomFieldCreateResponse type object</returns>
        public Task<Tuple<OrderCustomFieldCreateResponse, string>> RemoveOrderCustomUserFieldAsync(OrderCustomFieldParameters orderCustomUserField)
        {
            return GetJsonObjectFromAPIAsync<OrderCustomFieldCreateResponse>
            (orderCustomUserField, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Delete, null, false, false);
        }

        /// <summary>
        ///     Update the order's custom user fields.
        /// </summary>
        /// <param name="orderCustomUserFieldParams">The order's custom user fields</param>
        /// <param name="errorString">Error string</param>
        /// <returns>An OrderCustomFieldCreateResponse type object</returns>
        public OrderCustomFieldCreateResponse UpdateOrderCustomUserField(
            OrderCustomFieldParameters orderCustomUserFieldParams, out string errorString)
        {
            orderCustomUserFieldParams.PrepareForSerialization();

            var orderCustomField = GetJsonObjectFromAPI<OrderCustomFieldCreateResponse>
            (orderCustomUserFieldParams, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Put, false, false, out errorString);

            return orderCustomField;
        }

        /// <summary>
        ///     Update the order's custom user fields.
        /// </summary>
        /// <param name="orderCustomUserFieldParams">The order's custom user fields</param>
        /// <returns>An OrderCustomFieldCreateResponse type object</returns>
        public Task<Tuple<OrderCustomFieldCreateResponse, string>> UpdateOrderCustomUserFieldAsync(OrderCustomFieldParameters orderCustomUserFieldParams)
        {
            orderCustomUserFieldParams.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<OrderCustomFieldCreateResponse>
            (orderCustomUserFieldParams, R4MEInfrastructureSettings.OrderCustomField,
                HttpMethodType.Put, null, false, false);
        }

        #endregion

        #region Geocoding

        /// <summary>
        ///     Geocoding of the addresses
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The geocoded addresses</returns>
        public string Geocoding(GeocodingParameters geoParams, out string errorString)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var response = GetXmlObjectFromAPI(request, R4MEInfrastructureSettings.Geocoder,
                HttpMethodType.Post, null, true, out errorString);

            if (response == null) return errorString;
            return response;
        }

        /// <summary>
        ///     Geocoding of the addresses
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <returns>The geocoded addresses</returns>
        public async Task<Tuple<string, string>> GeocodingAsync(GeocodingParameters geoParams)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var response = await GetXmlObjectFromAPIAsync<string>(request, R4MEInfrastructureSettings.Geocoder,
                HttpMethodType.Post, null, true).ConfigureAwait(false);

            if (response.Item1 == null) return new Tuple<string, string>(response.Item2, response.Item2);
            return response;
        }

        /// <summary>
        ///     Batch geocoding of the addresses
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The geocoded addresses</returns>
        public string BatchGeocoding(GeocodingParameters geoParams, out string errorString)
        {
            var request = new GeocodingRequest();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("strExportFormat", geoParams.ExportFormat),
                new KeyValuePair<string, string>("addresses", geoParams.Addresses)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                return GetJsonObjectFromAPI<string>(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post,
                    httpContent, true, false, out errorString);
            }
        }

        /// <summary>
        ///     Batch geocoding of the addresses
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <returns>The geocoded addresses</returns>
        public async Task<Tuple<string, string>> BatchGeocodingAsync(GeocodingParameters geoParams)
        {
            var request = new GeocodingRequest();

            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("strExportFormat", geoParams.ExportFormat),
                new KeyValuePair<string, string>("addresses", geoParams.Addresses)
            };

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                return await GetJsonObjectFromAPIAsync<string>(request, R4MEInfrastructureSettings.Geocoder, HttpMethodType.Post,
                    httpContent, true, false).ConfigureAwait(false);
            }
        }

        /// <summary>
        ///     Uploads the addresses to temporary storage.
        /// </summary>
        /// <param name="jsonAddresses">The addresses, JSON formatted</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>The uploadAddressesToTemporaryStorageResponse type object</returns>
        public UploadAddressesToTemporaryStorageResponse UploadAddressesToTemporaryStorage(string jsonAddresses,
            out string errorString)
        {
            var request = new GeocodingRequest();

            using (HttpContent content = new StringContent(jsonAddresses))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = GetJsonObjectFromAPIAsync<UploadAddressesToTemporaryStorageResponse>(request,
                    R4MEInfrastructureSettings.FastGeocoder,
                    HttpMethodType.Post,
                    content, false, false).GetAwaiter().GetResult();

                errorString = result.Item2;

                return result.Item1;
            }
        }

        /// <summary>
        ///     Uploads the addresses to temporary storage.
        /// </summary>
        /// <param name="jsonAddresses">The addresses, JSON formatted</param>
        /// <returns>The uploadAddressesToTemporaryStorageResponse type object</returns>
        public async Task<Tuple<UploadAddressesToTemporaryStorageResponse, string>> UploadAddressesToTemporaryStorageAsync(string jsonAddresses)
        {
            var request = new GeocodingRequest();

            using (HttpContent content = new StringContent(jsonAddresses))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await GetJsonObjectFromAPIAsync<UploadAddressesToTemporaryStorageResponse>(request,
                    R4MEInfrastructureSettings.FastGeocoder,
                    HttpMethodType.Post,
                    content, false, false).ConfigureAwait(false);
                return result;
            }
        }

        /// <summary>
        ///     Returns rapid street data
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An list of the street data</returns>
        public List<Dictionary<string, string>> RapidStreetData(GeocodingParameters geoParams, out string errorString)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var url = R4MEInfrastructureSettings.RapidStreetData;

            var result = new List<Dictionary<string, string>>();

            if (geoParams.Pk > 0)
            {
                url = url + "/" + geoParams.Pk + "/";

                var response = GetJsonObjectFromAPI<RapidStreetResponse>(request, url, HttpMethodType.Get, null, false, false,
                    out errorString);

                var dresult = new Dictionary<string, string>();

                if (response != null)
                {
                    dresult["zipcode"] = response.Zipcode;
                    dresult["street_name"] = response.StreetName;
                    result.Add(dresult);
                }
            }
            else
            {
                if ((geoParams.Offset > 0) | (geoParams.Limit > 0))
                    url = url + "/" + geoParams.Offset + "/" + geoParams.Limit + "/";

                var response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url, HttpMethodType.Get, null,
                    false, false, out errorString);

                if (response != null)
                    foreach (var resp1 in response)
                    {
                        var dresult = new Dictionary<string, string>
                        {
                            {"zipcode", resp1.Zipcode},
                            {"street_name", resp1.StreetName}
                        };

                        result.Add(dresult);
                    }
            }

            return result;
        }

        /// <summary>
        ///     Returns rapid street data
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <returns>An array of the street data</returns>
        public async Task<Tuple<List<Dictionary<string, string>>, string>> RapidStreetDataAsync(GeocodingParameters geoParams)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var url = R4MEInfrastructureSettings.RapidStreetData;

            string errorString;
            var result = new List<Dictionary<string, string>>();

            if (geoParams.Pk > 0)
            {
                url = url + "/" + geoParams.Pk + "/";

                var response = await GetJsonObjectFromAPIAsync<RapidStreetResponse>(request, url, HttpMethodType.Get, null, false, false).ConfigureAwait(false);
                errorString = response.Item2;
                var dresult = new Dictionary<string, string>();

                if (response.Item1 != null)
                {
                    dresult["zipcode"] = response.Item1.Zipcode;
                    dresult["street_name"] = response.Item1.StreetName;
                    result.Add(dresult);
                }
            }
            else
            {
                if ((geoParams.Offset > 0) | (geoParams.Limit > 0))
                    url = url + "/" + geoParams.Offset + "/" + geoParams.Limit + "/";

                var response = await GetJsonObjectFromAPIAsync<RapidStreetResponse[]>(request, url, HttpMethodType.Get, null,
                    false, false).ConfigureAwait(false);
                errorString = response.Item2;

                if (response.Item1 != null)
                    foreach (var resp1 in response.Item1)
                    {
                        var dresult = new Dictionary<string, string>
                        {
                            {"zipcode", resp1.Zipcode},
                            {"street_name", resp1.StreetName}
                        };

                        result.Add(dresult);
                    }
            }

            return new Tuple<List<Dictionary<string, string>>, string>(result, errorString);
        }

        /// <summary>
        ///     Return the rapid street zip codes
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An list of the street zipcodes</returns>
        public List<Dictionary<string, string>> RapidStreetZipcode(GeocodingParameters geoParams, out string errorString)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var url = R4MEInfrastructureSettings.RapidStreetZipcode;

            var result = new List<Dictionary<string, string>>();

            if (geoParams.Zipcode != null)
            {
                url = url + "/" + geoParams.Zipcode + "/";
            }
            else
            {
                errorString = "Zipcode is not defined!...";
                return result;
            }

            if ((geoParams.Offset > 0) | (geoParams.Limit > 0))
                url = url + geoParams.Offset + "/" + geoParams.Limit + "/";

            var response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url,
                HttpMethodType.Get, null, false, false, out errorString);

            if (response != null)
                foreach (var resp1 in response)
                {
                    var dresult = new Dictionary<string, string>
                    {
                        {"zipcode", resp1.Zipcode},
                        {"street_name", resp1.StreetName}
                    };

                    result.Add(dresult);
                }

            return result;
        }

        /// <summary>
        ///     Return the rapid street zip codes
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <returns>An array of the street zipcodes</returns>
        public async Task<Tuple<List<Dictionary<string, string>>, string>> RapidStreetZipcodeAsync(GeocodingParameters geoParams)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var url = R4MEInfrastructureSettings.RapidStreetZipcode;

            var result = new List<Dictionary<string, string>>();
            string errorString = null;
            if (geoParams.Zipcode != null)
            {
                url = url + "/" + geoParams.Zipcode + "/";
            }
            else
            {
                errorString = "Zipcode is not defined!...";
                return new Tuple<List<Dictionary<string, string>>, string>(result, errorString);
            }

            if ((geoParams.Offset > 0) | (geoParams.Limit > 0))
                url = url + geoParams.Offset + "/" + geoParams.Limit + "/";

            var response = await GetJsonObjectFromAPIAsync<RapidStreetResponse[]>(request, url,
                HttpMethodType.Get, null, false, false).ConfigureAwait(false);
            errorString = response.Item2;

            if (response.Item1 != null)
                foreach (var resp1 in response.Item1)
                {
                    var dresult = new Dictionary<string, string>
                    {
                        {"zipcode", resp1.Zipcode},
                        {"street_name", resp1.StreetName}
                    };

                    result.Add(dresult);
                }

            return new Tuple<List<Dictionary<string, string>>, string>(result, errorString);
        }

        /// <summary>
        ///     Return the array of rapid street services
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An array of the street services</returns>
        public List<Dictionary<string, string>> RapidStreetService(GeocodingParameters geoParams, out string errorString)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var url = R4MEInfrastructureSettings.RapidStreetService;

            var result = new List<Dictionary<string, string>>();

            if (geoParams.Zipcode != null)
            {
                url = url + "/" + geoParams.Zipcode + "/";
            }
            else
            {
                errorString = "Zipcode is not defined!...";
                return result;
            }

            if (geoParams.Housenumber != null)
            {
                url = url + geoParams.Housenumber + "/";
            }
            else
            {
                errorString = "Housenumber is not defined!...";
                return result;
            }

            if ((geoParams.Offset > 0) | (geoParams.Limit > 0))
                url = url + geoParams.Offset + "/" + geoParams.Limit + "/";

            var response = GetJsonObjectFromAPI<RapidStreetResponse[]>(request, url,
                HttpMethodType.Get, null, false, false, out errorString);

            if (response != null)
                foreach (var resp1 in response)
                {
                    var dresult = new Dictionary<string, string>
                    {
                        {"zipcode", resp1.Zipcode},
                        {"street_name", resp1.StreetName}
                    };

                    result.Add(dresult);
                }

            return result;
        }

        /// <summary>
        ///     Return the array of rapid street services
        /// </summary>
        /// <param name="geoParams">The GeocodingParameters type object as the request parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An array of the street services</returns>
        public async Task<Tuple<List<Dictionary<string, string>>, string>> RapidStreetServiceAsync(GeocodingParameters geoParams)
        {
            var request = new GeocodingRequest
            {
                Addresses = geoParams.Addresses,
                Format = geoParams.ExportFormat
            };

            var url = R4MEInfrastructureSettings.RapidStreetService;

            var result = new List<Dictionary<string, string>>();
            string errorString = null;
            if (geoParams.Zipcode != null)
            {
                url = url + "/" + geoParams.Zipcode + "/";
            }
            else
            {
                errorString = "Zipcode is not defined!...";
                return new Tuple<List<Dictionary<string, string>>, string>(result, errorString);
            }

            if (geoParams.Housenumber != null)
            {
                url = url + geoParams.Housenumber + "/";
            }
            else
            {
                errorString = "Housenumber is not defined!...";
                return new Tuple<List<Dictionary<string, string>>, string>(result, errorString);
            }

            if ((geoParams.Offset > 0) | (geoParams.Limit > 0))
                url = url + geoParams.Offset + "/" + geoParams.Limit + "/";

            var response = await GetJsonObjectFromAPIAsync<RapidStreetResponse[]>(request, url,
                HttpMethodType.Get, null, false, false);
            errorString = response.Item2;

            if (response.Item1 != null)
                foreach (var resp1 in response.Item1)
                {
                    var dresult = new Dictionary<string, string>
                    {
                        {"zipcode", resp1.Zipcode},
                        {"street_name", resp1.StreetName}
                    };

                    result.Add(dresult);
                }

            return new Tuple<List<Dictionary<string, string>>, string>(result, errorString);
        }

        /// <summary>
        ///     Save addresses from temporary storage to database
        /// </summary>
        /// <param name="tempOptimizationProblemID">Temporary optimization problem ID with addresses</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>If true, the addresses saved to the database</returns>
        public bool SaveGeocodedAddressesToDatabase(string tempOptimizationProblemID, out string errorString)
        {
            var request = new GeocodingRequest();

            var json = "{\"optimization_problem_id\":" + tempOptimizationProblemID + "}";
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result =
                GetJsonObjectFromAPIAsync<UploadAddressesToTemporaryStorageResponse>(
                    request,
                    R4MEInfrastructureSettings.SaveGeocodedAddresses,
                    HttpMethodType.Post,
                    content,
                    false,
                    false).GetAwaiter().GetResult();

            errorString = result.Item2;

            return result.Item1?.Status ?? false;
        }

        /// <summary>
        ///     Save addresses from temporary storage to database
        /// </summary>
        /// <param name="tempOptimizationProblemID">Temporary optimization problem ID with addresses</param>
        /// <returns>If true, the addresses saved to the database</returns>
        public async Task<Tuple<bool, string>> SaveGeocodedAddressesToDatabaseAsync(string tempOptimizationProblemID)
        {
            var request = new GeocodingRequest();

            var json = "{\"optimization_problem_id\":" + tempOptimizationProblemID + "}";
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result =
                await GetJsonObjectFromAPIAsync<UploadAddressesToTemporaryStorageResponse>(
                    request,
                    R4MEInfrastructureSettings.SaveGeocodedAddresses,
                    HttpMethodType.Post,
                    content,
                    false,
                    false).ConfigureAwait(false);

            return new Tuple<bool, string>(result.Item1?.Status ?? false, result.Item2);
        }

        #endregion

        #region Vehicles

        /// <summary>
        ///     Creates a vehicle (deprecated)
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>A created vehicle </returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.CreateVehicle instead.")]
        public VehicleV4CreateResponse CreateVehicle(VehicleV4Parameters vehicle, out string errorString)
        {
            return GetJsonObjectFromAPI<VehicleV4CreateResponse>(vehicle,
                R4MEInfrastructureSettings.Vehicle_V4_API,
                HttpMethodType.Post,
                out errorString);
        }

        /// <summary>
        ///     Creates a vehicle (deprecated)
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <returns>A created vehicle </returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.CreateVehicleAsync instead.")]
        public Task<Tuple<VehicleV4CreateResponse, string>> CreateVehicleAsync(VehicleV4Parameters vehicle)
        {
            return GetJsonObjectFromAPIAsync<VehicleV4CreateResponse>(vehicle,
                R4MEInfrastructureSettings.Vehicle_V4_API,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Returns the array of the vehicles (deprecated)
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The VehiclesPaginated type object containing an array of the vehicles</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.GetVehicles instead.")]
        public Vehicle[] GetVehicles(VehicleParameters vehParams, out string errorString)
        {
            return GetJsonObjectFromAPI<Vehicle[]>(vehParams, R4MEInfrastructureSettings.Vehicle_V4,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Returns the array of the vehicles (deprecated)
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters </param>
        /// <returns> The VehiclesPaginated type object containing an array of the vehicles</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.GetVehiclesAsync instead.")]
        public Task<Tuple<Vehicle[], string>> GetVehiclesAsync(VehicleParameters vehParams)
        {
            return GetJsonObjectFromAPIAsync<Vehicle[]>(vehParams, R4MEInfrastructureSettings.Vehicle_V4,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Returns a vehicle (deprecated)
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> A vehicle </returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.GetVehicle instead.")]
        public VehicleV4Response GetVehicle(VehicleParameters vehParams, out string errorString)
        {
            if (vehParams == null || (vehParams.VehicleId?.Length ?? 0) != 32)
            {
                errorString = "The vehicle ID is not specified";
                return null;
            }

            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + @"/" + vehParams.VehicleId,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Returns a vehicle (deprecated)
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters </param>
        /// <returns> A vehicle </returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.GetVehicleAsync instead.")]
        public Task<Tuple<VehicleV4Response, string>> GetVehicleAsync(VehicleParameters vehParams)
        {
            if (vehParams == null || (vehParams.VehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<VehicleV4Response, string>(null, "The vehicle ID is not specified")) ;
            }

            return GetJsonObjectFromAPIAsync<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + @"/" + vehParams.VehicleId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Updates a vehicle (deprecated)
        /// </summary>
        /// <param name="vehParams">The VehicleV4Parameters type object as the request payload</param>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The updated vehicle</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.UpdateVehicle instead.")]
        public VehicleV4Response UpdateVehicle(VehicleV4Parameters vehParams, string vehicleId, out string errorString)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                errorString = "The vehicle ID is not specified";
                return null;
            }

            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + @"/" + vehicleId,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updates a vehicle (deprecated)
        /// </summary>
        /// <param name="vehParams">The VehicleV4Parameters type object as the request payload</param>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>The updated vehicle</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.UpdateVehicleAsync instead.")]
        public Task<Tuple<VehicleV4Response, string>> UpdateVehicleAsync(VehicleV4Parameters vehParams, string vehicleId)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                return new Task<Tuple<VehicleV4Response, string>>(null, "The vehicle ID is not specified");
            }

            return GetJsonObjectFromAPIAsync<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + @"/" + vehicleId,
                HttpMethodType.Put);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account (deprecated)
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters containing parameter VehicleId </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The removed vehicle</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.DeleteVehicle instead.")]
        public VehicleV4Response DeleteVehicle(VehicleV4Parameters vehParams, out string errorString)
        {
            if (vehParams == null || (vehParams.VehicleId?.Length ?? 0) != 32)
            {
                errorString = "The vehicle ID is not specified";
                return null;
            }

            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId,
                HttpMethodType.Delete,
                out errorString);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams"> The VehicleParameters type object as the query parameters containing parameter VehicleId </param>
        /// <returns>The removed vehicle</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.DeleteVehicleAsync instead.")]
        public Task<Tuple<VehicleV4Response, string>> DeleteVehicleAsync(VehicleV4Parameters vehParams)
        {
            if (vehParams == null || (vehParams.VehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<VehicleV4Response, string>(null, "The vehicle ID is not specified"));
            }

            return GetJsonObjectFromAPIAsync<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams">The VehicleParameters type object as the query parameters containing parameter VehicleId</param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The removed vehicle</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.DeleteVehicle instead.")]
        public VehicleV4Response DeleteVehicle(VehicleParameters vehParams, out string errorString)
        {
            if ((vehParams?.VehicleId?.Length ?? 0) != 32)
            {
                errorString = "The vehicle ID is not specified";
                return null;
            }

            return GetJsonObjectFromAPI<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehParams">The VehicleParameters type object as the query parameters containing parameter VehicleId</param>
        /// <returns>The removed vehicle</returns>
        [Obsolete("The method is obsolete, use the method VehicleManagerV5.DeleteVehicleAsync instead.")]
        public Task<Tuple<VehicleV4Response, string>> DeleteVehicleAsync(VehicleParameters vehParams)
        {
            if (vehParams == null || (vehParams.VehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<VehicleV4Response, string>(null, "The vehicle ID is not specified"));
            }

            return GetJsonObjectFromAPIAsync<VehicleV4Response>(vehParams,
                R4MEInfrastructureSettings.Vehicle_V4 + "/" + vehParams.VehicleId,
                HttpMethodType.Get);
        }

        #endregion

        #region Territories

        /// <summary>
        ///     Creates a territory
        /// </summary>
        /// <param name="avoidanceZoneParameters"> The AvoidanceZoneParameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The Territory type object </returns>
        public TerritoryZone CreateTerritory(AvoidanceZoneParameters avoidanceZoneParameters, out string errorString)
        {
            return GetJsonObjectFromAPI<TerritoryZone>(avoidanceZoneParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Post,
                out errorString);
        }

        /// <summary>
        ///     Creates a territory
        /// </summary>
        /// <param name="avoidanceZoneParameters"> The AvoidanceZoneParameters type object as the request payload </param>
        /// <returns> The Territory type object </returns>
        public Task<Tuple<TerritoryZone, string>> CreateTerritoryAsync(AvoidanceZoneParameters avoidanceZoneParameters)
        {
            return GetJsonObjectFromAPIAsync<TerritoryZone>(avoidanceZoneParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Creates a territory
        /// </summary>
        /// <param name="territoryZoneParameters"> The TerritoryZoneParameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The Territory type object </returns>
        public TerritoryZone CreateTerritory(TerritoryZoneParameters territoryZoneParameters, out string errorString)
        {
            return GetJsonObjectFromAPI<TerritoryZone>(territoryZoneParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Post,
                out errorString);
        }

        /// <summary>
        ///     Creates a territory
        /// </summary>
        /// <param name="territoryZoneParameters"> The TerritoryZoneParameters type object as the request payload </param>
        /// <returns> The Territory type object </returns>
        public Task<Tuple<TerritoryZone, string>> CreateTerritoryAsync(TerritoryZoneParameters territoryZoneParameters)
        {
            return GetJsonObjectFromAPIAsync<TerritoryZone>(territoryZoneParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Gets the territories by parameters
        /// </summary>
        /// <param name="avoidanceZoneQuery"> >The AvoidanceZoneQuery type object as the query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The teritories </returns>
        public TerritoryZone[] GetTerritories(AvoidanceZoneQuery avoidanceZoneQuery, out string errorString)
        {
            return GetJsonObjectFromAPI<TerritoryZone[]>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Gets the territories by parameters
        /// </summary>
        /// <param name="territoryQuery"> >The TerritoryQuery type object as the query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> The teritories </returns>
        public TerritoryZone[] GetTerritories(TerritoryQuery territoryQuery, out string errorString)
        {

            return GetJsonObjectFromAPI<TerritoryZone[]>(territoryQuery,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Gets the territories by parameters
        /// </summary>
        /// <param name="avoidanceZoneQuery"> >The AvoidanceZoneQuery type object as the query parameters </param>
        /// <returns> The teritories </returns>
        public Task<Tuple<AvoidanceZone[], string>> GetTerritoriesAsync(AvoidanceZoneQuery avoidanceZoneQuery)
        {
            return GetJsonObjectFromAPIAsync<AvoidanceZone[]>(avoidanceZoneQuery,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Gets the territories by parameters
        /// </summary>
        /// <param name="territoryQuery"> >The TerritoryQuery type object as the query parameters </param>
        /// <returns> The teritories </returns>
        public Task<Tuple<TerritoryZone[], string>> GetTerritoriesAsync(TerritoryQuery territoryQuery)
        {
            return GetJsonObjectFromAPIAsync<TerritoryZone[]>(territoryQuery,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Gets a territory by query parameters (TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The TerritoryQuery type object as query parmaeters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> A TerritoryZone type object </returns>
        public TerritoryZone GetTerritory(TerritoryQuery territoryQuery, out string errorString)
        {
            return GetJsonObjectFromAPI<TerritoryZone>(territoryQuery,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Gets a territory by query parameters (TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The TerritoryQuery type object as query parmaeters </param>
        /// <returns> A TerritoryZone type object </returns>
        public Task<Tuple<TerritoryZone, string>> GetTerritoryAsync(TerritoryQuery territoryQuery)
        {
            return GetJsonObjectFromAPIAsync<TerritoryZone>(territoryQuery,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Removes a trritory (by TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The AvoidanceZoneQuery type object as query parmaeters (TerritoryId) </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Result status: true/false </returns>
        public bool RemoveTerritory(AvoidanceZoneQuery territoryQuery, out string errorString)
        {
            var result = GetJsonObjectFromAPI<StatusResponse>(territoryQuery, R4MEInfrastructureSettings.Territory,
                HttpMethodType.Delete, out errorString);

            return result.Status;
        }

        /// <summary>
        ///     Removes a trritory (by TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The TerritoryQuery type object as query parmaeters (TerritoryId) </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Result status: true/false </returns>
        public bool RemoveTerritory(TerritoryQuery territoryQuery, out string errorString)
        {
            var result = GetJsonObjectFromAPI<StatusResponse>(territoryQuery, R4MEInfrastructureSettings.Territory,
                HttpMethodType.Delete, out errorString);

            return result.Status;
        }

        /// <summary>
        ///     Removes a trritory (by TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The AvoidanceZoneQuery type object as query parmaeters (TerritoryId) </param>
        /// <returns> Result status: true/false </returns>
        public async Task<Tuple<bool, string>> RemoveTerritoryAsync(AvoidanceZoneQuery territoryQuery)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(territoryQuery, R4MEInfrastructureSettings.Territory,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(result.Item1.Status, result.Item2);
        }

        /// <summary>
        ///     Removes a trritory (by TerritoryId)
        /// </summary>
        /// <param name="territoryQuery"> The TerritoryQuery type object as query parmaeters (TerritoryId) </param>
        /// <returns> Result status: true/false </returns>
        public async Task<Tuple<bool, string>> RemoveTerritoryAsync(TerritoryQuery territoryQuery)
        {
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(territoryQuery, R4MEInfrastructureSettings.Territory,
                HttpMethodType.Delete).ConfigureAwait(false);

            return new Tuple<bool, string>(result.Item1.Status, result.Item2);
        }

        /// <summary>
        ///     Updates a territory
        /// </summary>
        /// <param name="territoryParameters"> The AvoidanceZoneParameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Territory Object </returns>
        public AvoidanceZone UpdateTerritory(AvoidanceZoneParameters territoryParameters, out string errorString)
        {
            return GetJsonObjectFromAPI<AvoidanceZone>(territoryParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updates a territory
        /// </summary>
        /// <param name="territoryParameters"> The TerritoryZoneParameters type object as the request payload </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns> Territory Object </returns>
        public TerritoryZone UpdateTerritory(TerritoryZoneParameters territoryParameters, out string errorString)
        {
            return GetJsonObjectFromAPI<TerritoryZone>(territoryParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Put,
                out errorString);
        }

        /// <summary>
        ///     Updates a territory
        /// </summary>
        /// <param name="territoryParameters"> The AvoidanceZoneParameters type object as the request payload </param>
        /// <returns> Territory Object </returns>
        public Task<Tuple<AvoidanceZone, string>> UpdateTerritoryAsync(AvoidanceZoneParameters territoryParameters)
        {
            return GetJsonObjectFromAPIAsync<AvoidanceZone>(territoryParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Put);
        }

        /// <summary>
        ///     Updates a territory
        /// </summary>
        /// <param name="territoryParameters"> The TerritoryQuery type object as the request payload </param>
        /// <returns> Territory Object </returns>
        public Task<Tuple<TerritoryZone, string>> UpdateTerritoryAsync(TerritoryZoneParameters territoryParameters)
        {
            return GetJsonObjectFromAPIAsync<TerritoryZone>(territoryParameters,
                R4MEInfrastructureSettings.Territory,
                HttpMethodType.Put);
        }

        #endregion

        #region Telematics Vendors

        /// <summary>
        ///     Returns the telematics vendors
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The telematics vendors</returns>
        public TelematicsVendorsResponse GetAllTelematicsVendors(TelematicsVendorParameters vendorParams,
            out string errorString)
        {
            var result = GetJsonObjectFromAPI<TelematicsVendorsResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Returns the telematics vendors
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters </param>
        /// <returns>The telematics vendors</returns>
        public Task<Tuple<TelematicsVendorsResponse, string>> GetAllTelematicsVendorsAsync(TelematicsVendorParameters vendorParams)
        {
            return GetJsonObjectFromAPIAsync<TelematicsVendorsResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Returns a telematics vendor
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters (vendorID) </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>A telematics vendor</returns>
        public TelematicsVendorResponse GetTelematicsVendor(TelematicsVendorParameters vendorParams,
            out string errorString)
        {
            return GetJsonObjectFromAPI<TelematicsVendorResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Returns a telematics vendor
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters (vendorID) </param>
        /// <returns>A telematics vendor</returns>
        public Task<Tuple<TelematicsVendorResponse, string>> GetTelematicsVendorAsync(TelematicsVendorParameters vendorParams)
        {
            return GetJsonObjectFromAPIAsync<TelematicsVendorResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Searches for the telematics vendors
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters </param>
        /// <param name="errorString"> out: Error as string </param>
        /// <returns>The TelematicsVendorsSearchResponse type object containing found telematics vendors</returns>
        public TelematicsVendorsResponse SearchTelematicsVendors(TelematicsVendorParameters vendorParams,
            out string errorString)
        {
            return GetJsonObjectFromAPI<TelematicsVendorsResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Searches for the telematics vendors
        /// </summary>
        /// <param name="vendorParams"> The TelematicsVendorParameters type object as query parameters </param>
        /// <returns>The TelematicsVendorsSearchResponse type object containing found telematics vendors</returns>
        public Task<Tuple<TelematicsVendorsResponse, string>> SearchTelematicsVendorsAsync(TelematicsVendorParameters vendorParams)
        {
            return GetJsonObjectFromAPIAsync<TelematicsVendorsResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Register telematics member
        /// </summary>
        /// <param name="vendorParams">Parameters containing API key and member ID</param>
        /// <param name="errorString"> out: Error as string</param>
        /// <returns>Response with a member's API token</returns>
        public TelematicsRegisterMemberResponse RegisterTelematicsMember(TelematicsVendorParameters vendorParams,
            out string errorString)
        {
            vendorParams.PrepareForSerialization();

            return GetJsonObjectFromAPI<TelematicsRegisterMemberResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsRegisterHost,
                HttpMethodType.Get,
                out errorString);
        }

        /// <summary>
        ///     Register telematics member
        /// </summary>
        /// <param name="vendorParams">Parameters containing API key and member ID</param>
        /// <returns>Response with a member's API token</returns>
        public Task<Tuple<TelematicsRegisterMemberResponse, string>> RegisterTelematicsMemberAsync(TelematicsVendorParameters vendorParams)
        {
            vendorParams.PrepareForSerialization();

            return GetJsonObjectFromAPIAsync<TelematicsRegisterMemberResponse>(vendorParams,
                R4MEInfrastructureSettings.TelematicsRegisterHost,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get all telematics connections.
        /// </summary>
        /// <param name="vendorParams">Parameters containing API token</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>An array of the telematics connections</returns>
        public TelematicsConnection[] GetTelematicsConnections(TelematicsVendorParameters vendorParams,
            out string errorString)
        {
            var result = GetJsonObjectFromAPI<TelematicsConnection[]>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Get all telematics connections.
        /// </summary>
        /// <param name="vendorParams">Parameters containing API token</param>
        /// <returns>An array of the telematics connections</returns>
        public Task<Tuple<TelematicsConnection[], string>> GetTelematicsConnectionsAsync(TelematicsVendorParameters vendorParams)
        {
            return GetJsonObjectFromAPIAsync<TelematicsConnection[]>(vendorParams,
                R4MEInfrastructureSettings.TelematicsVendorsHost,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Create a telematics connection.
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionParams">Telematics connection parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Created telematics connection</returns>
        public TelematicsConnection CreateTelematicsConnection(string apiToken,
            TelematicsConnectionParameters connectionParams,
            out string errorString)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);

            var keyValues = new List<KeyValuePair<string, string>>();

            if (connectionParams.AccountId != null)
                keyValues.Add(new KeyValuePair<string, string>("account_id", connectionParams.AccountId));
            if (connectionParams.UserName != null)
                keyValues.Add(new KeyValuePair<string, string>("username", connectionParams.UserName));
            if (connectionParams.Password != null)
                keyValues.Add(new KeyValuePair<string, string>("password", connectionParams.Password));
            if (connectionParams.Host != null)
                keyValues.Add(new KeyValuePair<string, string>("host", connectionParams.Host));
            if (connectionParams.VendorID != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor_id", connectionParams.VendorID.ToString()));
            if (connectionParams.Name != null)
                keyValues.Add(new KeyValuePair<string, string>("name", connectionParams.Name));
            if (connectionParams.VehiclePositionRefreshRate != null)
                keyValues.Add(new KeyValuePair<string, string>("vehicle_position_refresh_rate",
                    connectionParams.VehiclePositionRefreshRate.ToString()));
            if (connectionParams.UserId != null)
                keyValues.Add(new KeyValuePair<string, string>("user_id", connectionParams.UserId.ToString()));
            if (connectionParams.ID != null)
                keyValues.Add(new KeyValuePair<string, string>("id", connectionParams.ID.ToString()));
            if (connectionParams.Vendor != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor", connectionParams.Vendor));

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = GetJsonObjectFromAPI<TelematicsConnection>
                (roParames, R4MEInfrastructureSettings.TelematicsConnection,
                    HttpMethodType.Post, httpContent, out errorString);

                return response;
            }
        }

        /// <summary>
        ///     Create a telematics connection.
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionParams">Telematics connection parameters</param>
        /// <returns>Created telematics connection</returns>
        public async Task<Tuple<TelematicsConnection, string>> CreateTelematicsConnectionAsync(string apiToken,
            TelematicsConnectionParameters connectionParams)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);

            var keyValues = new List<KeyValuePair<string, string>>();

            if (connectionParams.AccountId != null)
                keyValues.Add(new KeyValuePair<string, string>("account_id", connectionParams.AccountId));
            if (connectionParams.UserName != null)
                keyValues.Add(new KeyValuePair<string, string>("username", connectionParams.UserName));
            if (connectionParams.Password != null)
                keyValues.Add(new KeyValuePair<string, string>("password", connectionParams.Password));
            if (connectionParams.Host != null)
                keyValues.Add(new KeyValuePair<string, string>("host", connectionParams.Host));
            if (connectionParams.VendorID != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor_id", connectionParams.VendorID.ToString()));
            if (connectionParams.Name != null)
                keyValues.Add(new KeyValuePair<string, string>("name", connectionParams.Name));
            if (connectionParams.VehiclePositionRefreshRate != null)
                keyValues.Add(new KeyValuePair<string, string>("vehicle_position_refresh_rate",
                    connectionParams.VehiclePositionRefreshRate.ToString()));
            if (connectionParams.UserId != null)
                keyValues.Add(new KeyValuePair<string, string>("user_id", connectionParams.UserId.ToString()));
            if (connectionParams.ID != null)
                keyValues.Add(new KeyValuePair<string, string>("id", connectionParams.ID.ToString()));
            if (connectionParams.Vendor != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor", connectionParams.Vendor));

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = await GetJsonObjectFromAPIAsync<TelematicsConnection>
                (roParames, R4MEInfrastructureSettings.TelematicsConnection,
                    HttpMethodType.Post, httpContent, false, false).ConfigureAwait(false);

                return response;
            }
        }

        /// <summary>
        ///     Delete a telematics connection
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionToken">Connection token</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Deleted telematics connection</returns>
        public TelematicsConnection DeleteTelematicsConnection(string apiToken,
            string connectionToken,
            out string errorString)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);
            roParames.ParametersCollection.Add("connection_token", connectionToken);

            var result = GetJsonObjectFromAPI<TelematicsConnection>(roParames,
                R4MEInfrastructureSettings.TelematicsConnection,
                HttpMethodType.Delete,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Delete a telematics connection
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionToken">Connection token</param>
        /// <returns>Deleted telematics connection</returns>
        public Task<Tuple<TelematicsConnection, string>> DeleteTelematicsConnectionAsync(string apiToken,
            string connectionToken)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);
            roParames.ParametersCollection.Add("connection_token", connectionToken);

            return GetJsonObjectFromAPIAsync<TelematicsConnection>(roParames,
                R4MEInfrastructureSettings.TelematicsConnection,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Update telematics connection
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionToken">Connection token</param>
        /// <param name="connectionParams">Telematics connection parameters</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Updated telematics connection</returns>
        public TelematicsConnection UpdateTelematicsConnection(string apiToken,
            string connectionToken,
            TelematicsConnectionParameters connectionParams,
            out string errorString)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);
            roParames.ParametersCollection.Add("connection_token", connectionToken);

            var keyValues = new List<KeyValuePair<string, string>>();

            if (connectionParams.AccountId != null)
                keyValues.Add(new KeyValuePair<string, string>("account_id", connectionParams.AccountId));
            if (connectionParams.UserName != null)
                keyValues.Add(new KeyValuePair<string, string>("username", connectionParams.UserName));
            if (connectionParams.Password != null)
                keyValues.Add(new KeyValuePair<string, string>("password", connectionParams.Password));
            if (connectionParams.Host != null)
                keyValues.Add(new KeyValuePair<string, string>("host", connectionParams.Host));
            if (connectionParams.VendorID != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor_id", connectionParams.VendorID.ToString()));
            if (connectionParams.Name != null)
                keyValues.Add(new KeyValuePair<string, string>("name", connectionParams.Name));
            if (connectionParams.VehiclePositionRefreshRate != null)
                keyValues.Add(new KeyValuePair<string, string>("vehicle_position_refresh_rate",
                    connectionParams.VehiclePositionRefreshRate.ToString()));
            if (connectionParams.UserId != null)
                keyValues.Add(new KeyValuePair<string, string>("user_id", connectionParams.UserId.ToString()));
            if (connectionParams.Vendor != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor", connectionParams.Vendor));

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = GetJsonObjectFromAPI<TelematicsConnection>
                (roParames, R4MEInfrastructureSettings.TelematicsConnection,
                    HttpMethodType.Put, httpContent, out errorString);

                return response;
            }
        }

        /// <summary>
        ///     Update telematics connection
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionToken">Connection token</param>
        /// <param name="connectionParams">Telematics connection parameters</param>
        /// <returns>Updated telematics connection</returns>
        public async Task<Tuple<TelematicsConnection, string>> UpdateTelematicsConnectionAsync(string apiToken,
            string connectionToken,
            TelematicsConnectionParameters connectionParams)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);
            roParames.ParametersCollection.Add("connection_token", connectionToken);

            var keyValues = new List<KeyValuePair<string, string>>();

            if (connectionParams.AccountId != null)
                keyValues.Add(new KeyValuePair<string, string>("account_id", connectionParams.AccountId));
            if (connectionParams.UserName != null)
                keyValues.Add(new KeyValuePair<string, string>("username", connectionParams.UserName));
            if (connectionParams.Password != null)
                keyValues.Add(new KeyValuePair<string, string>("password", connectionParams.Password));
            if (connectionParams.Host != null)
                keyValues.Add(new KeyValuePair<string, string>("host", connectionParams.Host));
            if (connectionParams.VendorID != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor_id", connectionParams.VendorID.ToString()));
            if (connectionParams.Name != null)
                keyValues.Add(new KeyValuePair<string, string>("name", connectionParams.Name));
            if (connectionParams.VehiclePositionRefreshRate != null)
                keyValues.Add(new KeyValuePair<string, string>("vehicle_position_refresh_rate",
                    connectionParams.VehiclePositionRefreshRate.ToString()));
            if (connectionParams.UserId != null)
                keyValues.Add(new KeyValuePair<string, string>("user_id", connectionParams.UserId.ToString()));
            if (connectionParams.Vendor != null)
                keyValues.Add(new KeyValuePair<string, string>("vendor", connectionParams.Vendor));

            using (HttpContent httpContent = new FormUrlEncodedContent(keyValues))
            {
                var response = await GetJsonObjectFromAPIAsync<TelematicsConnection>
                (roParames, R4MEInfrastructureSettings.TelematicsConnection,
                    HttpMethodType.Put, httpContent, false, false).ConfigureAwait(false);

                return response;
            }
        }

        /// <summary>
        ///     Get a telematics connection
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionToken">Connection token</param>
        /// <param name="errorString">out: Error as string</param>
        /// <returns>Telematics connection</returns>
        public TelematicsConnection GetTelematicsConnection(string apiToken,
            string connectionToken,
            out string errorString)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);
            roParames.ParametersCollection.Add("connection_token", connectionToken);

            var result = GetJsonObjectFromAPI<TelematicsConnection>(roParames,
                R4MEInfrastructureSettings.TelematicsConnection,
                HttpMethodType.Get,
                out errorString);

            return result;
        }

        /// <summary>
        ///     Get a telematics connection
        /// </summary>
        /// <param name="apiToken">API token</param>
        /// <param name="connectionToken">Connection token</param>
        /// <returns>Telematics connection</returns>
        public Task<Tuple<TelematicsConnection, string>> GetTelematicsConnectionAsync(string apiToken,
            string connectionToken)
        {
            var roParames = new GenericParameters();
            roParames.ParametersCollection.Add("api_token", apiToken);
            roParames.ParametersCollection.Add("connection_token", connectionToken);

            return GetJsonObjectFromAPIAsync<TelematicsConnection>(roParames,
                R4MEInfrastructureSettings.TelematicsConnection,
                HttpMethodType.Get);
        }

        #endregion

        #endregion

        #region Generic Methods

        public string GetStringResponseFromAPI(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            out string errorMessage)
        {
            var result = GetJsonObjectFromAPI<string>(optimizationParameters,
                url,
                httpMethod,
                true,
                false,
                out errorMessage);

            return result;
        }

        public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            out string errorMessage)
            where T : class
        {
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                false,
                false,
                out errorMessage);

            return result;
        }

        public T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            out string errorMessage)
            where T : class
        {
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                httpContent,
                false,
                false,
                out errorMessage);

            return result;
        }

        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            bool isString,
            bool parseWithNewtonJson,
            out string errorMessage)
            where T : class
        {
            var result = GetJsonObjectFromAPI<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                isString,
                parseWithNewtonJson,
                out errorMessage);

            return result;
        }

        private Task<Tuple<T, string>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod)
            where T : class
        {
            var result = GetJsonObjectFromAPIAsync<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                false,
                false);
            return result;
        }

        private Task<Tuple<T, string>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            bool isString)
            where T : class
        {
            var result = GetJsonObjectFromAPIAsync<T>(optimizationParameters,
                url,
                httpMethod,
                null,
                isString,
                false);
            return result;
        }

        private async Task<Tuple<T, string>> GetJsonObjectFromAPIAsync<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool isString,
            bool parseWithNewtonJson)
            where T : class
        {
            T result = default;
            var errorMessage = string.Empty;

            var parametersUri = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersUri}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                        {
                            var response = await httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery).ConfigureAwait(false);

                            if (isString)
                            {
                                result = response.ReadString() as T;
                            }
                            else
                            {
                                result = parseWithNewtonJson
                                    ? response.ReadObjectNew<T>()
                                    : response.ReadObject<T>();
                            }
                            break;
                        }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Delete:
                        {
                            var isPut = httpMethod == HttpMethodType.Put;
                            var isDelete = httpMethod == HttpMethodType.Delete;
                            HttpContent content;
                            if (httpContent != null)
                            {
                                content = httpContent;
                            }
                            else
                            {
                                var jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                content = new StringContent(jsonString);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            }

                            HttpResponseMessage response;

                            if (isPut)
                            {
                                response = await httpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content).ConfigureAwait(false);
                            }
                            else if (isDelete)
                            {
                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                response = await httpClientHolder.HttpClient.SendAsync(request).ConfigureAwait(false);
                            }
                            else
                            {
                                response = await httpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content)
                                    .ConfigureAwait(false);
                            }

                            // Check if successful
                            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK ||
                                response.StatusCode == HttpStatusCode.RedirectMethod)
                            {
                                var streamTask = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                                if (isString)
                                {
                                    result = streamTask.ReadString() as T;
                                }
                                else
                                {
                                    result = parseWithNewtonJson
                                        ? streamTask.ReadObjectNew<T>()
                                        : streamTask.ReadObject<T>();
                                }
                            }
                            else
                            {
                                using (var streamTask = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                                {
                                    ErrorResponse errorResponse;

                                    try
                                    {
                                        errorResponse = streamTask.ReadObject<ErrorResponse>();
                                    }
                                    catch (Exception)
                                    {
                                        if (response.ReasonPhrase != null)
                                        {
                                            errorResponse = new ErrorResponse
                                            {
                                                Errors = new List<string> {response.ReasonPhrase}
                                            };

                                            var reqMessage = response.RequestMessage?.Content.ReadAsStringAsync()
                                                .Result ?? "";

                                            if (reqMessage != "")
                                                errorResponse.Errors.Add(
                                                    $"Request content: {Environment.NewLine} {reqMessage}");
                                        }
                                        else
                                        {
                                            errorResponse = default;
                                        }
                                    }

                                    if (errorResponse?.Errors != null && errorResponse.Errors.Count > 0)
                                    {
                                        foreach (var error in errorResponse.Errors)
                                        {
                                            if (errorMessage.Length > 0)
                                                errorMessage += "; ";
                                            errorMessage += error;
                                        }
                                    }
                                    else
                                    {
                                        var responseStream = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                        var responseString = responseStream;
                                        if (responseString != null)
                                            errorMessage = "Response: " + responseString;
                                    }
                                }
                            }

                            response.Dispose();
                            break;
                        }
                    }
                }
            }
            catch (HttpListenerException e)
            {
                errorMessage = e.Message + " --- " + errorMessage;
                result = null;
            }
            catch (Exception e)
            {
                errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;
                result = default;
            }

            return new Tuple<T, string>(result, errorMessage);
        }


        private T GetJsonObjectFromAPI<T>(GenericParameters optimizationParameters,
            string url,
            HttpMethodType httpMethod,
            HttpContent httpContent,
            bool isString,
            bool parseWithNewtonJson,
            out string errorMessage)
            where T : class
        {
            T result = default;
            errorMessage = string.Empty;

            // Get the parameters
            var hasApiKey = optimizationParameters
                .GetType()
                .GetProperties()
                .FirstOrDefault(x => x.Name == "ApiKey" || x.Name == "api_key")
                ?.GetValue(optimizationParameters) != null;
            var parametersUri = hasApiKey
                ? optimizationParameters.Serialize(string.Empty)
                : optimizationParameters.Serialize(_mApiKey);
            parametersUri = parametersUri.Replace("?&", "?");
            var uri = new Uri($"{url}{parametersUri}");

            try
            {
                using (var acquireHttpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                        {
                            var response = acquireHttpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery);
                            response.Wait();

                            if (isString)
                            {
                                result = response.Result.ReadString() as T;
                            }
                            else
                            {
                                result = parseWithNewtonJson
                                    ? response.Result.ReadObjectNew<T>()
                                    : response.Result.ReadObject<T>();
                            }
                            break;
                        }
                        case HttpMethodType.Post:
                        case HttpMethodType.Put:
                        case HttpMethodType.Delete:
                        {
                            var isPut = httpMethod == HttpMethodType.Put;
                            var isDelete = httpMethod == HttpMethodType.Delete;
                            HttpContent content;
                            if (httpContent != null)
                            {
                                content = httpContent;
                            }
                            else
                            {
                                var jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                content = new StringContent(jsonString);
                                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            }

                            Task<HttpResponseMessage> response;

                            if (isPut)
                            {
                                response = acquireHttpClientHolder.HttpClient.PutAsync(uri.PathAndQuery, content);
                            }
                            else if (isDelete)
                            {
                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                response = acquireHttpClientHolder.HttpClient.SendAsync(request);
                            }
                            else
                            {
                                using (var cts = new CancellationTokenSource())
                                {
                                    cts.CancelAfter(1000 * 60 * 5); // 3 seconds

                                    response = acquireHttpClientHolder.HttpClient.PostAsync(uri.PathAndQuery, content,
                                        cts.Token);
                                }
                            }

                            // Wait for response
                            response.Wait();

                            // Check if successful
                            if (response.IsCompleted &&
                                response.Result.IsSuccessStatusCode &&
                                response.Result.StatusCode == HttpStatusCode.OK)
                            {
                                var streamTask = response.Result.Content.ReadAsStreamAsync();
                                streamTask.Wait();

                                if (streamTask.IsCompleted)
                                {
                                    if (isString)
                                    {
                                        result = streamTask.Result.ReadString() as T;
                                    }
                                    else
                                    {
                                        result = parseWithNewtonJson
                                            ? streamTask.Result.ReadObjectNew<T>()
                                            : streamTask.Result.ReadObject<T>();
                                    }
                                }
                            }
                            else
                            {
                                ErrorResponse errorResponse;

                                try
                                {
                                    var streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
                                    streamTask.Wait();

                                    errorResponse = streamTask.Result.ReadObject<ErrorResponse>();
                                }
                                catch (Exception) // If cannot read ErrorResponse from the stream, try another way
                                {
                                    if ((response.Result?.ReasonPhrase) != null)
                                    {
                                        errorResponse = new ErrorResponse
                                        {
                                            Errors = new List<string> {response.Result.ReasonPhrase}
                                        };

                                        var reqMessage = response.Result?.RequestMessage?.Content?.ReadAsStringAsync()
                                            .Result ?? "";

                                        if (reqMessage != "")
                                            errorResponse.Errors.Add(
                                                $"Request content: {Environment.NewLine} {reqMessage}");
                                    }
                                    else
                                    {
                                        errorResponse = default;
                                    }
                                }

                                if (errorResponse?.Errors != null && errorResponse.Errors.Count > 0)
                                {
                                    foreach (var error in errorResponse.Errors)
                                    {
                                        if (errorMessage.Length > 0)
                                            errorMessage += "; ";
                                        errorMessage += error;
                                    }
                                }
                                else
                                {
                                    if (response.Result != null)
                                    {
                                        var responseStream = response.Result.Content.ReadAsStringAsync();
                                        responseStream.Wait();
                                        var responseString = responseStream.Result;
                                        if (responseString != null)
                                            errorMessage = "Response: " + responseString;
                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }
            catch (HttpListenerException e)
            {
                errorMessage = e.Message + " --- " + errorMessage;
                result = null;
            }
            catch (Exception e)
            {
                errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;
                result = default;
            }

            return result;
        }

        private string GetXmlObjectFromAPI(GenericParameters optimizationParameters, string url,
            HttpMethodType httpMethod, HttpContent httpContent, bool isString, out string errorMessage)
        {
            var result = string.Empty;
            errorMessage = string.Empty;

            var parametersUri = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersUri}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                            if (true)
                            {
                                var response = httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery);
                                response.Wait();

                                if (response.IsCompleted)
                                    result = isString
                                        ? response.Result.ReadString()
                                        : response.Result.ReadObject<string>();
                            }

                            break;
                        case HttpMethodType.Post:
                            if (true)
                            {
                                var response = httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery);
                                response.Wait();

                                if (response.IsCompleted)
                                    result = isString
                                        ? response.Result.ReadString()
                                        : response.Result.ReadObject<string>();
                            }

                            break;
                        case HttpMethodType.Put:
                            break;
                        case HttpMethodType.Delete:
                            if (true)
                            {
                                HttpContent content;
                                if (httpContent != null)
                                {
                                    content = httpContent;
                                }
                                else
                                {
                                    var jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                    content = new StringContent(jsonString);
                                }

                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                var response = httpClientHolder.HttpClient.SendAsync(request);

                                // Wait for response
                                response.Wait();

                                // Check if successful
                                if (response.IsCompleted && response.Result.IsSuccessStatusCode &&
                                    response.Result.Content is StreamContent)
                                {
                                    var streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
                                    streamTask.Wait();

                                    if (streamTask.IsCompleted) result = streamTask.Result.ReadString();
                                }
                                else
                                {
                                    ErrorResponse errorResponse;

                                    try
                                    {
                                        var streamTask = ((StreamContent) response.Result.Content).ReadAsStreamAsync();
                                        streamTask.Wait();

                                        errorResponse = streamTask.Result.ReadObject<ErrorResponse>();
                                    }
                                    catch (Exception)
                                    {
                                        if (response.Result.ReasonPhrase != null)
                                        {
                                            errorResponse = new ErrorResponse
                                            {
                                                Errors = new List<string> {response.Result.ReasonPhrase}
                                            };

                                            var reqMessage = response.Result?.RequestMessage?.Content
                                                .ReadAsStringAsync().Result ?? "";

                                            if (reqMessage != "")
                                                errorResponse.Errors.Add(
                                                    $"Request content: {Environment.NewLine} {reqMessage}");
                                        }
                                        else
                                        {
                                            errorResponse = default;
                                        }
                                    }

                                    if (errorResponse?.Errors != null && errorResponse.Errors.Count > 0)
                                    {
                                        foreach (var error in errorResponse.Errors)
                                        {
                                            if (errorMessage.Length > 0) errorMessage += "; ";
                                            errorMessage += error;
                                        }
                                    }
                                    else
                                    {
                                        var responseStream = response.Result.Content.ReadAsStringAsync();
                                        responseStream.Wait();
                                        var responseString = responseStream.Result;
                                        if (responseString != null) errorMessage = "Response: " + responseString;
                                    }
                                }
                            }

                            break;
                    }
                }
            }
            catch (HttpListenerException e)
            {
                errorMessage = e.Message + " --- " + errorMessage;
                result = null;
            }
            catch (Exception e)
            {
                errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;

                result = null;
            }

            return result;
        }

        private async Task<Tuple<T, string>> GetXmlObjectFromAPIAsync<T>(GenericParameters optimizationParameters, string url,
            HttpMethodType httpMethod, HttpContent httpContent, bool isString)
            where T : class
        {
            T result = default(T);
            var errorMessage = string.Empty;

            var parametersUri = optimizationParameters.Serialize(_mApiKey);
            var uri = new Uri($"{url}{parametersUri}");

            try
            {
                using (var httpClientHolder =
                    HttpClientHolderManager.AcquireHttpClientHolder(uri.GetLeftPart(UriPartial.Authority)))
                {
                    switch (httpMethod)
                    {
                        case HttpMethodType.Get:
                            if (true)
                            {
                                var response = await httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery).ConfigureAwait(false);

                                result = isString
                                    ? response.ReadString() as T
                                    : response.ReadObject<T>();
                            }
                            break;
                        case HttpMethodType.Post:
                            if (true)
                            {
                                var response = await httpClientHolder.HttpClient.GetStreamAsync(uri.PathAndQuery).ConfigureAwait(false);
                                result = isString
                                    ? response.ReadString() as T
                                    : response.ReadObject<T>();
                            }

                            break;
                        case HttpMethodType.Put:
                            break;
                        case HttpMethodType.Delete:
                            if (true)
                            {
                                HttpContent content;
                                if (httpContent != null)
                                {
                                    content = httpContent;
                                }
                                else
                                {
                                    var jsonString = R4MeUtils.SerializeObjectToJson(optimizationParameters);
                                    content = new StringContent(jsonString);
                                }

                                var request = new HttpRequestMessage
                                {
                                    Content = content,
                                    Method = HttpMethod.Delete,
                                    RequestUri = new Uri(uri.PathAndQuery, UriKind.Relative)
                                };
                                var response = await httpClientHolder.HttpClient.SendAsync(request).ConfigureAwait(false);

                                // Check if successful
                                if (response.IsSuccessStatusCode && response.Content is StreamContent)
                                {
                                    var stream = await ((StreamContent)response.Content).ReadAsStreamAsync().ConfigureAwait(false);

                                     result = stream.ReadString() as T;
                                }
                                else
                                {
                                    ErrorResponse errorResponse;

                                    try
                                    {
                                        var stream = await ((StreamContent)response.Content).ReadAsStreamAsync().ConfigureAwait(false);

                                        errorResponse = stream.ReadObject<ErrorResponse>();
                                    }
                                    catch (Exception)
                                    {
                                        if (response.ReasonPhrase != null)
                                        {
                                            errorResponse = new ErrorResponse
                                            {
                                                Errors = new List<string> { response.ReasonPhrase }
                                            };

                                            var reqMessage = response.RequestMessage?.Content.ReadAsStringAsync()
                                                .Result ?? "";

                                            if (reqMessage != "")
                                                errorResponse.Errors.Add(
                                                    $"Request content: {Environment.NewLine} {reqMessage}");
                                        }
                                        else
                                        {
                                            errorResponse = default;
                                        }
                                    }

                                    if (errorResponse?.Errors != null && errorResponse.Errors.Count > 0)
                                    {
                                        foreach (var error in errorResponse.Errors)
                                        {
                                            if (errorMessage.Length > 0) errorMessage += "; ";
                                            errorMessage += error;
                                        }
                                    }
                                    else
                                    {
                                        var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                        if (responseString != null) errorMessage = "Response: " + responseString;
                                    }
                                }
                            }

                            break;
                    }
                }
            }
            catch (HttpListenerException e)
            {
                errorMessage = e.Message + " --- " + errorMessage;
                result = null;
            }
            catch (Exception e)
            {
                errorMessage = e is AggregateException ? e.InnerException.Message : e.Message;

                result = null;
            }

            return new Tuple<T, string>(result, errorMessage);
        }

        #endregion

        #endregion
    }
}