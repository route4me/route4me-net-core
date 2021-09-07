using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route4MeSDK.DataTypes;
using System;

namespace AspNetCoreExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
                    RunExamples runExamples = new RunExamples();

                    // Please, change the demo API key with your own API key
                    runExamples.c_ApiKey = "11111111111111111111111111111111";

                    var dataObject = runExamples.SingleDriverRoute10Stops(out string errorString);

                    string outputMessage = (dataObject != null && dataObject.GetType() == typeof(DataObject))
                            ? $"Created the optimization with ID={dataObject.OptimizationProblemId} and route name = {dataObject.Parameters.RouteName}"
                            : $"Cannot create the optimization - change input data and try again{Environment.NewLine} {errorString}";

                    await context.Response.WriteAsync(outputMessage);
                });
            });
        }
    }
}
