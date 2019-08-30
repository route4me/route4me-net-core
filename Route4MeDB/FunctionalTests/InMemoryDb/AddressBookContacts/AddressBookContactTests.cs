using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.UnitTests.Builders;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Specifications;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Route4MeDB.FunctionalTests.InMemoryDb
{
    public class AddressBookContactTests : IDisposable
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly Route4MeDbManager r4mdbManager;
        private AddressBookContactBuilder addressBookContactBuilder { get; } = new AddressBookContactBuilder();
        private readonly ITestOutputHelper _output;

        public AddressBookContactTests(ITestOutputHelper output, IConfiguration Configuration=null)
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.InMemory;
            r4mdbManager = new Route4MeDbManager(Configuration);

            _route4meDbContext = r4mdbManager.Route4MeContext;

            _output = output;
        }

        public void Dispose()
        {
            //_route4meDbContext.Dispose();
        }

        [Fact]
        public void GetAddressBookContactsTest()
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

            var contacts = _route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var linqContacts = _route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }

        [Fact]
        public async void GetsExistingAddressBookContactAsync()
        {
            var existingAddressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(existingAddressBookContact);
            _route4meDbContext.SaveChanges();
            int addressDbId = existingAddressBookContact.AddressDbId;
            _output.WriteLine($"AddressDbId: {addressDbId}");

            var addressSpec = new AddressBookContactSpecification(addressDbId);

            var addressBookContactFromRepo = await r4mdbManager.ContactsRepository.GetByIdAsync(addressSpec);

            Assert.Equal(addressBookContactBuilder.testData.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(addressBookContactBuilder.testData.LastName, addressBookContactFromRepo.LastName);
        }

        [Fact]
        public async void UpdateAddressBookContactAsync()
        {
            var addressBookContact = addressBookContactBuilder.WithDefaultValues();
            _route4meDbContext.AddressBookContacts.Add(addressBookContact);
            int firstAddressDbId = addressBookContact.AddressDbId;

            _route4meDbContext.SaveChanges();

            addressBookContact.FirstName = "Peter Modified";
            addressBookContact.LastName = "Newman Modified";

            var updatedContact = await r4mdbManager.ContactsRepository
                .UpdateAddressBookContactAsync(addressBookContact.AddressDbId, addressBookContact);

            _route4meDbContext.SaveChanges();

            var linqContact = _route4meDbContext.AddressBookContacts
                .Where(x => x.AddressId == updatedContact.AddressId).FirstOrDefault();

            Assert.Equal(updatedContact.Address1, linqContact.Address1);
            Assert.Equal(updatedContact.AddressId, linqContact.AddressId);
        }
    }
}
