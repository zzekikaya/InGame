using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Text;
using InGame.Api.Extensions;
using InGame.Core.Entities;
using InGame.Core.Interfaces;
using InGame.Core.Services;
using InGame.Infrastructure.Data;
using InGame.Infrastructure.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using InGame.Common.MailService;


namespace InGame.Api
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
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
             
            services.AddAuthentication( /*o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                }*/ /*Uncomment this if you don't want to use JWT for all of your api*/)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["TokenOptions:Issuer"],
                        ValidAudience = Configuration["TokenOptions:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenOptions:Key"])),
                    };
                });

            //email göndermek için
            services.Configure<AuthMessageSenderOptions>(options => Configuration.GetSection("Secret").Bind(options));

            services.AddMvc();

            //action methodların geriye json dönmeleri için tanımlandı.
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //api için kullanıcıların claim yetkisi tanımlandı.
            //kayıtlı olan her kullanıcı token alabilir fakat apiden Crud işlemleri için mvc projesinde claimlardan api yetkisi kullanabilmesi için.
            //böylece her kullanıcı api kullanmak yerine biraz daha spesifikleştirerek claim yetkisi de tanımlanması gerekir.
            services.AddAuthorization(options => options.AddPolicy("Trusted", policy => policy.RequireClaim("Api", "Api")));
             
            services.Configure<Extensions.TokenOptions>(Configuration.GetSection("TokenOptions")); 
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
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{api}/{controller}/{action}/{id?}");
            });
        }
    }
}
