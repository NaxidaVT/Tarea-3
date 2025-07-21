using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using SistemaVotacion.MVC.Models;

namespace SistemaVotacion.MVC.Services
{

    public class VotoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7240/api/votos/EmitirVoto";

        public VotoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> EmitirVoto(Voto voto)
        {
            var contenido = new StringContent(
                JsonSerializer.Serialize(voto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_url, contenido);
            return response.IsSuccessStatusCode;
        }
    }

}

