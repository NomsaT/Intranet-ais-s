using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class BinException : Exception
    {
        public BinException(string msg) : base(msg)
        {

        }
        protected BinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
