using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using URLShortener.Data;
using URLShortener.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Get the connection string
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Add services to the container
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDbContext<UrlContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Configure Identity
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        // Configure CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configure HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // Enable CORS
        app.UseCors("AllowAllOrigins");

        app.UseAuthorization();

        // Map controller routes
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Optionally, you can remove this if you are not using Razor Pages
        app.MapRazorPages();

        app.Run();
    }
}
