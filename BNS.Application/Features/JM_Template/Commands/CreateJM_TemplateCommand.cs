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

namespace BNS.Application.Features.JM_Template.Commands
{
    public class CreateJM_TemplateCommand
    {
        public class CreateTemplateRequest : CommandBase<ApiResult<Guid>>
        {
            [Required]
            public string Name { get; set; }
            public string Description { get; set; }
            public string IssueType { get; set; }
            public string ReporterIssueStatus { get; set; }
            public string AssigneeIssueStatus { get; set; }
        }
        public class CreateJM_TemplateCommandHandler : IRequestHandler<CreateTemplateRequest, ApiResult<Guid>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public CreateJM_TemplateCommandHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<Guid>> Handle(CreateTemplateRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<Guid>();
                var dataCheck = await _context.JM_Templates.Where(s => s.Name.Equals(request.Name)).FirstOrDefaultAsync();
                if (dataCheck != null)
                {
                    response.errorCode = EErrorCode.IsExistsData.ToString();
                    response.title = _sharedLocalizer[LocalizedBackendMessages.MSG_ExistsData];
                    return response;
                }
                var data = new BNS.Data.Entities.JM_Entities.JM_Template
                {
                    Index = Guid.NewGuid(),
                    Name = request.Name,
                    Description = request.Description,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUser = request.CreatedBy,
                    IssueType = request.IssueType,
                    AssigneeIssueStatus = request.AssigneeIssueStatus,
                    ReporterIssueStatus = request.ReporterIssueStatus

                };

                await _context.JM_Templates.AddAsync(data);
                await _context.SaveChangesAsync();
                response.data = data.Index;
                return response;
            }

        }
    }
}
