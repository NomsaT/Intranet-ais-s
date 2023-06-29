using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class BankNameException : Exception
    {
        public BankNameException(string msg) : base(msg)
        {

        }
        protected BankNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
