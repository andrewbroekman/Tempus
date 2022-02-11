using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tempus.Commands;

namespace Tempus.Stores.EFCore.Commands.Configuration
{
    public class SerializedCommandConfiguration : IEntityTypeConfiguration<SerializedCommand>
    {
        public void Configure(EntityTypeBuilder<SerializedCommand> builder)
        {
            builder.HasKey(p => p.CommandIdentifier);
            builder.HasIndex(p => p.AggregateIdentifier);
            builder.HasIndex(p => p.IdentityTenant);
            builder.HasIndex(p => p.IdentityUser);
        }
    }
}
