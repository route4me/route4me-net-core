using System;
using Xunit;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Route4MeDB.FunctionalTest
{
    public class IgnoreNoServerDb : FactAttribute
    {
        public void CheckServerDb(DatabaseProviders dbProvider)
        {
            Route4MeDbContext _route4meDbContext;
            Route4MeDbManager r4mdbManager;

            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            Route4MeDbManager.DatabaseProvider = dbProvider;
            r4mdbManager = new Route4MeDbManager(config);

            _route4meDbContext = r4mdbManager.Route4MeContext;

            try
            {
                if (!_route4meDbContext.Database.CanConnect())
                {
                    Skip = "Cannot find "+ dbProvider + " server";
                    //return;
                }
                if (!(_route4meDbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    Skip = "Route4MeDB not exists in the " + dbProvider + " server.";
                    return;
                }
            }
            catch (Exception)
            {
                Skip = "Cannot run the tests for " + dbProvider + " server.";
            }
        }
    }
}
