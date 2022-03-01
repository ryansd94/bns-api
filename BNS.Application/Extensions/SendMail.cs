using BNS.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BNS.Service.Extensions
{
    public sealed class SendMail
    {
        public static async Task<bool> SendMailAsync(string toEmail, string subject, string body, MyConfiguration myConfig)
        {

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = true;
                smtpClient.Host = myConfig.MailConfig.SmtpHost;
                smtpClient.Port = int.Parse(myConfig.MailConfig.SmtpPort);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(myConfig.MailConfig.SmtpUserName, myConfig.MailConfig.SmtpPassword);
                var msg = new MailMessage
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    From = new MailAddress(myConfig.MailConfig.SmtpEmailAddress),
                    Subject = subject,
                    Body = body,
                    Priority = MailPriority.Normal,
                };
                msg.To.Add(toEmail);

                await smtpClient.SendMailAsync(msg);
                return true;
            }

        }
    }
}
