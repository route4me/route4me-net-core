using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.OrderTests
{
    public class GetOrdersByIdsAsync
    {
        private OrderRepository _mockOrderRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper _output;

        public GetOrdersByIdsAsync(ITestOutputHelper output)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockOrderRepo = new OrderRepository(_mockContext);
            _output = output;
        }

        [Fact]
        public async Task Should_InvokeGetOrderByIdAsync_Once()
        {
            Order order1 = Fixture.Build<Order>()
                .With(o => o.Address1, "address1")
                .With(o => o.AddressAlias, "alias1").Create();

            var createdOrder1 = await _mockOrderRepo.AddAsync(order1);

            Order order2 = Fixture.Build<Order>()
                .With(o => o.Address2, "address2")
                .With(o => o.AddressAlias, "alias2").Create();

            var createdOrder2 = await _mockOrderRepo.AddAsync(order2);

            _mockContext.SaveChanges();

            var orderDbIDs = new List<int>()
            {
                createdOrder1.OrderDbId, createdOrder2.OrderDbId
            };

            var orderService = new OrderService(_mockOrderRepo);

            var results = await orderService.GetOrdersByIDsAsync(orderDbIDs.ToArray());
            _output.WriteLine($"results: {results}");

            Assert.True(results.Count() >= 2);
        }
    }
}
