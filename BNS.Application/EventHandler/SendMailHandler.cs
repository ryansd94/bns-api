using BNS.Service.Subcriber;
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Models;
using MediatR;
using Microsoft.Extensions.Localization;
using Nest;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace BNS.Service.EventHandler
{
    public class SendMailHandler : IRequestHandler<SendMailSubcriberMQ>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly MyConfiguration _config;
        public SendMailHandler(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IElasticClient elasticClient,
        IOptions<MyConfiguration> config,
         IUnitOfWork unitOfWork)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _unitOfWork = unitOfWork;
            _config = config.Value;
        }
        public async Task<Unit> Handle(SendMailSubcriberMQ request, CancellationToken cancellationToken)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = true;
                smtpClient.Host = _config.MailConfig.SmtpHost;
                smtpClient.Port = int.Parse(_config.MailConfig.SmtpPort);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(_config.MailConfig.SmtpUserName, _config.MailConfig.SmtpPassword);
                foreach (var email in request.Items)
                {
                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(_config.MailConfig.SmtpEmailAddress),
                        Subject = email.Subject,
                        Body = email.Body,
                        Priority = MailPriority.Normal,
                    };
                    msg.To.Add(email.Email);
                    await smtpClient.SendMailAsync(msg);
                }
            }
            return Unit.Value;
        }

    }
}
