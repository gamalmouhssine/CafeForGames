using CafeForGames.Data;
using CafeForGames.Models;
using CafeForGames.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CafeForGames.Services.Repository
{
    public class GamesService : IGamesService
    {
        public GamesService(AppDbContext Context)
        {
            _Context = Context;
        }

        public AppDbContext _Context { get; }

        public async Task UpdateGameAsync(Games game)
        {
            _Context.Games.Update(game);
            await _Context.SaveChangesAsync();
        }
    }
}
