using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using VotacionApp.Data.Models;

namespace VotacionApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Votante> Votantes { get; set; }
        public DbSet<PartidoPolitico> PartidosPoliticos { get; set; }
        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración adicional si es necesaria (ej. índices únicos)
            modelBuilder.Entity<Votante>()
                .HasIndex(v => v.Cedula)
                .IsUnique();

            // Configurar la relación uno a muchos entre PartidoPolitico y Voto
            modelBuilder.Entity<Voto>()
                .HasOne(v => v.PartidoPolitico)
                .WithMany()
                .HasForeignKey(v => v.PartidoPoliticoId);

            // Configurar la relación uno a muchos entre Votante y Voto
            modelBuilder.Entity<Voto>()
                .HasOne(v => v.Votante)
                .WithMany()
                .HasForeignKey(v => v.VotanteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}