using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using InGame.Infrastructure;
using InGame.Infrastructure.Data;
using InGame.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using InGame.Web.UI.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace InGame.Web.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookieSettings(services);
            CreateIdentityIfNotCreated(services);

            //user context
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            //product inGameContext
            services.AddDbContext<InGameContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("InGame.Infrastructure"))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("Secret"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // Add memory cache services
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
        }

        //cache ayarları
        private static void ConfigureCookieSettings(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Signout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                };
            });
        }

        private static void CreateIdentityIfNotCreated(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider
                    .GetService<UserManager<ApplicationUser>>();



                if (existingUserManager == null)
                {
                    services.AddIdentity<ApplicationUser, IdentityRole>(
                            options =>
                            {
                                options.Password.RequiredLength = 4;
                                options.Password.RequireNonAlphanumeric = false;
                                options.Password.RequireUppercase = false;
                                options.User.RequireUniqueEmail = true;

                            })
                        .AddDefaultUI(UIFramework.Bootstrap4)
                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                        .AddDefaultTokenProviders();


                }
            }
        }


        private async Task CreateRoles(IServiceProvider serviceProvider)
        {


            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            AppIdentityDbContextSeed.SeedAsync(_userManager).Wait();
            //// administrator
            //var user = new ApplicationUser
            //{
            //    UserName = "Administrator",
            //    Email = "admin@admin.com",
            //    EmailConfirmed = true
            //};
            //var i = await _userManager.FindByEmailAsync(user.Email);
            //if (i == null)
            //{
            //    try
            //    {
            //        var adminUser = await _userManager.CreateAsync(user, "!Sonypsp123");
            //        if (adminUser.Succeeded)
            //        {
            //            await _userManager.AddToRoleAsync(user, "Admin");
            //            //

            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
            //    }

            //}

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                CreateRoles(serviceProvider);
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
