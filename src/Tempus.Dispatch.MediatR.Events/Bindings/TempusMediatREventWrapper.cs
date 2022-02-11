using MediatR;
using Tempus.Abstractions.Events;

namespace Tempus.Dispatch.MediatR.Events.Bindings
{
    public class TempusMediatREventWrapper<TEvent> : INotification where TEvent : IEvent
    {
        public TEvent Event { get; init; }
    }
}
