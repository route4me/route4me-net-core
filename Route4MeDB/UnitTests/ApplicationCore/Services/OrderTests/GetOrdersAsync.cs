using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.OrderTests
{
    public class GetOrdersAsync
    {
        private OrderRepository _mockOrderRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper output;

        public GetOrdersAsync(ITestOutputHelper outputHelper)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockOrderRepo = new OrderRepository(_mockContext);
            output = outputHelper;
        }

        [Fact]
        public async Task Should_InvokeGetOrdersAsync_Once()
        {
            Order order1 = Fixture.Build<Order>()
                .With(o => o.Address1, "address1")
                .With(o => o.AddressAlias, "alias1").Create();
            await _mockOrderRepo.AddAsync(order1);

            Order order2 = Fixture.Build<Order>()
                .With(o => o.Address1, "address2")
                .With(o => o.AddressAlias, "alias2").Create();
            await _mockOrderRepo.AddAsync(order2);

            Order order3 = Fixture.Build<Order>()
                .With(o => o.Address1, "address3")
                .With(o => o.AddressAlias, "alias3").Create();
            await _mockOrderRepo.AddAsync(order3);

            var orderService = new OrderService(_mockOrderRepo);

            var results = await orderService.GetOrdersAsync(0, 3);
            output.WriteLine("results -> " + results.ToString());

            Assert.True(results.Count() == 3);
        }
    }
}
