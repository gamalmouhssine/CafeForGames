using Microsoft.AspNetCore.Mvc;
using CafeForGames.Models;
using CafeForGames.Services.IRepository;
using System.Net;

namespace CafeForGames.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class HomeController : ControllerBase
    {
        protected ApiResponse _response;
        public IGamesService _Service;
        public HomeController(IGamesService Service)
        {
            _Service = Service;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> Index()
        {
            try
            {
                var result = await _Service.GetGamesAllAsync();
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetGameById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GamesById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var result = await _Service.GetGamesByIdAsync(c => c.Id == id);
                if (result == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> AddGame([FromBody] Games Game)
        {
            try
            {
                var result = await _Service.GetGamesAllAsync(u => u.Id == Game.Id);
                if (Game == null)
                {
                    return BadRequest(Game);
                }
                await _Service.AddGameAsync(Game);
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetGameById", new { id = Game.Id }, Game);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteGame(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var result = await _Service.GetGamesByIdAsync(c => c.Id == id);

                if (result == null) { return NotFound(); }

                await _Service.DeleteGameAsync(result);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateGame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateGame(int id, [FromBody] Games game)
        {
            try
            {
                if (game == null || id != game.Id)
                {
                    return BadRequest();
                }
                await _Service.UpdateGameAsync(game);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
