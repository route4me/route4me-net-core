using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Route4MeDB.FunctionalTest;

namespace Route4MeDB.FunctionalTests.MySqlDb
{
    public class DatabaseContactsFixture : DatabaseFixtureBase
    {
        public DatabaseContactsFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.MySql;

            GetDbContext(DatabaseProviders.MySql);

            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
        }

        public readonly AddressBookContactRepository _addressBookContactRepository;
    }

    public class AddressBookContactTests : FactAttribute, IClassFixture<DatabaseContactsFixture>, IDisposable
    {
        public IConfigurationRoot Configuration { get; }
        DatabaseContactsFixture fixture;
        private readonly ITestOutputHelper _output;

        public AddressBookContactTests(DatabaseContactsFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        public void Dispose()
        {
            //this.Dispose();
        }

        [IgnoreIfNoMySqlDb]
        public async Task GetAddressBookContactsTest()
        {
            var addresDbIDs = new List<int>();

            var firstAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(firstAddressBookContact);
            int firstAddressDbId = firstAddressBookContact.AddressDbId;
            addresDbIDs.Add(firstAddressDbId);

            var secondAddressBookContact = fixture.addressBookContactBuilder.WithSchedule();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(secondAddressBookContact);
            int secondAddressDbId = secondAddressBookContact.AddressDbId;
            addresDbIDs.Add(secondAddressDbId);

            await fixture._route4meDbContext.SaveChangesAsync();

            var contacts = fixture._route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var linqContacts = fixture._route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }

        [IgnoreIfNoMySqlDb]
        public async Task GetExistingAddressBookContactAsync()
        {
            var firstAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(firstAddressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            int createdAddressDbId = firstAddressBookContact.AddressDbId;

            var addressBookContactFromRepo = await fixture._addressBookContactRepository
                .GetAddressBookContactByIdAsync(createdAddressDbId);

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => createdAddressDbId == x.AddressDbId).FirstOrDefault();

            Assert.Equal(createdAddressDbId, addressBookContactFromRepo.AddressDbId);
            Assert.Equal(firstAddressBookContact.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(firstAddressBookContact.LastName, addressBookContactFromRepo.LastName);
            Assert.Equal(firstAddressBookContact.FirstName, linqContact.FirstName);
            Assert.Equal(firstAddressBookContact.LastName, linqContact.LastName);
        }

        [IgnoreIfNoMySqlDb]
        public async Task UpdateAddressBookContactAsync()
        {
            var addressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            addressBookContact.FirstName = "Peter Modified";
            addressBookContact.LastName = "Newman Modified";

            var updatedContact = await fixture._addressBookContactRepository
                .UpdateAddressBookContactAsync(addressBookContact.AddressDbId, addressBookContact);

            await fixture._route4meDbContext.SaveChangesAsync();

            var linqContact = fixture._route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == updatedContact.AddressDbId).FirstOrDefault();

            Assert.Equal<AddressBookContact>(updatedContact, linqContact);
        }
    }
}
