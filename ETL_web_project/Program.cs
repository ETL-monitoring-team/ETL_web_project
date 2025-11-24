using ETL_web_project.Data.Context;
using ETL_web_project.Interfaces;
using ETL_web_project.Mappings;
using ETL_web_project.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC
builder.Services.AddControllersWithViews();

// AutoMapper
builder.Services.AddAutoMapper(typeof(UserAccountProfile).Assembly);

// Account Service (Login/Register için)
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<IEtlLogService, EtlLogService>();

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Sýra önemli: önce auth sonra authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
