using System.ComponentModel.DataAnnotations;

namespace VotacionApp.Api.DTOs
{
    public class PartidoPoliticoDto
    {
        public int PartidoPoliticoId { get; set; }
        public string Nombre { get; set; }
        public string Siglas { get; set; }
    }

    public class CreatePartidoPoliticoDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(10)]
        public string Siglas { get; set; }
    }

    public class UpdatePartidoPoliticoDto
    {
        public int PartidoPoliticoId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(10)]
        public string Siglas { get; set; }
    }
}