using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class UnitOfMeasurementException : Exception
    {
        public UnitOfMeasurementException(string msg) : base(msg)
        {

        }
        protected UnitOfMeasurementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
