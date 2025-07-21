using Microsoft.AspNetCore.Mvc;
using VotacionApp.Api.DTOs;
using VotacionApp.Business.Services;
using VotacionApp.Data.Models;

namespace VotacionApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotantesController : ControllerBase
    {
        private readonly VotacionService _votacionService;

        public VotantesController(VotacionService votacionService)
        {
            _votacionService = votacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VotanteDto>>> GetVotantes()
        {
            var votantes = await _votacionService.GetAllVotantesAsync();
            var votantesDto = votantes.Select(v => new VotanteDto
            {
                VotanteId = v.VotanteId,
                Cedula = v.Cedula,
                Nombre = v.Nombre,
                Apellido = v.Apellido,
                HaVotado = v.HaVotado
            });
            return Ok(votantesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VotanteDto>> GetVotante(int id)
        {
            var votante = await _votacionService.GetVotanteByIdAsync(id);
            if (votante == null)
            {
                return NotFound();
            }
            var votanteDto = new VotanteDto
            {
                VotanteId = votante.VotanteId,
                Cedula = votante.Cedula,
                Nombre = votante.Nombre,
                Apellido = votante.Apellido,
                HaVotado = votante.HaVotado
            };
            return Ok(votanteDto);
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<VotanteDto>> GetVotanteByCedula(string cedula)
        {
            var votante = await _votacionService.GetVotanteByCedulaAsync(cedula);
            if (votante == null)
            {
                return NotFound();
            }
            var votanteDto = new VotanteDto
            {
                VotanteId = votante.VotanteId,
                Cedula = votante.Cedula,
                Nombre = votante.Nombre,
                Apellido = votante.Apellido,
                HaVotado = votante.HaVotado
            };
            return Ok(votanteDto);
        }

        [HttpPost]
        public async Task<ActionResult<VotanteDto>> PostVotante(CreateVotanteDto createVotanteDto)
        {
            var votante = new Votante
            {
                Cedula = createVotanteDto.Cedula,
                Nombre = createVotanteDto.Nombre,
                Apellido = createVotanteDto.Apellido,
                HaVotado = false // Por defecto, al agregar no ha votado
            };
            try
            {
                await _votacionService.AddVotanteAsync(votante);
                var votanteDto = new VotanteDto
                {
                    VotanteId = votante.VotanteId,
                    Cedula = votante.Cedula,
                    Nombre = votante.Nombre,
                    Apellido = votante.Apellido,
                    HaVotado = votante.HaVotado
                };
                return CreatedAtAction(nameof(GetVotante), new { id = votante.VotanteId }, votanteDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al agregar el votante.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVotante(int id, UpdateVotanteDto updateVotanteDto)
        {
            if (id != updateVotanteDto.VotanteId)
            {
                return BadRequest("El ID del votante en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            var votante = await _votacionService.GetVotanteByIdAsync(id);
            if (votante == null)
            {
                return NotFound();
            }

            votante.Cedula = updateVotanteDto.Cedula;
            votante.Nombre = updateVotanteDto.Nombre;
            votante.Apellido = updateVotanteDto.Apellido;
            // No se debería permitir actualizar HaVotado directamente por la API en este endpoint

            try
            {
                await _votacionService.UpdateVotanteAsync(votante);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error interno al actualizar el votante.", error = ex.Message });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVotante(int id)
        {
            var votante = await _votacionService.GetVotanteByIdAsync(id);
            if (votante == null)
            {
                return NotFound();
            }

            await _votacionService.DeleteVotanteAsync(id);
            return NoContent();
        }
    }
}