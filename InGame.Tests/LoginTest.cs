using System;
using System.Threading.Tasks;
using InGame.Core.Entities;
using InGame.Infrastructure;
using InGame.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InGame.Tests
{
    public class LoginTest
    {
        [Fact]
        public async Task LogsInSampleUser()
        {
            var services = new ServiceCollection(); 

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer("DefaultConnection");
            });
            var serviceProvider = new ServiceCollection()
                .BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (AppIdentityDbContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                try
                {
                    // seed sample user data
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();

                    var signInManager = scopedServices.GetRequiredService<SignInManager<ApplicationUser>>();

                    var email = "zzeki@gmail.com";
                    var password = "Pass@word1";

                    var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

                    Assert.True(result.Succeeded);

                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
