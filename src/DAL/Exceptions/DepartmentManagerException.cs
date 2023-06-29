using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class DepartmentManagerException : Exception
    {
        public DepartmentManagerException(string msg) : base(msg)
        {

        }

        protected DepartmentManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
