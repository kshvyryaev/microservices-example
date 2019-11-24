using System.Threading.Tasks;

namespace Micro.Common.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
