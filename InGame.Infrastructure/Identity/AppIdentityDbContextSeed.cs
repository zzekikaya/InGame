using InGame.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InGame.Infrastructure
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            //admin user sadece admin olan admin controller actionlarını kullanır.
            var adminUser = new ApplicationUser { UserName = "admin@microsoft.com", Email = "admin@microsoft.com", EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, "Pass@word1");
            await userManager.AddToRoleAsync(adminUser, "Admin");
            await userManager.AddClaimAsync(adminUser, new Claim("IsAdmin", "true"));


            //product_view user sadece bu product_view yetkisi olanlar product ekranı görebilir ve kullanabilir. bu yetkisi yoksa layout'da dahi olmak üzere menü gelmiyor  
            var productViewUser = new ApplicationUser { UserName = "productUser@microsoft.com", Email = "productUser@microsoft.com", EmailConfirmed = true };
            await userManager.CreateAsync(productViewUser, "Pass@word1");
            await userManager.AddToRoleAsync(productViewUser, "product_view");


            // api kullanmak için kullanıcıya Claim'lardan api yetkisi vermemiz gerekiyor. her üye token alabilir ama api actionlarını kullanabilmek için claim yetkisi ayrıca verilmesi gerekir.
            var apiUser = new ApplicationUser { UserName = "apiUserUser@microsoft.com", Email = "productUser@microsoft.com", EmailConfirmed = true };
            await userManager.CreateAsync(apiUser, "Pass@word1");
            await userManager.AddClaimAsync(productViewUser, new Claim("api", "api"));
        }
    }
}
