using System;

namespace Tempus.Abstractions.Commands
{
    public interface ISerializedCommand
    {
        Guid AggregateIdentifier { get; set; }
        int? ExpectedVersion { get; set; }

        Guid IdentityTenant { get; set; }
        Guid IdentityUser { get; set; }

        Guid CommandIdentifier { get; set; }
        string CommandClass { get; set; }
        string CommandType { get; set; }
        string CommandData { get; set; }

        DateTimeOffset? SendScheduled { get; set; }
        DateTimeOffset? SendStarted { get; set; }
        DateTimeOffset? SendCompleted { get; set; }
        DateTimeOffset? SendCancelled { get; set; }

        Status SendStatus { get; set; }
        string SendError { get; set; }

      
        public enum Status
        {
            Error = -1,
            Pending = 1,
            Started = 2,
            Completed = 3,
            Scheduled = 4
        }
    }
}
