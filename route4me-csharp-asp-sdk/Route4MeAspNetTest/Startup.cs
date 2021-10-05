using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route4MeSDK;
using Route4MeSDK.Controllers;
using Route4MeSDK.QueryTypes;
using Route4MeSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route4MeAspNetTest
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<Route4MeApi4Service>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var r4mService = new Route4MeApi4Service(context.RequestServices.GetService<Route4MeApi4Service>().Client);
                    var r4mController = new Route4MeApi4Controller(r4mService, r4mService.Client);

                    var activityParameters = new ActivityParameters()
                    {
                        Limit = 10,
                        Offset = 0
                    };


                    var result = await r4mController.GetActivityFeed(activityParameters);

                    await context.Response.WriteAsync($"Total activities: {result.Item1.Total}");
                });
            });
        }
    }
}
