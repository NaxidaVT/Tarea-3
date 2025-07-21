using System.Text.Json;
using SistemaVotacion.MVC.Models;

namespace SistemaVotacion.MVC.Services
{
public class VotanteService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7240/api/votantes"; // Cambia el puerto si tu API usa otro

    public VotanteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Votante?> ObtenerVotantePorCedula(string cedula)
    {
        var response = await _httpClient.GetAsync(_baseUrl);
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var votantes = JsonSerializer.Deserialize<List<Votante>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return votantes?.FirstOrDefault(v => v.Cedula?.Trim() == cedula.Trim());
    }
}

}
