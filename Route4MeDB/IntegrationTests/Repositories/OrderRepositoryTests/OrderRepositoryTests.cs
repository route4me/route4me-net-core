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

namespace Route4MeDB.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class OrderRepositoryTests
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder orderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;

        public OrderRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseInMemoryDatabase(databaseName: "Route4MeDB")
                .Options;
            _route4meDbContext = new Route4MeDbContext(dbOptions);
            _orderRepository = new OrderRepository(_route4meDbContext);
        }

        [Fact]
        public async Task GetsExistingOrder()
        {
            var existingOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(existingOrder);
            _route4meDbContext.SaveChanges();
            int orderDbId = existingOrder.OrderDbId;
            _output.WriteLine($"OrderDbId: {orderDbId}");

            var orderSpec = new OrderSpecification(orderDbId);

            var orderFromRepo = await _orderRepository.GetByIdAsync(orderSpec);
            Assert.Equal(orderBuilder.testData.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(orderBuilder.testData.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
        }

        [Fact]
        public async Task GetsExistingOrderAsync()
        {
            var firstOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(firstOrder);
            int firstOrdeDbId = firstOrder.OrderDbId;

            var secondOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(secondOrder);
            int secondOrdeDbId = secondOrder.OrderDbId;

            _route4meDbContext.SaveChanges();

            var orderFilterSpecification = new OrderSpecification(new int[] { firstOrdeDbId, secondOrdeDbId});
            int orderCount = await _orderRepository.CountAsync(orderFilterSpecification);
            Assert.True(orderCount>=2);

            var orderFromRepo = await _orderRepository.GetOrderByIdAsync(secondOrdeDbId);

            Assert.Equal(secondOrdeDbId, orderFromRepo.OrderDbId);
            Assert.Equal(secondOrder.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(secondOrder.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
        }

        [Fact]
        public async Task GetOrdersAsync()
        {
            var orderDbIDs = new List<int>();

            var firstOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(firstOrder);
            int firstAddressDbId = firstOrder.OrderDbId;
            orderDbIDs.Add(firstAddressDbId);

            var secondOrder = orderBuilder.WithCustomData();
            _route4meDbContext.Orders.Add(secondOrder);
            int secondOrderDbId = secondOrder.OrderDbId;
            orderDbIDs.Add(secondOrderDbId);

            _route4meDbContext.SaveChanges();

            var orders = await _orderRepository.GetOrdersAsync(0, 1000);

            var linqOrders =await _route4meDbContext.Orders
                .Where(x => orderDbIDs.Contains(x.OrderDbId)).ToListAsync<Order>();

            foreach (var linqOrder in linqOrders)
            {
                Assert.Contains(linqOrder, orders);
            }
        }

        [Fact]
        public async Task UpdateOrderAsync()
        {
            var order = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(order);
            int firstAddressDbId = order.OrderDbId;

            _route4meDbContext.SaveChanges();

            order.EXT_FIELD_first_name = "Peter Modified";
            order.EXT_FIELD_last_name = "Newman Modified";

            var updatedOrder = await _orderRepository.UpdateOrderAsync(order.OrderDbId, order);

            _route4meDbContext.SaveChanges();

            var linqOrder = _route4meDbContext.Orders
                .Where(x => x.OrderDbId == updatedOrder.OrderDbId).FirstOrDefault();

            Assert.Equal<Order>(updatedOrder, linqOrder);
        }
    }

}
