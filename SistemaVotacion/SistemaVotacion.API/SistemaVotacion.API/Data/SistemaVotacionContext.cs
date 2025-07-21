using Microsoft.EntityFrameworkCore;
using SistemaVotacion.API.Models;


namespace SistemaVotacion.API.Data
{
    namespace SistemaVotacion.API.Data
    {
        public class SistemaVotacionContext : DbContext
        {
            public SistemaVotacionContext(DbContextOptions<SistemaVotacionContext> options) : base(options)
            {
            }

            public DbSet<Votante> Votantes { get; set; }
            public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }
            public DbSet<Voto> Votos { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Voto>()
                    .HasIndex(v => v.VotanteId)
                    .IsUnique();

                modelBuilder.Entity<Voto>()
                    .HasOne(v => v.Votante)
                    .WithOne(v => v.Voto)
                    .HasForeignKey<Voto>(v => v.VotanteId);

                modelBuilder.Entity<Voto>()
                    .HasOne(v => v.Partido)
                    .WithMany(p => p.Votos)
                    .HasForeignKey(v => v.PartidoId);
            }
        }
    }

}
