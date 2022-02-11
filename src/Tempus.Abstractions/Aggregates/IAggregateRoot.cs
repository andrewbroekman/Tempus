using System;
using System.Collections.Generic;
using Tempus.Abstractions.Events;

namespace Tempus.Abstractions.Aggregates
{
    public interface IAggregateRoot
    {
        IAggregateState State { get; set; }

        Guid AggregateIdentifier { get; set; }

        int AggregateVersion { get; set; }

        IEvent[] GetUncommittedChanges();

        IEvent[] FlushUncommittedChanges();

        void Rehydrate(IEnumerable<IEvent> history);
    }
}
