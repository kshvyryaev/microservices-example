using System.Threading.Tasks;

namespace Micro.Common.Events
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
