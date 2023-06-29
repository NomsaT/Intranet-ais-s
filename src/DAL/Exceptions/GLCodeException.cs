using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class GLCodeException : Exception
    {
        public GLCodeException(string msg) : base(msg)
        {

        }
        protected GLCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
