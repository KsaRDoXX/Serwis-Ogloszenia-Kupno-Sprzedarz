using SerwisKupnoSprzedaz.Data;
using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Services;
using System;
using System.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MySqlConn");

builder.Services.AddRazorPages();
builder.Services.AddScoped<SessionCheckFilter>(); // Rejestracja filtra
builder.Services.AddDbContext<AppDBContext>(
    options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).UseLazyLoadingProxies();
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<SessionCheckFilter>(); // Rejestracja filtra globalnie
});

builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IAnnouncmentServcie, AnnouncmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseRouting();
app.UseSession();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Announcments}/{action=Index}/{id?}");

app.Run();
