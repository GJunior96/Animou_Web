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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AnimouContext database) : base(database) { }

        public async Task<User> GetUserAnimes(Guid id)
        {
            return await Database.Users.AsNoTracking()
                .Include(x => x.Animes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
