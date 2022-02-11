using Tempus.Abstractions.Commands;

namespace Tempus.Commands
{
    public class Command : ICommand
    {
        public Guid AggregateIdentifier { get; set; }

        public int? ExpectedVersion { get; set; }

        public Guid IdentityTenant { get; set; }

        public Guid IdentityUser { get; set; }

        public Guid? ImpersonatedUser { get; set; }

        public Guid CommandIdentifier { get; set; }

        public Command() { CommandIdentifier = Guid.NewGuid(); }
    }
}
