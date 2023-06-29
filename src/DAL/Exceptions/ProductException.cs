using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class ProductException : Exception
    {
        public ProductException(string msg) : base(msg)
        {

        }
        protected ProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
