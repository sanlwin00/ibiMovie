using IbiMovie.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IbiMovie.WebUI.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public PeopleController(ILogger<PeopleController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("ApiSettings:SWApiUrl").Value);
        }

        public async Task<IActionResult> Index()
        {          

            return View();
        }

        // Search by character name
        public async Task<IActionResult> SearchByName(string searchName)
        {
            if (string.IsNullOrWhiteSpace(searchName))
            {
                return View("Index", null);
            }
            else
            {
                try
                {
                    var response = await _httpClient.GetAsync("people");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var results = JsonDocument.Parse(content).RootElement.GetProperty("results");
                        var filteredResults = results.EnumerateArray()
                            .Where(element => element.GetProperty("name").GetString().Contains(searchName, StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        return View("Index", await ConvertSWapiResultsToActorList(filteredResults));
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel(new Exception(response.StatusCode.ToString())));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching the actors.");
                    return View("Error", new ErrorViewModel(ex));
                }
            }
        }

        // convert SWAPI response and create actor list
        private async Task<List<ActorViewModel>> ConvertSWapiResultsToActorList(List<JsonElement> results)
        {
            var actors = new List<ActorViewModel>();
            foreach (var result in results)
            {
                ActorViewModel actor = new ActorViewModel(result);
                actor.Movies = new List<MovieViewModel>();
                foreach (var filmApiUrl in result.GetProperty("films").EnumerateArray())
                {
                    var response = await _httpClient.GetAsync(new Uri(filmApiUrl.GetString()));
                    var content = await response.Content.ReadAsStringAsync();
                    var film = JsonDocument.Parse(content).RootElement;
                    actor.Movies.Add(new MovieViewModel(film));
                }

                actors.Add(actor);
            }
            return actors;
        }
    }

}
