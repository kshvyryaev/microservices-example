using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Micro.Common.Events;
using Micro.Api.Repositories;
using Micro.Api.Models;

namespace Micro.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger<ActivityCreatedHandler> _logger;

        public ActivityCreatedHandler(
            IActivityRepository activityRepository,
            ILogger<ActivityCreatedHandler> logger)
        {
            _activityRepository = activityRepository;
            _logger = logger;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity
            {
                Id = @event.Id,
                UserId = @event.UserId,
                Category = @event.Category,
                Name = @event.Name,
                Description = @event.Description,
                CreatedAt = @event.CreatedAt
            });

            _logger.LogInformation($"Activity created: {@event.Name}");
        }
    }
}
