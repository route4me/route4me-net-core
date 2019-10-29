using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Services;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Route4MeDB.Infrastructure.Data;
using Route4MeDB.ApplicationCore.Interfaces;
using Route4MeDB.Infrastructure.Logging;
//using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Route4MeDB.Route4MeDbLibrary
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public DatabaseProviders DatabaseProvider { get; set; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            switch (DatabaseProvider)
            {
                case DatabaseProviders.LocalDb:
                    
                    break;
                case DatabaseProviders.MsSql:
                    
                    break;
                case DatabaseProviders.Sqlexpress:
                    
                    break;
                case DatabaseProviders.SQLite:
                    
                    break;
                case DatabaseProviders.PostgreSql:
                    
                    break;
                case DatabaseProviders.MySql:
                    
                    break;
                default:
                    ConfigureInMemoryDatabases(services);
                    break;
            }
            // use in-memory database
            //ConfigureInMemoryDatabases(services);

            // use real database
            // ConfigureProductionServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<Route4MeDbContext>(c => c.UseInMemoryDatabase("Route4MeDB"));

            // Add Identity DbContext
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<Route4MeDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")));

            // Add Identity DbContext
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ConfigureCookieSettings(services);

            //CreateIdentityIfNotCreated(services);

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();
            services.AddScoped<IAddressBookContactService, AddressBookContactService>();
            //services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAddressBookContactRepository, AddressBookContactRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<CatalogViewModelService>();
            //services.Configure<Route4MeDbSettings>(Configuration);
            //services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<Route4MeDbSettings>()));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            //services.AddTransient<IEmailSender, EmailSender>();

            // Add memory cache services
            services.AddMemoryCache();

            /*
            services.AddRouting(options =>
            {
                // Replace the type and the name used to refer to it with your own
                // IOutboundParameterTransformer implementation
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
            */
            /*
            services.AddMvc(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                         new SlugifyParameterTransformer()));

            }
            )
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Basket/Checkout");
                    options.AllowAreas = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddHealthChecks()
                .AddCheck<HomePageHealthCheck>("home_page_health_check")
                .AddCheck<ApiHealthCheck>("api_health_check");
            */
            /*
            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                config.Path = "/allservices";
            });
            */
            _services = services; // used to debug registered services
        }

        
    }
}
