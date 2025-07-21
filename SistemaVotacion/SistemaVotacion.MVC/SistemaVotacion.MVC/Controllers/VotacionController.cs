using Microsoft.AspNetCore.Mvc;

namespace SistemaVotacion.MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SistemaVotacion.MVC.Services;
    using SistemaVotacion.MVC.Models;

    public class VotacionController : Controller
    {
        private readonly VotanteService _votanteService;
        private readonly PartidoService _partidoService;
        private readonly VotoService _votoService;
        private readonly ResultadoService _resultadoService;

        public VotacionController(VotanteService votanteService, PartidoService partidoService, VotoService votoService, ResultadoService resultadoService)
        {
            _votanteService = votanteService;
            _partidoService = partidoService;
            _votoService = votoService;
            _resultadoService = resultadoService;
        }

        [HttpGet]
        public IActionResult IngresoCedula()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IngresoCedula(string cedula)
        {
            var votante = await _votanteService.ObtenerVotantePorCedula(cedula);

            if (votante == null)
            {
                ViewBag.Mensaje = "Votante no registrado.";
                return View();
            }

            if (votante.YaVoto)
            {
                ViewBag.Mensaje = "Este votante ya emitió su voto.";
                return View();
            }

            // Guardar ID en sesión (opcional)
            TempData["VotanteId"] = votante.Id;
            return RedirectToAction("SeleccionarPartido");
        }

        public async Task<IActionResult> SeleccionarPartido()
        {
            var partidos = await _partidoService.ObtenerPartidos();
            ViewBag.Partidos = partidos;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SeleccionarPartido(int partidoId)
        {
            if (TempData["VotanteId"] == null)
                return RedirectToAction("IngresoCedula");

           
            

            int votanteId = Convert.ToInt32(TempData["VotanteId"]);

            var voto = new Voto
            {
                VotanteId = votanteId,
                PartidoId = partidoId,
                FechaHora = DateTime.Now
            };

            bool exito = await _votoService.EmitirVoto(voto);
            if (exito)
                return RedirectToAction("Gracias");
            else
            {
                ViewBag.Mensaje = "Ocurrió un error al emitir el voto.";
                return View();
            }
        }

        public async Task<IActionResult> Resultados()
        {
            var resultados = await _resultadoService.ObtenerResultados();
            return View(resultados);
        }

        public IActionResult Gracias()
        {
            return View();
        }

    }

}
