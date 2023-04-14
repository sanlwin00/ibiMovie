using IbiMovie.Core;
using IbiMovie.Infra.Data;
using IbiMovie.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IbiMovie.Infra.Repositories
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _dbContext;

        public MovieService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _dbContext.Movies
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _dbContext.Movies
            .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .SingleOrDefaultAsync(m => m.Id == id);
        }
    }

}
