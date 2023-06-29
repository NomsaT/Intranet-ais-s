using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class SupplierTypesException : Exception
    {
        public SupplierTypesException(string msg) : base(msg)
        {

        }
        protected SupplierTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
