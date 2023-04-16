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
            try
            {
                var actors = await _actorService.GetAllAsync();
                return Ok(actors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var actors = await _actorService.GetByNameAsync(name);
                return Ok(actors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }
    }

}
