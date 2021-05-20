using ReadingIsGood.Core.Entities;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);

        Task<bool> IsValidUserAsync(string username, string password);
    }
}
