using Microsoft.AspNetCore.Mvc;

namespace SistemaVotacion.MVC.Models
{
    public class ResultadoVotacion
    {
        public int PartidoId { get; set; }
        public int CantidadVotos { get; set; }
        public string? Partido { get; set; }
    }
}
