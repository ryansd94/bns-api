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
    public class DeleteJM_TeamCommand : IRequestHandler<DeleteJM_TeamRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeleteJM_TeamCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(DeleteJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataChecks = await _unitOfWork.JM_TeamRepository.GetAsync(s => request.ids.Contains(s.Id) &&
            s.CompanyId == request.CompanyId);
            if (dataChecks == null || dataChecks.Count() ==0)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }
            foreach (var item in dataChecks)
            {
                item.IsDelete = true;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedUser = request.UserId;
                await _unitOfWork.JM_TeamRepository.UpdateAsync(item);
            }

            var teamMembers = await _unitOfWork.JM_TeamMemberRepository.GetAsync(s => request.ids.Contains(s.TeamId));
            foreach (var item in teamMembers)
            {
                item.IsDelete = true;
                item.UpdatedDate = DateTime.UtcNow;
                item.UpdatedUser = request.UserId;
                await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
            }
            response =await _unitOfWork.SaveChangesAsync();
            return response;
        }

    }
}
