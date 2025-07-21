using Microsoft.AspNetCore.Mvc;

namespace SistemaVotacion.MVC.Models
{
    public class Voto
    {
        public int VotanteId { get; set; }
        public int PartidoId { get; set; }
        public DateTime FechaHora { get; set; }
    }

}
