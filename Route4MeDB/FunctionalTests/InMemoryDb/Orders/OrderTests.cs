using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.UnitTests.Builders;
using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Specifications;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Route4MeDB.FunctionalTests.InMemoryDb
{
    public class OrderTests
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly Route4MeDbManager r4mdbManager;
        private OrderBuilder orderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;

        public OrderTests(ITestOutputHelper output, IConfiguration Configuration = null)
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.InMemory;
            r4mdbManager = new Route4MeDbManager(Configuration);

            _route4meDbContext = r4mdbManager.Route4MeContext;

            _output = output;
        }

        [Fact]
        public async void GetOrdersAsync()
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

            var orders = await r4mdbManager.OrdersRepository.GetOrdersAsync(0, 1000);

            var linqOrders = await _route4meDbContext.Orders
                .Where(x => orderDbIDs.Contains(x.OrderDbId)).ToListAsync<Order>();

            foreach (var linqOrder in linqOrders)
            {
                Assert.True(orders.Where(x => x.OrderDbId == linqOrder.OrderDbId).Count()>0);
            }
        }

        [Fact]
        public async void GetsExistingOrder()
        {
            var existingOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(existingOrder);
            _route4meDbContext.SaveChanges();
            int orderDbId = existingOrder.OrderDbId;
            _output.WriteLine($"OrderDbId: {orderDbId}");

            var orderSpec = new OrderSpecification(orderDbId);

            var orderFromRepo = await r4mdbManager.OrdersRepository.GetByIdAsync(orderSpec);
            Assert.Equal(orderBuilder.testData.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(orderBuilder.testData.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
        }

        [Fact]
        public async void UpdateOrderAsync()
        {
            var order = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(order);
            int firstAddressDbId = order.OrderDbId;

            _route4meDbContext.SaveChanges();

            order.EXT_FIELD_first_name = "Peter Modified";
            order.EXT_FIELD_last_name = "Newman Modified";

            var updatedOrder = await r4mdbManager.OrdersRepository.UpdateOrderAsync(order.OrderDbId, order);

            _route4meDbContext.SaveChanges();

            var linqOrder = _route4meDbContext.Orders
                .Where(x => x.OrderDbId == updatedOrder.OrderDbId).FirstOrDefault();

            //Assert.Equal(updatedOrder, linqOrder);
            Assert.Equal(updatedOrder.Address1, linqOrder.Address1);
            Assert.Equal(updatedOrder.OrderDbId, linqOrder.OrderDbId);

            Assert.Equal("Peter Modified", linqOrder.EXT_FIELD_first_name);
            Assert.Equal("Newman Modified", linqOrder.EXT_FIELD_last_name);
        }
    }
}
