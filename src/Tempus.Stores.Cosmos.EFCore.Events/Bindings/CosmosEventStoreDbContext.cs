using Microsoft.EntityFrameworkCore;
using Tempus.Aggregates;
using Tempus.Events;
using Tempus.Stores.EFCore.Events.Configurations;

namespace Tempus.Stores.Cosmos.EFCore.Events.Bindings
{
    public class CosmosEventStoreDbContext : DbContext
    {
        public CosmosEventStoreDbContext(DbContextOptions<CosmosEventStoreDbContext> options)
            : base(options) { }

        public DbSet<SerializedEvent> Events { get; private set; }

        public DbSet<SerializedAggregate> Aggregates { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SerializedAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new SerializedEventConfiguration());
        }
    }
}
