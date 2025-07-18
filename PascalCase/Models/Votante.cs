using System.ComponentModel.DataAnnotations;

public class Votante
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La cédula es obligatoria")]
    [StringLength(13, ErrorMessage = "Máximo 13 caracteres")]
    public string Cedula { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    public bool HaVotado { get; set; }
}