using VotacionApp.Data.Models;

namespace VotacionApp.Business.Interfaces
{
    public interface IVotoRepository
    {
        Task<IEnumerable<Voto>> GetAllVotosAsync();
        Task<Voto> GetVotoByIdAsync(int id);
        Task AddVotoAsync(Voto voto);
        Task<Dictionary<string, int>> GetResultadosVotacionAsync();
        Task SaveChangesAsync();
    }
}