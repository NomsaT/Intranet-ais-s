using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class EmployeePositionException : Exception
    {
        public EmployeePositionException(string msg) : base(msg)
        {

        }
        protected EmployeePositionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
