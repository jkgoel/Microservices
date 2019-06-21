using System;

namespace JKTech.Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
         Guid UserId{ get; }
    }
}