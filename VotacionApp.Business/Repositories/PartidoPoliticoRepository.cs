using Microsoft.EntityFrameworkCore;
using VotacionApp.Business.Interfaces;
using VotacionApp.Data;
using VotacionApp.Data.Models;

namespace VotacionApp.Business.Repositories
{
    public class PartidoPoliticoRepository : IPartidoPoliticoRepository
    {
        private readonly ApplicationDbContext _context;

        public PartidoPoliticoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PartidoPolitico>> GetAllPartidosPoliticosAsync()
        {
            return await _context.PartidosPoliticos.ToListAsync();
        }

        public async Task<PartidoPolitico> GetPartidoPoliticoByIdAsync(int id)
        {
            return await _context.PartidosPoliticos.FindAsync(id);
        }

        public async Task AddPartidoPoliticoAsync(PartidoPolitico partido)
        {
            await _context.PartidosPoliticos.AddAsync(partido);
        }

        public async Task UpdatePartidoPoliticoAsync(PartidoPolitico partido)
        {
            _context.PartidosPoliticos.Update(partido);
        }

        public async Task DeletePartidoPoliticoAsync(int id)
        {
            var partido = await _context.PartidosPoliticos.FindAsync(id);
            if (partido != null)
            {
                _context.PartidosPoliticos.Remove(partido);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}