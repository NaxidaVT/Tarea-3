using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVotacion.API.Data;
using SistemaVotacion.API.Data.SistemaVotacion.API.Data;
using SistemaVotacion.API.Models;

namespace SistemaVotacion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotantesController : ControllerBase
    {
        private readonly SistemaVotacionContext _context;

        public VotantesController(SistemaVotacionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Votante>>> GetVotantes()
        {
            return await _context.Votantes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Votante>> GetVotante(int id)
        {
            var votante = await _context.Votantes.FindAsync(id);
            if (votante == null) return NotFound();
            return votante;
        }

        [HttpPost]
        public async Task<ActionResult<Votante>> PostVotante(Votante votante)
        {
            _context.Votantes.Add(votante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVotante), new { id = votante.Id }, votante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVotante(int id, Votante votante)
        {
            if (id != votante.Id) return BadRequest();

            _context.Entry(votante).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVotante(int id)
        {
            var votante = await _context.Votantes.FindAsync(id);
            if (votante == null) return NotFound();

            _context.Votantes.Remove(votante);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
