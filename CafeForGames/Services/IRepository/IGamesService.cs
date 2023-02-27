using CafeForGames.Models;
using System.Linq.Expressions;

namespace CafeForGames.Services.IRepository
{
    public interface IGamesService
    {
        Task UpdateGameAsync(Games game);
    }
}
