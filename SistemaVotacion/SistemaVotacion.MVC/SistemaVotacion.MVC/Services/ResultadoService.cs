using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SistemaVotacion.MVC.Models;

namespace SistemaVotacion.MVC.Services
{

    public class ResultadoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7240/api/votos/Resultados";

        public ResultadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultadoVotacion>?> ObtenerResultados()
        {
            var response = await _httpClient.GetAsync(_url);
            if (!response.IsSuccessStatusCode) return new();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ResultadoVotacion>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
