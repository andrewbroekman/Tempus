using System.Runtime.Serialization;

namespace Tempus.Abstractions.Exceptions
{
    [Serializable]
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string message) : base(message)
        {
        }

        protected CommandNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
