﻿using System.Runtime.Serialization;

namespace Tempus.Abstractions.Exceptions
{
    [Serializable]
    public class UnorderedEventsException : Exception
    {
        public UnorderedEventsException(Guid aggregate)
            : base($"The events for this aggregate are not in the expected order ({aggregate}).")
        {
        }

        protected UnorderedEventsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
