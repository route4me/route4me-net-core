using System;
using Xunit.Abstractions;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.UnitTests.Builders;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Route4MeDB.FunctionalTests
{
    public class DatabaseFixtureBase : IDisposable
    {
        public Route4MeDbContext _route4meDbContext;
        public Route4MeDbManager r4mdbManager;
        public ITestOutputHelper _output;
        public AddressBookContactBuilder addressBookContactBuilder { get; } = new AddressBookContactBuilder();
        public OrderBuilder orderBuilder { get; } = new OrderBuilder();

        public void GetDbContext(DatabaseProviders dbProvider)
        {
            Route4MeDbManager.DatabaseProvider = dbProvider;

            var curPath = Directory.GetCurrentDirectory();

            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            r4mdbManager = new Route4MeDbManager(config);

            _route4meDbContext = r4mdbManager.Route4MeContext;
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
