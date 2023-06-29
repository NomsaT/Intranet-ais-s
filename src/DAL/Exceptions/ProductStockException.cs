using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class ProductStockException : Exception
    {
        public ProductStockException(string msg) : base(msg)
        {

        }
        protected ProductStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
