using Route4MeSDK.DataTypes;
using Route4MeSDKLibrary.Managers;
using System;
using System.Linq;
using Route4MeSDKLibrary.DataTypes.V5.Orders;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        public void OrdersV5Examples()
        {
            var route4Me = new OrderManagerV5(ActualApiKey);

            // Create
            var createdOrder = route4Me.CreateOrder(new Route4MeSDKLibrary.DataTypes.V5.Orders.Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next(),
                AddressGeo = new GeoPoint()
                {
                    Latitude = 37.9,
                    Longitude = -34.6
                }
            }, out _);

            // Read
            var loadedOrder = route4Me.GetOrder(new GetOrderParameters(){ OrderUuid = createdOrder.OrderUuid}, out _);

            // Update
            loadedOrder.AddressCity = "Madrid";
            var updatedOrder = route4Me.UpdateOrder(loadedOrder, out _);

            //Search
            var searchedOrder = route4Me.SearchOrders(new SearchOrdersRequest() { OrderIds = new[] { updatedOrder.OrderUuid } }, out _);

            // batch (self) update by filter
            var data = searchedOrder.Results.Single();
            route4Me.BatchUpdateFilter(new BatchUpdateFilterOrderRequest() { Data = data, Filters = new FiltersParamRequestBody() { OrderIds = new[] { data.OrderUuid } } }, out _);

            // Delete
            route4Me.DeleteOrder(createdOrder.OrderUuid, out _);
        }
    }
}
