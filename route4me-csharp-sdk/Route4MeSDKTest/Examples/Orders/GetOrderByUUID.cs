using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using System;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Get Order details by order_id
        /// </summary>
        public void GetOrderByUUID(string orderIds = null)
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var order = new Order
            {
                Address1 = "Test Address1 " + new Random().Next(),
                AddressAlias = "Test AddressAlias " + new Random().Next(),
                CachedLat = 37.773972,
                CachedLng = -122.431297
            };

            var createdOrder = route4Me.AddOrder(order, out _);

            var orderParameters = new OrderParameters
            {
                order_id = createdOrder.OrderUuid
            };

            var loadedOrder = route4Me.GetOrderByID(orderParameters, out var errorString);

            route4Me.RemoveOrders(new[] { loadedOrder.OrderUuid }, out _);
        }
    }
}