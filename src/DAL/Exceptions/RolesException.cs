using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class RolesException : Exception
    {
        public RolesException(string msg) : base(msg)
        {

        }
        protected RolesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
