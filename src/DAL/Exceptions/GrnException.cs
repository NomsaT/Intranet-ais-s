using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class GrnException : Exception
    {
        public GrnException(string msg) : base(msg)
        {

        }
        protected GrnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
