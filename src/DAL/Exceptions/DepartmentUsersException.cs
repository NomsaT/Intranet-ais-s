using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class DepartmentUsersException : Exception
    {
        public DepartmentUsersException(string msg) : base(msg)
        {

        }
        protected DepartmentUsersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
