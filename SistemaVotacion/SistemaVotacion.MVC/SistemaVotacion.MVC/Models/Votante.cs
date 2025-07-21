namespace SistemaVotacion.MVC.Models
{
    public class Votante
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool YaVoto { get; set; }
    }

}
