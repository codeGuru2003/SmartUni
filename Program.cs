using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartUni.Data;
using SmartUni.Models;
using SmartUni.Services;

var builder = WebApplication.CreateBuilder(args);
//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Vdk1iW3tWcnJRT2hc");
// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSingleton<ITempDataProvider, CustomTempDataProvider>();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<IRandomStringGenerator, RandomStringGenerator>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
        options.EnableSensitiveDataLogging();
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";

});

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GroupPolicy", policy =>
    {
        policy.RequireClaim("GroupId");
    });
});
var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // Retrieve the necessary services
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Create a superuser role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("SuperAdmin"))
    {
        await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
    }

    // Create a superuser if there are no users in the system
    if (!userManager.Users.Any())
    {
        var superUser = new ApplicationUser
        {
            UserName = "superadmin",
            Email = "superadmin@test.com",
            PhoneNumber = "0778337220",
            LoginHint = "P@55w0rd",
        };

        var result = await userManager.CreateAsync(superUser, "P@55w0rd");

        if (result.Succeeded)
        {
            // Add the superuser to the Superuser role
            await userManager.AddToRoleAsync(superUser, "SuperAdmin");
        }
        else
        {
            // Handle user creation failure
            Console.WriteLine("Superuser creation failed:");
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }
}
catch (Exception ex)
{
    // Handle exception if any
    Console.WriteLine("An error occurred while creating the superuser:");
    Console.WriteLine(ex.Message);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
