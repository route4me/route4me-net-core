using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;

using Route4MeSDKLibrary.DataTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;

namespace Route4MeSDKLibrary.Managers
{
    public class OrderManagerV5 : Route4MeManagerBase
    {
        public OrderManagerV5(string apiKey) : base(apiKey)
        {
        }

        public OrderManagerV5(string apiKey, ILogger logger) : base(apiKey, logger)
        {
        }

        /// <summary>
        ///  Display the Archive Orders.
        /// </summary>
        /// <param name="parameters">Request payload</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Archived Orders</returns>
        public ArchiveOrdersResponse ArchiveOrders(ArchiveOrdersParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<ArchiveOrdersResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersArchive,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Display the Archive Orders.
        /// </summary>
        /// <param name="parameters">Request payload</param>
        /// <returns>Archived Orders</returns>
        public Task<Tuple<ArchiveOrdersResponse, ResultResponse, string>> ArchiveOrdersAsync(ArchiveOrdersParameters parameters)
        {
            return GetJsonObjectFromAPIAsync<ArchiveOrdersResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersArchive,
                HttpMethodType.Post,
                null,
                true,
                false);
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Order history</returns>
        public OrderHistoryResponse GetOrderHistory(OrderHistoryParameters parameters, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<OrderHistoryResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersHistory,
                HttpMethodType.Get,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Display the order history.
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Order history</returns>
        public async Task<Tuple<OrderHistoryResponse, ResultResponse>> GetOrderHistoryAsync(OrderHistoryParameters parameters)
        {
            var result = await GetJsonObjectFromAPIAsync<OrderHistoryResponse>(parameters,
                R4MEInfrastructureSettingsV5.OrdersHistory,
                HttpMethodType.Get, null, true, false).ConfigureAwait(false);

            return new Tuple<OrderHistoryResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Get single order by its UUID.
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Order</returns>
        public Order GetOrder(GetOrderParameters parameters, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatform;
            return GetJsonObjectFromAPI<Order>(parameters,
                url + $"/{parameters.OrderUuid}",
                HttpMethodType.Get,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Get single order by its UUID.
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Order</returns>
        public async Task<Tuple<Order, ResultResponse>> GetOrderAsync(GetOrderParameters parameters)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatform;
            var result = await GetJsonObjectFromAPIAsync<Order>(parameters,
                url + $"/{parameters.OrderUuid}",
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<Order, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Update order.
        /// </summary>
        /// <param name="order">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated order</returns>
        public Order UpdateOrder(Order order, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatform;
            return GetJsonObjectFromAPI<Order>(order,
                url + $"/{order.OrderUuid}",
                HttpMethodType.Put,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Update order.
        /// </summary>
        /// <param name="order">Request body</param>
        /// <returns>Updated order</returns>
        public async Task<Tuple<Order, ResultResponse>> UpdateOrderAsync(Order order)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatform;
            var result = await GetJsonObjectFromAPIAsync<Order>(order,
                url + $"/{order.OrderUuid}",
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<Order, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Delete (soft) order.
        /// </summary>
        /// <param name="orderUuid">Order UUID</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Status</returns>
        public StatusResponse DeleteOrder(string orderUuid, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatform;
            return GetJsonObjectFromAPI<StatusResponse>(new GenericParameters(),
                url + $"/{orderUuid}",
                HttpMethodType.Delete,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Delete (soft) order.
        /// </summary>
        /// <param name="orderUuid">Order UUID</param>
        /// <returns>Status</returns>
        public async Task<Tuple<StatusResponse, ResultResponse>> DeleteOrderAsync(string orderUuid)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatform;
            var result = await GetJsonObjectFromAPIAsync<StatusResponse>(new GenericParameters(),
                url + $"/{orderUuid}",
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<StatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Create order.
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created order</returns>
        public Order CreateOrder(Order order, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Order>(order,
                R4MEInfrastructureSettingsV5.OrdersPlatformCreate,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Create order.
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Created order</returns>
        public async Task<Tuple<Order, ResultResponse>> CreateOrderAsync(Order order)
        {
            var result = await GetJsonObjectFromAPIAsync<Order>(order,
                R4MEInfrastructureSettingsV5.OrdersPlatformCreate,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<Order, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Search orders.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Orders</returns>
        public SearchOrdersResponse SearchOrders(SearchOrdersRequest request, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<SearchOrdersResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatform,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Search orders.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Orders</returns>
        public async Task<Tuple<SearchOrdersResponse, ResultResponse>> SearchOrdersAsync(SearchOrdersRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<SearchOrdersResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatform,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<SearchOrdersResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Batch update orders (with filter).
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Status</returns>
        public AsyncStatusResponse BatchUpdateFilter(BatchUpdateFilterOrderRequest request, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<AsyncStatusResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchUpdateFilter,
                HttpMethodType.Put,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Batch update orders (with filter).
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Status</returns>
        public async Task<Tuple<AsyncStatusResponse, ResultResponse>> BatchUpdateFilterAsync(BatchUpdateFilterOrderRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<AsyncStatusResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchUpdateFilter,
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AsyncStatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Batch delete orders.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Status</returns>
        public StatusResponse BatchDelete(BatchDeleteOrdersRequest request, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<StatusResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchDelete,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Batch delete orders.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Status</returns>
        public async Task<Tuple<AsyncStatusResponse, ResultResponse>> BatchDeleteAsync(BatchDeleteOrdersRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<AsyncStatusResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchDelete,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AsyncStatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Batch update orders.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Status</returns>
        public Order[] BatchUpdate(BatchUpdateOrdersRequest request, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<Order[]>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchUpdate,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Batch update orders.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Status</returns>
        public async Task<Tuple<Order[], ResultResponse>> BatchUpdateAsync(BatchUpdateOrdersRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<Order[]>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchUpdate,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<Order[], ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Batch create order.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created order</returns>
        public AsyncStatusResponse BatchCreateOrders(BatchCreateOrdersRequest request, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<AsyncStatusResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchCreate,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Batch create order.
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Created order</returns>
        public async Task<Tuple<AsyncStatusResponse, ResultResponse>> BatchCreateOrdersAsync(BatchCreateOrdersRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<AsyncStatusResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformBatchCreate,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<AsyncStatusResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Get custom user fields 
        /// </summary>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Custom user fields</returns>
        public CustomUserFieldsResponse GetCustomUserFields(out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<CustomUserFieldsResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields,
                HttpMethodType.Get,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Get custom user fields 
        /// </summary>
        /// <returns>Custom user fields</returns>
        public async Task<Tuple<CustomUserFieldsResponse, ResultResponse>> GetCustomUserFieldsAsync()
        {
            var result = await GetJsonObjectFromAPIAsync<CustomUserFieldsResponse>(new GenericParameters(),
                R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields,
                HttpMethodType.Get,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CustomUserFieldsResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Create custom user field
        /// </summary>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Created Custom user field</returns>
        public CustomUserFieldResponse CreateCustomUserFields(CreateCustomUserFieldRequest request, out ResultResponse resultResponse)
        {
            return GetJsonObjectFromAPI<CustomUserFieldResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields,
                HttpMethodType.Post,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Create custom user field
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Created Custom user field</returns>
        public async Task<Tuple<CustomUserFieldResponse, ResultResponse>> CreateCustomUserFieldsAsync(CreateCustomUserFieldRequest request)
        {
            var result = await GetJsonObjectFromAPIAsync<CustomUserFieldResponse>(request,
                R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields,
                HttpMethodType.Post,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CustomUserFieldResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Update custom user field
        /// </summary>
        /// <param name="orderCustomFieldUuid">Order custom field uuid</param>
        /// <param name="request">Request body</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Updated Custom user field</returns>
        public CustomUserFieldResponse UpdateCustomUserFields(string orderCustomFieldUuid, UpdateCustomUserFieldRequest request, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields;
            return GetJsonObjectFromAPI<CustomUserFieldResponse>(request,
                url + $"/{orderCustomFieldUuid}",
                HttpMethodType.Put,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Update custom user field
        /// </summary>
        /// <param name="orderCustomFieldUuid">Order custom field uuid</param>
        /// <param name="request">Request body</param>
        /// <returns>Updated Custom user field</returns>
        public async Task<Tuple<CustomUserFieldResponse, ResultResponse>> UpdateCustomUserFieldsAsync(string orderCustomFieldUuid, UpdateCustomUserFieldRequest request)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields;
            var result = await GetJsonObjectFromAPIAsync<CustomUserFieldResponse>(request,
                url + $"/{orderCustomFieldUuid}",
                HttpMethodType.Put,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CustomUserFieldResponse, ResultResponse>(result.Item1, result.Item2);
        }

        /// <summary>
        ///  Delete custom user field
        /// </summary>
        /// <param name="orderCustomFieldUuid">Order custom field uuid</param>
        /// <param name="resultResponse">Error response</param>
        /// <returns>Deleted Custom user field</returns>
        public CustomUserFieldResponse DeleteCustomUserFields(string orderCustomFieldUuid, out ResultResponse resultResponse)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields;
            return GetJsonObjectFromAPI<CustomUserFieldResponse>(new GenericParameters(),
                url + $"/{orderCustomFieldUuid}",
                HttpMethodType.Delete,
                null,
                false,
                true,
                out resultResponse);
        }

        /// <summary>
        ///  Delete custom user field
        /// </summary>
        /// <param name="orderCustomFieldUuid">Order custom field uuid</param>
        /// <returns>Deleted Custom user field</returns>
        public async Task<Tuple<CustomUserFieldResponse, ResultResponse>> DeleteCustomUserFieldsAsync(string orderCustomFieldUuid)
        {
            var url = R4MEInfrastructureSettingsV5.OrdersPlatformCustomUserFields;
            var result = await GetJsonObjectFromAPIAsync<CustomUserFieldResponse>(new GenericParameters(),
                url + $"/{orderCustomFieldUuid}",
                HttpMethodType.Delete,
                null,
                true,
                false).ConfigureAwait(false);

            return new Tuple<CustomUserFieldResponse, ResultResponse>(result.Item1, result.Item2);
        }
    }
}