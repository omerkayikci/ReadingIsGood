using ReadingIsGood.Core.CQRS;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Mediator.Query
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }
}
