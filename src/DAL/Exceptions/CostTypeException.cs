using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class CostTypeException : Exception
    {
        public CostTypeException(string msg) : base(msg)
        {

        }
        protected CostTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
