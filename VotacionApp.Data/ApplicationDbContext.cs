using Microsoft.EntityFrameworkCore;
using VotacionApp.Data.Models;

namespace VotacionApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Votante> Votantes { get; set; }
        public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }
        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Votante>()
                .HasIndex(v => v.Cedula)
                .IsUnique();

            modelBuilder.Entity<PartidoPolitico>()
                .HasIndex(p => p.Siglas)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}