using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class DeliveryMethodException : Exception
    {
        public DeliveryMethodException(string msg) : base(msg)
        {

        }
        protected DeliveryMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
