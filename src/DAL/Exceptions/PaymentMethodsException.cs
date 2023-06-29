using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class PaymentMethodsException : Exception
    {
        public PaymentMethodsException(string msg) : base(msg)
        {

        }
        protected PaymentMethodsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
