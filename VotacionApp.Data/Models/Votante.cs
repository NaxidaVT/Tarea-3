using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotacionApp.Data.Models
{
    public class Votante
    {
        [Key]
        public int VotanteId { get; set; }

        [Required]
        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string Cedula { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public bool HaVotado { get; set; } = false;
    }
}