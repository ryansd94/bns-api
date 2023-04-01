using BNS.Data.Entities.JM_Entities;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain;

namespace BNS.Service.Features
{
    public class CreateProjectCommand : IRequestHandler<CreateProjectRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateProjectCommand(IUnitOfWork unitOfWork,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.JM_ProjectRepository.FirstOrDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyId == request.CompanyId);
            if (dataCheck != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists];
                return response;
            }
            var project = new JM_Project
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                CompanyId = request.CompanyId
            };
            if (request.Teams != null && request.Teams.Count > 0)
            {
                foreach (var team in request.Teams)
                {
                    await _unitOfWork.JM_ProjectTeamRepository.AddAsync(new JM_ProjectTeam
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = project.Id,
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
                        ProjectId = project.Id,
                        UserId = team,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
            }

            #region Add user created project

            await _unitOfWork.JM_ProjectMemberRepository.AddAsync(new JM_ProjectMember
            {
                Id = Guid.NewGuid(),
                ProjectId = project.Id,
                UserId = request.UserId,
                IsCreated = true,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
            });

            #endregion
            await _unitOfWork.JM_ProjectRepository.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
            response.data = project.Id;
            return response;
        }

    }
}
