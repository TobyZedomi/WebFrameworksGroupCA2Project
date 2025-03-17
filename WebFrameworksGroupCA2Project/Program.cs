using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<WebFrameworksGroupCA2ProjectContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("WebFrameworksGroupCA2ProjectContext") ?? throw new InvalidOperationException("Connection string 'WebFrameworksGroupCA2ProjectContext' not found.")));

        builder.Services.AddIdentity<AppUser, IdentityRole>(
        options =>

        {
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<WebFrameworksGroupCA2ProjectContext>().AddDefaultTokenProviders();




        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.Run();

        }

}
