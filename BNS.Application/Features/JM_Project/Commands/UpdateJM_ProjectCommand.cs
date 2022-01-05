using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
namespace BNS.Application.Features.JM_Project.Commands
{
    public class UpdateJM_ProjectCommand
    {
        public class UpdateJM_ProjectRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public List<Guid> Teams { get; set; }
            public List<Guid> Members { get; set; }
        }
        public class UpdateJM_ProjectCommandHandler : IRequestHandler<UpdateJM_ProjectRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public UpdateJM_ProjectCommandHandler(BNSDbContext context,
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
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_NotExistsData];
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
                dataCheck.UpdatedUser = request.CreatedBy;

                if (request.Teams != null && request.Teams.Count > 0)
                {
                    var projectTeams = await _context.JM_ProjectTeams.Where(s => s.ProjectId == request.Id && !s.IsDelete).ToListAsync();
                    var projectTeamDelete = projectTeams.Where(s => !request.Teams.Contains(s.Id)).ToList();
                    var projectTeamAdd = request.Teams.Where(s => !projectTeams.Select(s => s.Id).ToList().Contains(s)).ToList();

                    foreach (var team in projectTeamDelete)
                    {
                        team.IsDelete = true;
                        team.UpdatedDate = DateTime.UtcNow;
                        team.UpdatedUser = request.CreatedBy;
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
                            CreatedUser = request.CreatedBy,
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
}
