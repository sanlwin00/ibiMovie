using IbiMovie.Core;

namespace IbiMovie.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
    }
}
