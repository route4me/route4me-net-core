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
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Specifications;
namespace Route4MeDB.FunctionalTests.BigQueryDb
{
    public class DatabaseContactsFixture : DatabaseFixtureBase
    {
        public DatabaseContactsFixture()
        {
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.BigQuery;

            GetDbContext(DatabaseProviders.BigQuery);

            _addressBookContactRepository = new AddressBookContactRepository(_route4meDbContext);
        }

        public AddressBookContactRepository _addressBookContactRepository;
    }

    public class AddressBookContactTests : FactAttribute, IClassFixture<DatabaseContactsFixture>
    {
        private readonly ITestOutputHelper _output;
        DatabaseContactsFixture fixture;
        public IConfigurationRoot Configuration { get; }

        public AddressBookContactTests(DatabaseContactsFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            _output = output;
        }

        [IgnoreIfNoBigQueryDb]
        public async Task GetAddressBookContactsTest()
        {
            var addresDbIDs = new List<int>();

            var firstAddressBookContact = fixture.addressBookContactBuilder.WithDefaultValues();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(firstAddressBookContact);
            int firstAddressDbId = firstAddressBookContact.AddressDbId;
            addresDbIDs.Add(firstAddressDbId);

            var secondAddressBookContact = fixture.addressBookContactBuilder.WithCustomData();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(secondAddressBookContact);
            int secondAddressDbId = secondAddressBookContact.AddressDbId;
            addresDbIDs.Add(secondAddressDbId);

            var thirdAddressBookContact = fixture.addressBookContactBuilder.WithSchedule();
            await fixture._route4meDbContext.AddressBookContacts.AddAsync(thirdAddressBookContact);
            int thirdAddressDbId = thirdAddressBookContact.AddressDbId;
            addresDbIDs.Add(thirdAddressDbId);

            await fixture._route4meDbContext.SaveChangesAsync();

            var contacts = fixture._route4meDbContext.AddressBookContacts.Skip(0).Take(2);

            var linqContacts = fixture._route4meDbContext.AddressBookContacts
                .Where(x => addresDbIDs.Contains(x.AddressDbId)).ToList<AddressBookContact>();

            foreach (var linqContact in linqContacts)
            {
                Assert.Contains(linqContact, contacts);
            }
        }
    }
}
