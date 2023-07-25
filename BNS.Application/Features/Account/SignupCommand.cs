using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Utilities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain.Responses;

namespace BNS.Service.Features
{
    public class SignupCommand : IRequestHandler<SignupRequest, ApiResult<LoginResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        //protected readonly IElasticClient _elasticClient;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;
        private readonly IUnitOfWork _unitOfWork;
        private IMediator _mediator;
        public SignupCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IOptions<MyConfiguration> config,
        ICipherService CipherService,
         IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            _sharedLocalizer = sharedLocalizer;
            _config = config.Value;
            _cipherService = CipherService;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<ApiResult<LoginResponse>> Handle(SignupRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<LoginResponse>();
            var data = JsonConvert.DeserializeObject<JoinTeamResponse>(await _cipherService.DecryptString(request.Token));
            if (data.Key != _config.Default.CipherKey)
            {
                response.errorCode = EErrorCode.TokenNotValid.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_TokenNotValid];
                return response;
            }
            var userCompany = await _unitOfWork.JM_AccountCompanyRepository.FirstOrDefaultAsync(s => s.CompanyId == data.CompanyId
            && s.Id == data.Id
            && !s.IsDelete);
            if (userCompany == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;
            }
            if (userCompany.Status != EUserStatus.WAILTING_CONFIRM_MAIL)
            {
                response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
                return response;
            }
            var user = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.Email == data.EmailJoin);

            if (user == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;

            }
            if (!request.IsHasAccount)
            {
                user.PasswordHash = Ultility.MD5Encrypt(request.Password);
                user.FullName = request.FullName;
                user.IsActive = true;
                user.Image = request.Image;
                await _unitOfWork.JM_AccountRepository.UpdateAsync(user);
            }
            userCompany.Status = EUserStatus.ACTIVE;
            await _unitOfWork.JM_AccountCompanyRepository.UpdateAsync(userCompany);
            await _unitOfWork.SaveChangesAsync();
            var loginRequest = new LoginNoPasswordRequest
            {
                Username = user.UserName
            };
            var rs = await _mediator.Send(loginRequest);
            return rs;
        }
    }
}
