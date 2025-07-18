using Microsoft.AspNetCore.Mvc;

public class VotanteController : Controller
{
    private readonly HttpClient _httpClient;

    public VotanteController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/votantes/");
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("");
        var votantes = await response.Content.ReadFromJsonAsync<List<Votante>>();
        return View(votantes);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Votante votante)
    {
        var response = await _httpClient.PostAsJsonAsync("", votante);
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Error al crear votante");
        return View(votante);
    }

    // Similar para Edit, Delete
}