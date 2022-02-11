using Tempus.Abstractions.Commands;

namespace Tempus.Commands
{
    public class SerializedCommand : ISerializedCommand
    {
        public Guid AggregateIdentifier { get; set; }
        public int? ExpectedVersion { get; set; }

        public Guid IdentityTenant { get; set; }
        public Guid IdentityUser { get; set; }
        public Guid? ImpersonatedUser { get; set; }

        public string CommandClass { get; set; }
        public string CommandType { get; set; }
        public string CommandData { get; set; }

        public Guid CommandIdentifier { get; set; }

        public DateTimeOffset? SendScheduled { get; set; }
        public DateTimeOffset? SendStarted { get; set; }
        public DateTimeOffset? SendCompleted { get; set; }
        public DateTimeOffset? SendCancelled { get; set; }

        public ISerializedCommand.Status SendStatus { get; set; } = ISerializedCommand.Status.Pending;
        public string SendError { get; set; }
    }
}
