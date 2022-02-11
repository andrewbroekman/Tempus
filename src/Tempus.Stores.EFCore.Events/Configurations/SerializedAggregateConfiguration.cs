using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Aggregates;

namespace Tempus.Stores.EFCore.Events.Configurations
{
    public class SerializedAggregateConfiguration : IEntityTypeConfiguration<SerializedAggregate>
    {
        public void Configure(EntityTypeBuilder<SerializedAggregate> builder)
        {
            builder.HasKey(p => p.AggregateIdentifier);
            builder.HasIndex(p => p.AggregateClass);
            builder.HasIndex(p => p.TenantIdentifier);
        }
    }
}
