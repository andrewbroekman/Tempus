using Tempus.Abstractions.Aggregates;
using Tempus.Abstractions.Events;
using Tempus.Abstractions.Exceptions;
using Tempus.Aggregates;

namespace Tempus.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly IEventStore[] _stores;

        public EventRepository(IEnumerable<IEventStore> stores)
        {
            _stores = stores.ToArray() ?? throw new ArgumentNullException(nameof(stores));
        }

        public bool Exists<T>(Guid id) where T : IAggregateRoot
        {

            // Check whether the aggregate has any events associated with it.
            return _stores[0].Get(id, -1).Any();
        }

        public T Get<T>(Guid aggregate) where T : IAggregateRoot
        {
            return Rehydrate<T>(aggregate);
        }

        public IEvent[] Save<T>(T aggregate, int? version) where T : IAggregateRoot
        {
            if (version != null && (_stores[0].Exists(aggregate.AggregateIdentifier, version.Value)))
                throw new ConcurrencyException(aggregate.AggregateIdentifier);

            // Get the list of events that are not yet saved. 
            var events = aggregate.FlushUncommittedChanges();

            // Save the uncommitted changes.
            foreach (var store in _stores)
            {
                store.Save(aggregate, events);
            }
            
            // The event repository is not responsible for publishing these events. Instead they are returned to the 
            // caller for that purpose.
            return events;
        }

        private T Rehydrate<T>(Guid id) where T : IAggregateRoot
        {
            // Get all the events for the aggregate.
            var events = _stores[0].Get(id, -1);

            // Disallow empty event streams.
            if (!events.Any())
                throw new AggregateNotFoundException(typeof(T), id);

            // Create and load the aggregate.
            var aggregate = AggregateFactory<T>.CreateAggregate();
            aggregate.Rehydrate(events);
            return aggregate;
        }
    }
}
