using Microsoft.AspNetCore.Mvc;
using VotacionApp.Api.DTOs;
using VotacionApp.Business.Services;

namespace VotacionApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotacionController : ControllerBase
    {
        private readonly VotacionService _votacionService;

        public VotacionController(VotacionService votacionService)
        {
            _votacionService = votacionService;
        }

        [HttpPost("votar")]
        public async Task<IActionResult> PostVoto(VotarDto votarDto)
        {
            try
            {
                var resultado = await _votacionService.RealizarVotacionAsync(votarDto.CedulaVotante, votarDto.IdPartidoPolitico);
                if (resultado)
                {
                    return Ok(new { message = "Voto registrado exitosamente." });
                }
                return BadRequest(new { message = "No se pudo registrar el voto." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al registrar el voto.", error = ex.Message });
            }
        }

        [HttpGet("resultados")]
        public async Task<ActionResult<Dictionary<string, int>>> GetResultados()
        {
            var resultados = await _votacionService.GetResultadosVotacionAsync();
            return Ok(resultados);
        }
    }
}