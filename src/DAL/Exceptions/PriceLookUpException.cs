using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class PriceLookUpException : Exception
    {
        public PriceLookUpException(string msg) : base(msg)
        {

        }
        protected PriceLookUpException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
