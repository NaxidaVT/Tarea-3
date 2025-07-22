using Microsoft.AspNetCore.Mvc;
using VotacionApp.WebMVC.Models;
using VotacionApp.WebMVC.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace VotacionApp.WebMVC.Controllers
{
    public class PartidosPoliticosController : Controller
    {
        private readonly ApiClientService _apiClientService;

        public PartidosPoliticosController(ApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        // GET: PartidosPoliticos
        public async Task<IActionResult> Index()
        {
            var partidos = await _apiClientService.GetAllPartidosPoliticosAsync();
            return View(partidos);
        }

        // GET: PartidosPoliticos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _apiClientService.GetPartidoPoliticoByIdAsync(id.Value);
            if (partido == null)
            {
                return NotFound();
            }
            return View(partido);
        }

        // GET: PartidosPoliticos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PartidosPoliticos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePartidoPoliticoViewModel partido)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiClientService.CreatePartidoPoliticoAsync(partido);
                if (success)
                {
                    TempData["SuccessMessage"] = "Partido político creado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error al crear el partido político en la API.");
            }
            return View(partido);
        }

        // GET: PartidosPoliticos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _apiClientService.GetPartidoPoliticoByIdAsync(id.Value);
            if (partido == null)
            {
                return NotFound();
            }
            return View(partido);
        }

        // POST: PartidosPoliticos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PartidoPoliticoViewModel partido)
        {
            if (id != partido.PartidoPoliticoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _apiClientService.UpdatePartidoPoliticoAsync(partido);
                if (success)
                {
                    TempData["SuccessMessage"] = "Partido político actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error al actualizar el partido político en la API.");
            }
            return View(partido);
        }

        // GET: PartidosPoliticos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partido = await _apiClientService.GetPartidoPoliticoByIdAsync(id.Value);
            if (partido == null)
            {
                return NotFound();
            }
            return View(partido);
        }

        // POST: PartidosPoliticos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiClientService.DeletePartidoPoliticoAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Partido político eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Error al eliminar el partido político en la API.";
            return RedirectToAction(nameof(Index));
        }
    }
}