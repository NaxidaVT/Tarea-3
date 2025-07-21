using System.ComponentModel.DataAnnotations;

namespace VotacionApp.Api.DTOs
{
    public class VotanteDto
    {
        public int VotanteId { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool HaVotado { get; set; }
    }

    public class CreateVotanteDto
    {
        [Required]
        [StringLength(15)]
        public string Cedula { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
    }

    public class UpdateVotanteDto
    {
        public int VotanteId { get; set; }
        [Required]
        [StringLength(15)]
        public string Cedula { get; set; }
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
    }
}