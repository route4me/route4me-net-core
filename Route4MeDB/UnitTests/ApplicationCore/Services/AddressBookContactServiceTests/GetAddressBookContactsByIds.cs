using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Services;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using Route4MeDB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoFixture;

namespace Route4MeDB.UnitTests.ApplicationCore.Services.AddressBookContactServiceTests
{
    public class GetAddressBookContactsByIds
    {
        private AddressBookContactRepository _mockContactRepo;
        private Route4MeDbContext _mockContext;
        private DbContextOptions<Route4MeDbContext> _options;
        private static readonly Fixture Fixture = new Fixture();
        private readonly ITestOutputHelper output;

        public GetAddressBookContactsByIds(ITestOutputHelper outputHelper)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
            .UseInMemoryDatabase(databaseName: "Route4MeDB").Options;
            _mockContext = new Route4MeDbContext(_options);
            _mockContactRepo = new AddressBookContactRepository(_mockContext);
            output = outputHelper;
        }

        [Fact]
        public async Task Should_InvokeGetAddressBookContactsByIdAsync_Once()
        {
            AddressBookContact contact1 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address1, "address1")
                .With(c => c.AddressAlias, "alias1").Create();

            var createdContact1 = await _mockContactRepo.AddAsync(contact1);

            AddressBookContact contact2 = Fixture.Build<AddressBookContact>()
                .With(c => c.Address2, "address2")
                .With(c => c.AddressAlias, "alias2").Create();

            var createdContact2 = await _mockContactRepo.AddAsync(contact2);

            _mockContext.SaveChanges();

            var contactIDs = new List<int>()
            {
                createdContact1.AddressId, createdContact2.AddressId
            };

            var contactService = new AddressBookContactService(_mockContactRepo);

            var results = await contactService.GetAddressBookContactsByIdsAsync(contactIDs.ToArray());
            output.WriteLine("results -> ");
            output.WriteLine(results.ToString());

            Assert.True(results.Count() >= 2);
        }
    }
}
