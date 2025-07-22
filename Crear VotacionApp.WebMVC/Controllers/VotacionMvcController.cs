using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VotacionApp.WebMVC.Models;
using VotacionApp.WebMVC.Services;

namespace VotacionApp.WebMVC.Controllers
{
    public class VotacionMvcController : Controller
    {
        private readonly ApiClientService _apiClientService;

        public VotacionMvcController(ApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        // GET: VotacionMvc/Votar
        public async Task<IActionResult> Votar()
        {
            var partidos = await _apiClientService.GetPartidosPoliticosAsync();
            var model = new RealizarVotoViewModel
            {
                PartidosPoliticos = partidos.Select(p => new SelectListItem
                {
                    Value = p.PartidoPoliticoId.ToString(),
                    Text = p.Nombre
                }).ToList()
            };
            return View(model);
        }

        // POST: VotacionMvc/Votar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Votar(RealizarVotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el votante existe y no ha votado
                var votante = await _apiClientService.GetVotanteByCedulaAsync(model.CedulaVotante);
                if (votante == null)
                {
                    ModelState.AddModelError(string.Empty, "Votante no registrado. Por favor, inscríbase primero.");
                }
                else if (votante.HaVotado)
                {
                    ModelState.AddModelError(string.Empty, "Este votante ya ha emitido su voto.");
                }
                else
                {
                    var (success, message) = await _apiClientService.RealizarVotoAsync(model);
                    if (success)
                    {
                        TempData["SuccessMessage"] = message;
                        return RedirectToAction(nameof(Resultados)); // Redirigir a resultados o a una página de confirmación
                    }
                    ModelState.AddModelError(string.Empty, message);
                }
            }

            // Recargar la lista de partidos si hay un error de validación para volver a mostrar la vista
            model.PartidosPoliticos = (await _apiClientService.GetPartidosPoliticosAsync())
                                        .Select(static p => new SelectListItem
                                        {
                                            Value = p.PartidoPoliticoId.ToString(),
                                            Text = p.Nombre
                                        }).ToList();
            return View(model);
        }

        // GET: VotacionMvc/Resultados
        public async Task<IActionResult> Resultados()
        {
            var resultados = await _apiClientService.GetResultadosVotacionAsync();
            var model = new ResultadosViewModel
            {
                Resultados = resultados
            };
            return View(model);
        }
    }
}