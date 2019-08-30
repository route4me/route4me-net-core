using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Route4MeDB.UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Collections.Generic;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;

namespace Route4MeDB.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class AddressBookContactRepositoryTests
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly AddressBookContactRepository _addressBookContactRepository;
        private AddressBookContactBuilder addressBookContactBuilder { get; } = new AddressBookContactBuilder();
        private readonly ITestOutputHelper _output;

        public AddressBookContactRepositoryTests(ITestOutputHelper output)
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
            int addressDbId = existingAddressBookContact.AddressDbId;
            _output.WriteLine($"AddressDbId: {addressDbId}");

            var addressSpec = new AddressBookContactSpecification(addressDbId);

            var addressBookContactFromRepo = await _addressBookContactRepository.GetByIdAsync(addressSpec);

            Assert.Equal(addressBookContactBuilder.testData.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(addressBookContactBuilder.testData.LastName, addressBookContactFromRepo.LastName);
        }

        [Fact]
        public async Task GetsExistingAddressBookContactAsync()
        {
            var firstAddressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(firstAddressBookContact);
            int firstAddressDbId = firstAddressBookContact.AddressDbId;

            var secondAddressBookContact = addressBookContactBuilder.WithSchedule();
            _route4meDbContext.AddressBookContacts.Add(secondAddressBookContact);
            int secondAddressDbId = secondAddressBookContact.AddressDbId;

            _route4meDbContext.SaveChanges();

            var addressBookContactFromRepo = await _addressBookContactRepository.GetAddressBookContactByIdAsync(secondAddressDbId);

            Assert.Equal(secondAddressDbId, addressBookContactFromRepo.AddressDbId);
            Assert.Equal(secondAddressBookContact.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(secondAddressBookContact.LastName, addressBookContactFromRepo.LastName);
        }

        [Fact]
        public async Task GetAddressBookContactsAsync()
        {
            var addresDbIDs = new List<int>();

            var firstAddressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(firstAddressBookContact);
            int firstAddressDbId = firstAddressBookContact.AddressDbId;
            addresDbIDs.Add(firstAddressDbId);

            var secondAddressBookContact = addressBookContactBuilder.WithSchedule();
            _route4meDbContext.AddressBookContacts.Add(secondAddressBookContact);
            int secondAddressDbId = secondAddressBookContact.AddressDbId;
            addresDbIDs.Add(secondAddressDbId);

            _route4meDbContext.SaveChanges();

            var contacts = await _addressBookContactRepository.GetAddressBookContactsAsync(0, 1000);

            var linqContacts = _route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }

        [Fact]
        public async Task UpdateAddressBookContactAsync()
        {
            var addressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(addressBookContact);
            //uint firstAddressDbId = addressBookContact.AddressDbId;

            _route4meDbContext.SaveChanges();

            addressBookContact.FirstName = "Peter Modified";
            addressBookContact.LastName = "Newman Modified";

            var updatedContact = await _addressBookContactRepository
                .UpdateAddressBookContactAsync(addressBookContact.AddressDbId, addressBookContact);

            _route4meDbContext.SaveChanges();

            var linqContact = _route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId== updatedContact.AddressDbId).FirstOrDefault();

            Assert.Equal<AddressBookContact>(updatedContact, linqContact);
        }
    }
}

