using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecurityDemo.Common.Logging;
using SecurityDemo.Data;
using SecurityDemo.Data.Repositories.Implementation;
using SecurityDemo.Domain.Logging;
using SecurityDemo.Domain.Repositories.Interfaces;
using SecurityDemo.Services.Definition;
using SecurityDemo.Services.Definition.Services;
using SecurityDemo.Services.Services.Definition;
using SecurityDemo.Services.Services.Implementation;
using SecurityDemo.Services.Services.Implementation.Security;

namespace SecurityDemo
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
            //1. configuring session
            services.AddDistributedMemoryCache();

            //2. configuring session
            services.AddSession(options =>
            {
                options.Cookie.Name = "GMaps.Session";
                // Set a short timeout for easy testing.
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToDouble(Configuration.GetSection("SessionOptions:IdleTimeout").Value));
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //1. configuring cookie auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/User/Login";
                        options.AccessDeniedPath = "/Home/Forbidden";
                        options.Cookie.Name = "GMaps.AuthCookie";
                    });

            //3. configuring cookie auth and athorisation
            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeAdmin", policy => policy.RequireAuthenticatedUser().RequireRole("SystemAdmin", "Admin"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeSystemAdmin", policy => policy.RequireAuthenticatedUser().RequireRole("SystemAdmin"));
            });

            //1. configuring context
            services.AddDbContext<ISecurityDemoContext, SecurityDemoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            //registering services in DI container
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPasswordHasher<object>, PasswordHasher<object>>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IPasswordGenerator, PasswordGeneratorService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserActivationRepository, UserActivationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //3. configuring session
            app.UseSession();
            app.UseCookiePolicy();

            //2. configuring cookie auth
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
