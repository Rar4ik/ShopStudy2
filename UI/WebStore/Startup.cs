using System;
using AutoMapper;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.Services;
using WebStore.Services.Product;
using WebStore.Clients.Values;
using WebStore.Infrastructure.AutoMapper;
using WebStore.Logger;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Config) => Configuration = Config;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<WebStoreContext>(opt => 
            //    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<WebStoreContextInitializer>();

            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<ViewModelMapping>();
                //opt.AddProfile<DTOMapping>();
            }, typeof(Startup));

            //services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IEmployeesData, EmployeesClient>();
            //services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IProductData, ProductClient>();
            //services.AddScoped<IProductData, InMemoryProductData>();
            services.AddScoped<ICartService, CookieCartService>();
            services.AddScoped<ICartStore, CookiesCartStore>();
            //services.AddScoped<IOrderService, SqlOrderService>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddScoped<IValueService, ValuesClient>();

            services.AddIdentity<User, Role>()
               //.AddEntityFrameworkStores<WebStoreContext>()
               .AddDefaultTokenProviders();

            #region Service Implementation

            services.AddTransient<IUserStore<User>, UserClient>();
            services.AddTransient<IUserPasswordStore<User>, UserClient>();
            services.AddTransient<IUserEmailStore<User>, UserClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UserClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UserClient>();
            services.AddTransient<IUserLockoutStore<User>, UserClient>();
            services.AddTransient<IUserClaimStore<User>, UserClient>();
            services.AddTransient<IUserLoginStore<User>, UserClient>();

            services.AddTransient<IRoleStore<Role>, RoleClient>();
            #endregion

            services.Configure<IdentityOptions>(
                opt =>
                {
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredUniqueChars = 3;

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.MaxFailedAccessAttempts = 10;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABC123";
                    opt.User.RequireUniqueEmail = false; // Грабли - на этапе отладки при попытке регистрации двух пользователей без email
                });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore-Identity";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Expiration = TimeSpan.FromDays(150);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            //services.AddSession();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            //app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
