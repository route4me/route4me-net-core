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
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Route4MeDB.FunctionalTests.LocalDb
{
    public class AddressBookContactTests
    {
        private readonly Route4MeDbContext _route4meDbContext;
        private readonly Route4MeDbManager r4mdbManager;
        private AddressBookContactBuilder addressBookContactBuilder { get; } = new AddressBookContactBuilder();
        private readonly ITestOutputHelper _output;
        private readonly AddressBookContactRepository _addressBookContactRepository;

        public IConfigurationRoot Configuration { get; }

        public AddressBookContactTests(ITestOutputHelper output)
        {
            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
   .SetBasePath(curPath)
   .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            //var config = (new ConfigurationBuilder()).Build();

            //var builder = new ConfigurationBuilder();
            //builder.AddJsonFile("appsettings.json", optional: false);
             
            //var configuration = builder.Build();
            //AppSettingsReader appreader = new AppSettingsReader();
            //var appSettings = ConfigurationManager.AppSettings;
            //var _connectionString = configuration.GetConnectionString("DefaultConnection");

            Route4MeDbManager.DatabaseProvider = DatabaseProviders.LocalDb;
            r4mdbManager = new Route4MeDbManager(config);

            _route4meDbContext = r4mdbManager.Route4MeContext;

            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
            _output = output;
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
        public async Task GetExistingAddressBookContactAsync()
        {
            var firstAddressBookContact = addressBookContactBuilder.WithDefaultValues();
            await _route4meDbContext.AddressBookContacts.AddAsync(firstAddressBookContact);

            await _route4meDbContext.SaveChangesAsync();

            int createdAddressDbId = firstAddressBookContact.AddressDbId;

            var addressBookContactFromRepo = await _addressBookContactRepository
                .GetAddressBookContactByIdAsync(createdAddressDbId);

            var linqContact = _route4meDbContext.AddressBookContacts
                .Where(x => createdAddressDbId == x.AddressDbId).FirstOrDefault();

            Assert.Equal(createdAddressDbId, addressBookContactFromRepo.AddressDbId);
            Assert.Equal(firstAddressBookContact.FirstName, addressBookContactFromRepo.FirstName);
            Assert.Equal(firstAddressBookContact.LastName, addressBookContactFromRepo.LastName);
            Assert.Equal(firstAddressBookContact.FirstName, linqContact.FirstName);
            Assert.Equal(firstAddressBookContact.LastName, linqContact.LastName);
        }

        [Fact]
        public async Task UpdateAddressBookContactAsync()
        {
            var addressBookContact = addressBookContactBuilder.WithDefaultValues();
            await _route4meDbContext.AddressBookContacts.AddAsync(addressBookContact);

            await _route4meDbContext.SaveChangesAsync();

            addressBookContact.FirstName = "Peter Modified";
            addressBookContact.LastName = "Newman Modified";

            var updatedContact = await _addressBookContactRepository
                .UpdateAddressBookContactAsync(addressBookContact.AddressDbId, addressBookContact);

            await _route4meDbContext.SaveChangesAsync();

            var linqContact = _route4meDbContext.AddressBookContacts
                .Where(x => x.AddressDbId == updatedContact.AddressDbId).FirstOrDefault();

            Assert.Equal<AddressBookContact>(updatedContact, linqContact);
        }
    }
}
