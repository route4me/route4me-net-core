using System;
using System.Collections.Generic;
using System.Text;
using Route4MeDB.ApplicationCore.Services;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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


    }
}
