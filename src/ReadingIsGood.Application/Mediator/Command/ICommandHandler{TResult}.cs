using MediatR;
using ReadingIsGood.Core.CQRS;

namespace ReadingIsGood.Application.Mediator.Command
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
