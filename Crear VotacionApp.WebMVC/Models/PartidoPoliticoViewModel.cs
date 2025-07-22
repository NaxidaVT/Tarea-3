using System.ComponentModel.DataAnnotations;

namespace VotacionApp.WebMVC.Models
{
    public class PartidoPoliticoViewModel
    {
        public int PartidoPoliticoId { get; set; }

        [Display(Name = "Nombre del Partido")]
        [Required(ErrorMessage = "El nombre del partido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty; // Inicializar para evitar advertencias CS8618

        [Display(Name = "Siglas")]
        [Required(ErrorMessage = "Las siglas son obligatorias.")]
        [StringLength(10, ErrorMessage = "Las siglas no pueden exceder los 10 caracteres.")]
        public string Siglas { get; set; } = string.Empty; // Inicializar para evitar advertencias CS8618
    }
}