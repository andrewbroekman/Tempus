using Tempus.Abstractions.Aggregates;
using Tempus.Abstractions.Utilities;

namespace Tempus.Abstractions.Events
{
    public interface IEventStore
    {
        ISerializer Serializer { get; }

        bool Exists(Guid aggregate);

        bool Exists(Guid aggregate, int version);

        IEnumerable<IEvent> Get(Guid aggregate, int version);

        IEnumerable<Guid> GetExpired(DateTimeOffset at);

        void Save(IAggregateRoot aggregate, IEnumerable<IEvent> events);
    }
}
