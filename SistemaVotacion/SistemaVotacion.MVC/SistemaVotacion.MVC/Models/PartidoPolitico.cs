using Microsoft.AspNetCore.Mvc;

namespace SistemaVotacion.MVC.Models
{
    public class PartidoPolitico
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Siglas { get; set; }
        public string? Descripcion { get; set; }
    }
}
