using ReadingIsGood.Core.Request;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Abstractions
{
    public interface IAuthenticationService : IApplicationService
    {
        Task<string> GenerateToken(AuthRequest request);
    }
}
