using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
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
using BNS.Domain.Commands;
using BNS.Domain;

namespace BNS.Service.Features
{
    public class CreateJM_ProjectCommand : IRequestHandler<CreateJM_ProjectRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateJM_ProjectCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateJM_ProjectRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.JM_ProjectRepository.FirstOrDefaultAsync(s => s.Name.Equals(request.Name));
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists];
                return response;
            }
            var data = new BNS.Data.Entities.JM_Entities.JM_Project
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                JM_TemplateId = request.TemplateId
            };
            if (request.Teams != null && request.Teams.Count > 0)
            {
                foreach (var team in request.Teams)
                {
                    await _unitOfWork.JM_ProjectTeamRepository.AddAsync(new JM_ProjectTeam
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = data.Id,
                        TeamId = team,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
            }
            if (request.Members != null && request.Members.Count > 0)
            {
                foreach (var team in request.Members)
                {
                    await _unitOfWork.JM_ProjectMemberRepository.AddAsync(new JM_ProjectMember
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = data.Id,
                        UserId = team,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
            }
            await _unitOfWork.JM_ProjectRepository.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            response.data = data.Id;
            return response;
        }

    }
}
