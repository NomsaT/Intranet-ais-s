using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class QuotationException : Exception
    {
        public QuotationException(string msg) : base(msg)
        {

        }
        protected QuotationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
