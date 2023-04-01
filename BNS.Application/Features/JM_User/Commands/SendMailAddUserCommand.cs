
using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Resource;
using BNS.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Service.Subcriber;

namespace BNS.Service.Features
{
    public class SendMailAddUserCommand : IRequestHandler<SendMailAddUserRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IBusPublisher _busPublisher;
        private IMediator _mediator;
        public SendMailAddUserCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
        ICipherService CipherService,
         IUnitOfWork unitOfWork,
         IMediator mediator
        //IBusPublisher busPublisher
            )
        {
            _sharedLocalizer = sharedLocalizer;
            _config = config.Value;
            _cipherService = CipherService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            //_busPublisher = busPublisher;
        }
        public async Task<ApiResult<Guid>> Handle(SendMailAddUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var subject = "JOIN TEAM";
            var rootBody = "";
            var filePath = Environment.CurrentDirectory + @"\EmailTemplate\" + "AddUser.html";

            using (StreamReader reader = File.OpenText(filePath))
            {
                rootBody = reader.ReadToEnd();
            }
            var emails = request.Emails.Distinct().ToList();
            var userActive = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => !s.IsDelete
              && s.CompanyId == request.CompanyId
              && emails.Contains(s.Account.Email), null, s => s.Account);


            emails = emails.Where(s => !userActive.Where(s => s.Status == EUserStatus.ACTIVE)
            .Select(s => s.Account.Email).Contains(s)).ToList();

            var accounts = await _unitOfWork.JM_AccountRepository.GetAsync(s => !s.IsDelete && emails.Contains(s.Email));
            var sendMailItems = new List<SendMailSubcriberMQItem>();
            foreach (var email in emails)
            {
                var account = accounts.Where(s => s.Email.Equals(email)).FirstOrDefault();
                if (account == null)
                {
                    account = new JM_Account
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        UserName = email,
                        IsDelete = false,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUser = request.UserId,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                    };
                    await _unitOfWork.JM_AccountRepository.AddAsync(account);
                }
                var accountCompany = await userActive.Where(s => s.Account.Email.Equals(email)).FirstOrDefaultAsync();
                if (accountCompany == null)
                {
                    accountCompany = new JM_AccountCompany
                    {
                        Id = Guid.NewGuid(),
                        IsDelete = false,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUser = request.UserId,
                        UserId = account.Id,
                        CompanyId = request.CompanyId,
                        Status = EUserStatus.WAILTING_CONFIRM_MAIL,
                        Email = email,
                        IsMainAccount = false,
                        IsDefault = true,
                        EmailTimestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds()
                    };
                    await _unitOfWork.JM_AccountCompanyRepository.AddAsync(accountCompany);
                }
                else
                {
                    var currentTimestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
                    var aaaa = currentTimestamp - accountCompany.EmailTimestamp;
                    if (aaaa < 60)
                    {
                        continue;
                    }
                    accountCompany.EmailTimestamp = currentTimestamp;
                    await _unitOfWork.JM_AccountCompanyRepository.UpdateAsync(accountCompany);
                }
                var joinTeam = new JoinTeamResponse
                {
                    CompanyId = request.CompanyId,
                    EmailJoin = email,
                    Key = _config.Default.CipherKey,
                    UserRequest = request.UserId,
                    Id = accountCompany.Id
                };
                var token = _cipherService.EncryptString(JsonConvert.SerializeObject(joinTeam));
                var body = string.Format(rootBody, $"{_config.Default.WebUserDomain}/signup/jointeam?token={token}");
                sendMailItems.Add(new SendMailSubcriberMQItem
                {
                    Body = body,
                    Email = email,
                    Subject = subject
                });
            }
            await _unitOfWork.SaveChangesAsync();
            var sendMailRequest = new SendMailSubcriberMQ
            {
                Items = sendMailItems
            };
            await _mediator.Send(sendMailRequest);
            return response;
        }

    }

}
