using CafeForGames.Data;
using CafeForGames.Models;
using CafeForGames.Services.Base;
using CafeForGames.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CafeForGames.Services.Repository
{
    public class GamesService : BaseRepository<Games>,IGamesService
    {
        public GamesService(AppDbContext Context):base(Context)
        {
            _Contexts = Context;
        }

        public AppDbContext _Contexts;

        public async Task<Games> UpdateGameAsync(Games game)
        {
            _Contexts.Games.Update(game);
            await _Contexts.SaveChangesAsync();
            return game;
        }
    }
}
