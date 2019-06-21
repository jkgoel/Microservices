using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Common.Events;

namespace JKTech.Api.Handlers
{
    public class UserCreatedHandler: IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User created: {@event.Name}");
        }
    }
}
