using CafeForGames.Models;
using System.Linq.Expressions;

namespace CafeForGames.Services.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetGamesAllAsync(Expression<Func<T, bool>>? Fillter = null);
        Task SaveGamesAsync();
        Task<T> GetGamesByIdAsync(Expression<Func<T, bool>>? Fillter = null, bool tracked = true);
        Task AddGameAsync(T game);
        Task DeleteGameAsync(T game);
    }
}
