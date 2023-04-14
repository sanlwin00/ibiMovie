using IbiMovie.Core;

namespace IbiMovie.Application.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<IEnumerable<Actor>> GetByNameAsync(string name);
    }
}
