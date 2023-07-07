using Route4MeSDK.DataTypes;

namespace Route4MeSDK.Examples
{
    public sealed partial class Route4MeExamples
    {
        /// <summary>
        /// Add Order With Demands
        /// </summary>
        public void AddOrAddOrderWithDemandsder()
        {
            // Create the manager with the api key
            var route4Me = new Route4MeManager(ActualApiKey);

            var order = new Order()
            {
                Address1 = "Some address",
                CachedLat = 48.335991,
                CachedLng = 31.18287,
                Weight = 500.0,
                Cost = 100.0,
                Revenue = 1500.0,
                Cube = 2500.0,
                Pieces = 1500
            };

            // Run the query
            Order resultOrder = route4Me.AddOrder(order, out string errorString);

            if (resultOrder != null && resultOrder.GetType() == typeof(Order))
                OrdersToRemove.Add(resultOrder.OrderId.ToString());

            PrintExampleOrder(resultOrder, errorString);

            RemoveTestOrders();
        }
    }
}