using BNS.Service.Subcriber;
using BNS.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.IO;
using System;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using BNS.Data;

namespace BNS.Service.EventHandler
{
    public class SendMailHandler : IRequestHandler<SendMailSubcriber>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly MyConfiguration _config;
        public SendMailHandler(
         IStringLocalizer<SharedResource> sharedLocalizer,
        IOptions<MyConfiguration> config)
        {
            _sharedLocalizer = sharedLocalizer;
            _config = config.Value;
        }

        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }

        private void sendMailWithGmailAPI(string emailTo, string subject, string body)
        {
            string applicationName = "Web client 1";
            string clientId = "362278678727-hgk7fo8agm3k50cskupbl28lblroaj45.apps.googleusercontent.com";
            string clientSecret = "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";
            string userEmail = "ryansd94@gmail.com"; // Địa chỉ email của bạn
            try
            {
                //Create Message
                MailMessage mail = new MailMessage();
                mail.Subject = "Subject!";
                mail.Body = "This is <b><i>body</i></b> of message";
                mail.From = new MailAddress(userEmail);
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(emailTo));
                MimeKit.MimeMessage mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mail);

                Message message = new Message();
                message.Raw = Base64UrlEncode(mimeMessage.ToString());
                string[] Scope = { GmailService.Scope.GmailSend };
                //Gmail API credentials
                UserCredential credential;
                using (var stream =
                    new FileStream("email_config.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart2.json");

                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scope,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Gmail API service.
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "test",
                });
                //Send Email
                var result = service.Users.Messages.Send(message, "me/OR UserId/EmailAddress").Execute();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<Unit> Handle(SendMailSubcriber request, CancellationToken cancellationToken)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = true;
                smtpClient.Host = _config.MailConfig.SmtpHost;
                smtpClient.Port = int.Parse(_config.MailConfig.SmtpPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_config.MailConfig.SmtpUserName, _config.MailConfig.SmtpPassword);
                foreach (var email in request.Items)
                {
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(_config.MailConfig.SmtpEmailAddress),
                        Subject = email.Subject,
                        Body = "abc",
                        Priority = MailPriority.Normal,
                    };
                    msg.To.Add(email.Email);
                    //await smtpClient.SendMailAsync(msg);
                    sendMailWithGmailAPI(email.Email, "", "");
                }
            }
            return Unit.Value;
        }

    }
}
