using Microsoft.AspNetCore.Mvc;
using VotacionApp.WebMVC.Models;
using VotacionApp.WebMVC.Services;

namespace VotacionApp.WebMVC.Controllers
{
    public class VotantesMvcController : Controller
    {
        private readonly ApiClientService _apiClientService;

        public VotantesMvcController(ApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        // GET: VotantesMvc
        public async Task<IActionResult> Index()
        {
            var votantes = await _apiClientService.GetVotantesAsync();
            return View(votantes);
        }

        // GET: VotantesMvc/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var votante = await _apiClientService.GetVotanteByIdAsync(id);
            if (votante == null)
            {
                return NotFound();
            }
            return View(votante);
        }

        // GET: VotantesMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VotantesMvc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVotanteViewModel votante)
        {
            if (ModelState.IsValid)
            {
                var success = await _apiClientService.CreateVotanteAsync(votante);
                if (success)
                {
                    TempData["SuccessMessage"] = "Votante agregado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear el votante. Ya existe una cédula similar o hubo un error en el servidor.");
            }
            return View(votante);
        }

        // GET: VotantesMvc/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var votante = await _apiClientService.GetVotanteByIdAsync(id);
            if (votante == null)
            {
                return NotFound();
            }
            return View(votante);
        }

        // POST: VotantesMvc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VotanteViewModel votante)
        {
            if (id != votante.VotanteId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var success = await _apiClientService.UpdateVotanteAsync(votante);
                if (success)
                {
                    TempData["SuccessMessage"] = "Votante actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el votante.");
            }
            return View(votante);
        }

        // GET: VotantesMvc/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var votante = await _apiClientService.GetVotanteByIdAsync(id);
            if (votante == null)
            {
                return NotFound();
            }
            return View(votante);
        }

        // POST: VotantesMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _apiClientService.DeleteVotanteAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = "Votante eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al eliminar el votante.");
            return View();
        }
    }
}