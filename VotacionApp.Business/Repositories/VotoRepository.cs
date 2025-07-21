using Microsoft.EntityFrameworkCore;
using VotacionApp.Business.Interfaces;
using VotacionApp.Data;
using VotacionApp.Data.Models;

namespace VotacionApp.Business.Repositories
{
    public class VotoRepository : IVotoRepository
    {
        private readonly ApplicationDbContext _context;

        public VotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voto>> GetAllVotosAsync()
        {
            return await _context.Votos.Include(v => v.Votante).Include(v => v.PartidoPolitico).ToListAsync();
        }

        public async Task<Voto> GetVotoByIdAsync(int id)
        {
            return await _context.Votos.Include(v => v.Votante).Include(v => v.PartidoPolitico).FirstOrDefaultAsync(v => v.VotoId == id);
        }

        public async Task AddVotoAsync(Voto voto)
        {
            await _context.Votos.AddAsync(voto);
        }

        public async Task<Dictionary<string, int>> GetResultadosVotacionAsync()
        {
            var resultados = await _context.Votos
                .Include(v => v.PartidoPolitico)
                .GroupBy(v => v.PartidoPolitico.Nombre)
                .Select(g => new { Partido = g.Key, Votos = g.Count() })
                .ToDictionaryAsync(x => x.Partido, x => x.Votos);

            // Asegurarse de incluir partidos sin votos
            var todosPartidos = await _context.PartidosPoliticos.ToListAsync();
            foreach (var partido in todosPartidos)
            {
                if (!resultados.ContainsKey(partido.Nombre))
                {
                    resultados.Add(partido.Nombre, 0);
                }
            }

            return resultados;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}