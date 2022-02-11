using System;

namespace Tempus.Abstractions.Aggregates
{
    public interface ISerializedAggregate
    {
        public string AggregateClass { get; set; }

        public DateTimeOffset? AggregateExpires { get; set; }

        public Guid AggregateIdentifier { get; set; }

        public string AggregateType { get; set; }

        public Guid TenantIdentifier { get; set; }
    }
}
