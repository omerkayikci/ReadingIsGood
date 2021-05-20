using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User, string> genericRepository;
        public UserRepository(
            IGenericRepository<User, string> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task AddUserAsync(User user)
        {
            await this.genericRepository
                                .AddOneAsync(user);
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            return await genericRepository.Query().AnyAsync(r => r.Username == username && r.Password == password);
        }
    }
}
