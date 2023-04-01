using BNS.Domain;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class DeleteUserCommand : IRequestHandler<DeleteUserRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeleteUserCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataChecks = await _unitOfWork.JM_AccountCompanyRepository.GetAsync(s => request.Ids.Contains(s.Id) &&
            s.CompanyId == request.CompanyId && !s.IsDelete);
            if (dataChecks == null || dataChecks.Count() == 0)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            if(dataChecks.Count() ==0)
            {
                if(dataChecks.ToList()[0].IsMainAccount)
                {
                    response.errorCode = EErrorCode.Failed.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.User.MSG_CannotActionMainUser];
                    return response;
                }
            }
            foreach (var item in dataChecks)
            {
                item.IsDelete = true;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedUser = request.UserId;
                await _unitOfWork.JM_AccountCompanyRepository.UpdateAsync(item);
            }
            response = await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
