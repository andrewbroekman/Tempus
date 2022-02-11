using System;

namespace Tempus.Abstractions.Events
{
    public interface IEventQueue
    {
        void Publish(IEvent @event);

        void Subscribe<T>(Action<T> action) where T : IEvent;

        void Override<T>(Action<T> action, Guid tenant) where T : IEvent;
    }
}
