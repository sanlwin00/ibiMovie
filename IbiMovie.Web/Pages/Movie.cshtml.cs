using IbiMovie.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IbiMovie.Web.Pages
{
    public class MovieModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public MovieModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IEnumerable<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:5001/api/movies");
            response.EnsureSuccessStatusCode();

            Movies = await response.Content.ReadAsAsync<IEnumerable<Movie>>();
        }
    }    
}
