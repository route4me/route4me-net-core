using System;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.DataTypes.V5;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;

namespace Route4MeSDKLibrary.Managers
{
    internal class OrderManagerV5 : Route4MeManagerBase
    {
        public OrderManagerV5(string apiKey) : base(apiKey)
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
    }
}
