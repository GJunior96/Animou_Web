using Animou.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animou.Business.Interfaces
{
    public interface IAnimeRepository : IRepository<Anime>
    {
        public Task<IEnumerable<Anime>> GetAnimesByUserId(Guid userId);

        public Task<Anime> GetAnimeByUserIdAndAnimeId(Guid userId, int animeId);
    }
}
