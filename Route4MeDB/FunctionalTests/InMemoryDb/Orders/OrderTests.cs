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
using System.IO;

namespace Route4MeDB.FunctionalTests.InMemoryDb
{
    public class DatabaseOrdersFixture : DatabaseFixtureBase
    {
        public DatabaseOrdersFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.InMemory;

            GetDbContext(DatabaseProviders.InMemory);

            _orderRepository = new OrderRepository(_route4meDbContext);
        }

        public OrderRepository _orderRepository;
    }

    public class OrderTests : IClassFixture<DatabaseOrdersFixture>
    {
        private readonly ITestOutputHelper _output;
        DatabaseOrdersFixture fixture;
        public IConfigurationRoot Configuration { get; }

        public OrderTests(DatabaseOrdersFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        [Fact]
        public async void GetOrdersAsync()
        {
            var orderDbIDs = new List<int>();

            var firstOrder = fixture.orderBuilder.WithDefaultValues();
            fixture._route4meDbContext.Orders.Add(firstOrder);
            int firstAddressDbId = firstOrder.OrderDbId;
            orderDbIDs.Add(firstAddressDbId);

            var secondOrder = fixture.orderBuilder.WithCustomData();
            fixture._route4meDbContext.Orders.Add(secondOrder);
            int secondOrderDbId = secondOrder.OrderDbId;
            orderDbIDs.Add(secondOrderDbId);

            fixture._route4meDbContext.SaveChanges();

            var orders = await fixture.r4mdbManager.OrdersRepository.GetOrdersAsync(0, 1000);

            var linqOrders = await fixture._route4meDbContext.Orders
                .Where(x => orderDbIDs.Contains(x.OrderDbId)).ToListAsync<Order>();

            foreach (var linqOrder in linqOrders)
            {
                Assert.True(orders.Where(x => x.OrderDbId == linqOrder.OrderDbId).Count()>0);
            }
        }

        [Fact]
        public async void ImportJsonDataToDataBaseTest()
        {
            string testDataFile = @"TestData/one_complex_order.json";

            DataExchangeHelper dataExchange = new DataExchangeHelper();

            using (StreamReader reader = new StreamReader(testDataFile))
            {
                var jsonContent = reader.ReadToEnd();
                reader.Close();

                Order importedOrder = dataExchange.ConvertSdkJsonContentToEntity<Order>(jsonContent, out string errorString);

                fixture._route4meDbContext.Orders.Add(importedOrder);

                await fixture._route4meDbContext.SaveChangesAsync();
                int orderDbId = importedOrder.OrderDbId;

                var orderSpec = new OrderSpecification(orderDbId);

                var orderFromRepo = await fixture.r4mdbManager.OrdersRepository.GetByIdAsync(orderSpec);

                Assert.IsType<Order>(orderFromRepo);
            }
        }

        [Fact]
        public async void GetsExistingOrder()
        {
            var existingOrder = fixture.orderBuilder.WithDefaultValues();
            fixture._route4meDbContext.Orders.Add(existingOrder);
            fixture._route4meDbContext.SaveChanges();
            int orderDbId = existingOrder.OrderDbId;
            _output.WriteLine($"OrderDbId: {orderDbId}");

            var orderSpec = new OrderSpecification(orderDbId);

            var orderFromRepo = await fixture.r4mdbManager.OrdersRepository.GetByIdAsync(orderSpec);
            Assert.Equal(fixture.orderBuilder.testData.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(fixture.orderBuilder.testData.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
        }

        [Fact]
        public async void UpdateOrderAsync()
        {
            var order = fixture.orderBuilder.WithDefaultValues();
            fixture._route4meDbContext.Orders.Add(order);
            int firstAddressDbId = order.OrderDbId;

            fixture._route4meDbContext.SaveChanges();

            order.EXT_FIELD_first_name = "Peter Modified";
            order.EXT_FIELD_last_name = "Newman Modified";

            var updatedOrder = await fixture.r4mdbManager.OrdersRepository.UpdateOrderAsync(order.OrderDbId, order);

            fixture._route4meDbContext.SaveChanges();

            var linqOrder = fixture._route4meDbContext.Orders
                .Where(x => x.OrderDbId == updatedOrder.OrderDbId).FirstOrDefault();

            //Assert.Equal(updatedOrder, linqOrder);
            Assert.Equal(updatedOrder.Address1, linqOrder.Address1);
            Assert.Equal(updatedOrder.OrderDbId, linqOrder.OrderDbId);

            Assert.Equal("Peter Modified", linqOrder.EXT_FIELD_first_name);
            Assert.Equal("Newman Modified", linqOrder.EXT_FIELD_last_name);
        }

        [Fact]
        public async void RemoveOrderAsync()
        {
            var order = fixture.orderBuilder.WithDefaultValues();
            await fixture._route4meDbContext.Orders.AddAsync(order);

            await fixture._route4meDbContext.SaveChangesAsync();

            var createdOrderDbID = order.OrderDbId;

            var removedOrders = await fixture._orderRepository
                .RemoveOrderAsync(new int[] { order.OrderDbId });

            await fixture._route4meDbContext.SaveChangesAsync();

            Assert.Equal(removedOrders[0], createdOrderDbID);

            var linqOrder = fixture._route4meDbContext.Orders
                .Where(x => x.OrderDbId == createdOrderDbID).FirstOrDefault();

            Assert.Null(linqOrder);
        }

        [Fact]
        public async void CustomDataTestAsync()
        {
            var order = fixture.orderBuilder.WithCustomData();
            await fixture._route4meDbContext.Orders.AddAsync(order);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqOrder = fixture._route4meDbContext.Orders
                .Where(x => x.OrderDbId == order.OrderDbId).FirstOrDefault();

            Assert.Equal(order.ExtFieldCustomData, linqOrder.ExtFieldCustomData);
        }
    }
}
