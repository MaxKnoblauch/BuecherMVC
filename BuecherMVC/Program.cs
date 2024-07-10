// Erforderliche Namespaces importieren
using BuecherDatenbank;  // Zugriff auf die BuecherDatenbank-Komponenten
using Microsoft.EntityFrameworkCore;  // Zugriff auf Entity Framework Core

// Erstellen des Webanwendungs-Builders
var builder = WebApplication.CreateBuilder(args);

// Konfiguration laden aus appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Dependency Injection (DI) Konfiguration hinzufügen
builder.Services.AddTransient<BuecherRepository>();  // Registrierung des BuecherRepository als Transient Service
builder.Services.AddDbContext<BuecherDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MariaDB"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MariaDB"))));
// Hinzufügen der BuecherDBContext als DbContext mit MySQL-Datenbankverbindung, automatische Ermittlung der Serverversion

// Weitere Services hinzufügen
builder.Services.AddAuthorization();  // Autorisierungsdienst hinzufügen
builder.Services.AddControllersWithViews();  // MVC-Controller und Views hinzufügen

// Build der Webanwendung
var app = builder.Build();

// Middleware-Konfiguration für Produktionsumgebung
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");  // Fehlerbehandlungsmiddleware für Fehlerseite konfigurieren
    app.UseHsts();  // HTTP Strict Transport Security (HSTS) Middleware hinzufügen
}

app.UseHttpsRedirection();  // HTTPS-Umleitung aktivieren
app.UseStaticFiles();  // Statische Dateien (z.B. CSS, JavaScript) bereitstellen

app.UseRouting();  // Routing Middleware hinzufügen
app.UseAuthorization();  // Autorisierungsmiddleware aktivieren

// Controller-Routing konfigurieren
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Standard-Route für Controller festlegen

app.Run();  // Anwendung ausführen

