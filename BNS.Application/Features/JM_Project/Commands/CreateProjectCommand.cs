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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using BNS.Domain.Interface;

namespace BNS.Service.Features
{
    public class CreateProjectCommand : IRequestHandler<CreateProjectRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        protected readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public CreateProjectCommand(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IMapper mapper,
            IProjectService projectService)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _mapper = mapper;
            _projectService = projectService;
        }
        public async Task<ApiResult<Guid>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _unitOfWork.Repository<JM_Project>().FirstOrDefaultAsync(s => s.Name.Equals(request.Name) && s.CompanyId == request.CompanyId);
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
                Type = request.Type,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
                CompanyId = request.CompanyId
            };

            #region Add teams and members

            if (request.Teams != null && request.Teams.Count > 0)
            {
                foreach (var team in request.Teams)
                {
                    await _unitOfWork.Repository<JM_ProjectTeam>().AddAsync(new JM_ProjectTeam
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
                    await _unitOfWork.Repository<JM_ProjectMember>().AddAsync(new JM_ProjectMember
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = project.Id,
                        UserId = team,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUserId = request.UserId,
                    });
                }
            }

            #endregion

            #region Add user created project

            await _unitOfWork.Repository<JM_ProjectMember>().AddAsync(new JM_ProjectMember
            {
                Id = Guid.NewGuid(),
                ProjectId = project.Id,
                UserId = request.UserId,
                IsCreated = true,
                CreatedDate = DateTime.UtcNow,
                CreatedUserId = request.UserId,
            });

            #endregion

            #region Add Sprints

            if (request.Sprints != null && request.Sprints.Count > 0)
            {
                foreach (var item in request.Sprints)
                {
                    var phase = new JM_ProjectPhase
                    {
                        ProjectId = project.Id,
                        CreatedUserId = request.UserId,
                        Name = item.Name,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        CompanyId = request.CompanyId
                    };
                    await _unitOfWork.Repository<JM_ProjectPhase>().AddAsync(phase);
                    var childs = _projectService.GetAllChilds(phase.Id, item, request.UserId, project.Id, request.CompanyId);
                    if (childs.Count > 0)
                    {
                        await _unitOfWork.Repository<JM_ProjectPhase>().AddRangeAsync(childs);
                    }
                }
            }

            #endregion

            await _unitOfWork.Repository<JM_Project>().AddAsync(project);
            await _unitOfWork.SaveChangesAsync();
            response.data = project.Id;
            return response;
        }
    }
}
