using Animou.Business.Models;

namespace Animou.Business.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserAnimes(Guid id);
    }
}
