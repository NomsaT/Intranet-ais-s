using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class ProjectException : Exception
    {
        public ProjectException(string msg) : base(msg)
        {

        }
        protected ProjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
