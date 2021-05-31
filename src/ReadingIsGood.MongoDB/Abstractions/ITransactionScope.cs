using System;
using System.Threading.Tasks;

namespace ReadingIsGood.MongoDB.Abstractions
{
    public interface ITransactionScope : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        Task CommitTransactionAsync();
    }
}
