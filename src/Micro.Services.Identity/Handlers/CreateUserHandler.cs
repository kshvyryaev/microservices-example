using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RawRabbit;
using Micro.Common.Commands;
using Micro.Services.Identity.Services;
using Micro.Common.Events;
using Micro.Common.Exceptions;

namespace Micro.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(
            IBusClient busClient,
            IUserService userService,
            ILogger<CreateUserHandler> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating user: {command.Email} {command.Name}");

            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
            }
            catch (ValidationException ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(
                    command.Email,
                    ex.Message,
                    ex.Code));

                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(
                    command.Email,
                    ex.Message,
                    "error"));

                _logger.LogError(ex.Message);
            }
        }
    }
}
