using Microsoft.EntityFrameworkCore;
using PustokBookStore.Data;
using System.Diagnostics;
using System.Drawing;

namespace PustokBookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string sliderImagesDir = Path.Combine(builder.Environment.WebRootPath, "slider_images");

            // ---- BOOTSTRAPPING ----
            if ( ! Directory.Exists(sliderImagesDir) ){
                try
                {
                    Directory.CreateDirectory(sliderImagesDir);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Exception Happened When Creating Directory - {0}\nPlease Consider To Run Application as Adminstrative Privileges...", sliderImagesDir);
                    Console.ResetColor();
                    Process.GetCurrentProcess().Kill();
                }
            }

            // +++++ BOOTSTRAPPING +++++

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=;Database=PustokStore;Trusted_Connection=True;TrustServerCertificate=True;");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.\
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "admin",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Slider}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}