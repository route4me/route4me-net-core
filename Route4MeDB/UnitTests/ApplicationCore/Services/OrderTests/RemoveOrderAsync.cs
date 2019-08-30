using Route4MeDB.ApplicationCore.Entities.OrderAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.OrderTests
{
    public class RemoveOrderAsync
    {
        private OrderRepository _mockOrderRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper output;

        public RemoveOrderAsync(ITestOutputHelper outputHelper)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockOrderRepo = new OrderRepository(_mockContext);
            output = outputHelper;
        }

        [Fact]
        public async Task Should_InvokeRemoveOrderAsync_Once()
        {
            Order order1 = Fixture.Build<Order>()
                .With(o => o.Address1, "address1")
                .With(o => o.AddressAlias, "alias1").Create();

            Order order = await _mockOrderRepo.AddAsync(order1);

            int retOrderDbId = order.OrderDbId;

            var orderService = new OrderService(_mockOrderRepo);

            var result = await orderService.GetOrderByIdAsync(retOrderDbId);

            var remResult = await orderService.RemoveOrdersAsync(new int[] { result.OrderDbId });
            output.WriteLine("remResult -> " + remResult.ToString());

            Assert.True(remResult);
        }
    }
}
