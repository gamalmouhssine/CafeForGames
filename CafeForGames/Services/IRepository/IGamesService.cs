using CafeForGames.Models;

namespace CafeForGames.Services.IRepository
{
    public interface IGamesService
    {
        Task<IEnumerable<Games>> GetGamesAllAsync();
        Task<Games> GetGamesByIdAsync(int id);
        Task<int> AddGameAsync(Games game);
        Task DeleteGameAsync(int id);
    }
}
