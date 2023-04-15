using IbiMovie.Core;
using IbiMovie.Application.Interfaces;

namespace IbiMovie.Application.UseCases
{
    public class GetAllMovies
    {
        private readonly IMovieService _movieService;

        public GetAllMovies(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IEnumerable<Movie>> ExecuteAsync()
        {
            return await _movieService.GetAllAsync();
        }
    }

}
