public class ApiVotacionService : IVotacionService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiVotacionService> _logger;

    public ApiVotacionService(
        IHttpClientFactory httpClientFactory,
        ILogger<ApiVotacionService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _logger = logger;
    }

    public async Task RegistrarVotoAsync(string cedula, int partidoId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "votacion/votar",
                new { cedula, partidoId });

            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error al registrar voto para cédula: {Cedula}", cedula);
            throw new ApplicationException("Error en comunicación con API", ex);
        }
    }

    public async Task<List<ResultadoVotacion>> ObtenerResultadosAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<ResultadoVotacion>>(
                "votacion/resultados");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error obteniendo resultados de votación");
            return new List<ResultadoVotacion>();
        }
    }
}