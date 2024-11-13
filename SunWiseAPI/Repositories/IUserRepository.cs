using SunWiseAPI.Models;

namespace SunWiseAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
    }
}
