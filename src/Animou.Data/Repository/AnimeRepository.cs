using Animou.Business.Interfaces;
using Animou.Business.Models;
using Animou.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animou.Data.Repository
{
    public class AnimeRepository : Repository<Anime>, IAnimeRepository
    {
        public AnimeRepository(AnimouContext database) : base(database) { }

        public async Task<Anime?> GetAnimeByUserIdAndAnimeId(Guid userId, int animeId)
        {
            return await Database.Animes.AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId == userId && x.AnimeId == animeId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Anime>> GetAnimesByUserId(Guid userId)
        {
            return await Database.Animes.AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
