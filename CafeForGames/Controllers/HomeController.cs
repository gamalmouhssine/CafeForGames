using CafeForGames.Repository;
using Microsoft.AspNetCore.Mvc;
using CafeForGames.Models;

namespace CafeForGames.Controllers
{
    public class HomeController : Controller
    {
        IGamerepository<Games> _CafeForGames;
        public HomeController(IGamerepository<Games> CafeForGames)
        {
            _CafeForGames = CafeForGames;
        }
        public IActionResult Index()
        {
            var result = _CafeForGames.List();
            return View(result);
        }
    }
}
