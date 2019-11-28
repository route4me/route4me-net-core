using Route4MeDB.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Route4MeDB.Route4MeDbLibrary
{
    /// <summary>
    /// Enumeration of the database providers supported by Route4MeDbLibrary.
    /// </summary>
    public enum DatabaseProviders
    {
        LocalDb,
        MsSql,
        Sqlexpress,
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

        public Route4MeDbManager(DatabaseProviders databaseProvider)
        {
            //var dbProvider = DatabaseProviders.PostgreSql;
            var curPath = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(curPath)
               .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();

            DatabaseProvider = databaseProvider;

            Config = config;
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
                case DatabaseProviders.MsSql:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlServer(Config.GetConnectionString("MsSqlConnection"))
                        .Options;
                    break;
                case DatabaseProviders.Sqlexpress:
                    _options = new DbContextOptionsBuilder<Route4MeDbContext>()
                        .UseSqlServer(Config.GetConnectionString("SqlExpressConnection"))
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
                        .UseMySql(Config.GetConnectionString("MySqlConnection"))
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
                case DatabaseProviders.MsSql:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlServer(Config.GetConnectionString("MsSqlConnection")));
                    break;
                case DatabaseProviders.Sqlexpress:
                    services.AddDbContext<Route4MeDbContext>(options =>
                    options.UseSqlServer(Config.GetConnectionString("SqlExpressConnection")));
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
                    options.UseMySql(Config.GetConnectionString("MySqlConnection")));
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
        }

        public void CreateRoute4MeDB()
        {

        }

    }
}
