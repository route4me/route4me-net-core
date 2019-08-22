using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Route4MeDB.ApplicationCore.Entities.AddressBookContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeDB.Infrastructure.Data
{
    public class Route4MeDbContextSeed
    {
        public static async Task SeedAsync(Route4MeDbContext route4MeDbContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!route4MeDbContext.AddressBookContacts.Any())
                {
                    route4MeDbContext.AddressBookContacts.AddRange(
                        GetPreconfiguredAddressBookContacts());

                    await route4MeDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<Route4MeDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(route4MeDbContext, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<AddressBookContact> GetPreconfiguredAddressBookContacts()
        {
            return new List<AddressBookContact>()
            {
                new AddressBookContact()
                {
                    FirstName = "Test name",
                    Address1 = "Test Address1",
                    CachedLat = 38.024654,
                    CachedLng = -77.338814
                },
                new AddressBookContact()
                {
                    Address1 = "1604 PARKRIDGE PKWY, Louisville, KY, 40214",
                    AddressAlias = "1604 PARKRIDGE PKWY 40214",
                    AddressGroup = "Scheduled daily",
                    FirstName = "Peter",
                    LastName = "Newman",
                    AddressEmail = "pnewman6564@yahoo.com",
                    AddressPhoneNumber = "65432178",
                    CachedLat = 38.141598,
                    CachedLng = -85.793846,
                    AddressCity = "Louisville",
                    AddressCustomData = JsonConvert.SerializeObject( 
                        new Dictionary<string, string>() {
                                { "scheduled", "yes" }, { "service type", "publishing" }
                            }
                        ),
                    Schedules = new Schedule[]
                    {
                         new Schedule()
                            {
                                Enabled = true,
                                Mode = "daily",
                                Daily = new ScheduleDaily(1)
                            }
                    }
                }
            };
        }
    }
}
