using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class PlantLocationException : Exception
    {
        public PlantLocationException(string msg) : base(msg)
        {

        }
        protected PlantLocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
