using System.Threading.Tasks;

namespace ReadingIsGood.MongoDB.Abstractions
{
    public interface IDatabaseConnection
    {
        ITransactionScope BeginTransactionScope();
        Task<ITransactionScope> BeginTransactionScopeAsync();
    }
}
