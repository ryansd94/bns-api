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
    public class DeleteMemberJM_TeamCommand : IRequestHandler<DeleteMemberJM_TeamRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public DeleteMemberJM_TeamCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(DeleteMemberJM_TeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var team = await _unitOfWork.JM_TeamRepository.FirstOrDefaultAsync(s => s.Id == request.Id &&
            s.CompanyId == request.CompanyId, x => x.JM_TeamMembers);
            if (team == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
                return response;
            }

            var teamMembers = team.JM_TeamMembers;
            if (teamMembers != null || teamMembers.Count >0)
            {
                var teamMemberDelete = teamMembers.Where(s => request.Members.Contains(s.UserId) && !s.IsDelete).ToList();
                foreach (var item in teamMemberDelete)
                {
                    item.IsDelete=true;
                    item.UpdatedDate=DateTime.UtcNow;
                    item.UpdatedUser=request.UserId;
                    await _unitOfWork.JM_TeamMemberRepository.UpdateAsync(item);
                }
            }
            team.UpdatedDate = DateTime.UtcNow;
            team.UpdatedUser = request.UserId;

            await _unitOfWork.JM_TeamRepository.UpdateAsync(team);
            response =await _unitOfWork.SaveChangesAsync();
            response.data = team.Id;
            return response;
        }
    }
}
