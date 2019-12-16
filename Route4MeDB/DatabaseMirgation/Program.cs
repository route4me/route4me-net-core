using Route4MeDB.Route4MeDbLibrary;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Route4MeDB.UnitTests.Builders;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace RouteMeDB.DatabaseMirgation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify here a database provider
            var dbProvider = DatabaseProviders.SQLite;
            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            Route4MeDbManager.DatabaseProvider = dbProvider;

            var r4mdbManager = new Route4MeDbManager(config);
           
            _route4meDbContext = r4mdbManager.Route4MeContext;

            //Specify here the parameters for appropriate DB enigne
            var optBuilder= new DbContextOptionsBuilder<Route4MeDbContext>();

            switch (dbProvider)
            {
                case DatabaseProviders.LocalDb:
                    optBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                    break;
                case DatabaseProviders.Sqlexpress:
                    optBuilder.UseSqlServer(config.GetConnectionString("SqlExpressConnection"));
                    break;
                case DatabaseProviders.MySql:
                    optBuilder.UseMySql(config.GetConnectionString("MySqlConnection"));
                    break;
                case DatabaseProviders.SQLite:
                    optBuilder.UseSqlite(config.GetConnectionString("SQLiteConnection"));
                    break;
                case DatabaseProviders.PostgreSql:
                    optBuilder.UseNpgsql(config.GetConnectionString("PostgreSqlConnection"));
                    break;
            }

            var services = new ServiceCollection()
            .AddLogging().AddSingleton<DbContextOptions>(optBuilder.Options)
            .AddOptions()
            .AddDbContext<Route4MeDbContext>()
            .BuildServiceProvider();

            if ((_route4meDbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                _route4meDbContext.Database.EnsureDeleted();


            _route4meDbContext.Database.EnsureCreated();

            if (dbProvider == DatabaseProviders.MySql)
            {
                string command = @"CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );";
                _route4meDbContext.Database.ExecuteSqlCommandAsync(command).Wait();
            }

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
        
        private string GetRoute4MeDbBaseDir(string curPath)
        {
            var dirinfo = new DirectoryInfo(curPath);

            while (dirinfo.Name != "Route4MeDB")
            {
                curPath = dirinfo.Parent.FullName;
                dirinfo = new DirectoryInfo(curPath);
            }

            return curPath;
        }

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
