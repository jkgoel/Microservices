using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Common.Events;

namespace JKTech.Api.Handlers
{
    public class UserAuthenticatedHandler :IEventHandler<UserAuthenticated>
    {
        public async Task HandleAsync(UserAuthenticated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User authenticated: {@event.Email}");
        }
    }
}
