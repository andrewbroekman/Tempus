using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tempus.Abstractions.Exceptions
{
    [Serializable]
    public class StoreInsertException : Exception
    {
        public StoreInsertException(string name)
            : base($"You must register at least one handler for this event ({name}).")
        {
        }

        protected StoreInsertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
