using BNS.Application.Interface;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses;
using BNS.ViewModels.ValidationModels;
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
            [PasswordValidationAttribute]
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
                var userCompany = await _unitOfWork.JM_AccountCompanyRepository.GetDefaultAsync(s => s.CompanyId == data.CompanyId && s.Email == data.EmailJoin);
                if (userCompany == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                    return response;
                }
                if (userCompany.Status != EStatus.WAILTING_CONFIRM_MAIL)
                {
                    response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
                    return response;
                }
                var user = await _unitOfWork.JM_AccountRepository.GetDefaultAsync(s => s.Email == data.EmailJoin);

                if (user == null)
                {
                    response.errorCode = EErrorCode.NotExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                    return response;

                }
                user.PasswordHash=Ultility.MD5Encrypt(request.Password);
                userCompany.Status=EStatus.ACTIVE;
                userCompany.FullName=request.FullName;
                await _unitOfWork.JM_AccountRepository.UpdateAsync(user);
                await _unitOfWork.JM_AccountCompanyRepository.UpdateAsync(userCompany);
                await _unitOfWork.SaveChangesAsync();
                return response;
            }

        }
    }
}
