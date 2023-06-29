using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StockGroupException : Exception
    {
        public StockGroupException(string msg) : base(msg)
        {

        }
        protected StockGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
