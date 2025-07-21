using VotacionApp.Data.Models;

namespace VotacionApp.Business.Interfaces
{
    public interface IPartidoPoliticoRepository
    {
        Task<IEnumerable<PartidoPolitico>> GetAllPartidosPoliticosAsync();
        Task<PartidoPolitico> GetPartidoPoliticoByIdAsync(int id);
        Task AddPartidoPoliticoAsync(PartidoPolitico partido);
        Task UpdatePartidoPoliticoAsync(PartidoPolitico partido);
        Task DeletePartidoPoliticoAsync(int id);
        Task SaveChangesAsync();
    }
}