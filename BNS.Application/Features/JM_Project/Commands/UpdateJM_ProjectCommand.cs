using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands.JM_Project;

namespace BNS.Service.Features
{
    public class UpdateJM_ProjectCommand : IRequestHandler<UpdateJM_ProjectRequest, ApiResult<Guid>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UpdateJM_ProjectCommand(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(UpdateJM_ProjectRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResult<Guid>();
            var dataCheck = await _context.JM_Projects.Where(s => s.Id == request.Id).FirstOrDefaultAsync();
            if (dataCheck == null)
            {
                response.errorCode = EErrorCode.NotExistsData.ToString();
                response.title = string.Format(_sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists], _sharedLocalizer[LocalizedBackendMessages.Project.ProjectName]);
                return response;
            }

            var checkDuplicate = await _context.JM_Projects.Where(s => s.Name.Equals(request.Name)
            && s.Id != request.Id).FirstOrDefaultAsync();
            if (checkDuplicate != null)
            {
                response.errorCode = EErrorCode.IsExistsData.ToString();
                response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                return response;
            }
            dataCheck.Code = request.Code;
            dataCheck.Name = request.Name;
            dataCheck.StartDate = request.StartDate;
            dataCheck.EndDate = request.EndDate;
            dataCheck.Description = request.Description;
            dataCheck.UpdatedDate = DateTime.UtcNow;
            dataCheck.UpdatedUser = request.UserId;

            if (request.Teams != null && request.Teams.Count > 0)
            {
                var projectTeams = await _context.JM_ProjectTeams.Where(s => s.ProjectId == request.Id && !s.IsDelete).ToListAsync();
                var projectTeamDelete = projectTeams.Where(s => !request.Teams.Contains(s.Id)).ToList();
                var projectTeamAdd = request.Teams.Where(s => !projectTeams.Select(s => s.Id).ToList().Contains(s)).ToList();

                foreach (var team in projectTeamDelete)
                {
                    team.IsDelete = true;
                    team.UpdatedDate = DateTime.UtcNow;
                    team.UpdatedUser = request.UserId;
                }
                _context.JM_ProjectTeams.UpdateRange(projectTeamDelete);
                foreach (var team in projectTeamAdd)
                {
                    await _context.JM_ProjectTeams.AddAsync(new JM_ProjectTeam
                    {
                        Id = Guid.NewGuid(),
                        ProjectId = request.Id,
                        TeamId = team,
                        CreatedDate = DateTime.UtcNow,
                        CreatedUser = request.UserId,
                    });
                }
            }

            _context.JM_Projects.Update(dataCheck);
            await _context.SaveChangesAsync();
            response.data = dataCheck.Id;
            return response;
        }

    }

}
