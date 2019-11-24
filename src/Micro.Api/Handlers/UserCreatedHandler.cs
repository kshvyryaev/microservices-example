using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Micro.Common.Events;

namespace Micro.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly ILogger<UserCreatedHandler> _logger;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            await Task.CompletedTask;
            _logger.LogInformation($"User created: {@event.Email}");
        }
    }
}
