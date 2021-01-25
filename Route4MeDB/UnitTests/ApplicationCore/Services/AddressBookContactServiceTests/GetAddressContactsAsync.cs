using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Interfaces;
using Route4MeDB.ApplicationCore.Services;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.AddressBookContactServiceTests
{
    public class GetAddressContactsAsync
    {
        private AddressBookContactRepository _mockContactRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper output;

        public GetAddressContactsAsync(ITestOutputHelper outputHelper)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockContactRepo = new AddressBookContactRepository(_mockContext);
            output = outputHelper;
        }

        [Fact]
        public async Task Should_InvokeGetAddressBookContactsAsync_Once()
        {
            AddressBookContact contact1 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address1, "address1")
                .With(c => c.AddressAlias, "alias1").Create();
            await _mockContactRepo.AddAsync(contact1);

            AddressBookContact contact2 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address1, "address2")
                .With(c => c.AddressAlias, "alias2").Create();
            await _mockContactRepo.AddAsync(contact2);

            AddressBookContact contact3 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address1, "address3")
                .With(c => c.AddressAlias, "alias3").Create();
            await _mockContactRepo.AddAsync(contact3);

            var addressBookService = new AddressBookContactService(_mockContactRepo);

            var results = await addressBookService.GetAddressBookContactsAsync(0,3);
            output.WriteLine("results ### -> " + results.ToString());
            output.WriteLine("results count ### -> " + results.Count());

            Assert.True(results.Count() == 3);
        }
    }
}
