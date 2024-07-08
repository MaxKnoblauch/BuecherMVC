using BuecherDatenbank;
using BuecherMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuecherMVC.Controllers
{
    public class BuecherController : Controller
    {
        private readonly BuecherRepository _repository;

        public BuecherController(BuecherRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var aktuelleBuecher = await _repository.HoleAlleAktuellenBuecherAsync();
            var archivierteBuecher = await _repository.HoleAlleArchiviertenBuecherAsync();

            var model = new BuecherListeModel(aktuelleBuecher, archivierteBuecher);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerschiebeZuArchiviertenBuechern(int id)
        {
            await _repository.VerschiebeZuArchiviertenBuechern(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> VerschiebeZuAktuellenBuechern(int id)
        {
            await _repository.VerschiebeZuAktuellenBuechern(id);
            return RedirectToAction("Index");
        }
    }
}
