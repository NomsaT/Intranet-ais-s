using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class ProfitCenterException : Exception
    {
        public ProfitCenterException(string msg) : base(msg)
        {

        }
        protected ProfitCenterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
