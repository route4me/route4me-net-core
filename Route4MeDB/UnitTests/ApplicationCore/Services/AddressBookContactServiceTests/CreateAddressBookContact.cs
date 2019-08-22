using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.AddressBookContactServiceTests
{
    public class CreateAddressBookContact
    {
        private AddressBookContactRepository _mockContactRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();

        public CreateAddressBookContact()
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockContactRepo = new AddressBookContactRepository(_mockContext);
        }

        [Fact]
        public async Task Should_InvokeRepositoryCreateAddressBookContactAsync_Once()
        {
            AddressBookContact contact1 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address1, "address1")
                .With(c => c.AddressAlias, "alias1").Create();

            var contact = await _mockContactRepo.AddAsync(contact1);

            var contactService = new AddressBookContactService(_mockContactRepo);

            var result = await contactService.GetAddressBookContactByIdAsync(contact.AddressId);

            Assert.NotNull(result);
            Assert.Equal("address1", result.Address1);
            Assert.Equal("alias1", result.AddressAlias);

            _mockContext.Database.EnsureDeleted();
        }
    }
}
