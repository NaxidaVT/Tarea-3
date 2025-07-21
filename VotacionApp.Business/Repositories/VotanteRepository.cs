using Microsoft.EntityFrameworkCore;
using VotacionApp.Business.Interfaces;
using VotacionApp.Data;
using VotacionApp.Data.Models;

namespace VotacionApp.Business.Repositories
{
    public class VotanteRepository : IVotanteRepository
    {
        private readonly ApplicationDbContext _context;

        public VotanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Votante>> GetAllVotantesAsync()
        {
            return await _context.Votantes.ToListAsync();
        }

        public async Task<Votante> GetVotanteByIdAsync(int id)
        {
            return await _context.Votantes.FindAsync(id);
        }

        public async Task<Votante> GetVotanteByCedulaAsync(string cedula)
        {
            return await _context.Votantes.FirstOrDefaultAsync(v => v.Cedula == cedula);
        }

        public async Task AddVotanteAsync(Votante votante)
        {
            await _context.Votantes.AddAsync(votante);
        }

        public async Task UpdateVotanteAsync(Votante votante)
        {
            _context.Votantes.Update(votante);
        }

        public async Task DeleteVotanteAsync(int id)
        {
            var votante = await _context.Votantes.FindAsync(id);
            if (votante != null)
            {
                _context.Votantes.Remove(votante);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}