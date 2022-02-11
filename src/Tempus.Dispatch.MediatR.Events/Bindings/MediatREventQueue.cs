using MediatR;
using Tempus.Abstractions.Events;

namespace Tempus.Dispatch.MediatR.Events.Bindings
{
    public class MediatREventQueue : IEventQueue
    {
        private readonly IMediator _mediator;

        public MediatREventQueue(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Override<T>(Action<T> action, Guid tenant) where T : IEvent
        {
            throw new NotImplementedException();
        }

        public void Publish(IEvent @event)
        {
            var wrapper = typeof(TempusMediatREventWrapper<>);
            var type = @event.GetType();
            var notificationType = wrapper.MakeGenericType(type);
            var notification = Activator.CreateInstance(notificationType);

            var eventProperty = notification.GetType().GetProperty("Event");
            eventProperty.SetValue(notification, @event);

            _mediator.Publish(notification);
        }

        public void Subscribe<T>(Action<T> action) where T : IEvent
        {
            throw new NotImplementedException();
        }
    }
}
