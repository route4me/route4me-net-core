using Route4MeDB.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using System;

namespace Route4MeDB.Route4MeDbLibrary
{
    public class CreateRoute4MeDb
    {
        static Route4MeDbContext _route4meDbContext;

        public CreateRoute4MeDb(DatabaseProviders databaseProvider)
        {
            if (!Utils.CheckIfAppsettingsFileExists())
            {
                var eventArgs = new AppSettingsFileCreatedEventArgs();

                bool created = Utils.CreateAppsettingsFileIfNotExists(out string errorString);

                if (created)
                {
                    eventArgs.AppSettingsFileCreated = true;
                    eventArgs.MessageText = "The file appSetings.json was created with the demo connection strings. \n Please, put in it actual connection strings and then try again.";
                }
                else
                {
                    eventArgs.AppSettingsFileCreated = false;
                    eventArgs.MessageText = "Cannot create the file appSetings.json with the demo connection strings. \n Create it manually on the project root folder.";
                }

                OnAppSettingsFileCreated(eventArgs);

                return;
            }

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

        #region // AppSettingsFileCreated event handler

        public event EventHandler AppSettingsFileCreated;

        public void OnAppSettingsFileCreated(AppSettingsFileCreatedEventArgs e)
        {
            EventHandler handler = AppSettingsFileCreated;
            handler?.Invoke(this, e);
        }

        public class AppSettingsFileCreatedEventArgs : EventArgs
        {
            public bool AppSettingsFileCreated { get; set; }
            public string MessageText { get; set; }
        }

        #endregion
    }
}
