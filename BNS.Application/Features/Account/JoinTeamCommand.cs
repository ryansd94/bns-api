using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Utilities;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain.Responses;
using BNS.Data;
using BNS.Domain.Commands.Account;

namespace BNS.Service.Features
{
    public class JoinTeamCommand : IRequestHandler<JoinTeamRequest, ApiResult<LoginResponse>>
    {
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        //protected readonly IElasticClient _elasticClient;
        protected readonly MyConfiguration _config;
        private readonly ICipherService _cipherService;
        private readonly IUnitOfWork _unitOfWork;
        private IMediator _mediator;
        public JoinTeamCommand(
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
        public async Task<ApiResult<LoginResponse>> Handle(JoinTeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<LoginResponse>();
            var userCompany = await _unitOfWork.JM_AccountCompanyRepository.FirstOrDefaultAsync(s => s.Id == request.AccountCompanyId
             && !s.IsDelete);

            if (userCompany == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;
            }
            //if (userCompany.Status != EUserStatus.WAILTING_CONFIRM_MAIL)
            //{
            //    response.errorCode = EErrorCode.UserHasJoinTeam.ToString();
            //    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_ExistsUser];
            //    return response;
            //}
            var user = await _unitOfWork.JM_AccountRepository.FirstOrDefaultAsync(s => s.Id == userCompany.UserId);

            if (user == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_NotExistsUser];
                return response;

            }
            if (!request.IsHasAccount)
            {
                user.PasswordHash = Ultility.MD5Encrypt(request.Password);
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
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
