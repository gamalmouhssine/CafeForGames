using CafeForGames.Models;
using CafeForGames.Services.Base;
using System.Linq.Expressions;

namespace CafeForGames.Services.IRepository
{
    public interface IGamesService:IBaseRepository<Games>
    {
        Task<Games> UpdateGameAsync(Games game);
    }
}
