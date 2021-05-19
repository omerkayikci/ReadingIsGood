using MediatR;
using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.CQRS;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Mediator
{
    public class CommandQueryMediator : ICommandSender, IQueryProcessor
    {
        private readonly IMediator mediator;
        public CommandQueryMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            return mediator.Send(query);
        }

        public Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        {
            return mediator.Send(command);
        }
    }
}
