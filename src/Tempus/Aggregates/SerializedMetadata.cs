namespace Tempus.Aggregates
{
    public class SerializedMetadata
    {
        public string ApiVersion => "2021-10-12";

        public Guid AggregateIdentifier { get; set; }

        public string AggregateClass { get; set; }

        public string AggregateType { get; set; }

        public int AggregateVersion { get; set; }

        public Guid IdentityTenant { get; set; }

        public Guid IdentityUser { get; set; }

        public Guid EventIdentifier { get; set; }

        public string EventClass { get; set; }

        public string EventType { get; set; }

        public DateTimeOffset EventTime { get; set; }
    }
}
