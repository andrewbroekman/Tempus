using System.Runtime.Serialization;

namespace Tempus.Abstractions.Exceptions
{
    [Serializable]
    public class UnhandledEventException : Exception
    {
        public UnhandledEventException(string name)
            : base($"You must register at least one handler for this event ({name}).")
        {
        }

        protected UnhandledEventException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
