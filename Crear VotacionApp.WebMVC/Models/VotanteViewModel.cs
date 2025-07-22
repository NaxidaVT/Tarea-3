using System.ComponentModel.DataAnnotations;

namespace VotacionApp.WebMVC.Models
{
    public class VotanteViewModel
    {
        public int VotanteId { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(15, ErrorMessage = "La cédula no puede exceder los 15 caracteres.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Display(Name = "Ha Votado")]
        public bool HaVotado { get; set; }
    }

    public class CreateVotanteViewModel
    {
        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(15, ErrorMessage = "La cédula no puede exceder los 15 caracteres.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
    }
}