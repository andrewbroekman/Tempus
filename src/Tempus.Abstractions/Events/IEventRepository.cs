using System;
using Tempus.Abstractions.Aggregates;

namespace Tempus.Abstractions.Events
{
    public interface IEventRepository
    {
        bool Exists<T>(Guid id) where T : IAggregateRoot;

        T Get<T>(Guid id) where T : IAggregateRoot;

        IEvent[] Save<T>(T aggregate, int? version = null) where T : IAggregateRoot;
    }
}
