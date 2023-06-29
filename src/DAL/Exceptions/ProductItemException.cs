using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class ProductItemException : Exception
    {
        public ProductItemException(string msg) : base(msg)
        {

        }
        protected ProductItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
