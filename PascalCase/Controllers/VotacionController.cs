using Microsoft.AspNetCore.Mvc;

public class VotacionController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IVotacionService _votacionService;

    public VotacionController(
        IHttpClientFactory httpClientFactory,
        IVotacionService votacionService)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
        _votacionService = votacionService;
    }

    [HttpGet]
    public IActionResult Verificar() => View();

    [HttpPost]
    public async Task<IActionResult> Verificar(string cedula)
    {
        var response = await _httpClient.GetAsync($"votantes/por-cedula/{cedula}");

        if (!response.IsSuccessStatusCode)
        {
            TempData["Error"] = "Votante no registrado";
            return View();
        }

        return RedirectToAction("Votar", new { cedula });
    }

    [HttpGet]
    public async Task<IActionResult> Votar(string cedula)
    {
        var partidos = await _httpClient.GetFromJsonAsync<List<Partido>>("partidos");
        ViewBag.Cedula = cedula;
        return View(partidos);
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarVoto(string cedula, int partidoId)
    {
        try
        {
            await _votacionService.RegistrarVotoAsync(cedula, partidoId);
            return RedirectToAction("Gracias");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Votar", new { cedula });
        }
    }

    public IActionResult Gracias() => View();

    public async Task<IActionResult> Resultados()
    {
        var resultados = await _votacionService.ObtenerResultadosAsync();
        return View(resultados);
    }
}