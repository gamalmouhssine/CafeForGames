using CafeForGames.Data;
using CafeForGames.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CafeForGames.Services.Base
{
    public class BaseRepository<T> :IBaseRepository<T> where T : class
    {
        internal DbSet<T> _DbSet;
        public BaseRepository(AppDbContext Context)
        {
            _Context = Context;
            _DbSet = Context.Set<T>();
        }

        public AppDbContext _Context;

        public async Task AddGameAsync(T game)
        {
            await _DbSet.AddAsync(game);
            await SaveGamesAsync();
        }

        public async Task DeleteGameAsync(T game)
        {
            _Context.Remove(game);
            await SaveGamesAsync();
        }

        public async Task<IEnumerable<T>> GetGamesAllAsync(Expression<Func<T, bool>>? Fillter = null)
        {
            IQueryable<T> query = _DbSet;

            if (Fillter != null)
            {
                query = query.Where(Fillter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetGamesByIdAsync(Expression<Func<T, bool>>? Fillter = null, bool tracked = true)
        {
            IQueryable<T> query = _DbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (Fillter != null)
            {
                query = query.Where(Fillter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task SaveGamesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
