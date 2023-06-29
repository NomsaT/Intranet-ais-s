using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace DAL.Classes
{
    public static class EmailLogic
    {
        public static bool sendEmailHtml(string recipient, List<string> cc, string title, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("mailer@s4.co.za", "mdrbqnbuckgpaouk");
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("mailer@s4.co.za");
                mailMessage.To.Add(recipient);


                foreach (string person in cc)
                {
                    mailMessage.CC.Add(person);
                }


                mailMessage.Subject = title;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);

                return true;

            }
            catch
            {
                return false;
            }






        }


    }
}
