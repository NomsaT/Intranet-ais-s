using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StockException : Exception
    {
        public StockException(string msg) : base(msg)
        {

        }
        protected StockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
