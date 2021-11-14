using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMvc.Data;
using DemoMvc.Services;
using DemoMvc.Services.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoMvc
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

            services.AddDbContext<DemoDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<DemoDbContext>(); // where are users stored?
            services.AddScoped<IUserService, IdentityUserService>();

            services.AddScoped<IFamilyRepository, DatabaseFamilyRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            services.AddSingleton<IFileUploadService, AzureFileUploadService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Load CSS and JS files from wwwroot

            app.UseRouting();

            // Actually check for an Auth cookie!
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Used this for APIs, which specify their own routes
                endpoints.MapControllers();

                // Default route "convention"
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // /                 => HomeController.Index()
                // /About            => AboutController.Index()
                // /About/Contact    => AboutController.Contact()
                // /About/Staff/1    => AboutController.Staff(id: 1)
            });
        }
    }
}
