using Route4MeDB.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Route4MeDB.Route4MeDbLibrary
{
    public class CreateRoute4MeDb
    {
        static Route4MeDbContext _route4meDbContext;

        public CreateRoute4MeDb(DatabaseProviders databaseProvider)
        {
            //var dbProvider = DatabaseProviders.PostgreSql;
            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            Route4MeDbManager.DatabaseProvider = databaseProvider;

            var r4mdbManager = new Route4MeDbManager(config);

            _route4meDbContext = r4mdbManager.Route4MeContext;

            var optBuilder = new DbContextOptionsBuilder<Route4MeDbContext>();

            switch (databaseProvider)
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

            if (databaseProvider == DatabaseProviders.MySql)
            {
                string command = @"CREATE TABLE `__EFMigrationsHistory` ( `MigrationId` nvarchar(150) NOT NULL, `ProductVersion` nvarchar(32) NOT NULL, PRIMARY KEY (`MigrationId`) );";
                _route4meDbContext.Database.ExecuteSqlCommandAsync(command).Wait();
            }

            IApplicationBuilder app = new ApplicationBuilder(services);

            r4mdbManager.MigrateDatabase(app);

            Task.Run(async () =>
            {
                await _route4meDbContext.SaveChangesAsync();
            });
        }

    }
}
