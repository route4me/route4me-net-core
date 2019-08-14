using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Route4MeDB.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Route4MeDB.ApplicationCore.Specifications;

namespace Route4MeDB.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetOrderId
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly OrderRepository _orderRepository;
        private OrderBuilder orderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;

        public GetOrderId(ITestOutputHelper output)
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
            int orderId = existingOrder.Id;
            _output.WriteLine($"OrderId: {orderId}");

            var orderFromRepo = await _orderRepository.GetByIdAsync(orderId);
            Assert.Equal(orderBuilder.testData.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(orderBuilder.testData.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
        }

        [Fact]
        public async Task GetsExistingOrderAsync()
        {
            var firstOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(firstOrder);
            int firstOrdeId = firstOrder.Id;

            var secondOrder = orderBuilder.WithDefaultValues();
            _route4meDbContext.Orders.Add(secondOrder);
            int secondOrdeId = secondOrder.Id;

            _route4meDbContext.SaveChanges();

            var orderFilterSpecification = new OrderSpecification();
            int orderCount = await _orderRepository.CountAsync(orderFilterSpecification);
            Assert.True(orderCount>=2);

            var orderFromRepo = await _orderRepository.GetOrderByIdAsync(secondOrdeId);

            Assert.Equal(secondOrdeId, orderFromRepo.Id);
            Assert.Equal(secondOrder.EXT_FIELD_first_name, orderFromRepo.EXT_FIELD_first_name);
            Assert.Equal(secondOrder.EXT_FIELD_last_name, orderFromRepo.EXT_FIELD_last_name);
        }
    }
}
