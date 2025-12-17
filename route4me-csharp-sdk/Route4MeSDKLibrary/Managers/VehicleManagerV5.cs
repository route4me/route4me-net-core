using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

using RouteParametersQuery = Route4MeSDK.QueryTypes.V5.RouteParametersQuery;
using VehicleParameters = Route4MeSDK.QueryTypes.V5.VehicleParameters;

namespace Route4MeSDKLibrary.Managers
{
    public class VehicleManagerV5 : Route4MeManagerBase
    {
        public VehicleManagerV5(string apiKey) : base(apiKey)
        {
        }

        public VehicleManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        /// <summary>
        ///     Get the paginated list of Vehicles.
        /// </summary>
        /// <param name="vehicleParams">Query params</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetVehicles(GetVehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///     Get the paginated list of Vehicles.
        /// </summary>
        /// <param name="vehicleParams">Query params</param>
        /// <returns>An array of the vehicles</returns>
        public async Task<Tuple<Vehicle[], ResultResponse>> GetVehiclesAsync(GetVehicleParameters vehicleParams)
        {
            var result = await GetJsonObjectFromAPIAsync<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<Vehicle[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///     Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The created vehicle </returns>
        public Vehicle CreateVehicle(Vehicle vehicle, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Vehicle>(
                vehicle,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Post,
                out resultResponse);
        }

        /// <summary>
        ///     Creates a vehicle
        /// </summary>
        /// <param name="vehicle">The VehicleV4Parameters type object as the request payload </param>
        /// <returns>The created vehicle </returns>
        public Task<Tuple<Vehicle, ResultResponse>> CreateVehicleAsync(Vehicle vehicle)
        {
            return GetJsonObjectFromAPIAsync<Vehicle>(
                vehicle,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public VehiclesResponse GetPaginatedVehiclesList(VehicleParameters vehicleParams, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<VehiclesResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehiclePaginated,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///     Returns the VehiclesPaginated type object containing an array of the vehicles
        /// </summary>
        /// <param name="vehicleParams">The VehicleParameters type object as the query parameters</param>
        /// <returns>An array of the vehicles</returns>
        public Task<Tuple<Vehicle[], ResultResponse>> GetPaginatedVehiclesListAsync(VehicleParameters vehicleParams)
        {
            return GetJsonObjectFromAPIAsync<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehiclePaginated,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehicleId"> The Vehicle ID </param>
        /// <param name="resultResponse"> Failing response </param>
        /// <returns>The removed vehicle</returns>
        public Vehicle DeleteVehicle(string vehicleId, out ResultResponse resultResponse)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            return GetJsonObjectFromAPI<Vehicle>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Delete,
                out resultResponse);
        }

        /// <summary>
        ///     Removes a vehicle from a user's account
        /// </summary>
        /// <param name="vehicleId"> The Vehicle ID </param>
        /// <returns>The removed vehicle</returns>
        public Task<Tuple<Vehicle, ResultResponse>> DeleteVehicleAsync(string vehicleId)
        {
            if ((vehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<Vehicle, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<Vehicle>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>A result with an order ID</returns>
        public VehicleTemporary CreateTemporaryVehicle(VehicleTemporary vehParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleTemporary>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleTemporary,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Creates temporary vehicle in the database.
        /// </summary>
        /// <param name="vehParams">Request parameters for creating a temporary vehicle</param>
        /// <returns>A result with an order ID</returns>
        public Task<Tuple<VehicleTemporary, ResultResponse>> CreateTemporaryVehicleAsync(VehicleTemporary vehParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleTemporary>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleTemporary,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created vehicle order</returns>
        public VehicleOrderResponse ExecuteVehicleOrder(VehicleOrderParameters vehOrderParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleOrderResponse>(
                vehOrderParams,
                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Execute a vehicle order
        /// </summary>
        /// <param name="vehOrderParams">Vehicle order parameters</param>
        /// <returns>Created vehicle order</returns>
        public Task<Tuple<VehicleOrderResponse, ResultResponse>> ExecuteVehicleOrderAsync(VehicleOrderParameters vehOrderParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleOrderResponse>(
                vehOrderParams,
                R4MEInfrastructureSettingsV5.VehicleExecuteOrder,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle object</returns>
        public Vehicle GetVehicleById(string vehicleId, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>Vehicle object</returns>
        public Task<Tuple<Vehicle, ResultResponse>> GetVehicleByIdAsync(string vehicleId)
        {
            return GetJsonObjectFromAPIAsync<Vehicle>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get vehicle by license plate.
        /// </summary>
        /// <param name="licensePlate">Vehicle parameter containing vehicle license plate</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle</returns>
        public VehicleResponse GetVehicleByLicensePlate(string licensePlate,
            out ResultResponse resultResponse)
        {
            if ((licensePlate?.Length ?? 0) < 2)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
                    }
                };

                return null;
            }

            //var vehicleParams = new VehicleParameters()
            //{
            //    VehicleLicensePlate = licensePlate
            //};

            var gparams = new GenericParameters();
            gparams.ParametersCollection.Add("vehicle_license_plate", licensePlate);

            var result = GetJsonObjectFromAPI<VehicleResponse>(
                gparams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle by license plate asynchronously.
        /// </summary>
        /// <param name="licensePlate">Vehicle parameter containing vehicle license plate</param>
        /// <returns>Vehicle</returns>
        public Task<Tuple<VehicleResponse, ResultResponse, string>> GetVehicleByLicensePlateAsync(string licensePlate)
        {
            if ((licensePlate?.Length ?? 0) < 2)
            {
                return Task.FromResult(new Tuple<VehicleResponse, ResultResponse, string>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle license plate is not specified"}}
                    }
                },
                null));
            }

            var vehicleParams = new VehicleParameters()
            {
                VehicleLicensePlate = licensePlate
            };

            return GetJsonObjectFromAPIAsync<VehicleResponse>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.VehicleLicense,
                HttpMethodType.Get,
                null,
                true,
                false);
        }

        /// <summary>
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle </returns>
        public Vehicle UpdateVehicle(Vehicle vehicleParams, out ResultResponse resultResponse)
        {
            if (vehicleParams == null || (vehicleParams.VehicleId?.Length ?? 0) != 32)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<Vehicle>(
                genParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParams.VehicleId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update a vehicle
        /// </summary>
        /// <param name="vehicleParams">Vehicle body parameters</param>
        /// <returns>Updated vehicle </returns>
        public Task<Tuple<Vehicle, ResultResponse, string>> UpdateVehicleAsync(Vehicle vehicleParams)
        {
            if (vehicleParams == null || (vehicleParams.VehicleId?.Length ?? 0) != 32)
            {
                return Task.FromResult(new Tuple<Vehicle, ResultResponse, string>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                },
                null));
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(vehicleParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<Vehicle>(
                genParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParams.VehicleId,
                HttpMethodType.Patch,
                content,
                false,
                false);
        }

        /// <summary>
        /// Get the vehicles by their state.
        /// </summary>
        /// <param name="vehicleState">Vehicle state. <see cref="VehicleStates" /> </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the vehicles</returns>
        public Vehicle[] GetVehiclesByState(VehicleStates vehicleState, out ResultResponse resultResponse)
        {
            var vehicleParams = new VehicleParameters()
            {
                Show = vehicleState.Description()
            };

            var result = GetJsonObjectFromAPI<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get asynchronously the vehicles by their state.
        /// </summary>
        /// <param name="vehicleState">Vehicle state <see cref="VehicleStates"/></param>
        /// <returns>>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> GetVehiclesByStateAsync(VehicleStates vehicleState)
        {
            var vehicleParams = new GenericParameters();
            vehicleParams.ParametersCollection.Add("show", vehicleState.Description());


            var result = GetJsonObjectFromAPIAsync<Vehicle[]>(
                vehicleParams,
                R4MEInfrastructureSettingsV5.Vehicles,
                HttpMethodType.Get,
                null,
                false,
                false);

            return result;
        }


        #region Bulk Vehicle Operations

        /// <summary>
        /// Activates deactivated vehicles
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Activated vehicles</returns>
        public VehiclesResults ActivateVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectFromAPI<VehiclesResults>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkActivate,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Activates deactivated vehicles asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<VehiclesResults, ResultResponse, string>> ActivateVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();

            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPIAsync<VehiclesResults>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkActivate,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        ///     Deactivates active vehicles
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Deactivated vehicles</returns>
        public Vehicle[] DeactivateVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectFromAPI<Vehicle[]>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDeactivate,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Deactivates active vehicles asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>A Tuple type object containing a vehicle list or/and failure response</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> DeactivateVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPIAsync<Vehicle[]>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDeactivate,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        ///     Updates multiple vehicles at once
        /// </summary>
        /// <param name="vehicleArray">An array of the vehicles</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>Status response</returns>
        public StatusResponse UpdateVehicles(Vehicles vehicleArray, out ResultResponse resultResponse)
        {

            var result = GetJsonObjectFromAPI<StatusResponse>(
                vehicleArray,
                R4MEInfrastructureSettingsV5.VehicleBulkUpdate,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Updates multiple vehicles at once asynchronously.
        /// </summary>
        /// <param name="vehicleArray">An object containing an array of the vehicles</param>
        /// <returns>Returns a result as a tuple object:
        /// - object: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<object, ResultResponse, string>> UpdateVehiclesAsync(Vehicles vehicleArray)
        {

            var result = GetJsonObjectAndJobFromAPIAsync<object>(
                vehicleArray,
                R4MEInfrastructureSettingsV5.VehicleBulkUpdate,
                HttpMethodType.Post);

            return result;
        }

        /// <summary>
        /// Removes multiple vehicles at once.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Status of the update operation</returns>
        public StatusResponse DeleteVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPI<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDelete,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);
        }

        /// <summary>
        /// Removes multiple vehicles at once asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Returns a result as a tuple object:
        /// - Status response: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> DeleteVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectAndJobFromAPIAsync<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkDelete,
                HttpMethodType.Post);

            return result;
        }

        /// <summary>
        /// Restore multiple vehicles at once.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>StatusResponse: if success, this items is not null
        /// ResultResponse: if failed, this items is not null
        /// </returns>
        public StatusResponse RestoreVehicles(string[] vehicleIDs, out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            return GetJsonObjectFromAPI<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkRestore,
                HttpMethodType.Post,
                null,
                false,
                false,
                out resultResponse);
        }

        /// <summary>
        /// Restore multiple vehicles at once asynchronously.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Returns a result as a tuple object:
        /// - StatusResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> RestoreVehiclesAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("vehicle_ids[]", vehicleId);
            }

            var result = GetJsonObjectAndJobFromAPIAsync<StatusResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleBulkRestore,
                HttpMethodType.Post);

            return result;
        }

        /// <summary>
        /// Returns vehicle job result.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The object containing status of the JOB</returns>
        public StatusResponse GetVehicleJobResult(string jobId, out ResultResponse resultResponse)
        {
            var emptyParams = new GenericParameters();

            var result = GetJsonObjectFromAPI<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobResult + "/" + jobId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a vehicle job result.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>Returns a result as a tuple object:
        /// - StatusResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<StatusResponse, ResultResponse, string>> GetVehicleJobResultAsync(string jobId)
        {
            var emptyParams = new GenericParameters();

            var result = GetJsonObjectFromAPIAsync<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobResult + "/" + jobId,
                HttpMethodType.Get,
                null,
                false,
                false);

            return result;
        }

        /// <summary>
        /// Returns vehicle job status.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The object containing status of the JOB</returns>
        public StatusResponse GetVehicleJobStatus(string jobId, out ResultResponse resultResponse)
        {
            var emptyParams = new GenericParameters();

            var result = GetJsonObjectFromAPI<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobStatus + "/" + jobId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously retrieves a vehicle job status.
        /// </summary>
        /// <param name="jobId">Job ID</param>
        /// <returns>Returns a job status </returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> GetVehicleJobStatusAsync(string jobId)
        {
            var emptyParams = new GenericParameters();

            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(
                emptyParams,
                R4MEInfrastructureSettingsV5.VehicleJobStatus + "/" + jobId,
                HttpMethodType.Get,
                null,
                false,
                false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        #endregion

        #region Vehicle Tracking

        /// <summary>
        ///     Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Data with vehicles</returns>
        public VehicleLocationResponse GetVehicleLocations(VehicleParameters vehParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleLocationResponse>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///  Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <param name="resultResponse">Failure response</param>
        /// <returns>A VehicleLocationResponse type object <see cref="VehicleLocationResponse"/></returns>
        public VehicleLocationResponse GetVehicleLocations(string[] vehicleIDs,
            out ResultResponse resultResponse)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("ids[]", vehicleId);
            }

            var result = GetJsonObjectFromAPI<VehicleLocationResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Asynchronously get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehicleIDs">An array of the vehicle IDs</param>
        /// <returns>Returns a result as a tuple object:
        /// - VehicleLocationResponse: if success, this items is not null
        /// - ResultResponse: if failed, this items is not null
        /// - string: if success, this items is a job ID</returns>
        public Task<Tuple<VehicleLocationResponse, ResultResponse, string>> GetVehicleLocationsAsync(string[] vehicleIDs)
        {
            var parameters = new GenericParameters();
            foreach (var vehicleId in vehicleIDs)
            {
                parameters.ParametersCollection.Add("ids[]", vehicleId);
            }

            return GetJsonObjectFromAPIAsync<VehicleLocationResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get,
                null,
                false,
                false);
        }

        /// <summary>
        ///     Get latest vehicle locations by specified vehicle IDs.
        /// </summary>
        /// <param name="vehParams">Vehicle query parameters containing vehicle IDs.</param>
        /// <returns>Data with vehicles</returns>
        public Task<Tuple<VehicleLocationResponse, ResultResponse>> GetVehicleLocationsAsync(VehicleParameters vehParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleLocationResponse>(
                vehParams,
                R4MEInfrastructureSettingsV5.VehicleLocation,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleParameters">Vehicle ID</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle track object</returns>
        public VehicleTrackResponse GetVehicleTrack(VehicleParameters vehicleParameters, out ResultResponse resultResponse)
        {
            var newParams = new VehicleParameters()
            {
                Start = vehicleParameters.Start,
                End = vehicleParameters.End
            };

            var result = GetJsonObjectFromAPI<VehicleTrackResponse>(
                newParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParameters.VehicleId + "/" + "track",
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get the Vehicle track by specifying vehicle ID.
        /// </summary>
        /// <param name="vehicleParameters">Vehicle param</param>
        /// <returns>Vehicle track object</returns>
        public Task<Tuple<VehicleTrackResponse, ResultResponse>> GetVehicleTrackAsync(VehicleParameters vehicleParameters)
        {
            var newParams = new VehicleParameters()
            {
                Start = vehicleParameters.Start,
                End = vehicleParameters.End
            };

            return GetJsonObjectFromAPIAsync<VehicleTrackResponse>(
                newParams,
                R4MEInfrastructureSettingsV5.Vehicles + "/" + vehicleParameters.VehicleId + "/" + "track",
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>An array of the found vehicles</returns>
        public Vehicle[] SearchVehicles(VehicleSearchParameters searchParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle[]>(
                searchParams,
                R4MEInfrastructureSettingsV5.VehicleSearch,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     asynchronously search vehicles by sending request body.
        /// </summary>
        /// <param name="searchParams">Search parameters</param>
        /// <returns>Returns a result as a tuple object:
        /// - Vehicle[]: if success, this item is not null
        /// - ResultResponse: if failed, this item is not null
        /// - string: if success, this item is a job ID</returns>
        public Task<Tuple<Vehicle[], ResultResponse, string>> SearchVehiclesAsync(VehicleSearchParameters searchParams)
        {
            return GetJsonObjectFromAPIAsync<Vehicle[]>(
                searchParams,
                R4MEInfrastructureSettingsV5.VehicleSearch,
                HttpMethodType.Post,
                null,
                false,
                false);
        }

        /// <summary>
        /// Sync the Vehicle by sending the corresponding body payload.
        /// </summary>
        /// <param name="syncParams">Query parameters <see cref="VehicleTelematicsSync"/></param>
        /// <param name="resultResponse"></param>
        /// <returns>Synchronized vehicle</returns>
        public Vehicle SyncPendingTelematicsData(VehicleTelematicsSync syncParams, out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<Vehicle>(
                syncParams,
                R4MEInfrastructureSettingsV5.VehicleSyncTelematics,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }


        public Task<Tuple<Vehicle, ResultResponse, string>> SyncPendingTelematicsDataAsync(VehicleTelematicsSync syncParams)
        {
            var result = GetJsonObjectFromAPIAsync<Vehicle>(
                syncParams,
                R4MEInfrastructureSettingsV5.VehicleSyncTelematics,
                HttpMethodType.Post,
                null,
                false,
                false);

            return result;
        }

        #endregion


        #region Vehicle Profiles

        /// <summary>
        ///     Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleProfilesResponse GetVehicleProfiles(VehicleProfileParameters profileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfilesResponse>(
                profileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get paginated list of the vehicle profiles.
        /// </summary>
        /// <param name="profileParams">Vehicle profile request parameters</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public Task<Tuple<VehicleProfilesResponse, ResultResponse>> GetVehicleProfilesAsync(VehicleProfileParameters profileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfilesResponse>(
                profileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created vehicle profile</returns>
        public VehicleProfile CreateVehicleProfile(VehicleProfile vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleProfile>(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Create a vehicle profile.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile body parameters</param>
        /// <returns>Created vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse, string>> CreateVehicleProfileAsync(VehicleProfile vehicleProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                vehicleProfileParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles,
                HttpMethodType.Post,
                null,
                true,
                false);
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle profile</returns>
        public VehicleProfile DeleteVehicleProfile(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleProfile>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Remove a vehicle profile from database.
        ///     TO DO: adjust response structure.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <returns>Removed vehicle profile</returns>
        public Task<Tuple<object, ResultResponse>> DeleteVehicleProfileAsync(VehicleProfileParameters vehicleProfileParams)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                return Task.FromResult(new Tuple<object, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<object>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle profile</returns>
        public VehicleProfile GetVehicleProfileById(VehicleProfileParameters vehicleProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleProfileParams == null || vehicleProfileParams.VehicleProfileId == null || vehicleProfileParams.VehicleProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle profile by ID.
        /// </summary>
        /// <param name="vehicleProfileParams">Vehicle profile parameter containing a vehicle profile ID </param>
        /// <returns>Vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse>> GetVehicleProfileByIdAsync(VehicleProfileParameters vehicleProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                new VehicleParameters(),
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + vehicleProfileParams.VehicleProfileId,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle profile</returns>
        public VehicleProfile UpdateVehicleProfile(VehicleProfile profileParams, out ResultResponse resultResponse)
        {
            if (profileParams == null || (profileParams.VehicleProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
                    }
                };

                return null;
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<VehicleProfile>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + profileParams.VehicleProfileId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Update a vehicle profile.
        /// </summary>
        /// <param name="profileParams">Vehicle profile object as body payload</param>
        /// <returns>Updated vehicle profile</returns>
        public Task<Tuple<VehicleProfile, ResultResponse, string>> UpdateVehicleProfileAsync(VehicleProfile profileParams)
        {
            if (profileParams == null || (profileParams.VehicleProfileId ?? 0) < 1)
            {
                return Task.FromResult(new Tuple<VehicleProfile, ResultResponse, string>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
                    }
                },
                null));
            }

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(profileParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<VehicleProfile>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleProfiles + "/" + profileParams.VehicleProfileId,
                HttpMethodType.Patch,
                content,
                false,
                false);
        }

        #endregion


        #region Vehicle Capacity Profile

        /// <summary>
        ///     Create a vehicle capacity profile.
        /// </summary>
        /// <param name="vehicleCapacityProfile">Vehicle capacity profile body parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Created vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse CreateVehicleCapacityProfile(VehicleCapacityProfile vehicleCapacityProfile,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(
                vehicleCapacityProfile,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Post,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Create a vehicle capacity profile asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfile">Vehicle capacity profile body parameters</param>
        /// <returns>A tuple type object with a created vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> CreateVehicleCapacityProfileAsync(VehicleCapacityProfile vehicleCapacityProfile)
        {
            return GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(
                vehicleCapacityProfile,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Post);
        }

        /// <summary>
        ///     Remove a vehicle capacity profile from database.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Removed vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse DeleteVehicleCapacityProfile(VehicleCapacityProfileParameters vehicleCapacityProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleCapacityProfileParams == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle capacity profile ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Delete,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Remove a vehicle capacity profile from database asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID</param>
        /// <returns>A tuple type object with a deleted vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> DeleteVehicleCapacityProfileAsync(VehicleCapacityProfileParameters vehicleCapacityProfileParams)
        {
            if (vehicleCapacityProfileParams == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == 0)
            {
                return Task.FromResult(new Tuple<VehicleCapacityProfileResponse, ResultResponse>(null, new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"he vehicle capacity profile ID is not specified"}}
                    }
                }));
            }

            return GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Delete);
        }

        /// <summary>
        ///     Get paginated list of the vehicle capacity profiles.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile request parameters</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>The data including list of the vehicle profiles.</returns>
        public VehicleCapacityProfilesResponse GetVehicleCapacityProfiles(VehicleCapacityProfileParameters capacityProfileParams,
            out ResultResponse resultResponse)
        {
            var result = GetJsonObjectFromAPI<VehicleCapacityProfilesResponse>(
                capacityProfileParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Get paginated list of the vehicle capacity profiles asynchronously.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile request parameters</param>
        /// <returns>A tuple type object with the retrieved vehicle capacity profiles
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfilesResponse, ResultResponse>> GetVehicleCapacityProfilesAsync(VehicleCapacityProfileParameters capacityProfileParams)
        {
            return GetJsonObjectFromAPIAsync<VehicleCapacityProfilesResponse>(
                capacityProfileParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles,
                HttpMethodType.Get);
        }

        /// <summary>
        ///     Get vehicle capacity profile by ID.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter containing a vehicle capacity profile ID </param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse GetVehicleCapacityProfileById(VehicleCapacityProfileParameters vehicleCapacityProfileParams,
            out ResultResponse resultResponse)
        {
            if (vehicleCapacityProfileParams == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == null ||
                vehicleCapacityProfileParams.VehicleCapacityProfileId == 0)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle ID is not specified"}}
                    }
                };

                return null;
            }

            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(
                new VehicleCapacityProfileParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Get,
                out resultResponse);

            return result;
        }

        /// <summary>
        ///     Get vehicle capacity profile by ID asynchronously.
        /// </summary>
        /// <param name="vehicleCapacityProfileParams">Vehicle capacity profile parameter 
        /// containing a vehicle capacity profile ID</param>
        /// <returns>A tuple type object with a retrieved vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse>> GetVehicleCapacityProfileByIdAsync(VehicleCapacityProfileParameters vehicleCapacityProfileParams)
        {
            var result = GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(
                new VehicleCapacityProfileParameters(),
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + vehicleCapacityProfileParams.VehicleCapacityProfileId,
                HttpMethodType.Get);

            return result;
        }

        /// <summary>
        ///     Update a vehicle capacity profile.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile object as body payload</param>
        /// <param name="resultResponse">Failing response</param>
        /// <returns>Updated vehicle capacity profile</returns>
        public VehicleCapacityProfileResponse UpdateVehicleCapacityProfile(VehicleCapacityProfile capacityProfileParams, out ResultResponse resultResponse)
        {
            if (capacityProfileParams == null || (capacityProfileParams.VehicleCapacityProfileId ?? 0) < 1)
            {
                resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle profile ID is not specified"}}
                    }
                };

                return null;
            }

            var capProfId = capacityProfileParams.VehicleCapacityProfileId;

            capacityProfileParams.VehicleCapacityProfileId = null;

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(capacityProfileParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            var result = GetJsonObjectFromAPI<VehicleCapacityProfileResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + capProfId,
                HttpMethodType.Patch,
                content,
                out resultResponse);

            return result;
        }

        /// <summary>
        /// Update a vehicle capacity profile asynchronously.
        /// </summary>
        /// <param name="capacityProfileParams">Vehicle capacity profile object as body payload</param>
        /// <returns>A tuple type object with updated vehicle capacity profile
        /// or/and failure response</returns>
        public Task<Tuple<VehicleCapacityProfileResponse, ResultResponse, string>> UpdateVehicleCapacityProfileAsync(VehicleCapacityProfile capacityProfileParams)
        {
            if (capacityProfileParams == null || (capacityProfileParams.VehicleCapacityProfileId ?? 0) < 1)
            {
                var resultResponse = new ResultResponse
                {
                    Status = false,
                    Messages = new Dictionary<string, string[]>
                    {
                        {"Error", new[] {"The vehicle capacity profile ID is not specified"}}
                    }
                };

                return Task.FromResult(new Tuple<VehicleCapacityProfileResponse, ResultResponse, string>(
                    null,
                    resultResponse,
                    null));
            }

            var capProfId = capacityProfileParams.VehicleCapacityProfileId;

            capacityProfileParams.VehicleCapacityProfileId = null;

            var updateBodyJsonString = R4MeUtils.SerializeObjectToJson(capacityProfileParams, true);

            var content = new StringContent(updateBodyJsonString, Encoding.UTF8, "application/json");

            var genParams = new RouteParametersQuery();

            return GetJsonObjectFromAPIAsync<VehicleCapacityProfileResponse>(
                genParams,
                R4MEInfrastructureSettingsV5.VehicleCapacityProfiles + "/" + capProfId,
                HttpMethodType.Patch,
                content,
                false,
                false);
        }

        #endregion
    }
}