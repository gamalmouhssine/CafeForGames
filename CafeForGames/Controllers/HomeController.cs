using Microsoft.AspNetCore.Mvc;
using CafeForGames.Models;
using CafeForGames.Services.IRepository;

namespace CafeForGames.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class HomeController : Controller
    {
        public IGamesService _Service;
        public HomeController(IGamesService Service)
        {
            _Service = Service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            var result = await _Service.GetGamesAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GamesById(int id)
        {
            var result = await _Service.GetGamesByIdAsync(id);
            if (result == null) 
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("")]
        public async Task<IActionResult> AddGame([FromBody] Games Game)
        {
            var result = await _Service.AddGameAsync(Game);
            return CreatedAtAction(nameof(GamesById),new {id = result,Controller = "Home"},result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _Service.GetGamesByIdAsync(id);
            if (result == null) { return NotFound(); }
            await _Service.DeleteGameAsync(id);
            return Ok(id);
        }
    }
}
