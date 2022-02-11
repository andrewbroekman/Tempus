namespace Tempus.Abstractions.Events
{
    public interface ISerializedEvent : IEvent
    {
        public string EventClass { get; set; }

        public string EventType { get; set; }

        public string EventData { get; set; }
    }
}
