using System;
using System.Threading.Tasks;
using JKTech.Common.Commands;
using JKTech.Common.Events;
using JKTech.Common.Exceptions;
using JKTech.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace JKTech.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient busClient, IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating Activity: {command.Name}");
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name,
                    command.Description, command.CreatedAt);
                await _busClient.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Category,
                    command.Name, command.Description, command.CreatedAt));
            }
            catch (JKTechException e)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, e.Code, e.Message));
                _logger.LogError(e.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateActivityRejected(command.Id, "error", ex.Message));
                _logger.LogError(ex.Message);
            }

        }
    }
}