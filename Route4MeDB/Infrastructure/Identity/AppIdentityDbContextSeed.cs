using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Route4MeDB.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "demouser@route4me.com", Email = "demouser@route4me.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
        }
    }
}