using BuecherDatenbank;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Konfiguration laden
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// DI-Konfiguration hinzufügen
builder.Services.AddTransient<BuecherRepository>();
builder.Services.AddDbContext<BuecherDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDB"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MariaDB"))));

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware-Konfiguration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
