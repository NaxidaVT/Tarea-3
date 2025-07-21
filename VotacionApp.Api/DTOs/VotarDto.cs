using System.ComponentModel.DataAnnotations;

namespace VotacionApp.Api.DTOs
{
    public class VotarDto
    {
        [Required]
        public string CedulaVotante { get; set; }
        [Required]
        public int IdPartidoPolitico { get; set; }
    }
}