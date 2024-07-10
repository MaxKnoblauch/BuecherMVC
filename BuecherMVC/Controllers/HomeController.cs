using BuecherMVC.Models; // Einbindung des Models (Datenstruktur) der Anwendung
using Microsoft.AspNetCore.Mvc; // Einbindung von ASP.NET Core MVC Funktionen
using System.Diagnostics; // Einbindung für Diagnosezwecke

namespace BuecherMVC.Controllers // Namensraum der Anwendung
{
    public class HomeController : Controller // Definition des HomeControllers, der von der Basis Controller Klasse erbt
    {
        private readonly ILogger<HomeController> _logger; // Deklaration eines privaten, nur lesbaren Loggers für den HomeController

        // Konstruktor der HomeController Klasse, initialisiert den Logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Aktion für die Startseite der Anwendung
        public IActionResult Index()
        {
            return View(); // Rückgabe der zugehörigen View
        }

        // Aktion für die Datenschutzseite der Anwendung
        public IActionResult Privacy()
        {
            return View(); // Rückgabe der zugehörigen View
        }

        // Aktion für die Fehlerseite der Anwendung, wird bei einem Fehler aufgerufen
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // Verhindert das Caching der Fehlerseite
        public IActionResult Error()
        {
            // Rückgabe der Fehler-View mit einem ErrorViewModel, das die RequestId enthält
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
