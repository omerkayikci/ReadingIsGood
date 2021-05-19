using MediatR;

namespace ReadingIsGood.Core.CQRS
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}
