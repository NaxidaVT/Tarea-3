using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering; // Para SelectListItem

namespace VotacionApp.WebMVC.Models
{
    public class RealizarVotoViewModel
    {
        [Required(ErrorMessage = "Por favor, ingrese su número de cédula.")]
        [Display(Name = "Su Cédula")]
        public string CedulaVotante { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un partido político.")]
        [Display(Name = "Partido Político")]
        public int IdPartidoPolitico { get; set; }

        public List<SelectListItem> PartidosPoliticos { get; set; } = new List<SelectListItem>();

        public string MensajeError { get; set; }
        public string MensajeExito { get; set; }
    }
}