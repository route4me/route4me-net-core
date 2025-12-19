using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;

using Route4MeSDKLibrary.DataTypes.V5.Customers;
using Route4MeSDKLibrary.QueryTypes.V5.Customers;

namespace Route4MeSDKLibrary.Managers
{
    public sealed class CustomerManagerV5 : Route4MeManagerBase
    {
        public CustomerManagerV5(string apiKey) : base(apiKey)
        {
        }

        public CustomerManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        #region Get Customers List

        /// <summary>
        ///     Get list with customers data.
        /// </summary>
        /// <param name="parameters">Request parameters.</param>
        /// <param name="resultResponse">Failing response.</param>
        /// <returns>Customer list response.</returns>
        public CustomerListResponse GetCustomersList(CustomerListParameters parameters, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<CustomerListResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.CustomersList,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Get list with customers data (async).
        /// </summary>
        /// <param name="parameters">Request parameters.</param>
        /// <returns>Customer list response.</returns>
        public Task<Tuple<CustomerListResponse, ResultResponse>> GetCustomersListAsync(CustomerListParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<CustomerListResponse>(
                parameters,
                R4MEInfrastructureSettingsV5.CustomersList,
                HttpMethodType.Post);
        }

        #endregion

        #region Get Customer By ID

        /// <summary>
        ///     Get customer by ID.
        /// </summary>
        /// <param name="parameters">Customer ID parameter.</param>
        /// <param name="resultResponse">Failing response.</param>
        /// <returns>Customer show resource.</returns>
        public CustomerShowResource GetCustomerById(CustomerIdParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Customers + "/" + parameters.CustomerId;

            var response = GetJsonObjectFromAPI<CustomerShowResource>(
                new GenericParameters(),
                url,
                HttpMethodType.Get,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Get customer by ID (async).
        /// </summary>
        /// <param name="parameters">Customer ID parameter.</param>
        /// <returns>Customer show resource.</returns>
        public Task<Tuple<CustomerShowResource, ResultResponse>> GetCustomerByIdAsync(CustomerIdParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.Customers + "/" + parameters.CustomerId;

            return GetJsonObjectFromAPIAsync<CustomerShowResource>(
                new GenericParameters(),
                url,
                HttpMethodType.Get);
        }

        #endregion

        #region Create Customer

        /// <summary>
        ///     Create a new customer.
        /// </summary>
        /// <param name="request">Create customer request.</param>
        /// <param name="resultResponse">Failing response.</param>
        /// <returns>Customer resource.</returns>
        public CustomerResource CreateCustomer(StoreRequest request, out ResultResponse resultResponse)
        {
            var response = GetJsonObjectFromAPI<CustomerResource>(
                request,
                R4MEInfrastructureSettingsV5.Customers,
                HttpMethodType.Post,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Create a new customer (async).
        /// </summary>
        /// <param name="request">Create customer request.</param>
        /// <returns>Customer resource.</returns>
        public Task<Tuple<CustomerResource, ResultResponse>> CreateCustomerAsync(StoreRequest request)
        {
            return GetJsonObjectFromAPIAsync<CustomerResource>(
                request,
                R4MEInfrastructureSettingsV5.Customers,
                HttpMethodType.Post);
        }

        #endregion

        #region Update Customer

        /// <summary>
        ///     Update a customer.
        /// </summary>
        /// <param name="parameters">Update customer parameters.</param>
        /// <param name="resultResponse">Failing response.</param>
        /// <returns>Updated customer resource.</returns>
        public CustomerResource UpdateCustomer(UpdateCustomerParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Customers + "/" + parameters.CustomerId;

            var response = GetJsonObjectFromAPI<CustomerResource>(
                parameters,
                url,
                HttpMethodType.Put,
                out resultResponse);

            return response;
        }

        /// <summary>
        ///     Update a customer (async).
        /// </summary>
        /// <param name="parameters">Update customer parameters.</param>
        /// <returns>Updated customer resource.</returns>
        public Task<Tuple<CustomerResource, ResultResponse>> UpdateCustomerAsync(UpdateCustomerParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.Customers + "/" + parameters.CustomerId;

            return GetJsonObjectFromAPIAsync<CustomerResource>(
                parameters,
                url,
                HttpMethodType.Put);
        }

        #endregion

        #region Delete Customer

        /// <summary>
        ///     Delete a customer.
        /// </summary>
        /// <param name="parameters">Customer ID parameter.</param>
        /// <param name="resultResponse">Failing response.</param>
        /// <returns>Status result.</returns>
        public ResultResponse DeleteCustomer(CustomerIdParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.Customers + "/" + parameters.CustomerId;

            var response = GetJsonObjectFromAPI<ResultResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Delete,
                out resultResponse);

            // Handle empty or null response (204 No Content)
            if (response == null && (resultResponse == null || resultResponse.Code == 0))
            {
                resultResponse = new ResultResponse
                {
                    Status = true,
                    Code = 204,
                    ExitCode = 0,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Info", new[] { "No Content (204) — Customer deleted successfully." } }
                    }
                };
            }

            return resultResponse;
        }

        /// <summary>
        ///     Delete a customer (async).
        /// </summary>
        /// <param name="parameters">Customer ID parameter.</param>
        /// <returns>Status result.</returns>
        public async Task<Tuple<ResultResponse, ResultResponse>> DeleteCustomerAsync(CustomerIdParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.Customers + "/" + parameters.CustomerId;

            var responseTuple = await GetJsonObjectFromAPIAsync<ResultResponse>(
                new GenericParameters(),
                url,
                HttpMethodType.Delete);

            var result = responseTuple?.Item1;
            var meta = responseTuple?.Item2;

            // If the API returned 204 or no content at all, it's a success.
            if (result == null && (meta == null || meta.Code == 0))
            {
                var successResponse = new ResultResponse
                {
                    Status = true,
                    Code = 204,
                    ExitCode = 0,
                    Messages = new Dictionary<string, string[]>
                    {
                        { "Info", new[] { "No Content (204) — Customer deleted successfully." } }
                    }
                };

                return Tuple.Create(successResponse, successResponse);
            }

            // If the API still returned 204 in meta
            if (meta != null && meta.Code == 204)
            {
                meta.Status = true;
                if (meta.Messages == null)
                {
                    meta.Messages = new Dictionary<string, string[]>
                    {
                        { "Info", new[] { "Customer deleted successfully (204)." } }
                    };
                }

                return new Tuple<ResultResponse, ResultResponse>(meta, meta);
            }

            return responseTuple;
        }

        #endregion
    }
}