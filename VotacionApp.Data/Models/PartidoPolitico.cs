using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotacionApp.Data.Models
{
    public class PartidoPolitico
    {
        [Key]
        public int PartidoPoliticoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Siglas { get; set; }
    }
}