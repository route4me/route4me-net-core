﻿using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.OrderTests
{
    public class UpdateOrderAsync
    {
        private OrderRepository _mockOrderRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper output;

        public UpdateOrderAsync(ITestOutputHelper outputHelper)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockOrderRepo = new OrderRepository(_mockContext);
            output = outputHelper;
        }

        [Fact]
        public async Task Should_InvokeUpdateOrderAsync_Once()
        {
            Order order1 = Fixture.Build<Order>()
                .With(o => o.Address1, "address1")
                .With(o => o.AddressAlias, "alias1").Create();

            Order order = await _mockOrderRepo.AddAsync(order1);

            int retOrderId = order.OrderId;

            var orderService = new OrderService(_mockOrderRepo);

            var result = await orderService.GetOrderByIdAsync(retOrderId);

            order.Address1 = "address1 Modified";
            order.AddressAlias = "alias1 Modified";

            Order orderUpdated = await orderService.UpdateOrderAsync(order.OrderId, order);

            output.WriteLine("orderUpdated -> " + orderUpdated.ToString());

            Assert.NotNull(orderUpdated);
            Assert.Equal("address1 Modified", orderUpdated.Address1);
            Assert.Equal("alias1 Modified", orderUpdated.AddressAlias);
        }
    }
}
