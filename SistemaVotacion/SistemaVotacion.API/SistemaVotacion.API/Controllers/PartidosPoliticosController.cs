using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVotacion.API.Data;
using SistemaVotacion.API.Data.SistemaVotacion.API.Data;
using SistemaVotacion.API.Models;

namespace SistemaVotacion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidosPoliticosController : ControllerBase
    {
        private readonly SistemaVotacionContext _context;

        public PartidosPoliticosController(SistemaVotacionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartidoPolitico>>> GetPartidos()
        {
            return await _context.PartidosPoliticos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartidoPolitico>> GetPartido(int id)
        {
            var partido = await _context.PartidosPoliticos.FindAsync(id);
            if (partido == null) return NotFound();
            return partido;
        }

        [HttpPost]
        public async Task<ActionResult<PartidoPolitico>> PostPartido(PartidoPolitico partido)
        {
            _context.PartidosPoliticos.Add(partido);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPartido), new { id = partido.Id }, partido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartido(int id, PartidoPolitico partido)
        {
            if (id != partido.Id) return BadRequest();

            _context.Entry(partido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartido(int id)
        {
            var partido = await _context.PartidosPoliticos.FindAsync(id);
            if (partido == null) return NotFound();

            _context.PartidosPoliticos.Remove(partido);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
