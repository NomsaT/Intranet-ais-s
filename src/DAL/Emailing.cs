using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace DAL
{
    public static class Emailing
    {
        #region config
        #region config SmtpClient
        private static string _smtpClient;
        public static string SmtpClient
        {
            get
            {
                if (string.IsNullOrEmpty(_smtpClient))
                {
                    SetSmtpClient();
                }
                return _smtpClient;
            }
            set
            {
                _smtpClient = value;
            }
        }

        private static void SetSmtpClient()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _smtpClient = configuration["EmailSettings:SmtpClient"];
        }

        #endregion

        #region config Port
        private static int? _port;
        public static int? Port
        {
            get
            {
                if (_port == null)
                {
                    SetPort();
                }
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        private static void SetPort()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _port = int.Parse(configuration["EmailSettings:Port"]);
        }

        #endregion

        #region config Email
        private static string _email;
        public static string Email
        {
            get
            {
                if (string.IsNullOrEmpty(_email))
                {
                    SetEmail();
                }
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        private static void SetEmail()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _email = configuration["EmailSettings:Email"];
        }

        #endregion

        #region config Password
        private static string _password;
        public static string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                {
                    SetPassword();
                }
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private static void SetPassword()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _password = configuration["EmailSettings:Password"];
        }

        #endregion

        #region config SSL
        private static bool? _ssl;
        public static bool? SSL
        {
            get
            {
                if (_ssl == null)
                {
                    SetSSL();
                }
                return _ssl;
            }
            set
            {
                _ssl = value;
            }
        }

        private static void SetSSL()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _ssl = bool.Parse(configuration["EmailSettings:Ssl"]);
        }

        #endregion

        #region config From
        private static string _from;
        public static string From
        {
            get
            {
                if (string.IsNullOrEmpty(_from))
                {
                    SetFrom();
                }
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        private static void SetFrom()
        {
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory()) // Directory where the json files are located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _from = configuration["EmailSettings:From"];
        }

        #endregion        

        public static bool sendEmailHtmlAttachment(string recipient, List<string> cc, string title, string body, System.IO.Stream attachment = null, string fileName = null)
        {
            try
            {
                SmtpClient client = new SmtpClient(SmtpClient, (int)Port); //smtp and port
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Email, Password); //email and password
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.EnableSsl = (bool)SSL; //ssl

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(From); //from
                mailMessage.To.Add(recipient);


                foreach (string person in cc)
                {
                    mailMessage.CC.Add(person);
                }


                mailMessage.Subject = title;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                if (attachment != null)
                {
                    mailMessage.Attachments.Add(new Attachment(attachment, fileName));
                }

                client.Send(mailMessage);

                if (mailMessage.Attachments.Count > 0)
                {
                    mailMessage.Attachments.Dispose();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool sendEmailHtml(string recipient, List<string> cc, string title, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient(SmtpClient, (int)Port); //smtp and port
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Email, Password); //email and password
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.EnableSsl = (bool)SSL; //ssl

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(From); //from
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
        #endregion
    }
}
