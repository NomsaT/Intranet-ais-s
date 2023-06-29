using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class SupplierException : Exception
    {
        public SupplierException(string msg) : base(msg)
        {

        }
        protected SupplierException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
