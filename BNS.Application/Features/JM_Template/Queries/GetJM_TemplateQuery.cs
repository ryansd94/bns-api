using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Category;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Application.Features 
{
    public class GetJM_TemplateQuery
    {
        public class GetJM_TemplateRequest : CommandRequest<ApiResult<JM_TemplateResponse>>
        {
        }
        public class GetJM_TemplateRequestHandler : IRequestHandler<GetJM_TemplateRequest, ApiResult<JM_TemplateResponse>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public GetJM_TemplateRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_TemplateResponse>> Handle(GetJM_TemplateRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_TemplateResponse>();
                response.data = new JM_TemplateResponse();
                var query = _context.JM_Templates.Where(s => !string.IsNullOrEmpty(s.Name) &&
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
                        IssueType = s.IssueType,
                        ReporterIssueStatus = s.ReporterIssueStatus,
                        AssigneeIssueStatus = s.AssigneeIssueStatus
                    });
                if (request.sortModel != null && request.sortModel.Count > 0)
                {
                    var columnSort = request.sortModel[0].field;
                    var sortType = request.sortModel[0].sort;
                    if (!string.IsNullOrEmpty(columnSort) && !request.isAdd && !request.isEdit)
                    {
                        columnSort = columnSort[0].ToString().ToUpper() + columnSort.Substring(1, columnSort.Length - 1);
                        query = Common.OrderBy(query, columnSort, sortType == ESortEnum.desc.ToString() ? false : true);

                    }
                }
                if (request.isAdd)
                    query = query.OrderByDescending(s => s.CreatedDate);
                if (request.isEdit)
                    query = query.OrderByDescending(s => s.UpdatedDate);

                response.recordsTotal = await query.CountAsync();
                query = query.Skip(request.start).Take(request.length);

                var rs = await query.ToListAsync();
                response.data.Items = rs;
                return response;
            }

        }
    }
}
