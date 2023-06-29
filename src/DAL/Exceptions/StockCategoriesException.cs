using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StockCategoryException : Exception
    {
        public StockCategoryException(string msg) : base(msg)
        {

        }
        protected StockCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
