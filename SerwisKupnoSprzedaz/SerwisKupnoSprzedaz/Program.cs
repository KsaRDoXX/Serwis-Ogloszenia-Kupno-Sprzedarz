using SerwisKupnoSprzedaz.Data;
using Microsoft.EntityFrameworkCore;
using SerwisKupnoSprzedaz.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MySqlConn");

builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDBContext>(
    options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).UseLazyLoadingProxies();
    });
    
  
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
