using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Mapping;
using RedeyeMusic.Web.Infrastructure.Extensions;
using RedeyeMusic.Web.ViewModels.Home;
using System.Reflection;
using static RedeyeMusic.Common.GeneralApplicationConstants;
namespace RedeyeMusic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString
                = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<RedeyeMusicDbContext>(options =>
    options.UseSqlServer(connectionString));

            builder.Services
                .AddDbContext<RedeyeMusicDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");

                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<RedeyeMusicDbContext>();

            builder.Services.AddApplicationService(typeof(ISongService));
            builder.Services.AddRecaptchaService();

            builder.Services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/User/Login";
                cfg.AccessDeniedPath = "/Home/Error/401";
            });

            builder.Services.AddControllersWithViews()
            .AddMvcOptions(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });


            WebApplication app = builder.Build();

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            /// <summary>
            /// This method seeds admin role if it does not exist
            /// Passed email should be valid email of an existing user in the applciation
            /// </summary>
            /// <param name="application"></param>
            /// <param name="email"></param>
            /// <returns></returns>

            if (app.Environment.IsDevelopment())
            {
                app.SeedAdministrator(DevelopmentAdminEmail);
            }

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "areas",
                pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                config.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


                config.MapDefaultControllerRoute();
                config.MapRazorPages();
            });
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}