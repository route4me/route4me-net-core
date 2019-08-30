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
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Route4MeDB.FunctionalTests.LocalDb
{
    public class OrderTests
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly Route4MeDbManager r4mdbManager;
        private OrderBuilder orderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;
        private readonly OrderRepository _orderRepository;

        public IConfigurationRoot Configuration { get; }

        public OrderTests(ITestOutputHelper output)
        {
            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            Route4MeDbManager.DatabaseProvider = DatabaseProviders.LocalDb;
            r4mdbManager = new Route4MeDbManager(config);

            _route4meDbContext = r4mdbManager.Route4MeContext;

            _orderRepository = new OrderRepository(_route4meDbContext);
            _output = output;
        }

        [Fact]
        public async Task GetOrdersTest()
        {
            var orderDbIDs = new List<int>();

            var firstOrder = orderBuilder.WithDefaultValues();
            await _route4meDbContext.Orders.AddAsync(firstOrder);

            int firstOrderDbId = firstOrder.OrderDbId;
            orderDbIDs.Add(firstOrderDbId);

            var secondOrder = orderBuilder.WithCustomData();
            await _route4meDbContext.Orders.AddAsync(secondOrder);

            int secondOrderDbId = secondOrder.OrderDbId;
            orderDbIDs.Add(secondOrderDbId);

            await _route4meDbContext.SaveChangesAsync();

            var orders = _route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var linqOrders = _route4meDbContext.Orders
                .Where(x => orderDbIDs.Contains(x.OrderDbId)).ToList<Order>();

            foreach (var linqOrder in linqOrders)
            {
                Assert.Contains<int>(linqOrder.OrderDbId, orders.Select(x => x.AddressDbId));
            }
        }

        [Fact]
        public async Task GetExistingOrderAsync()
        {
            var firstOrder = orderBuilder.WithDefaultValues();
            await _route4meDbContext.Orders.AddAsync(firstOrder);

            await _route4meDbContext.SaveChangesAsync();

            int createdOrderDbId = firstOrder.OrderDbId;

            var orderFromRepo = await _orderRepository
                .GetOrderByIdAsync(createdOrderDbId);

            var linqOrder = _route4meDbContext.Orders
                .Where(x => createdOrderDbId == x.OrderDbId).FirstOrDefault();

            Assert.Equal(createdOrderDbId, orderFromRepo.OrderDbId);
            Assert.Equal(firstOrder.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(firstOrder.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
            Assert.Equal(firstOrder.EXT_FIELD_first_name, linqOrder.EXT_FIELD_first_name);
            Assert.Equal(firstOrder.EXT_FIELD_last_name, linqOrder.EXT_FIELD_last_name);
        }

        [Fact]
        public async Task UpdateOrderAsync()
        {
            var order = orderBuilder.WithDefaultValues();
            await _route4meDbContext.Orders.AddAsync(order);

            await _route4meDbContext.SaveChangesAsync();

            order.EXT_FIELD_first_name = "Peter Modified";
            order.EXT_FIELD_last_name = "Newman Modified";

            var updatedOrder = await _orderRepository
                .UpdateOrderAsync(order.OrderDbId, order);

            await _route4meDbContext.SaveChangesAsync();

            var linqOrder = _route4meDbContext.Orders
                .Where(x => x.OrderDbId == updatedOrder.OrderDbId).FirstOrDefault();

            Assert.Equal<Order>(updatedOrder, linqOrder);
        }
    }
}
