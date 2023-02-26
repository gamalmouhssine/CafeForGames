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
    }
}
