using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StockManageException : Exception
    {
        public StockManageException(string msg) : base(msg)
        {

        }
        protected StockManageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
