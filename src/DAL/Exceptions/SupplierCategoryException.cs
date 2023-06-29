using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class SupplierCategoryException : Exception
    {
        public SupplierCategoryException(string msg) : base(msg)
        {

        }
        protected SupplierCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
