using ReadingIsGood.Core.CQRS;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Mediator.Command
{
    public interface ICommandSender
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command);
    }
}
