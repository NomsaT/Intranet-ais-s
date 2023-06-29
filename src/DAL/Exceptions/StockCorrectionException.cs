using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class StockCorrectionException : Exception
    {
        public StockCorrectionException(string msg) : base(msg)
        {

        }
        protected StockCorrectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
