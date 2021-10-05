using Castle.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Route4MeSDK.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using Xunit;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace ClientFactoryTest
{
    public class Startup
    {
        //private readonly IWebHostEnvironment _env;

        //public Startup(IConfiguration configuration, IWebHostEnvironment env)
        //{
        //    Configuration = configuration;
        //    _env = env;
        //}

        //public IConfiguration Configuration { get; }

        //public ILoggerFactory _loggerFactory { get; }

        public Startup()
        {
            //_loggerFactory = new ILoggerFactory();
        }

        //private IHttpClientFactory _httpClientFactory;

        public void ConfigureHost(IHostBuilder hostBuilder) =>
            hostBuilder.ConfigureWebHost(webHostBuilder => webHostBuilder
                .UseTestServer()
                .Configure(Configure));

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<Dependency>();

            //var provider = services.BuildServiceProvider();

            //var dependency = provider.GetRequiredService<Dependency>();
            //Console.WriteLine(dependency.Name);
            services.AddControllersWithViews();
            services.AddHttpClient();

            services.AddHttpClient<Route4MeApi4Service>()
                .ConfigurePrimaryHttpMessageHandler((c) =>
                     new SocketsHttpHandler()
                     {
                         AllowAutoRedirect = false,
                         SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                         {
                             EnabledSslProtocols = SslProtocols.Tls13 |
                                                   SslProtocols.Tls12 |
                                                   SslProtocols.Tls11 |
                                                   SslProtocols.Tls
                         }
                     }
                   );
                //.ConfigureHttpMessageHandlerBuilder((c) =>
                //     new SocketsHttpHandler()
                //     {
                //         AllowAutoRedirect = false,
                //         SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                //         {
                //             EnabledSslProtocols = SslProtocols.Tls13 | 
                //                                   SslProtocols.Tls12 | 
                //                                   SslProtocols.Tls11 | 
                //                                   SslProtocols.Tls
                //         }
                //     }
                //   );

            //services.AddSingleton<Route4MeApi4Service>();

            //_httpClientFactory = services.
        }


        private void Configure(IApplicationBuilder app) 
         {
            //app.Run(context =>
            //context.Response.WriteAsync(TestServerTest.Key //"Hello World"
            //    ));
            var loggerFactory = LoggerFactory.Create(builder => { /*configure*/ });

            //var dependency = app
            //    .ApplicationServices
            //    .GetRequiredService<Dependency>();

            //app.ApplicationServices.GetRequiredService<HttpClient>();

            //.AddConsole()
            //.AddDebug();
            //_httpClientFactory = HttpClientFactory
            //var httpClient = _httpClientFactory.CreateClient();
            //var logger = loggerFactory.CreateLogger("TodoApi.Startup");


            //TestServerTest.httpClient = httpClient;

            /*
            app.Run(context => {
                return context.Response.WriteAsync(TestServerTest.Key);
            });
            */
        }



        //public void Configure(IApplicationBuilder app)
        //{
        //    app.Run(async (context) =>
        //    {
        //        //TestServerTest.httpClient = _httpClientFactory.CreateClient();

        //        await context.Response.WriteAsync("Hello World!");
        //    });
        //}
    }
}
