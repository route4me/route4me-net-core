using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;
namespace Route4MeDB.UnitTests.ApplicationCore.Services.AddressBookContactServiceTests
{
    public class UpdateAddressBookContactAsync
    {
        private AddressBookContactRepository _mockContactRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper output;

        public UpdateAddressBookContactAsync(ITestOutputHelper outputHelper)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockContactRepo = new AddressBookContactRepository(_mockContext);
            output = outputHelper;
        }

        [Fact]
        public async Task Should_InvokeUpdateContactAsync_Once()
        {
            AddressBookContact contact1 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address1, "address1")
                .With(c => c.AddressAlias, "alias1").Create();

            AddressBookContact contact = await _mockContactRepo.AddAsync(contact1);

            var contactService = new AddressBookContactService(_mockContactRepo);

            contact.Address1 = "address1 Modified";
            contact.AddressAlias = "alias1 Modified";

            AddressBookContact contactUpdated = await contactService.UpdateAddressBookContactAsync(contact.AddressId, contact);
            output.WriteLine("contactUpdated -> " + contactUpdated.ToString());

            Assert.NotNull(contactUpdated);
            Assert.Equal("address1 Modified", contactUpdated.Address1);
            Assert.Equal("alias1 Modified", contactUpdated.AddressAlias);
        }
    }
}
