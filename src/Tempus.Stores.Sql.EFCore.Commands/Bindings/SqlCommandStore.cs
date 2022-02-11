using Microsoft.EntityFrameworkCore;
using Tempus.Abstractions.Commands;
using Tempus.Abstractions.Exceptions;
using Tempus.Abstractions.Utilities;
using Tempus.Commands;

namespace Tempus.Stores.Sql.EFCore.Commands.Bindings
{
    public class SqlCommandStore : ICommandStore
    {
        private readonly SqlCommandStoreDbContext _dbContext;
        private readonly ISerializer _serializer;

        public SqlCommandStore(SqlCommandStoreDbContext dbContext,
            ISerializer serializer)
        {
            _dbContext = dbContext;
            _serializer = serializer;
            _dbContext.Database.EnsureCreated();
        }

        public ISerializer Serializer => _serializer;

        public bool Exists(Guid command)
        {
            return _dbContext.Commands
                .AsNoTracking()
                .Any(x => x.CommandIdentifier == command);
        }

        public ISerializedCommand Get(Guid command)
        {
            var entity = _dbContext.Commands
                    .AsNoTracking()
                    .Where(x => x.CommandIdentifier == command)
                    .FirstOrDefault();

            return entity ?? throw new CommandNotFoundException($"Command not found: {command}");
        }

        public IEnumerable<ISerializedCommand> GetExpired(DateTimeOffset at)
        {
            var commands = _dbContext.Commands
                    .AsNoTracking()
                    .Where(x => x.SendScheduled <= at && x.SendStatus == ISerializedCommand.Status.Scheduled)
                    .ToArray();

            return commands;
        }

        public void Save(ISerializedCommand command, bool isNew)
        {
            if (isNew)
                _dbContext.Commands.Add((SerializedCommand)command);
            else
                _dbContext.Commands.Update((SerializedCommand)command);
            _dbContext.SaveChanges();
        }

        public ISerializedCommand Serialize(ICommand command)
        {
            return command.Serialize(Serializer, command.AggregateIdentifier, command.ExpectedVersion);
        }
    }
}
