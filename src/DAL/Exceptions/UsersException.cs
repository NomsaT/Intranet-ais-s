using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class UsersException : Exception
    {
        public UsersException(string msg) : base(msg)
        {

        }
        protected UsersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
