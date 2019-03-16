using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using InGame.Infrastructure.Data;
using InGame.Infrastructure.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApi
{
    //public class Startup
    //{
    //    public Startup(IConfiguration configuration)
    //    {
    //        Configuration = configuration;
    //    }

    //    public IConfiguration Configuration { get; }

    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        CreateIdentityIfNotCreated(services);
    //        //user context
    //        services.AddDbContext<AppIdentityDbContext>(options =>
    //            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


    //        //product inGameContext
    //        services.AddDbContext<InGameContext>(options =>
    //            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("InGame.Infrastructure"))
    //        );

    //        services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

    //        services.AddScoped<IProductService, ProductService>();
    //        services.AddScoped<ISubCategoryService, SubCategoryService>();
    //        services.AddScoped<ICategoryService, CategoryService>();
    //        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
    //        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
    //        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //            .AddJwtBearer(options =>
    //            {
    //                options.TokenValidationParameters = new TokenValidationParameters
    //                {
    //                    ValidateIssuer = true,
    //                    ValidateAudience = true,
    //                    ValidateLifetime = true,
    //                    ValidateIssuerSigningKey = true,
    //                    //ValidIssuer = Configuration["Jwt:Issuer"],
    //                    //ValidAudience = Configuration["Jwt:Issuer"],
    //                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
    //                };
    //            });
    //        services.AddCors();
    //        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    //    }

    //    private static void CreateIdentityIfNotCreated(IServiceCollection services)
    //    {
    //        var sp = services.BuildServiceProvider();
    //        using (var scope = sp.CreateScope())
    //        {
    //            var existingUserManager = scope.ServiceProvider
    //                .GetService<UserManager<ApplicationUser>>();

    //            if (existingUserManager == null)
    //            {
    //                services.AddIdentity<ApplicationUser, IdentityRole>(
    //                        options =>
    //                        {
    //                            options.Password.RequiredLength = 4;
    //                            options.Password.RequireNonAlphanumeric = false;
    //                            options.Password.RequireUppercase = false;
    //                            options.User.RequireUniqueEmail = true;

    //                        })
    //                    .AddDefaultUI(UIFramework.Bootstrap4)
    //                    .AddEntityFrameworkStores<AppIdentityDbContext>()
    //                    .AddDefaultTokenProviders();


    //            }
    //        }
    //    }
    //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //    {
    //        if (env.IsDevelopment())
    //        {
    //            app.UseDeveloperExceptionPage();
    //        }
    //        else
    //        {
    //            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //            app.UseHsts();
    //        }

    //        app.UseHttpsRedirection();
    //        // global cors policy
    //        app.UseCors(x => x
    //            .AllowAnyOrigin()
    //            .AllowAnyMethod()
    //            .AllowAnyHeader());

    //        app.UseAuthentication();
    //        app.UseMvc();
    //    }
    //}

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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            //services.AddScoped<IUserService, UserService>();
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
                        //.AddDefaultUI(UIFramework.Bootstrap4)
                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                        .AddDefaultTokenProviders();


                }
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();
        }
    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
