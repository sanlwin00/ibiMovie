using IbiMovie.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
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

                return View(await ConvertResponseContentToActorList(response.Content));
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
            try
            {
                var response = await _httpClient.GetAsync($"actors/{searchTerm}");

                return View("Index", await ConvertResponseContentToActorList(response.Content));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the actors.");
                return View("Error", new ErrorViewModel(ex));
            }
        }

        //loop through json array and create actor objects
        private async Task<List<ActorViewModel>> ConvertResponseContentToActorList(HttpContent content)
        {
            var jsonDocument = JsonDocument.Parse(await content.ReadAsStringAsync());
            var results = jsonDocument.RootElement;

            var actors = new List<ActorViewModel>();            
            foreach (var result in results.EnumerateArray())
            {
                ActorViewModel actor = new ActorViewModel(result);
                actor.Movies = new List<MovieViewModel>();
                foreach (var ma in result.GetProperty("movieActors").EnumerateArray())
                {
                    actor.Movies.Add(new MovieViewModel(ma.GetProperty("movie")));
                }
                actors.Add(actor);
            }

            return actors;
        }
    }
}
