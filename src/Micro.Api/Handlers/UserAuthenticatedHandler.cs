using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Micro.Common.Events;

namespace Micro.Api.Handlers
{
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticated>
    {
        private readonly ILogger<UserAuthenticatedHandler> _logger;

        public UserAuthenticatedHandler(ILogger<UserAuthenticatedHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(UserAuthenticated @event)
        {
            await Task.CompletedTask;
            _logger.LogInformation($"User authenticated: {@event.Email}");
        }
    }
}
