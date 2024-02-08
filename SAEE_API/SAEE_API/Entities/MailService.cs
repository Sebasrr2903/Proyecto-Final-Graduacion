using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace SAEE_API.Entities
{
    public class MailService
    {
        //To send automatic notifications via email
        string emailAccount = ConfigurationManager.AppSettings["emailAccount"];
        string emailPassword = ConfigurationManager.AppSettings["emailPassword"];
        string emailServer = ConfigurationManager.AppSettings["emailServer"];
        public void SendEmail(string destination, string subject, string content)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(emailAccount);
            message.To.Add(new MailAddress(destination));
            message.Subject = subject;
            message.Body = content;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = emailServer;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(emailAccount, emailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }
    }
}