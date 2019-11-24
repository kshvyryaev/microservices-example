using System;
using System.Threading.Tasks;
using RawRabbit;
using Micro.Common.Commands;
using Micro.Common.Events;
using Micro.Services.Activities.Services;
using Micro.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace Micro.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger _logger;

        public CreateActivityHandler(
            IBusClient busClient,
            IActivityService activityService,
            ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating activity: {command.Category} {command.Name}");

            try
            {
                await _activityService.AddAsync(
                    command.Id,
                    command.UserId,
                    command.Category,
                    command.Name,
                    command.Description,
                    command.CreatedAt);

                await _busClient.PublishAsync(new ActivityCreated(
                    command.Id,
                    command.UserId,
                    command.Category,
                    command.Name,
                    command.Description,
                    command.CreatedAt));
            }
            catch (ValidationException ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(
                    command.Id,
                    ex.Message,
                    ex.Code));

                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(
                    command.Id,
                    ex.Message,
                    "error"));

                _logger.LogError(ex.Message);
            }
        }
    }
}
