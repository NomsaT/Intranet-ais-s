using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class WarehouseException : Exception
    {
        public WarehouseException(string msg) : base(msg)
        {

        }
        protected WarehouseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
