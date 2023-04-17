using IbiMovie.Core;
using IbiMovie.Infra.Data;
using IbiMovie.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IbiMovie.Infra.Repositories
{
    public class ActorService : IActorService
    {
        private readonly AppDbContext _dbContext;

        public ActorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _dbContext.Actors
                .Include(a => a.MovieActors)
                .ThenInclude(ma => ma.Movie)
                .ToListAsync();
        }

        public async Task<IEnumerable<Actor>> GetByNameAsync(string name)
        {
            return await _dbContext.Actors
            .Include(a => a.MovieActors)
                .ThenInclude(ma => ma.Movie)
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}
