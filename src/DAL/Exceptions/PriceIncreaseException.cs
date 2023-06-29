using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class PriceIncreaseException : Exception
    {
        public PriceIncreaseException(string msg) : base(msg)
        {

        }
        protected PriceIncreaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
