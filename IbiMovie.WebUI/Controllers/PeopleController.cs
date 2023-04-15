using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using IbiMovie.Core;
using IbiMovie.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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
            var response = await _httpClient.GetAsync("people");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(content);
                var results = jsonDocument.RootElement.GetProperty("results");

                var people = new List<PersonViewModel>();
                foreach (var result in results.EnumerateArray())
                {
                    people.Add(new PersonViewModel(result));
                }

                if (!string.IsNullOrWhiteSpace(searchName))
                {
                    people = people.Where(p => p.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                return View("Index", people);
            }
            else
            {
                // Handle the error response
                return View("Error");
            }
        }
    }

}
