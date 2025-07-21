using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVotacion.API.Data;
using SistemaVotacion.API.Data.SistemaVotacion.API.Data;
using SistemaVotacion.API.Models;

namespace SistemaVotacion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotosController : ControllerBase
    {
        private readonly SistemaVotacionContext _context;

        public VotosController(SistemaVotacionContext context)
        {
            _context = context;
        }

        [HttpPost("EmitirVoto")]
        public async Task<IActionResult> EmitirVoto([FromBody] Voto voto)
        {
            var votante = await _context.Votantes.FindAsync(voto.VotanteId);
            if (votante == null) return NotFound("El votante no está registrado.");
            if (votante.YaVoto) return BadRequest("El votante ya ha votado.");

            _context.Votos.Add(voto);
            votante.YaVoto = true;

            await _context.SaveChangesAsync();
            return Ok("Voto registrado correctamente.");
        }

        [HttpGet("Resultados")]
        public async Task<ActionResult> GetResultados()
        {
            var resultados = await _context.Votos
                .GroupBy(v => v.PartidoId)
                .Select(g => new
                {
                    PartidoId = g.Key,
                    CantidadVotos = g.Count(),
                    Partido = _context.PartidosPoliticos.FirstOrDefault(p => p.Id == g.Key).Nombre
                })
                .ToListAsync();

            return Ok(resultados);
        }
    }
}
