using IbiMovie.Core;
using IbiMovie.Application.Interfaces;

namespace IbiMovie.Application.UseCases
{
    public class GetAllActors
    {
        private readonly IActorService _actorService;

        public GetAllActors(IActorService actorService)
        {
            _actorService = actorService;
        }

        public async Task<IEnumerable<Actor>> ExecuteAsync()
        {
            return await _actorService.GetAllAsync();
        }
    }
}
