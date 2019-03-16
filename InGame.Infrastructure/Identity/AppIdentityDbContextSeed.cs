using System.Security.Claims;
using InGame.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InGame.Infrastructure
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "admin@microsoft.com", Email = "admin@microsoft.com", EmailConfirmed = true };
            await userManager.CreateAsync(defaultUser, "Pass@word1");

            await userManager.AddToRoleAsync(defaultUser, "Admin");

            await userManager.AddClaimAsync(defaultUser, new Claim("IsAdmin", "true"));
        }
    }
}
