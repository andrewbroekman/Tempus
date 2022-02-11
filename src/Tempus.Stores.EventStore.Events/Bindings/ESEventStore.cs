using System.Text;
using EventStore.ClientAPI;
using Tempus.Abstractions.Aggregates;
using Tempus.Abstractions.Events;
using Tempus.Abstractions.Utilities;
using Tempus.Aggregates;
using Tempus.Events;

namespace Tempus.Stores.EventStore.Events.Bindings
{
    public class ESEventStore : IEventStore
    {

        private readonly IEventStoreConnection connection;
        private readonly ISerializer _serializer;

        public ESEventStore(IEventStoreConnection connection, ISerializer serializer)
        {
            this.connection = connection;
            _serializer = serializer;
        }

        public ISerializer Serializer => _serializer;

        public bool Exists(Guid aggregate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Guid aggregate, int version)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEvent> Get(Guid aggregate, int version)
        {
            var streamName = string.Empty;
            var result = connection.ReadStreamEventsForwardAsync(streamName, 0, 999, true).Result;
            return result.Events.Select(x => new Event
                {
                    AggregateIdentifier = Guid.Empty,

                    AggregateVersion = 0,

                    IdentityTenant = Guid.Empty,

                    IdentityUser = Guid.Empty,

                    EventClass = string.Empty,

                    EventType = string.Empty,

                    EventData = string.Empty,

                    EventTime = DateTimeOffset.MinValue
                });
        }

        public IEnumerable<Guid> GetExpired(DateTimeOffset at)
        {
            throw new NotImplementedException();
        }

        public void Save(IAggregateRoot aggregate, IEnumerable<IEvent> events)
        {
            var type = aggregate.GetType().Name.Replace("Aggregate", string.Empty);
            var identifier = aggregate.AggregateIdentifier;

            var list = new List<ISerializedEvent>();

            foreach (var e in events)
            {
                e.EventIdentifier = Guid.NewGuid();
                var item = e.Serialize(Serializer, aggregate, Guid.NewGuid(), Guid.NewGuid());

                list.Add(item);
            }

            var esEvents = list.Select(x => {

                var metadata = new SerializedMetadata
                {
                    AggregateIdentifier = x.AggregateIdentifier,
                    AggregateClass = aggregate.GetType().AssemblyQualifiedName,
                    AggregateType = aggregate.GetType().Name,
                    AggregateVersion = x.AggregateVersion,
                    IdentityTenant = x.IdentityTenant,
                    IdentityUser = x.IdentityUser,
                    EventIdentifier = x.EventIdentifier,
                    EventClass = x.EventClass,
                    EventType = x.EventType,
                    EventTime = x.EventTime
                };
                var metadataSerialized = _serializer.Serialize(metadata);

                return new EventData(x.EventIdentifier, x.EventType, true, Encoding.UTF8.GetBytes(x.EventData), Encoding.UTF8.GetBytes(_serializer.Serialize(metadata)));
            });

            connection.AppendToStreamAsync($"{type}-{identifier}", ExpectedVersion.Any, esEvents).Wait();
        }
    }
}
