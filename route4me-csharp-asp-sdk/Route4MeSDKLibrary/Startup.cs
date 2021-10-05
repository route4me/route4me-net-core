using Microsoft.Extensions.DependencyInjection;
using Route4MeSDK.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDK
{
    public class Startup
    {
        // Code deleted for brevity.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("Route4MeApi4Client", client =>
            {
                client.BaseAddress = new Uri("https://api.route4me.com");
                client.DefaultRequestHeaders.Add("X-Api-Key", "11111111111111111111111111111111");
            });

            services.AddHttpClient<Route4MeApi4Service>();

            // Remaining code deleted for brevity.
        }
        // Code deleted for brevity.
    }
}
