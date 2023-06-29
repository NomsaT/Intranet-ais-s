using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StoresException : Exception
    {
        public StoresException(string msg) : base(msg)
        {

        }
        protected StoresException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
