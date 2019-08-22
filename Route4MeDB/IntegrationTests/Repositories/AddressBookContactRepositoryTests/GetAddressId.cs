using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Route4MeDB.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Route4MeDB.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetAddressId
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly AddressBookContactRepository _addressBookContactRepository;
        private AddressBookContactBuilder addressBookContactBuilder { get; } = new AddressBookContactBuilder();
        private readonly ITestOutputHelper _output;

        public GetAddressId(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseInMemoryDatabase(databaseName: "Route4MeDB")
                .Options;
            _route4meDbContext = new Route4MeDbContext(dbOptions);
            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
        }

        [Fact]
        public async Task GetsExistingAddressBookContact()
        {
            var existingAddressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(existingAddressBookContact);
            _route4meDbContext.SaveChanges();
            int addressId = existingAddressBookContact.AddressId;
            _output.WriteLine($"AddressId: {addressId}");

            var addressSpec = new AddressBookContactSpecification(addressId);

            var addressBookContactFromRepo = await _addressBookContactRepository.GetByIdAsync(addressSpec);

            Assert.Equal(addressBookContactBuilder.testData.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(addressBookContactBuilder.testData.LastName, addressBookContactFromRepo.LastName);
        }

        [Fact]
        public async Task GetsExistingAddressBookContactAsync()
        {
            var firstAddressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(firstAddressBookContact);
            int firstAddressId = firstAddressBookContact.AddressId;

            var secondAddressBookContact = addressBookContactBuilder.WithSchedule();
            _route4meDbContext.AddressBookContacts.Add(secondAddressBookContact);
            int secondAddressId = secondAddressBookContact.AddressId;

            _route4meDbContext.SaveChanges();

            var addressBookContactFromRepo = await _addressBookContactRepository.GetAddressBookContactByIdAsync(secondAddressId);

            Assert.Equal(secondAddressId, addressBookContactFromRepo.AddressId);
            Assert.Equal(secondAddressBookContact.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(secondAddressBookContact.LastName, addressBookContactFromRepo.LastName);
        }
    }
}
