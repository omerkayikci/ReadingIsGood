using MediatR;

namespace ReadingIsGood.Core.CQRS
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
