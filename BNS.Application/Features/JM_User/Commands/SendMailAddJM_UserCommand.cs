using BNS.Application.Extensions;
using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;


namespace BNS.Application.Features
{
    public class SendMailAddJM_UserCommand : CommandBase<ApiResult<Guid>>
    {
        public class SendMailAddJM_UserCommandRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public List<string> Emails { get; set; }
        }
        public class SendMailAddJM_UserCommandHandler : IRequestHandler<SendMailAddJM_UserCommandRequest, ApiResult<Guid>>
        {
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            protected readonly IElasticClient _elasticClient;
            protected readonly MyConfiguration _config;
            private readonly ICipherService _cipherService;
            private readonly IUnitOfWork _unitOfWork;
            public SendMailAddJM_UserCommandHandler(
             IStringLocalizer<SharedResource> sharedLocalizer,
             IOptions<MyConfiguration> config,
            ICipherService CipherService,
             IElasticClient elasticClient,
             IUnitOfWork unitOfWork)
            {
                _sharedLocalizer = sharedLocalizer;
                _elasticClient = elasticClient;
                _config = config.Value;
                _cipherService = CipherService;
                _unitOfWork = unitOfWork;
            }
            public async Task<ApiResult<Guid>> Handle(SendMailAddJM_UserCommandRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var subject = "JOIN TEAM";
                var rootBody = "";
                var filePath = Path.Combine(Environment.CurrentDirectory, @"../EmailTemplate", "AddUser.html"); ;

                using (StreamReader reader = File.OpenText(filePath))
                {
                    rootBody = reader.ReadToEnd();
                }
                var emails = request.Emails.Distinct();
                var queryUser = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => !s.IsDelete
                && s.Status == EStatus.ACTIVE
                && s.CompanyId == request.CompanyId, null, s => s.JM_Account);


                var users = await queryUser.Select(s => s.JM_Account.Email).ToListAsync();
                emails = emails.Where(s => !users.Contains(s)).ToList();


                foreach (var email in emails)
                {
                    var joinTeam = new JoinTeamResponse
                    {
                        CompanyId = request.CompanyId,
                        EmailJoin = email,
                        Key = _config.Default.CipherKey,
                        UserRequest = request.CreatedBy
                    };
                    var token = _cipherService.EncryptString(JsonConvert.SerializeObject(joinTeam));
                    var body = string.Format(rootBody, $"{_config.Default.WebUserDomain}/signup/jointeam?={token}");
                    await SendMail.SendMailAsync(email, subject, body, _config);
                }
                return response;
            }

        }
    }
}
