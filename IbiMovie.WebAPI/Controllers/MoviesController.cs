using IbiMovie.Application.Interfaces;
using IbiMovie.Application.UseCases;
using IbiMovie.Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IbiMovie.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var movies = await _movieService.GetAllAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    error = ex.Message
                });
            }
        }
    }

}
