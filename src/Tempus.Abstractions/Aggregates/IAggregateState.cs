using Tempus.Abstractions.Events;

namespace Tempus.Abstractions.Aggregates
{
    public interface IAggregateState
    {
        void Apply(IEvent @event);
    }
}
