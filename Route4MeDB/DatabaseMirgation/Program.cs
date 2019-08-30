using System;
using System.ComponentModel;
using System.Linq;
using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Route4MeDB.ApplicationCore.Specifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Route4MeDB.UnitTests.Builders;

namespace RouteMeDB.DatabaseMirgation
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();
            
            Route4MeDbManager.DatabaseProvider = DatabaseProviders.LocalDb;

            var r4mdbManager = new Route4MeDbManager(config);
           
            _route4meDbContext = r4mdbManager.Route4MeContext;

            var optBuilder = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseSqlServer(config.GetConnectionString("DefaultConnection"));

            var services = new ServiceCollection()
            .AddLogging().AddSingleton<DbContextOptions>(optBuilder.Options)
            .AddOptions()
            .AddDbContext<Route4MeDbContext>().AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            _route4meDbContext.Database.EnsureDeleted();
            _route4meDbContext.Database.EnsureCreated();

            IApplicationBuilder app = new ApplicationBuilder(services);

            // See results with Sql Studio --- server name: (LocalDB)\MSSQLLocalDB
            r4mdbManager.MigrateDatabase(app);

            var contact = (new Program()).CreateContact();

            Task.Run(async () =>
            {
                await _route4meDbContext.SaveChangesAsync();
            });
            

            _route4meDbContext.Update<AddressBookContact>(contact.Result);
        }

        private static AddressBookContactBuilder contactBuilder { get; } = new AddressBookContactBuilder();
        static Route4MeDbContext _route4meDbContext;
        

        public async Task<AddressBookContact> CreateContact()
        {
            var contactParams = contactBuilder.WithDefaultValues();

            var contact = await _route4meDbContext.AddressBookContacts.AddAsync(contactParams);

            await _route4meDbContext.SaveChangesAsync();

            return await Task.Run(() =>
            {
                return contact.Entity;
            });

        }
    }
}
