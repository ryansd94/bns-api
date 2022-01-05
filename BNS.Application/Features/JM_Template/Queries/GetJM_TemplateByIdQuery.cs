
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Category;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BNS.Application.Features
{
    public class GetJM_TemplateByIdQuery
    {
        public class GetJM_TemplateByIdRequest : CommandByIdRequest<ApiResult<JM_TemplateResponseItem>>
        {
        }
        public class GetJM_TemplateByIdRequestHandler : IRequestHandler<GetJM_TemplateByIdRequest, ApiResult<JM_TemplateResponseItem>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public GetJM_TemplateByIdRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_TemplateResponseItem>> Handle(GetJM_TemplateByIdRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TemplateResponseItem>();
                var query = _context.JM_Templates.Where(s => s.Id == request.Id &&
                !s.IsDelete)
                    .Select(s => new JM_TemplateResponseItem
                    {
                        Name = s.Name,
                        Description = s.Description,
                        Id = s.Id,
                        UpdatedDate = s.UpdatedDate,
                        CreatedDate = s.CreatedDate,
                        CreatedUserId = s.CreatedUser,
                        UpdatedUserId = s.UpdatedUser,
                        AssigneeIssueStatus = s.AssigneeIssueStatus,
                        ReporterIssueStatus = s.ReporterIssueStatus,
                        IssueType = s.IssueType
                    });
                var rs = await query.FirstOrDefaultAsync();
                response.data = rs;
                return response;
            }

        }
    }
}
