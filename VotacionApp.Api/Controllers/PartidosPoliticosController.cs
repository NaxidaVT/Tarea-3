using Microsoft.AspNetCore.Mvc;
using VotacionApp.Api.DTOs;
using VotacionApp.Business.Services;
using VotacionApp.Data.Models;

namespace VotacionApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidosPoliticosController : ControllerBase
    {
        private readonly VotacionService _votacionService;

        public PartidosPoliticosController(VotacionService votacionService)
        {
            _votacionService = votacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartidoPoliticoDto>>> GetPartidosPoliticos()
        {
            var partidos = await _votacionService.GetAllPartidosPoliticosAsync();
            var partidosDto = partidos.Select(p => new PartidoPoliticoDto
            {
                PartidoPoliticoId = p.PartidoPoliticoId,
                Nombre = p.Nombre,
                Siglas = p.Siglas
            });
            return Ok(partidosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartidoPoliticoDto>> GetPartidoPolitico(int id)
        {
            var partido = await _votacionService.GetPartidoPoliticoByIdAsync(id);
            if (partido == null)
            {
                return NotFound();
            }
            var partidoDto = new PartidoPoliticoDto
            {
                PartidoPoliticoId = partido.PartidoPoliticoId,
                Nombre = partido.Nombre,
                Siglas = partido.Siglas
            };
            return Ok(partidoDto);
        }

        [HttpPost]
        public async Task<ActionResult<PartidoPoliticoDto>> PostPartidoPolitico(CreatePartidoPoliticoDto createPartidoPoliticoDto)
        {
            var partido = new PartidoPolitico
            {
                Nombre = createPartidoPoliticoDto.Nombre,
                Siglas = createPartidoPoliticoDto.Siglas
            };
            await _votacionService.AddPartidoPoliticoAsync(partido);
            var partidoDto = new PartidoPoliticoDto
            {
                PartidoPoliticoId = partido.PartidoPoliticoId,
                Nombre = partido.Nombre,
                Siglas = partido.Siglas
            };
            return CreatedAtAction(nameof(GetPartidoPolitico), new { id = partido.PartidoPoliticoId }, partidoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartidoPolitico(int id, UpdatePartidoPoliticoDto updatePartidoPoliticoDto)
        {
            if (id != updatePartidoPoliticoDto.PartidoPoliticoId)
            {
                return BadRequest("El ID del partido en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            var partido = await _votacionService.GetPartidoPoliticoByIdAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            partido.Nombre = updatePartidoPoliticoDto.Nombre;
            partido.Siglas = updatePartidoPoliticoDto.Siglas;

            try
            {
                await _votacionService.UpdatePartidoPoliticoAsync(partido);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al actualizar el partido político.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartidoPolitico(int id)
        {
            var partido = await _votacionService.GetPartidoPoliticoByIdAsync(id);
            if (partido == null)
            {
                return NotFound();
            }

            await _votacionService.DeletePartidoPoliticoAsync(id);
            return NoContent();
        }
    }
}