namespace SistemaVotacion.API.Models
{
    public class Voto
    {
        public int Id { get; set; }
        public int VotanteId { get; set; }
        public int PartidoId { get; set; }
        public DateTime FechaHora { get; set; }

        public Votante Votante { get; set; }
        public PartidoPolitico Partido { get; set; }
    }

}
