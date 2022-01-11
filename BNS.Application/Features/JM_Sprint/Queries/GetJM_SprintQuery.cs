
using BNS.Data.EntityContext;
using BNS.Resource;
using BNS.Utilities;
using BNS.ViewModels;
using BNS.ViewModels.Responses.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;


namespace BNS.Application.Features
{
    public class GetJM_SprintQuery
    {
        public class GetJM_SprintRequest : CommandRequest<ApiResult<JM_SprintResponse>>
        {
            [Required]
            public Guid JM_ProjectId { get; set; }
        }
        public class GetJM_SprintRequestHandler : IRequestHandler<GetJM_SprintRequest, ApiResult<JM_SprintResponse>>
        {
            protected readonly BNSDbContext _context;
            protected readonly IStringLocalizer<SharedResource> _sharedLocalizer;

            public GetJM_SprintRequestHandler(BNSDbContext context,
             IStringLocalizer<SharedResource> sharedLocalizer)
            {
                _context = context;
                _sharedLocalizer = sharedLocalizer;
            }
            public async Task<ApiResult<JM_SprintResponse>> Handle(GetJM_SprintRequest request, CancellationToken cancellationToken)
            {
                var response = new ApiResult<JM_SprintResponse>();
                response.data = new JM_SprintResponse();
                var query = _context.JM_Sprints.Where(s => s.JM_ProjectId == request.JM_ProjectId &&
                !s.IsDelete);
                if (!string.IsNullOrEmpty(request.fieldSort))
                {
                    var columnSort = request.fieldSort;
                    var sortType = request.sort;
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

                var rs = await query.Select(s => new JM_SprintResponseItem
                {
                    Name = s.Name,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Id = s.Id,
                    UpdatedDate = s.UpdatedDate,
                    CreatedDate = s.CreatedDate,
                    CreatedUserId = s.CreatedUser,
                    UpdatedUserId = s.UpdatedUser
                }).ToListAsync();
                response.data.Items = rs;
                return response;
            }

        }
    }
}
