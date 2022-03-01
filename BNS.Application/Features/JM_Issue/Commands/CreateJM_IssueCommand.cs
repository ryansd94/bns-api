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
    public class CreateJM_IssueCommand
    {
       
        public class CreateJM_IssueCommandHandler : IRequestHandler<CreateJM_IssueRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public CreateJM_IssueCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(CreateJM_IssueRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var project = await _context.JM_Projects.Where(s => s.Id == request.ProjectId).FirstOrDefaultAsync();
                if (project == null)
                {
                    response.errorCode = EErrorCode.IsExistsData.ToString();
                    response.title = string.Format(_sharedLocalizer[LocalizedBackendMessages.MSG_ObjectNotExists], _sharedLocalizer[LocalizedBackendMessages.Project.ProjectName]);
                    return response;
                }
                var data = new JM_Issue
                {
                    Id = Guid.NewGuid(),
                    Summary = request.Summary,
                    Description = request.Description,
                    ProjectId = request.ProjectId,
                    IssueType = request.IssueType,
                    AssignUserId = request.AssignUserId,
                    SprintId = request.SprintId,
                    OriginalTime = request.OriginalTime,
                    RemainingTime = request.RemainingTime,
                    StartDate = request.StartDate,
                    DueDate = request.DueDate,
                    IssueParentId = request.IssueParentId,
                    ReporterId = request.UserId,
                    IssueStatus = EIssueStatus.NEW,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = request.UserId,
                };
                await _context.JM_Issues.AddAsync(data);
                await _context.SaveChangesAsync();
                response.data = data.Id;
                return response;
            }

        }
    }
}
