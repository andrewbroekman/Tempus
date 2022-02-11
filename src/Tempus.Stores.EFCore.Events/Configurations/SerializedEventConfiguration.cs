using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Events;

namespace Tempus.Stores.EFCore.Events.Configurations
{
    public class SerializedEventConfiguration : IEntityTypeConfiguration<SerializedEvent>
    {
        public void Configure(EntityTypeBuilder<SerializedEvent> builder)
        {
            builder.HasKey(p => p.EventIdentifier);
            builder.HasIndex(p => p.IdentityTenant);
            builder.HasIndex(p => p.IdentityUser);
        }
    }
}
