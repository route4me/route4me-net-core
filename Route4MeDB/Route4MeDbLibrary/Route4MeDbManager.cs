using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AutotrackEntityChange.DBContext;
using Route4MeDB.Infrastructure;
using Route4MeDB.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;

namespace Route4MeDB.Route4MeDbLibrary
{
    public enum DatabaseProviders
    {
        LocalDb,
        MsSq,
        Sqlexpress,
        SqlCompact40,
        SQLite,
        PostgreSql,
        MySql,
        InMemory
    }
    public class Route4MeDbManager
    {
        public Route4MeDbManager(IConfiguration configuration)
        {
            var curPath = Directory.GetCurrentDirectory();
            Config = configuration;
            _route4meDbContext = Route4MeContext;
        }

        public IConfiguration Config { get; set; }

        private IServiceCollection _services;

        private DbContextOptions<Route4MeDbContext> _options;

        public static DatabaseProviders DatabaseProvider { get; set; }

        private Route4MeDbContext _route4meDbContext;
        public Route4MeDbContext Route4MeContext
        {
            get { return new Route4MeDbContext(GetOptions(DatabaseProvider)); }
        }

        public AddressBookContactRepository ContactsRepository
        {
            get { return new AddressBookContactRepository(Route4MeContext); }
        }

        public OrderRepository OrdersRepository
        {
            get { return new OrderRepository(Route4MeContext); }
        }

        public DbContextOptions<Route4MeDbContext> GetOptions(DatabaseProviders databaseProvider)
        {
            _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                .UseInMemoryDatabase(databaseName: "Route4MeDB")
                .Options;
            switch (DatabaseProvider)
            {
                case DatabaseProviders.LocalDb:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlServer(ConfigurationExtensions.GetConnectionString(this.Config,"DefaultConnection"))
                        .Options;
                    break;
                case DatabaseProviders.MsSq:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlServer(Config.GetConnectionString("MsSqlConnection"))
                        .Options;
                    break;
                case DatabaseProviders.Sqlexpress:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlServer(Config.GetConnectionString("SqlExpressConnection"))
                        .Options;
                    break;
                case DatabaseProviders.SqlCompact40:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlServer(Config.GetConnectionString("SqlCompact40Connection"))
                        .Options;
                    break;
                case DatabaseProviders.SQLite:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlite(Config.GetConnectionString("SQLiteConnection"))
                        .Options;
                    break;
                case DatabaseProviders.PostgreSql:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseNpgsql(Config.GetConnectionString("PostgreSqlConnection"))
                        .Options;
                    break;
                case DatabaseProviders.MySql:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseMySQL(Config.GetConnectionString("MySqlConnection"))
                        .Options;
                    break;
                default:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseInMemoryDatabase(databaseName: "Route4MeDB")
                        .Options;
                    break;
            }

            return _options;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var opt = new DbContextOptions<Route4MeDbContext>();
            //Action<DbContextOptionsBuilder> opt = new Action<DbContextOptionsBuilder>();
            switch (DatabaseProvider)
            {
                case DatabaseProviders.LocalDb:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlServer(Config.GetConnectionString("DefaultConnection")));
                    break;
                case DatabaseProviders.MsSq:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlServer(Config.GetConnectionString("MsSqlConnection")));
                    break;
                case DatabaseProviders.Sqlexpress:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlServer(Config.GetConnectionString("SqlExpressConnection")));
                    break;
                case DatabaseProviders.SqlCompact40:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlServer(Config.GetConnectionString("SqlCompact40Connection")));
                    break;
                case DatabaseProviders.SQLite:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlite(Config.GetConnectionString("SQLiteConnection")));
                    break;
                case DatabaseProviders.PostgreSql:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseNpgsql(Config.GetConnectionString("PostgreSqlConnection")));
                    break;
                case DatabaseProviders.MySql:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseNpgsql(Config.GetConnectionString("MySqlConnection")));
                    break;
                default:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseInMemoryDatabase("Route4MeDB"));
                    break;
            }


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            _services = services;
        }

        public void MigrateDatabase(IApplicationBuilder app)
        {
            _route4meDbContext.Database.Migrate();
            _route4meDbContext.SaveChanges();
            //using (var serviceScope = app.ApplicationServices
            //.GetRequiredService<IServiceScopeFactory>()
            //.CreateScope())
            //{
            //    using (var context = serviceScope.ServiceProvider.GetService<Route4MeDbContext>())
            //    {
            //        context.Database.Migrate();
            //    }
            //}
        }

        public void CreateRoute4MeDB()
        {

        }

    }
}
