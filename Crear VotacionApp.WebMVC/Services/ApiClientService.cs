using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VotacionApp.WebMVC.Models;

namespace VotacionApp.WebMVC.Services
{
    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiClientService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Obtener la URL base de la API de appsettings.json
            _apiBaseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl")
                          ?? throw new ArgumentNullException("ApiSettings:BaseUrl not found in configuration.");

            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
        }

        // --- Métodos para Votantes (ya deberías tenerlos) ---
        public async Task<List<VotanteViewModel>> GetAllVotantesAsync()
        {
            var response = await _httpClient.GetAsync("api/Votantes");
            response.EnsureSuccessStatusCode(); // Lanza excepción si la respuesta no es 2xx
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<VotanteViewModel>>(content, options) ?? new List<VotanteViewModel>();
        }

        public async Task<VotanteViewModel?> GetVotanteByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Votantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<VotanteViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<VotanteViewModel?> GetVotanteByCedulaAsync(string cedula)
        {
            var response = await _httpClient.GetAsync($"api/Votantes/cedula/{cedula}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<VotanteViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<bool> CreateVotanteAsync(CreateVotanteViewModel votante)
        {
            var json = JsonSerializer.Serialize(votante);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Votantes", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateVotanteAsync(VotanteViewModel votante)
        {
            var json = JsonSerializer.Serialize(votante);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Votantes/{votante.VotanteId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVotanteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Votantes/{id}");
            return response.IsSuccessStatusCode;
        }


        // --- NUEVOS Métodos para Partidos Políticos ---

        public async Task<List<PartidoPoliticoViewModel>> GetAllPartidosPoliticosAsync()
        {
            var response = await _httpClient.GetAsync("api/PartidosPoliticos");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<PartidoPoliticoViewModel>>(content, options) ?? new List<PartidoPoliticoViewModel>();
        }

        public async Task<PartidoPoliticoViewModel?> GetPartidoPoliticoByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/PartidosPoliticos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PartidoPoliticoViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public async Task<bool> CreatePartidoPoliticoAsync(CreatePartidoPoliticoViewModel partido)
        {
            var json = JsonSerializer.Serialize(partido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/PartidosPoliticos", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePartidoPoliticoAsync(PartidoPoliticoViewModel partido)
        {
            var json = JsonSerializer.Serialize(partido);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/PartidosPoliticos/{partido.PartidoPoliticoId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePartidoPoliticoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/PartidosPoliticos/{id}");
            return response.IsSuccessStatusCode;
        }

        internal async Task<IEnumerable<object>> GetPartidosPoliticosAsync()
        {
            throw new NotImplementedException();
        }
    }
}