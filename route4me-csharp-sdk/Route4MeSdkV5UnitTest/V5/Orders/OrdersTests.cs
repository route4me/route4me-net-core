using System.Linq;
using System.Threading.Tasks;
using Route4MeSDK;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;
using Xunit;

namespace Route4MeSdkV5UnitTest.V5.Orders
{
    public class OrdersTests
    {
        [Fact]
        public void OrdersArchiveTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };
            var result = route4me.ArchiveOrders(parameters, out var resultResponse);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task OrdersArchiveAsyncTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
                //Cursor = "data",
                //Filters = new ArchiveOrdersFilters()
                //{
                //    DayAdded = new string[] { "2020-12-20", "2021-06-12"}
                //}
            };
            var result = await route4me.ArchiveOrdersAsync(parameters);
            Assert.NotNull(result.Item1);
            Assert.NotEmpty(result.Item1.Items);
        }

        [Fact]
        public void OrdersHistoryTest()
        {
            var route4meV5 = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var route4me = new Route4MeManager(ApiKeys.ActualApiKey);

            var orders = route4me.GetOrders(new OrderParameters() { Limit = 1 }, out _, out _);
            Assert.True(orders.Length > 0);

            var parameters = new OrderHistoryParameters()
            {
                OrderId = orders.Single().OrderId.Value, 
                TrackingNumber = orders.Single().TrackingNumber
            };

            var result = route4meV5.GetOrderHistory(parameters, out var resultResponse);

            Assert.NotEmpty(result.Results);
        }

        [Fact]
        public async Task OrderHistoryAsyncTest()
        {
            var route4meV5 = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var route4me = new Route4MeManager(ApiKeys.ActualApiKey);

            var orders = await route4me.GetOrdersAsync(new OrderParameters() { Limit = 1 });
            Assert.True(orders.Item1.Length == 1);

            var parameters = new OrderHistoryParameters()
            {
                OrderId = orders.Item1.Single().OrderId.Value,
                TrackingNumber = orders.Item1.Single().TrackingNumber
            };

            var result = await route4meV5.GetOrderHistoryAsync(parameters);

            Assert.NotEmpty(result.Item1.Results);
        }
    }
}
