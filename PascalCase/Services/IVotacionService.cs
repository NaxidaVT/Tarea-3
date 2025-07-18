public interface IVotacionService
{
    Task RegistrarVotoAsync(string cedula, int partidoId);
    Task<List<ResultadoVotacion>> ObtenerResultadosAsync();
}