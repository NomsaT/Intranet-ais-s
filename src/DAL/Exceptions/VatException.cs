using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class VatException : Exception
    {
        public VatException(string msg) : base(msg)
        {

        }
        protected VatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
