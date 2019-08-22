using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.OrderTests
{
    public class GetOrderByIdAsync
    {
        private OrderRepository _mockOrderRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();

        public GetOrderByIdAsync()
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockOrderRepo = new OrderRepository(_mockContext);
        }

        [Fact]
        public async Task Should_InvokeGetOrderByIdAsync_Once()
        {
            Order order1 = Fixture.Build<Order>()
                .With(o => o.Address1, "address1")
                .With(o => o.AddressAlias, "alias1").Create();

            Order order = await _mockOrderRepo.AddAsync(order1);

            int retOrderId = order.OrderId;

            var orderService = new OrderService(_mockOrderRepo);

            var result = await orderService.GetOrderByIdAsync(retOrderId);

            Assert.NotNull(result);
            Assert.Equal("address1", result.Address1);
            Assert.Equal("alias1", result.AddressAlias);
        }
    }
}
