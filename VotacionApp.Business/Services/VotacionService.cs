using VotacionApp.Business.Interfaces;
using VotacionApp.Data.Models;

namespace VotacionApp.Business.Services
{
    public class VotacionService
    {
        private readonly IVotanteRepository _votanteRepository;
        private readonly IPartidoPoliticoRepository _partidoPoliticoRepository;
        private readonly IVotoRepository _votoRepository;

        public VotacionService(
            IVotanteRepository votanteRepository,
            IPartidoPoliticoRepository partidoPoliticoRepository,
            IVotoRepository votoRepository)
        {
            _votanteRepository = votanteRepository;
            _partidoPoliticoRepository = partidoPoliticoRepository;
            _votoRepository = votoRepository;
        }

        // --- Operaciones de Votantes ---
        public async Task<IEnumerable<Votante>> GetAllVotantesAsync()
        {
            return await _votanteRepository.GetAllVotantesAsync();
        }

        public async Task<Votante> GetVotanteByIdAsync(int id)
        {
            return await _votanteRepository.GetVotanteByIdAsync(id);
        }

        public async Task<Votante> GetVotanteByCedulaAsync(string cedula)
        {
            return await _votanteRepository.GetVotanteByCedulaAsync(cedula);
        }

        public async Task AddVotanteAsync(Votante votante)
        {
            // Validaciones de negocio antes de agregar
            var existingVotante = await _votanteRepository.GetVotanteByCedulaAsync(votante.Cedula);
            if (existingVotante != null)
            {
                throw new InvalidOperationException("Ya existe un votante con esta cédula.");
            }
            await _votanteRepository.AddVotanteAsync(votante);
            await _votanteRepository.SaveChangesAsync();
        }

        public async Task UpdateVotanteAsync(Votante votante)
        {
            var existingVotante = await _votanteRepository.GetVotanteByIdAsync(votante.VotanteId);
            if (existingVotante == null)
            {
                throw new KeyNotFoundException("Votante no encontrado.");
            }
            // Puedes agregar más validaciones aquí
            _votanteRepository.UpdateVotanteAsync(votante);
            await _votanteRepository.SaveChangesAsync();
        }

        public async Task DeleteVotanteAsync(int id)
        {
            await _votanteRepository.DeleteVotanteAsync(id);
            await _votanteRepository.SaveChangesAsync();
        }

        // --- Operaciones de Partidos Políticos ---
        public async Task<IEnumerable<PartidoPolitico>> GetAllPartidosPoliticosAsync()
        {
            return await _partidoPoliticoRepository.GetAllPartidosPoliticosAsync();
        }

        public async Task<PartidoPolitico> GetPartidoPoliticoByIdAsync(int id)
        {
            return await _partidoPoliticoRepository.GetPartidoPoliticoByIdAsync(id);
        }

        public async Task AddPartidoPoliticoAsync(PartidoPolitico partido)
        {
            await _partidoPoliticoRepository.AddPartidoPoliticoAsync(partido);
            await _partidoPoliticoRepository.SaveChangesAsync();
        }

        public async Task UpdatePartidoPoliticoAsync(PartidoPolitico partido)
        {
            _partidoPoliticoRepository.UpdatePartidoPoliticoAsync(partido);
            await _partidoPoliticoRepository.SaveChangesAsync();
        }

        public async Task DeletePartidoPoliticoAsync(int id)
        {
            await _partidoPoliticoRepository.DeletePartidoPoliticoAsync(id);
            await _partidoPoliticoRepository.SaveChangesAsync();
        }

        // --- Operaciones de Votación ---
        public async Task<bool> RealizarVotacionAsync(string cedulaVotante, int idPartidoPolitico)
        {
            var votante = await _votanteRepository.GetVotanteByCedulaAsync(cedulaVotante);
            if (votante == null)
            {
                throw new InvalidOperationException("Votante no encontrado con la cédula proporcionada.");
            }

            if (votante.HaVotado)
            {
                throw new InvalidOperationException("El votante con esta cédula ya ha votado.");
            }

            var partido = await _partidoPoliticoRepository.GetPartidoPoliticoByIdAsync(idPartidoPolitico);
            if (partido == null)
            {
                throw new InvalidOperationException("Partido político no encontrado.");
            }

            var nuevoVoto = new Voto
            {
                VotanteId = votante.VotanteId,
                PartidoPoliticoId = idPartidoPolitico,
                FechaVoto = DateTime.Now
            };

            await _votoRepository.AddVotoAsync(nuevoVoto);

            votante.HaVotado = true; // Marcar al votante como que ya votó
            _votanteRepository.UpdateVotanteAsync(votante); // Actualizar el estado del votante

            await _votoRepository.SaveChangesAsync(); // Guardar el voto y el cambio en el votante
            return true;
        }

        public async Task<Dictionary<string, int>> GetResultadosVotacionAsync()
        {
            return await _votoRepository.GetResultadosVotacionAsync();
        }
    }
}