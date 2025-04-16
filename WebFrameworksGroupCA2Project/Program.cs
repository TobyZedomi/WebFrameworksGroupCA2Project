using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebFrameworksGroupCA2Project.Data;
using WebFrameworksGroupCA2Project.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
      
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession();
        
        // Load environment variables
        Env.Load();
        
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
        .AddEntityFrameworkStores<WebFrameworksGroupCA2ProjectContext>()
        .AddDefaultTokenProviders();
        


        // Add social login providers (Google and Microsoft)
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
                options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
            })
            .AddMicrosoftAccount(options =>
            {
                options.ClientId = Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_ID");
                options.ClientSecret = Environment.GetEnvironmentVariable("MICROSOFT_CLIENT_SECRET");
            });


        



        // Add services to the container.
        builder.Services.AddControllersWithViews();

       

        builder.Services.AddSingleton<IEmailSender, EmailSender>();
        

        builder.Services.AddRazorPages();



        var app = builder.Build();

        app.UseSession();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();

        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
        });




        /*
        builder.Services.AddRazorPages(options =>
        {
            options.Conventions.AddPageRoute("/Identity/Account/Login", "");

        });
        *
        

        /*
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        */



        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {

                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }



        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            string name = "admin";
            string email = "admin@gmail.com";
            string password = "Admin123@";
            string address = "dublin";

            if (await userManager.FindByEmailAsync(email) == null)
            {

                AppUser user = new()
                {
                    Name = name,
                    UserName = name,
                    Email = email,
                    Address = address,
                };


                await userManager.CreateAsync(user, password!);

                var Admin = new[] { "Admin" };


                await userManager.AddToRolesAsync(user, Admin);
            }
        }
        


        app.MapRazorPages();

        app.Run();

        }

}