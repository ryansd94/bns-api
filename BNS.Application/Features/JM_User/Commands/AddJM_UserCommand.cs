using BNS.Application.Interface;
using BNS.Data.Entities.JM_Entities;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features
{
    public class AddJM_UserCommand : CommandBase<ApiResult<Guid>>
    {
        public class AddJM_UserCommandRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Token { get; set; }
            [Required]
            public string FullName { get; set; }
            [Required]
            public string Password { get; set; }
        }
        public class AddJM_UserCommandHandler : IRequestHandler<AddJM_UserCommandRequest, ApiResult<Guid>>
        {
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
            protected readonly IElasticClient _elasticClient;
            protected readonly MyConfiguration _config;
            private readonly ICipherService _cipherService;
            private readonly IUnitOfWork _unitOfWork;
            public AddJM_UserCommandHandler(
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
            public async Task<ApiResult<Guid>> Handle(AddJM_UserCommandRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var data = JsonConvert.DeserializeObject<JoinTeamResponse>(_cipherService.DecryptString(request.Token));
                if (data.Key != _config.Default.CipherKey)
                {
                    response.errorCode = EErrorCode.TokenNotValid.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_TokenNotValid];
                    return response;
                }
                var userCompanyCheck = await _unitOfWork.JM_AccountCompanyRepository.GetDefaultAsync(s => s.CompanyId == data.CompanyId && s.JM_Account.Email == data.EmailJoin, s => s.JM_Account);
                if (userCompanyCheck != null)
                {
                    response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
                    return response;
                }
                var user = await _unitOfWork.JM_AccountRepository.GetDefaultAsync(s => s.Email == data.EmailJoin);
                var userId = Guid.NewGuid();
                if (user == null)
                {
                    user = new JM_Account
                    {
                        Id = userId,
                        UserName = data.EmailJoin,
                        Email = data.EmailJoin,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUser = data.UserRequest,
                        IsDelete = false,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = false,
                        AccessFailedCount = 0,
                        IsMainAccount = true,
                        FullName = request.FullName,
                    };

                    await _unitOfWork.JM_AccountRepository.AddAsync(user);
                }
                else
                    userId = user.Id;
                var userCompany = new JM_AccountCompany
                {
                    Id = Guid.NewGuid(),
                    IsDelete = false,
                    UserId = userId,
                    CompanyId = data.CompanyId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = data.UserRequest,
                };
                await _unitOfWork.JM_AccountCompanyRepository.AddAsync(userCompany);
                return response;
            }

        }
    }
}
