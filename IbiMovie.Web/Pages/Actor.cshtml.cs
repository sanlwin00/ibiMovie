using IbiMovie.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IbiMovie.Web.Pages
{
    public class ActorModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ActorModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IEnumerable<Actor> Actors { get; set; }

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:5001/api/actors");
            response.EnsureSuccessStatusCode();

            Actors = await response.Content.ReadAsAsync<IEnumerable<Actor>>();
        }
    }
}
