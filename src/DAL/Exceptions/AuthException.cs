using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class AuthException : Exception
    {
        public AuthException(string msg) : base(msg)
        {

        }
        protected AuthException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
