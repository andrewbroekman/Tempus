using Microsoft.EntityFrameworkCore;
using Tempus.Abstractions.Aggregates;
using Tempus.Abstractions.Events;
using Tempus.Abstractions.Utilities;
using Tempus.Aggregates;
using Tempus.Events;

namespace Tempus.Stores.Cosmos.EFCore.Events.Bindings
{
    public class CosmosEventStore : IEventStore
    {
        private readonly CosmosEventStoreDbContext _dbContext;

        public CosmosEventStore(CosmosEventStoreDbContext dbContext, ISerializer serializer)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();

            Serializer = serializer;
        }

        public ISerializer Serializer { get; }

        public bool Exists(Guid aggregate)
        {
            return _dbContext.Events.Any(x =>
                x.AggregateIdentifier == aggregate);
        }

        public bool Exists(Guid aggregate, int version)
        {
            return _dbContext.Events.Any(x =>
                x.AggregateIdentifier == aggregate &&
                x.AggregateVersion == version);
        }

        public IEnumerable<IEvent> Get(Guid aggregate, int version)
        {
            return GetSerialized(aggregate, version)
                .Select(x => x.Deserialize(Serializer))
                .ToList()
                .AsEnumerable();
        }

        public IEnumerable<Guid> GetExpired(DateTimeOffset at)
        {
            return _dbContext.Aggregates
                    .AsNoTracking()
                    .Where(x => x.AggregateExpires != null && x.AggregateExpires <= at)
                    .Select(x => x.AggregateIdentifier)
                    .ToList();
        }

        public void Save(IAggregateRoot aggregate, IEnumerable<IEvent> events)
        {
            var tenant = Guid.NewGuid();
            var user = Guid.NewGuid();

            var list = new List<ISerializedEvent>();

            foreach (var e in events)
            {
                var item = e.Serialize(Serializer, aggregate, tenant, user);

                list.Add(item);
            }

            EnsureAggregateExists(tenant, aggregate.AggregateIdentifier, aggregate.GetType().Name.Replace("Aggregate", string.Empty), aggregate.GetType().FullName);

            if (list.Count > 1)
                InsertEvents(list);
            else
                InsertEvent(list[0]);
        }

        private int Delete(Guid aggregate)
        {
            var aggregates = _dbContext.Aggregates.Where(x => x.AggregateIdentifier == aggregate);
            var events = _dbContext.Events.Where(x => x.AggregateIdentifier == aggregate);
            _dbContext.Aggregates.RemoveRange(aggregates);
            _dbContext.Events.RemoveRange(events);
            _dbContext.SaveChanges();
            return 0;
        }

        private void InsertEvent(ISerializedEvent e)
        {
            _dbContext.Events.Add((SerializedEvent)e);
            _dbContext.SaveChanges();
        }

        private void InsertEvents(List<ISerializedEvent> events)
        {
            _dbContext.Events.AddRange(events.Cast<SerializedEvent>());
            _dbContext.SaveChanges();
        }

        private void EnsureAggregateExists(Guid tenant, Guid aggregate, string name, string type)
        {
            var entity = _dbContext.Aggregates
                .AsNoTracking()
                .FirstOrDefault(x => x.AggregateIdentifier == aggregate);

            if (entity != default)
            {
                return;
            }

            if (entity == default)
            {
                _dbContext.Aggregates.Add(new SerializedAggregate
                {
                    AggregateClass = type,
                    AggregateIdentifier = aggregate,
                    AggregateType = name,
                    TenantIdentifier = Guid.NewGuid()
                });
                _dbContext.SaveChanges();
            }
        }

        private void GetClassAndTenant(Guid aggregate, out string @class, out Guid tenant)
        {

            var entity = _dbContext.Aggregates
                  .AsNoTracking()
                  .Where(x => x.AggregateIdentifier == aggregate)
                  .FirstOrDefault();

            @class = entity.AggregateClass;
            tenant = entity.TenantIdentifier;
        }

        private IEnumerable<SerializedEvent> GetSerialized(Guid aggregate, int fromVersion)
        {
            return _dbContext.Events.Where(x =>
                x.AggregateIdentifier == aggregate &&
                x.AggregateVersion == fromVersion);
        }
    }
}
