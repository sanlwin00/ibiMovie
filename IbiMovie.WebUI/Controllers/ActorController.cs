using IbiMovie.Core;
using IbiMovie.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IbiMovie.WebUI.Controllers
{
    public class ActorController : Controller
    {
        private readonly ILogger<ActorController> _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public ActorController(ILogger<ActorController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("ApiSettings:ApiUrl").Value);
        }

        // Show all actors
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("actors");

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new Exception($"Status Code:{response.StatusCode}"));
                }

                var responseStream = await response.Content.ReadAsStreamAsync();
                var actors = await JsonSerializer.DeserializeAsync<IEnumerable<Actor>>(responseStream, _jsonSerializerOptions);

                return View(actors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the actors.");
                return View("Error", new ErrorViewModel(ex));
            }
        }

        // Filter by actor name
        public async Task<IActionResult> FilterByActorName(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"actors/{searchTerm}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error");
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var actors = await JsonSerializer.DeserializeAsync<IEnumerable<Actor>>(responseStream, _jsonSerializerOptions);

            return View("Index", actors);
        }

    }
}
