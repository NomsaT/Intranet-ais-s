using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class DepartmentException : Exception
    {
        public DepartmentException(string msg) : base(msg)
        {

        }
        protected DepartmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
