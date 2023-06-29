using System;
using System.Runtime.Serialization;

namespace DAL.Exceptions
{

    [Serializable]
    public class CustomerException : Exception
    {
        public CustomerException(string msg) : base(msg)
        {

        }
        protected CustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
