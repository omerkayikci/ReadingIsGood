using MediatR;
using ReadingIsGood.Core.CQRS;

namespace ReadingIsGood.Application.Mediator.Query
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
