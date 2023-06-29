using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class CompanyException : Exception
    {
        public CompanyException(string msg) : base(msg)
        {

        }
        protected CompanyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
