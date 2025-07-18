using Microsoft.AspNetCore.Mvc;

public class PartidoController : Controller
{
    private readonly HttpClient _httpClient;

    public PartidoController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/partidos/");
    }

    public async Task<IActionResult> Index()
    {
        var partidos = await _httpClient.GetFromJsonAsync<List<Partido>>("");
        return View(partidos);
    }

    // CRUD similar a VotanteController
}