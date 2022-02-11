using Microsoft.EntityFrameworkCore;
using Tempus.Aggregates;
using Tempus.Events;
using Tempus.Stores.EFCore.Events.Configurations;

namespace Tempus.Stores.Sql.EFCore.Events.Bindings
{
    public class SqlEventStoreDbContext : DbContext
    {
        public SqlEventStoreDbContext(DbContextOptions<SqlEventStoreDbContext> options)
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
