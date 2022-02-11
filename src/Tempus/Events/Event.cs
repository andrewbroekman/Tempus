using Tempus.Abstractions.Events;

namespace Tempus.Events
{
    public class Event : IEvent
    {
        public Guid AggregateIdentifier { get; set; }

        public int AggregateVersion { get; set; }

        public Guid IdentityTenant { get; set; }

        public Guid IdentityUser { get; set; }

        public Guid? ImpersonatedUser { get; set; }

        public Guid EventIdentifier { get; set; }

        public string EventClass { get; set; }

        public string EventType { get; set; }

        public string EventData { get; set; }

        public DateTimeOffset EventTime { get; set; }

        public Event()
        {
            EventTime = DateTimeOffset.UtcNow;
        }
    }
}
