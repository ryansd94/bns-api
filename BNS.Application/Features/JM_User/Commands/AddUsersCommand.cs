using BNS.Data.Entities.JM_Entities;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Service.Features
{
    public class AddUsersCommand : IRequestHandler<CreateUsersRequest, ApiResult<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private IMediator _mediator;

        public AddUsersCommand(
         IStringLocalizer<SharedResource> sharedLocalizer,
         IUnitOfWork unitOfWork,
         IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mediator = mediator;
        }
        public async Task<ApiResult<object>> Handle(CreateUsersRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<object>();
            if (request.Users == null || request.Users.Count == 0)
            {
                return response;
            }
            var lstEmail = request.Users.Select(s => s.Email).ToList();
            var dataCheck = await _unitOfWork.Repository<JM_AccountCompany>().Where(s => s.CompanyId == request.CompanyId &&
            !s.IsDelete && lstEmail.Contains(s.Email)).ToListAsync();
            if (dataCheck != null && dataCheck.Count > 0)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                response.data = dataCheck.Select(s => s.Email).ToList();
                return response;
            }

            var requestSendMail = new SendMailAddUserRequest
            {
                CompanyId = request.CompanyId,
                Users = request.Users,
                UserId = request.UserId
            };
            await _mediator.Send(requestSendMail);
            return response;
        }

    }
}
