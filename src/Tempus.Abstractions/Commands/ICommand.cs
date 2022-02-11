using System;

namespace Tempus.Abstractions.Commands
{
    public interface ICommand
    {
        Guid AggregateIdentifier { get; set; }

        int? ExpectedVersion { get; set; }

        Guid IdentityTenant { get; set; }

        Guid IdentityUser { get; set; }

        Guid? ImpersonatedUser { get; set; }

        Guid CommandIdentifier { get; set; }
    }
}
