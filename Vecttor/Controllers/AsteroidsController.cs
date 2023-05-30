using Microsoft.AspNetCore.Mvc;
using Vecttor.Services;

namespace Vecttor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsteroidsController : ControllerBase
    {
        private readonly IAsteroidsService _asteroidService;

        public AsteroidsController(IAsteroidsService asteroidsService)
        {
            _asteroidService = asteroidsService;
        }

        [HttpGet(Name = "GetAsteroids")]
        public async Task<IActionResult> GetAsteroids(int days)
        {
            if (days < 1 || days > 7)
            {
                return BadRequest("The 'days' parameter must be a value between 1 and 7.");
            }

            var hazardousAsteroids = await _asteroidService.GetAsteroids(days);
            return Ok(hazardousAsteroids);

        }
    }
}
