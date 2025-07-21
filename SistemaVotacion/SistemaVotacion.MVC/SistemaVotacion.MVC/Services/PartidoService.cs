using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SistemaVotacion.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVotacion.MVC.Services
{

        public class PartidoService
        {
            private readonly HttpClient _httpClient;
            private readonly string _baseUrl = "https://localhost:7240/api/PartidosPoliticos";

            public PartidoService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<PartidoPolitico>?> ObtenerPartidos()
            {
                var response = await _httpClient.GetAsync(_baseUrl);
                if (!response.IsSuccessStatusCode) return new();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PartidoPolitico>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    }

