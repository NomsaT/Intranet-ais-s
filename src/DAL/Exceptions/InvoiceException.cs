using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class InvoiceException : Exception
    {
        public InvoiceException(string msg) : base(msg)
        {

        }
        protected InvoiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
