using System;
using System.Runtime.Serialization;

namespace DAL
{
    [Serializable]
    public class PrintBarcodeException : Exception
    {
        public PrintBarcodeException(string msg) : base(msg)
        {

        }
        protected PrintBarcodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
