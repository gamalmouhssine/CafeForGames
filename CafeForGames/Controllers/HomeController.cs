using Microsoft.AspNetCore.Mvc;
using CafeForGames.Models;
using CafeForGames.Services.IRepository;

namespace CafeForGames.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class HomeController : ControllerBase
    {
        public IGamesService _Service;
        public HomeController(IGamesService Service)
        {
            _Service = Service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Index()
        {
            var result = await _Service.GetGamesAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}",Name ="GetGameById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GamesById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await _Service.GetGamesByIdAsync(c=>c.Id ==id);
            if (result == null) 
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddGame([FromBody] Games Game)
        {
            if (await _Service.GetGamesAllAsync(u=>u.Id == Game.Id) != null)
            {
                ModelState.AddModelError("Custom Error", "Game Aleady exist");
                return BadRequest();
            }
            if (Game == null)
            {
                return BadRequest(Game);
            }
            var result = await _Service.AddGameAsync(Game);
            return CreatedAtAction(nameof(GamesById),new {id = result},result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await _Service.GetGamesByIdAsync(c => c.Id == id);

            if (result == null) { return NotFound(); }

            await _Service.DeleteGameAsync(result);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateGame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGame(int id,[FromBody] Games game)
        {
            if (game == null || id != game.Id)
            {
                return BadRequest();
            }
            await _Service.UpdateGameAsync(game);
            return NoContent();
        }
    }
}
