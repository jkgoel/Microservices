using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Common.Commands;
using JKTech.Common.Events;
using JKTech.Common.Exceptions;
using JKTech.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace JKTech.Services.Identity.Handlers
{
    public class CreateUserHandler:ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IBusClient busClient, IUserService userService, ILogger<CreateUserHandler> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }
        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating User: {command.Name}");
            try
            {
                await _userService.RegisterAsync(command.Email,command.Password,command.Name);
                await _busClient.PublishAsync(new UserCreated(command.Email,command.Name));
            }
            catch (JKTechException e)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email,e.Code,e.Message));
                _logger.LogError(e.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, "error", ex.Message));
                _logger.LogError(ex.Message);
            }

        }
    }
}
