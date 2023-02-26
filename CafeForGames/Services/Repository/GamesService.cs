using CafeForGames.Data;
using CafeForGames.Models;
using CafeForGames.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CafeForGames.Services.Repository
{
    public class GamesService : IGamesService
    {
        public GamesService(AppDbContext Context)
        {
            _Context = Context;
        }

        public AppDbContext _Context { get; }

        public async Task<int> AddGameAsync(Games game)
        {
            await _Context.Games.AddAsync(game);
            await _Context.SaveChangesAsync();
            return game.Id;
        }

        public async Task DeleteGameAsync(int id)
        {
            var result = await _Context.Games.FirstOrDefaultAsync(c => c.Id == id);
            EntityEntry entityEntry = _Context.Entry(result);
            entityEntry.State = EntityState.Deleted;
            await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Games>> GetGamesAllAsync()=> await _Context.Games.ToListAsync();

        public async Task<Games> GetGamesByIdAsync(int id)
        {
            return await _Context.Games.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
