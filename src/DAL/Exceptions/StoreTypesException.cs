using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StoreTypesException : Exception
    {
        public StoreTypesException(string msg) : base(msg)
        {

        }
        protected StoreTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
