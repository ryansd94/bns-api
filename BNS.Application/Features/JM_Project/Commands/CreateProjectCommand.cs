using BNS.Data.Entities.JM_Entities;
using BNS.Resource.LocalizationResources;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;
using BNS.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BNS.Domain.Interface;

namespace BNS.Service.Features
{
    public class CreateProjectCommand : IRequestHandler<CreateProjectRequest, ApiResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public CreateProjectCommand(IUnitOfWork unitOfWork,
            IMapper mapper,
            IProjectService projectService)
        {
            _unitOfWork = unitOfWork;
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
                response.title = LocalizedBackendMessages.MSG_ObjectNotExists;
                return response;
            }
            var data = _mapper.Map<JM_Project>(request);
            data.Sprints = null;
            #region Add teams and members

            if (request.Teams != null && request.Teams.Count > 0)
            {
                foreach (var team in request.Teams)
                {
                    await _unitOfWork.Repository<JM_ProjectTeam>().AddAsync(new JM_ProjectTeam
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
                foreach (var accountCompanyId in request.Members)
                {
                    await _unitOfWork.Repository<JM_ProjectMember>().AddAsync(new JM_ProjectMember
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = data.Id,
                        AccountCompanyId = accountCompanyId,
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
                ProjectId = data.Id,
                AccountCompanyId = request.AccountCompanyId,
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
                        ProjectId = data.Id,
                        CreatedUserId = request.UserId,
                        Name = item.Name,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        CompanyId = request.CompanyId,
                        Active = item.Active
                    };
                    await _unitOfWork.Repository<JM_ProjectPhase>().AddAsync(phase);
                    var childs = _projectService.GetAllChilds(phase.Id, item, request.UserId, data.Id, request.CompanyId);
                    if (childs.Count > 0)
                    {
                        await _unitOfWork.Repository<JM_ProjectPhase>().AddRangeAsync(childs);
                    }
                }
            }

            #endregion

            await _unitOfWork.Repository<JM_Project>().AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            response.data = data.Id;
            return response;
        }
    }
}
