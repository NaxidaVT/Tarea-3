using VotacionApp.Data.Models;

namespace VotacionApp.Business.Interfaces
{
    public interface IVotanteRepository
    {
        Task<IEnumerable<Votante>> GetAllVotantesAsync();
        Task<Votante> GetVotanteByIdAsync(int id);
        Task<Votante> GetVotanteByCedulaAsync(string cedula);
        Task AddVotanteAsync(Votante votante);
        Task UpdateVotanteAsync(Votante votante);
        Task DeleteVotanteAsync(int id);
        Task SaveChangesAsync();
    }
}