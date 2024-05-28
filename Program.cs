using Microsoft.EntityFrameworkCore;
using Online_Recharge.Db;

namespace Online_Recharge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			builder.Services.AddDistributedMemoryCache();

			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(1800);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection") ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "AboutUs",
                    pattern: "About",
                    defaults: new { controller = "Home", action = "About" });

                endpoints.MapControllerRoute(
                   name: "site",
                   pattern: "site",
                   defaults: new { controller = "Home", action = "site" });

                endpoints.MapControllerRoute(
              name: "Payment",
              pattern: "payment",
              defaults: new { controller = "Home", action = "Payment" });

                endpoints.MapControllerRoute(
              name: "Services",
              pattern: "services",
              defaults: new { controller = "Home", action = "Services" });

                endpoints.MapControllerRoute(
                name: "Contact",
                pattern: "contact",
                defaults: new { controller = "Home", action = "Contact" });

                endpoints.MapControllerRoute(
                name: "Customer",
                pattern: "CustCare",
                defaults: new { controller = "Home", action = "CustCare" }); 
                
                endpoints.MapControllerRoute(
                name: "dataServices",
                pattern: "dataServices",
                defaults: new { controller = "Home", action = "dataServices" });
                
                endpoints.MapControllerRoute(
                name: "InternetPayment",
                pattern: "InternetPayment",
                defaults: new { controller = "Home", action = "InternetPayment" });
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}