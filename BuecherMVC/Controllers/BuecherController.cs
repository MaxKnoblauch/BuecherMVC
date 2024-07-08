using BuecherDatenbank;  // Verweis auf das Datenbankprojekt
using BuecherMVC.Models;  // Verweis auf die Modellklassen für die Ansichten
using Microsoft.AspNetCore.Mvc;  // ASP.NET Core MVC-Namespace für Controller
using System.Collections.Generic;  // Namespace für generische Typen wie List<T>
using System.Threading.Tasks;  // Namespace für asynchrone Task-Unterstützung

namespace BuecherMVC.Controllers
{
    public class BuecherController : Controller
    {
        private readonly BuecherRepository _repository;  // Instanz der Datenbankrepositoryklasse

        public BuecherController(BuecherRepository repository)
        {
            _repository = repository;  // Konstruktor zur Injektion des Repositories
        }

        public async Task<IActionResult> Index()
        {
            // Action-Methode für die Indexansicht, die alle aktuellen und archivierten Bücher lädt
            var aktuelleBuecher = await _repository.HoleAlleAktuellenBuecherAsync();  // Lädt alle aktuellen Bücher
            var archivierteBuecher = await _repository.HoleAlleArchiviertenBuecherAsync();  // Lädt alle archivierten Bücher

            // Erstellt ein Modell für die Ansicht aus den geladenen Büchern
            var model = new BuecherListeModel(aktuelleBuecher, archivierteBuecher);

            return View(model);  // Liefert die Indexansicht mit dem erstellten Modell zurück
        }

        [HttpPost]
        public async Task<IActionResult> VerschiebeZuArchiviertenBuechern(int id)
        {
            // Action-Methode für das Verschieben eines Buches zu den archivierten Büchern
            await _repository.VerschiebeZuArchiviertenBuechern(id);  // Ruft die Methode im Repository auf, um das Buch zu verschieben
            return RedirectToAction("Index");  // Nach dem Verschieben leitet zur Indexansicht zurück
        }

        [HttpPost]
        public async Task<IActionResult> VerschiebeZuAktuellenBuechern(int id)
        {
            // Action-Methode für das Verschieben eines Buches zu den aktuellen Büchern
            await _repository.VerschiebeZuAktuellenBuechern(id);  // Ruft die Methode im Repository auf, um das Buch zu verschieben
            return RedirectToAction("Index");  // Nach dem Verschieben leitet zur Indexansicht zurück
        }
    }
}

