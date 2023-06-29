using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StorageTypeException : Exception
    {
        public StorageTypeException(string msg) : base(msg)
        {

        }
        protected StorageTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
