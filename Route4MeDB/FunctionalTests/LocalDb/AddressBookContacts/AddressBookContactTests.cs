using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Route4MeDB.FunctionalTest;

namespace Route4MeDB.FunctionalTests.LocalDb
{
    public class DatabaseContactsFixture : DatabaseFixtureBase
    {
        public DatabaseContactsFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.LocalDb;

            GetDbContext(DatabaseProviders.LocalDb);

            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
        }

        public AddressBookContactRepository _addressBookContactRepository;
    }

    public class AddressBookContactTests: FactAttribute, IClassFixture<DatabaseContactsFixture>
    {
        private readonly ITestOutputHelper _output;
        DatabaseContactsFixture fixture;
        public IConfigurationRoot Configuration { get; }

        public AddressBookContactTests(DatabaseContactsFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;

            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            Route4MeDbManager.DatabaseProvider = DatabaseProviders.LocalDb;
            fixture.r4mdbManager = new Route4MeDbManager(config);

            fixture._route4meDbContext = fixture.r4mdbManager.Route4MeContext;

            fixture._addressBookContactRepository = new AddressBookContactRepository(fixture._route4meDbContext);
            _output = output;
        }

        [IgnoreIfNoLocalDb]
        public void GetAddressBookContactsTest()
        {
            var addresDbIDs = new List<int>();

            var firstAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            fixture._route4meDbContext.AddressBookContacts.Add(firstAddressBookContact);
            int firstAddressDbId = firstAddressBookContact.AddressDbId;
            addresDbIDs.Add(firstAddressDbId);

            var secondAddressBookContact = fixture.addressBookContactBuilder.WithSchedule();
            fixture._route4meDbContext.AddressBookContacts.Add(secondAddressBookContact);
            int secondAddressDbId = secondAddressBookContact.AddressDbId;
            addresDbIDs.Add(secondAddressDbId);

            fixture._route4meDbContext.SaveChanges();

            var contacts = fixture._route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var linqContacts = fixture._route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }

        [IgnoreIfNoLocalDb]
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

        [IgnoreIfNoLocalDb]
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
