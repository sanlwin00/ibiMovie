using IbiMovie.Application.Interfaces;
using IbiMovie.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace IbiMovie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actors = await _actorService.GetAllAsync();
            return Ok(actors);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var actors = await _actorService.GetByNameAsync(name);
            return Ok(actors);
        }
    }

}
