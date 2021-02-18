using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyEcommerce.Bl;
using MyEcommerce.Models;

namespace MyEcommerce
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
            services.AddControllersWithViews();

            //Session
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            //  session if u want to change the default time out expire for session  the source https://www.tutorialspoint.com/how-to-enable-session-in-chash-asp-net-core#:~:text=The%20default%20session%20timeout%20at,sessions%20in%20ASP.NET%20Core.
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });



            //لكل الكنترولز اللى عندك Authorize لو طبقت الكود  التالى اكانه هيعمل
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                    .RequireAuthenticatedUser()
            //                    .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            // Dependecy Injecation
            services.AddDbContext<EcommerceWebsiteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IItemsServices, clsItems>();
           services.AddScoped<ICategoriesService, clsCategories>();

            //Toast Notification by c# Code
            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopLeft; });

            //  Identity  نظام الصلاحيات
            services.AddIdentity<MyUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
             //   options.Password.RequiredUniqueChars = 3;
                //options.SignIn.RequireConfirmedPhoneNumber = true;
                //options.SignIn.RequireConfirmedEmail = true;
                

            }).AddEntityFrameworkStores<EcommerceWebsiteContext>();

            services.ConfigureApplicationCookie(options =>
            {
                //options.AccessDeniedPath = "/Administration/AccessDenied";
                options.AccessDeniedPath = new PathString("/Admin/Administration/AccessDenied");

                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = "/Account/Login";
                
               options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;



            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
               

              //  app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //API 1-2
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting(); 
            //Session  
            app.UseSession();


            // Identity 
            app.UseAuthentication();
            app.UseAuthorization();

            //Toast 
            app.UseNotyf();

            

            app.UseEndpoints(endpoints =>
            {
               

                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //API 2-2
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
