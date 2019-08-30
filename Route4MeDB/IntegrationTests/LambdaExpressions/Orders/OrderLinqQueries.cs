using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Route4MeDB.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Route4MeDB.ApplicationCore.Specifications;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;

namespace Route4MeDB.IntegrationTests.LambdaExpressions
{
    public class OrderLinqQueries
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder orderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;

        public OrderLinqQueries(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseInMemoryDatabase(databaseName: "Route4MeDB")
                .Options;
            _route4meDbContext = new Route4MeDbContext(dbOptions);
            _orderRepository = new OrderRepository(_route4meDbContext);
        }

        [Fact]
        public async Task GetOrdersAsymc()
        {
            var createdOrders = await Create2Orders();

            var orders = _route4meDbContext.Orders.Skip(0).Take(2);

            var ordersFromRepo = await _orderRepository.GetOrdersAsync(0, 1000);

            await orders.ForEachAsync(x => 
            {
                Assert.Contains<Order>(x, ordersFromRepo);
            });
        }

        [Fact]
        public async Task GetOrderByIdAsync()
        {
            var createdOrder = await CreateOrder();

            var orderDbId = _route4meDbContext.Orders
                .Where(x => x.OrderDbId == createdOrder.OrderDbId)
                .FirstOrDefault().OrderDbId;

            var orderSpec = new OrderSpecification(orderDbId);

            var orderFromRepo = await _orderRepository.GetByIdAsync(orderSpec);

            Assert.Equal<Order>(orderFromRepo, createdOrder);
        }

        [Fact]
        public async Task GetOrdersByIdsAsync()
        {
            var createdOrders = await Create2Orders();

            var createdOrderDbIds = new int[] { createdOrders[0].OrderDbId, createdOrders[1].OrderDbId };

            var orders = _route4meDbContext.Orders
                .Where(x => (createdOrderDbIds.Contains(x.OrderDbId)));

            var ordersFromRepo = await _orderRepository.GetOrdersAsync(createdOrderDbIds);

            await orders.ForEachAsync<Order>(o =>
            {
                Assert.Contains<Order>(o, ordersFromRepo);
            });
        }

        [Fact]
        public async Task UpdateOrderAsync()
        {
            var createdOrder = await CreateOrder();

            createdOrder.EXT_FIELD_first_name = "Peter Modified";
            createdOrder.EXT_FIELD_last_name = "Newman Modified";

            _route4meDbContext.SaveChanges();

            var orderSpec = new OrderSpecification(createdOrder.OrderDbId);

            var orderFromRepo = await _orderRepository.GetByIdAsync(orderSpec);

            Assert.Equal("Peter Modified", orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal("Newman Modified", orderFromRepo.EXT_FIELD_last_name);
        }

        [Fact]
        public async Task RemoveOrderAsync()
        {
            var createdOrder = await CreateOrder();

            int createdOrderDbId = createdOrder.OrderDbId;

            var orderSpec = new OrderSpecification(createdOrderDbId);
            var orderExistedInRepo = await _orderRepository.GetByIdAsync(orderSpec);

            Assert.NotNull(orderExistedInRepo);

            _route4meDbContext.Orders.Remove(createdOrder);
            await _route4meDbContext.SaveChangesAsync();

            var result = await _orderRepository.GetByIdAsync(orderSpec);

            Assert.Null(result);
        }

        #region // Create Orders
        private async Task<List<Order>> Create2Orders()
        {
            var firstOrderParams = orderBuilder.WithDefaultValues();
            var secondOrderParams = orderBuilder.WithCustomData();

            var firstOrder = await _route4meDbContext.Orders.AddAsync(firstOrderParams);
            var secondtOrder = await _route4meDbContext.Orders.AddAsync(secondOrderParams);

            _route4meDbContext.SaveChanges();

            return await Task.Run(() =>
            {
                return new List<Order>() { firstOrder.Entity, secondtOrder.Entity };
            });

        }

        private async Task<Order> CreateOrder()
        {
            var orderParams = orderBuilder.WithDefaultValues();

            var order = await _route4meDbContext.Orders.AddAsync(orderParams);

            _route4meDbContext.SaveChanges();

            return await Task.Run(() =>
            {
                return order.Entity;
            });

        }

        #endregion
    }
}
