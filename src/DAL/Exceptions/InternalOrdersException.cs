using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class InternalOrdersException : Exception
    {
        public InternalOrdersException(string msg) : base(msg)
        {

        }
        protected InternalOrdersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
