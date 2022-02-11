using Tempus.Abstractions.Aggregates;
using Tempus.Abstractions.Events;
using Tempus.Abstractions.Utilities;

namespace Tempus.Events
{
    public static class EventExtensions
    {
        public static IEvent Deserialize(this SerializedEvent x, ISerializer serializer)
        {
            var data = serializer.Deserialize<IEvent>(x.EventData, Type.GetType(x.EventClass));

            data.AggregateIdentifier = x.AggregateIdentifier;
            data.AggregateVersion = x.AggregateVersion;
            data.EventTime = x.EventTime;
            data.IdentityTenant = x.IdentityTenant;
            data.IdentityUser = x.IdentityUser;

            return data;
        }

        public static ISerializedEvent Serialize(this IEvent @event, ISerializer serializer, IAggregateRoot aggregate, Guid tenant, Guid user)
        {
            var data = serializer.Serialize(@event, new[] { "AggregateIdentifier", "AggregateVersion", "EventIdentifier", "EventTime", "IdentityTenant", "IdentityUser" });

            var serialized = new SerializedEvent
            {
                AggregateIdentifier = aggregate.AggregateIdentifier,
                AggregateClass = aggregate.GetType().AssemblyQualifiedName,
                AggregateType = aggregate.GetType().Name,
                AggregateVersion = aggregate.AggregateVersion,

                EventIdentifier = @event.EventIdentifier,
                EventTime = @event.EventTime,
                EventClass = @event.GetType().AssemblyQualifiedName,
                EventType = @event.GetType().Name,
                EventData = data,

                IdentityTenant = Guid.Empty == @event.IdentityTenant ? tenant : @event.IdentityTenant,
                IdentityUser = Guid.Empty == @event.IdentityUser ? user : @event.IdentityUser
            };

            @event.IdentityTenant = serialized.IdentityTenant;
            @event.IdentityUser = serialized.IdentityUser;

            return serialized;
        }
    }
}
