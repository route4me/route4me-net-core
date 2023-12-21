using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Route4MeSDK;
using Route4MeSDK.DataTypes;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.QueryTypes.V5;
using Route4MeSDKLibrary.QueryTypes.V5.Orders;

namespace Route4MeSdkV5UnitTest.V5.Orders
{
    [TestFixture]
    public class OrdersTests
    {
        [Test]
        public void OrdersArchiveTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };
            var result = route4me.ArchiveOrders(parameters, out var resultResponse);
            Assert.That(result.Items, Is.Not.Empty);
        }

        [Test]
        public async Task OrdersArchiveAsyncTest()
        {
            var route4me = new Route4MeManagerV5(ApiKeys.ActualApiKey);

            var parameters = new ArchiveOrdersParameters()
            {
                PerPage = 100
            };
            var result = await route4me.ArchiveOrdersAsync(parameters);
            Assert.NotNull(result.Item1);
            Assert.That(result.Item1.Items, Is.Not.Empty);
        }

        [Test]
        public void OrdersHistoryTest()
        {
            var route4meV5 = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var route4me = new Route4MeManager(ApiKeys.ActualApiKey);

            var order = new Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next().ToString(),
                CachedLat = 37.9,
                CachedLng = -34.6
            };

            Order createdOrder = route4me.AddOrder(order, out string errorString);

            var parameters = new OrderHistoryParameters()
            {
                OrderId = createdOrder.OrderId,
                TrackingNumber = createdOrder.TrackingNumber
            };

            var result = route4meV5.GetOrderHistory(parameters, out var resultResponse);

            Assert.That(result.Results, Is.Not.Empty);
        }

        [Test]
        public async Task OrderHistoryAsyncTest()
        {
            var route4meV5 = new Route4MeManagerV5(ApiKeys.ActualApiKey);
            var route4me = new Route4MeManager(ApiKeys.ActualApiKey);

            var order = new Order()
            {
                Address1 = "Test Address1 " + (new Random()).Next().ToString(),
                AddressAlias = "Test AddressAlias " + (new Random()).Next().ToString(),
                CachedLat = 37.9,
                CachedLng = -34.6
            };

            var createdOrder = await route4me.AddOrderAsync(order);

            var parameters = new OrderHistoryParameters()
            {
                OrderId = createdOrder.Item1.OrderId,
                TrackingNumber = createdOrder.Item1.TrackingNumber
            };

            var result = await route4meV5.GetOrderHistoryAsync(parameters);

            Assert.That(result.Item1.Results, Is.Not.Empty);
        }
    }
}
