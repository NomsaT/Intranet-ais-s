using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class ProfitCenterStockException : Exception
    {
        public ProfitCenterStockException(string msg) : base(msg)
        {

        }
        protected ProfitCenterStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
