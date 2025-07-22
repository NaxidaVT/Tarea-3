using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotacionApp.Data.Models
{
    public class Voto
    {
        [Key]
        public int VotoId { get; set; }

        [Required]
        public int VotanteId { get; set; }

        [Required]
        public int PartidoPoliticoId { get; set; }

        [Required]
        public System.DateTime FechaVoto { get; set; }

        // Propiedades de navegación
        public Votante Votante { get; set; }
        public PartidoPolitico PartidoPolitico { get; set; }
    }
}