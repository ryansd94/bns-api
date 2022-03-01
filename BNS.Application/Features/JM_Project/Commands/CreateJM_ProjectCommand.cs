using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Resource.LocalizationResources;
using BNS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;
using BNS.Domain.Commands;

namespace BNS.Service.Features
{
    public class CreateJM_ProjectCommand : IRequestHandler<CreateJM_ProjectRequest, ApiResult<Guid>>
    {
        protected readonly BNSDbContext _context;
        protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CreateJM_ProjectCommand(BNSDbContext context,
         IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<ApiResult<Guid>> Handle(CreateJM_ProjectRequest request, CancellationToken cancellationToken)
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
                CreatedUser = request.UserId,
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
                        CreatedUser = request.UserId,
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
                        CreatedUser = request.UserId,
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
