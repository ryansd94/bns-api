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

namespace BNS.Application.Features
{
    public class CreateJM_ProjectCommand
    {
        public class CreateProjectRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public Guid TemplateId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public List<Guid> Teams { get; set; }
            public List<Guid> Members { get; set; }
        }
        public class CreateJM_ProjectCommandHandler : IRequestHandler<CreateProjectRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public CreateJM_ProjectCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _context.JM_Projects.Where(s => s.Name.Equals(request.Name)).FirstOrDefaultAsync();
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
                    CreatedUser = request.CreatedBy,
                    JM_TemplateId = request.TemplateId
                };
                if (request.Teams != null && request.Teams.Count > 0)
                {
                    foreach (var team in request.Teams)
                    {
                        await _context.JM_ProjectTeams.AddAsync(new JM_ProjectTeam
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = data.Id,
                            TeamId = team,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUser = request.CreatedBy,
                        });
                    }
                }
                if (request.Members != null && request.Members.Count > 0)
                {
                    foreach (var team in request.Members)
                    {
                        await _context.JM_ProjectMembers.AddAsync(new JM_ProjectMember
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = data.Id,
                            UserId = team,
                            CreatedDate = DateTime.UtcNow,
                            CreatedUser = request.CreatedBy,
                        });
                    }
                }
                await _context.JM_Projects.AddAsync(data);
                await _context.SaveChangesAsync();
                response.data = data.Id;
                return response;
            }

        }
    }
}
