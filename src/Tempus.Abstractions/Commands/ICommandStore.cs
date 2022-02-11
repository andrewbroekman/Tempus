using System;
using System.Collections.Generic;
using Tempus.Abstractions.Utilities;

namespace Tempus.Abstractions.Commands
{
    public interface ICommandStore
    {
        /// <summary>
        /// Utility for serializing and deserializing commands.
        /// </summary>
        ISerializer Serializer { get; }

        /// <summary>
        /// Returns true if a command exists.
        /// </summary>
        bool Exists(Guid command);

        /// <summary>
        /// Gets the serialized version of specific command.
        /// </summary>
        ISerializedCommand Get(Guid command);

        /// <summary>
        /// Gets all unstarted commands that are scheduled to send now.
        /// </summary>
        IEnumerable<ISerializedCommand> GetExpired(DateTimeOffset at);

        /// <summary>
        /// Saves a serialized command.
        /// </summary>
        void Save(ISerializedCommand command, bool isNew);

        /// <summary>
        /// Returns the serialized version of a command.
        /// </summary>
        ISerializedCommand Serialize(ICommand command);
    }
}
