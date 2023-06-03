using Gazi_BMT_308_Final;
using Gazi_BMT_308_Final.Models;
using Gazi_BMT_308_Final.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, ApplicationRole>() // Identity servislerini ekle.
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Ensure the database is created.
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.EnsureCreated();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
UserAndRoleDataInitializer.SeedData(userManager, roleManager).Wait();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
