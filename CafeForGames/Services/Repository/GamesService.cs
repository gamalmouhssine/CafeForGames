using CafeForGames.Data;
using CafeForGames.Models;
using CafeForGames.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CafeForGames.Services.Repository
{
    public class GamesService : IGamesService
    {
        public GamesService(AppDbContext Context)
        {
            _Context = Context;
        }

        public AppDbContext _Context { get; }

        public async Task<IEnumerable<Games>> GetGamesAllAsync()=> await _Context.Games.ToListAsync();

        public Task<Games> GetGamesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
