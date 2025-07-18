using System.ComponentModel.DataAnnotations;

public class Partido
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Las siglas son obligatorias")]
    [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
    public string Siglas { get; set; }
}