﻿
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
using BNS.Domain.Messaging;
using BNS.Service.Subcriber;

namespace BNS.Service.Features
{
    public class SendMailAddJM_UserCommand  : IRequestHandler<SendMailAddJM_UserRequest, ApiResult<Guid>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IElasticClient _elasticClient;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusPublisher _busPublisher;
        public SendMailAddJM_UserCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
        ICipherService CipherService,
         IElasticClient elasticClient,
         IUnitOfWork unitOfWork,
        IBusPublisher busPublisher)
        {
            _sharedLocalizer = sharedLocalizer;
            _elasticClient = elasticClient;
            _config = config.Value;
            _cipherService = CipherService;
            _unitOfWork = unitOfWork;
            _busPublisher = busPublisher;
        }
        public async Task<ApiResult<Guid>> Handle(SendMailAddJM_UserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var subject = "JOIN TEAM";
            var rootBody = "";
            var filePath = Path.Combine(Environment.CurrentDirectory, @"../EmailTemplate", "AddUser.html"); ;

            using (StreamReader reader = File.OpenText(filePath))
            {
                rootBody = reader.ReadToEnd();
            }
            var emails = request.Emails.Distinct().ToList();
            var userActive = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => !s.IsDelete
              && s.CompanyId == request.CompanyId
              && emails.Contains(s.JM_Account.Email), null, s => s.JM_Account);


            emails = emails.Where(s => !userActive.Where(s => s.Status == EStatus.ACTIVE).Select(s => s.JM_Account.Email).Contains(s)).ToList();

            var accounts = await _unitOfWork.JM_AccountRepository.GetAsync(s => !s.IsDelete && emails.Contains(s.Email));
            var sendMailItems = new List<SendMailSubcriberMQItem>();
            foreach (var email in emails)
            {
                var joinTeam = new JoinTeamResponse
                {
                    CompanyId = request.CompanyId,
                    EmailJoin = email,
                    Key = _config.Default.CipherKey,
                    UserRequest = request.UserId
                };
                var token = _cipherService.EncryptString(JsonConvert.SerializeObject(joinTeam));
                var body = string.Format(rootBody, $"{_config.Default.WebUserDomain}/signup/jointeam?={token}");
                var account = accounts.Where(s => s.Email.Equals(email)).FirstOrDefault();
                if (account==null)
                {
                    account = new JM_Account
                    {
                        Id=Guid.NewGuid(),
                        Email=email,
                        UserName=email,
                        IsDelete=false,
                        CreatedDate=DateTime.UtcNow,
                        CreatedUser=request.UserId,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                    };
                    await _unitOfWork.JM_AccountRepository.AddAsync(account);
                }
                var currentAccount = await userActive.Where(s => s.JM_Account.Email.Equals(email)).FirstOrDefaultAsync();
                if (currentAccount == null)
                {
                    await _unitOfWork.JM_AccountCompanyRepository.AddAsync(new JM_AccountCompany
                    {
                        IsDelete=false,
                        CreatedDate=DateTime.UtcNow,
                        CreatedUser=request.UserId,
                        UserId=account.Id,
                        CompanyId=request.CompanyId,
                        Status=EStatus.WAILTING_CONFIRM_MAIL,
                        Email=email,
                        IsMainAccount=false,
                        IsDefault=true
                    });
                }
                sendMailItems.Add(new SendMailSubcriberMQItem
                {
                    Body=body,
                    Email=email,
                    Subject=subject
                });
            }
            await _unitOfWork.SaveChangesAsync();
            await _busPublisher.PublishAsync(new SendMailSubcriberMQ
            {
                Items=sendMailItems
            });
            return response;
        }

    }

}
