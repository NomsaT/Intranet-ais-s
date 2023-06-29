using System;

namespace WEBL
{
    public class ErrorMessage
    {
        public static string GetMessage(Exception e)
        {
            string message = e.Message;
            if (e.InnerException != null)
            {
                message = GetMessage(e.InnerException);
            }
            return message;
        }
    }
}
