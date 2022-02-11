using Microsoft.EntityFrameworkCore;
using Tempus.Commands;
using Tempus.Stores.EFCore.Commands.Configuration;

namespace Tempus.Stores.Sql.EFCore.Commands.Bindings
{
    public class SqlCommandStoreDbContext : DbContext
    {
        public SqlCommandStoreDbContext(DbContextOptions<SqlCommandStoreDbContext> options)
            : base(options) { }

        public DbSet<SerializedCommand> Commands { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SerializedCommandConfiguration());
        }
    }
}
