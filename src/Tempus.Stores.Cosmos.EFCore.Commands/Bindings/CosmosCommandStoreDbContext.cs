using Microsoft.EntityFrameworkCore;
using Tempus.Commands;
using Tempus.Stores.EFCore.Commands.Configuration;

namespace Tempus.Stores.Cosmos.EFCore.Commands.Bindings
{
    public class CosmosCommandStoreDbContext : DbContext
    {
        public CosmosCommandStoreDbContext(DbContextOptions<CosmosCommandStoreDbContext> options)
            : base(options) { }

        public DbSet<SerializedCommand> Commands { get; private set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SerializedCommandConfiguration());
        }
    }
}
