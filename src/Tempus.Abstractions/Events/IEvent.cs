namespace Tempus.Abstractions.Events
{
    public interface IEvent
    {
        Guid AggregateIdentifier { get; set; }

        int AggregateVersion { get; set; }

        Guid IdentityTenant { get; set; }

        Guid IdentityUser { get; set; }

        Guid? ImpersonatedUser { get; set; }

        DateTimeOffset EventTime { get; set; }

        Guid EventIdentifier { get; set; }
    }
}
